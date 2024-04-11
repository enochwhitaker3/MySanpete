using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using System.Formats.Asn1;

namespace MySanpeteTests;

public class BundleTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public BundleTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void AddingNewBundleSuccessfullTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2]
        };

        var bundle = await bundleService.AddNewBundle(request);
        bundle.Should().NotBeNull();
        bundle.Name.Should().Be("Test Bundle");
        bundle.EndDate.Should().Be(new DateTime(2024, 3, 12).ToUniversalTime());
        bundle.StartDate.Should().Be(new DateTime(2024, 3, 10).ToUniversalTime());
        bundle.Vouchers.Should().HaveCount(2);
    }

    [Fact]
    public async void AddingBundleWithoutStartDateFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);

        AddBundleRequest request = new AddBundleRequest()
        {
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        await bundleService.Invoking(vs => vs.AddNewBundle(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Insufficient Request");
    }

    [Fact]
    public async void AddingBundleWithNoNameFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "testbusiness@gmail.com"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Vouchers = [voucher, voucher2],
        };

        await bundleService.Invoking(vs => vs.AddNewBundle(request))
                .Should()
                .ThrowAsync<Exception>()
                .Where(e => e.Message == "Insufficient Request");
    }

    [Fact]
    public async void AddingBundleWithNoVouchersFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
        };

        await bundleService.Invoking(vs => vs.AddNewBundle(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Insufficient Request");
    }

    [Fact]
    public async void AddingBundleWithoutEndDateFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);

        AddBundleRequest request = new AddBundleRequest()
        {
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        await bundleService.Invoking(vs => vs.AddNewBundle(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Insufficient Request");
    }

    [Fact]
    public async void DeletingBundleSuccessfullTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var result = await bundleService.DeleteBundle(bundle.Id);

        result.Should().BeTrue();
    }

    [Fact]
    public async void DeletingBundleThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();

        await bundleService.Invoking(vs => vs.DeleteBundle(1000000))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Can't delete bundle that doesn't exist");
    }

    [Fact]
    public async void GettingBundleSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var result = await bundleService.GetBundle(bundle.Id);
        result.Should().NotBeNull();
        result.StartDate.Should().Be(new DateTime(2024, 3, 10).ToUniversalTime());
        result.EndDate.Should().Be(new DateTime(2024, 3, 12).ToUniversalTime());
        result.Name.Should().Be("Test Bundle");
    }

    [Fact]
    public async void GettingBundleThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();

        await bundleService.Invoking(vs => vs.GetBundle(1000000))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Can't find bundle");
    }

    [Fact]
    public async void UpdatingBundleSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);
        var voucher3 = await voucherService.AddVoucher(voucherRequest3);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var newBundle = await bundleService.GetBundle(bundle.Id);

        newBundle.EndDate = new DateTime(2024, 3, 15).ToUniversalTime();
        newBundle.StartDate = new DateTime(2024, 3, 14).ToUniversalTime();
        newBundle.Name = "New Test";
        newBundle.Vouchers = [voucher, voucher3];

        var result = await bundleService.UpdateBundle(newBundle);
        result.Should().NotBeNull();
        result.EndDate.Should().Be(new DateTime(2024, 3, 15).ToUniversalTime());
        result.StartDate.Should().Be(new DateTime(2024, 3, 14).ToUniversalTime());
        result.Name.Should().Be("New Test");
        result.Vouchers.Should().HaveCount(2);
        result.Vouchers![0].PromoName.Should().Be("Promo Name");
        result.Vouchers![1].PromoName.Should().Be("Promo Name 3");
    }

    [Fact]
    public async void UpdatingBundleWithBadEndDateFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var newBundle = await bundleService.GetBundle(bundle.Id);

        newBundle.EndDate = null;
        newBundle.StartDate = new DateTime(2024, 3, 14).ToUniversalTime();
        newBundle.Name = "New Test";
        newBundle.Vouchers = [voucher, voucher3];

        await bundleService.Invoking(vs => vs.UpdateBundle(newBundle))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "End date for the occasion needs a value");
    }

    [Fact]
    public async void UpdatingBundleWithBadStartDateFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);
        var voucher3 = await voucherService.AddVoucher(voucherRequest3);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var newBundle = await bundleService.GetBundle(bundle.Id);

        newBundle.EndDate = new DateTime(2024, 3, 15).ToUniversalTime();
        newBundle.StartDate = null;
        newBundle.Name = "New Test";
        newBundle.Vouchers = [voucher, voucher3];

        await bundleService.Invoking(vs => vs.UpdateBundle(newBundle))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Start date for the occasion needs a value");
    }

    [Fact]
    public async void UpdatingBundleWithEndDateBehindStartDateFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);
        var voucher3 = await voucherService.AddVoucher(voucherRequest3);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var newBundle = await bundleService.GetBundle(bundle.Id);

        newBundle.EndDate = new DateTime(2024, 3, 13).ToUniversalTime();
        newBundle.StartDate = new DateTime(2024, 3, 14).ToUniversalTime();
        newBundle.Name = "New Test";
        newBundle.Vouchers = [voucher, voucher3];

        await bundleService.Invoking(vs => vs.UpdateBundle(newBundle))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "End date needs to be after the start date");
    }

    [Fact]
    public async void UpdatingBundleWithBadNameFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);
        var voucher3 = await voucherService.AddVoucher(voucherRequest3);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var newBundle = await bundleService.GetBundle(bundle.Id);

        newBundle.EndDate = new DateTime(2024, 3, 15).ToUniversalTime();
        newBundle.StartDate = new DateTime(2024, 3, 14).ToUniversalTime();
        newBundle.Name = "";
        newBundle.Vouchers = [voucher, voucher3];

        await bundleService.Invoking(vs => vs.UpdateBundle(newBundle))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Name needs a value for the occasion");
    }

    [Fact]
    public async void PurchasingBundleSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);
        var voucher3 = await voucherService.AddVoucher(voucherRequest3);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var purchaseRequest = new PurchaseBundleRequest()
        {
            BundleId = bundle.Id,
            UserId = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            ChargeId = "Successful Purchase"
        };

        var result = await bundleService.PurchaseBundle(purchaseRequest);

        result.Should().BeTrue();
    }

    [Fact]
    public async void PurchasingBundleThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);
        var voucher3 = await voucherService.AddVoucher(voucherRequest3);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var purchaseRequest = new PurchaseBundleRequest()
        {
            BundleId = 10000000,
            UserId = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            ChargeId = "Successful Purchase"
        };

        await bundleService.Invoking(vs => vs.PurchaseBundle(purchaseRequest))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Cannot purchase bundle that doesn't exist");
    }

    [Fact]
    public async void PurchasingBundleForUserThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBundleService bundleService = scope.ServiceProvider.GetRequiredService<IBundleService>();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        AddBusinessRequest businessRequest = new AddBusinessRequest()
        {
            Address = "Test Address",
            Name = "Test Business",
            Email = "Test Email"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
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
            StripeId = "prod_Pp6quxf4kRDS4c",
            PriceId = "price_1OzScbDb4weiXajfCMzzapIP"
        };

        var voucher = await voucherService.AddVoucher(voucherRequest);
        var voucher2 = await voucherService.AddVoucher(voucherRequest2);
        var voucher3 = await voucherService.AddVoucher(voucherRequest3);

        AddBundleRequest request = new AddBundleRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Name = "Test Bundle",
            Vouchers = [voucher, voucher2],
        };

        var bundle = await bundleService.AddNewBundle(request);

        var purchaseRequest = new PurchaseBundleRequest()
        {
            BundleId = bundle.Id,
            UserId = new Guid("1c892d2e-a43e-4b3a-97f3-00a3c2037612"),
            ChargeId = "Successful Purchase"
        };

        await bundleService.Invoking(vs => vs.PurchaseBundle(purchaseRequest))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Cannot purchase a bundle for a user that doesn't exist");
    }
}
