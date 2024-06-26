﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests;

public class BusinessTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public BusinessTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void CreateBusinessTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest request = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Name",
            Email = "Testemail@email.com"
        };

        var business = await businessService.AddBusiness(request);
        business.Should().NotBeNull();
        business!.BusinessName.Should().Be("Test Name");
        business.Address.Should().Be("Test Address");
    }

    [Fact]
    public async void CantCreateBusinessWithoutAddressTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest request = new AddBusinessRequest()
        {
            Name = "Test Name"
        };

        await businessService.Invoking(vs => vs.AddBusiness(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Can't make a business without an address");
    }

    [Fact]
    public async void CantCreateBusinessWithoutNameTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest request = new AddBusinessRequest()
        {
            Address = "Test Address"
        };

        await businessService.Invoking(vs => vs.AddBusiness(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Can't make a business without a name");
    }

    [Fact]
    public async void CanDeleteBusinessTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest request = new AddBusinessRequest()
        {
            Name = "Test Name",
            Address = "Test Address",
            Email = "business@gmail.com"
        };

        var business = await businessService.AddBusiness(request);
        business.Should().NotBeNull();

        var result = await businessService.DeleteBusiness(business!.Id);
        result.Should().NotBeNull();

        result.Should().BeTrue();
    }

    [Fact]

    public async void CantDeleteBusinessThatDoesntExistTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        var result = await businessService.DeleteBusiness(1000000);
        result.Should().BeFalse();
    }

    [Fact]
    public async void CanUpdateBusinessTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest request = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Name",
            Email = "Testemail@email.com"
        };

        var business = await businessService.AddBusiness(request);

        business!.BusinessName = "New Business Name";

        var updateRequest = new UpdateBusinessRequest()
        {
            Address = business.Address,
            BusinessName = business.BusinessName,
            Email = business.Email,
            Id = business.Id,
            Logo = [146]
        };

        var result = await businessService.UpdateBusiness(updateRequest);

        result.Should().NotBeNull();
        result!.BusinessName.Should().Be("New Business Name");
        result.Address.Should().Be("Test Address");
    }

    [Fact]
    public async void CanGetBusinessTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest request = new AddBusinessRequest()
        {
            Name = "Test Name",
            Address = "Test Address",
            Email = "business@gmail.com"
        };

        var business = await businessService.AddBusiness(request);
        var allBusiness = await businessService.GetAllBusinesses();
        int AddedBId = allBusiness.Last().Id;

        var result = await businessService.GetBusiness(AddedBId);
        result.Should().NotBeNull();
        result!.BusinessName.Should().Be("Test Name");
        result!.Address.Should().Be("Test Address");
        result!.Email.Should().Be("business@gmail.com");
    }

    [Fact]
    public async void CantGetBusinessThatDoesntExistTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        await businessService.Invoking(vs => vs.GetBusiness(1000000))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "No businesses found with given ID");
    }

}
