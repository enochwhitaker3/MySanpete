using FluentAssertions;
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
            Name = "Test Name"
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
            Address = "Test Address"
        };

        var business = await businessService.AddBusiness(request);
        business.Should().NotBeNull();

        var result = await businessService.DeleteBusiness(business!.Id);
        result.Should().NotBeNull();

        await businessService.Invoking(vs => vs.GetBusiness(business.Id))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "No businesses found with given ID");
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
            Name = "Test Name"
        };

        var business = await businessService.AddBusiness(request);

        business!.BusinessName = "New Business Name";

        var result = await businessService.UpdateBusiness(business);

        result.Should().NotBeNull();
        result!.BusinessName.Should().Be("New Business Name");
        result.Address.Should().Be("Test Address");
    }

    [Fact]
    public async void CanGetBusinessTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        var result = await businessService.GetBusiness(1);
        result.Should().NotBeNull();
        result!.BusinessName.Should().Be("Collaborative Music");
        result!.Address.Should().Be("123 Swag Street");
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
