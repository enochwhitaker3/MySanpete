﻿using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using MySanpeteWeb.Services;
using RazorClassLibrary.Data;

namespace MySanpeteTests;

public class GetAllVoucherTests : IClassFixture<MySanpeteFactory>
{
    static bool SeedRan = false;
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public GetAllVoucherTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
        Task.Run(() => SeedDatabase()).Wait();
    }

    private async Task SeedDatabase()
    {
        if (SeedRan)
        {
            return;
        }
        else
        {
            SeedRan = true;
        }
        using var scope = mySanpeteFactory.Services.CreateScope();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();

        AddVoucherRequest request = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            PromoDescription = "Default Description",
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoCode = "1738",
            PromoStock = 10,
            PromoName = "Test Name 1",
            RetailPrice = 5,
            TotalReclaimable = 1,
            BusinessId = 13,
        };

        AddVoucherRequest request2 = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            PromoDescription = "Default Description",
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoCode = "1738",
            PromoStock = 10,
            PromoName = "Test Name 2",
            RetailPrice = 5,
            TotalReclaimable = 1,
            BusinessId = 13,
        };

        AddVoucherRequest request3 = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            PromoDescription = "Default Description",
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoCode = "1738",
            PromoStock = 10,
            PromoName = "Test Name 3",
            RetailPrice = 5,
            TotalReclaimable = 1,
            BusinessId = 13,
        };

        await voucherService.AddVoucher(request);
        await voucherService.AddVoucher(request2);
        await voucherService.AddVoucher(request3);
    }

    [Fact]
    public async void CanGetAllVoucher()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();

        var result = await voucherService.GetAllVouchers();

        result.Should().NotBeEmpty();
        result.Count.Should().Be(12);
        result[9].PromoName.Should().Be("Test Name 1");
        result[10].PromoName.Should().Be("Test Name 2");
        result[11].PromoName.Should().Be("Test Name 3");
    }

    [Fact]
    public async void CanGetAllVouchersForBusinessTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();

        var result = await voucherService.GetAllBusinessVouchers(13);

        result.Count.Should().Be(5);
    }

    [Fact]
    public async void CantGetBusinessIfNotExistsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();

        var result = await voucherService.GetAllBusinessVouchers(1000000);

        result.Should().BeNullOrEmpty();
    }
}


