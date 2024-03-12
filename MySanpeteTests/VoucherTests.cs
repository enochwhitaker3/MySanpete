using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using MySanpeteWeb.Services;
using RazorClassLibrary.Data;

namespace MySanpeteTests;

public class VoucherTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public VoucherTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void CreateVoucherTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();

        AddVoucherRequest request = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            PromoDescription = "Default Description",
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoCode = "1738",
            PromoStock = 10,
            PromoName = "Test Name",
            RetailPrice = 5,
            TotalReclaimable = 1,
            BusinessId = 1,
        };

        var voucher = await voucherService.AddVoucher(request);
        voucher.Should().NotBeNull();
        voucher.AmmountReclaimable.Should().Be(1);
        voucher.EndDate.Should().Be(new DateTime(2024, 3, 12).ToUniversalTime());
        voucher.StartDate.Should().Be(new DateTime(2024, 3, 10).ToUniversalTime());
        voucher.PromoCode.Should().Be("1738");
        voucher.PromoName.Should().Be("Test Name");
        voucher.RetailPrice.Should().Be(5);
        voucher.LeftInStock.Should().Be(10);
    }

    [Fact]
    public async void DeleteVoucherTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();

        AddVoucherRequest request = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            PromoDescription = "Default Description",
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoCode = "1738",
            PromoStock = 10,
            PromoName = "Test Name",
            RetailPrice = 5,
            TotalReclaimable = 1,
            BusinessId = 1,
        };

        var voucher = await voucherService.AddVoucher(request);
        await voucherService.DeleteVoucher(voucher.Id);

        var voucherMaybe = await voucherService.GetVoucher(voucher.Id);

        voucherMaybe.Should().NotBeNull();
    }

    [Fact]
    public async void CantDeleteVoucherThatDoesntExistTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();

        try
        {
            var voucher = await voucherService.DeleteVoucher(10000);
        }
        catch(Exception ex) 
        {
            ex.Should().NotBeNull();
        }
    }

    [Fact]
    public async void UpdateVoucherTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();

        AddVoucherRequest request = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            PromoDescription = "Default Description",
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoCode = "1738",
            PromoStock = 10,
            PromoName = "Test Name",
            RetailPrice = 5,
            TotalReclaimable = 1,
            BusinessId = 1,
        };

        var voucher = await voucherService.AddVoucher(request);
        voucher = await voucherService.GetVoucher(voucher.Id);

        voucher.PromoName = "New Test Name";

        Voucher newVoucher = new Voucher()
        {
            PromoName= voucher.PromoName,
            EndDate = voucher.EndDate,
            StartDate = voucher.StartDate,  
            PromoDescription = voucher.PromoDescription,
            PromoCode = voucher.PromoCode,
            PromoStock = voucher.Stock,
            RetailPrice = voucher.RetailPrice,
            TotalReclaimable = voucher.AmmountReclaimable,
            BusinessId = request.BusinessId,
            Id = voucher.Id
        };

        var updatedVoucher = await voucherService.UpdateVoucher(newVoucher);

        updatedVoucher.Should().NotBeNull();
        updatedVoucher.PromoName.Should().Be("New Test Name");
    }

    [Fact]
    public async void CanGetVoucherTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IVoucherService voucherService = scope.ServiceProvider.GetRequiredService<IVoucherService>();

        AddVoucherRequest request = new AddVoucherRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            PromoDescription = "Default Description",
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            PromoCode = "1738",
            PromoStock = 10,
            PromoName = "Test Name",
            RetailPrice = 5,
            TotalReclaimable = 1,
            BusinessId = 1,
        };

        var voucher = await voucherService.AddVoucher(request);
        voucher = await voucherService.GetVoucher(voucher.Id);

        voucher.Should().NotBeNull();
    }
}
