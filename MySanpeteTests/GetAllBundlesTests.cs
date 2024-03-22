using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using System.Formats.Asn1;

namespace MySanpeteTests;

public class GetAllBundlesTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public GetAllBundlesTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void GettingAllBundlesSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
        };

        var business = await businessService.AddBusiness(businessRequest);

        AddVoucherRequest voucherRequest = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoDescription = "Test Description",
            BusinessId = business!.Id,
            PromoCode = "Promo Code",
            PromoName = "Promo Name",
            PromoStock = 5,
            RetailPrice = 5.99M,
            TotalReclaimable = 1,
        };

        AddVoucherRequest voucherRequest2 = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoDescription = "Test Description",
            BusinessId = business!.Id,
            PromoCode = "Promo Code",
            PromoName = "Promo Name",
            PromoStock = 5,
            RetailPrice = 5.99M,
            TotalReclaimable = 1,
        };

        AddVoucherRequest voucherRequest3 = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoDescription = "Test Description 3",
            BusinessId = business!.Id,
            PromoCode = "Promo Code 3",
            PromoName = "Promo Name 3",
            PromoStock = 1,
            RetailPrice = 8.99M,
            TotalReclaimable = 5,
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);
        var voucher3 = await voucherService.AddVoucher(voucherRequest3);

        AddBundleRequest request1 = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle 1",
            Vouchers = [voucher, voucher2],
        };

        AddBundleRequest request2 = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle 2",
            Vouchers = [voucher, voucher3],
        };

        var bundle1 = await bundleService.AddNewBundle(request1);
        var bundle2 = await bundleService.AddNewBundle(request2);

        var result = await bundleService.GetAllBundles();

        result.Count.Should().Be(2);
        result[0].Name.Should().Be("Test Bundle 1");
        result[1].Name.Should().Be("Test Bundle 2");
    }

    [Fact]
    public async void GettingAllBundlesIfNoneExistsFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();

        var result = await bundleService.GetAllBundles();
        result.Count.Should().Be(0);
    }
}
