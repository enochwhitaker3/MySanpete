using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using MySanpeteWeb.Services;
using RazorClassLibrary.Data;

namespace MySanpeteTests;

public class UserVoucherTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public UserVoucherTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void CreateUserVouchersuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        AddUserVoucherRequest request = new AddUserVoucherRequest()
        {
            chargeId = "Default Charge Id",
            finalPrice = 2.50M,
            userId = 1,
            voucherId = 9
        };

        var userVoucher = await userVoucherService.AddUserVoucher(request);
        userVoucher.Should().NotBeNull();
        userVoucher.Charge_Id.Should().Be("Default Charge Id");
        userVoucher.Final_Price.Should().Be(2.50M);
        userVoucher?.User?.Id.Should().Be(1);
        userVoucher?.Voucher?.Id.Should().Be(9);
    }

    [Fact]
    public async void CreatingUserVoucherWithoutChargeIdFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        AddUserVoucherRequest request = new AddUserVoucherRequest()
        {
            finalPrice = 2.50M,
            userId = 1,
            voucherId = 9
        };

        await userVoucherService.Invoking(x => x.AddUserVoucher(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Message == "User Voucher needs a charge Id from stripe");
    }

    [Fact]
    public async void CreatingUserVoucherWithoutUserIdFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        AddUserVoucherRequest request = new AddUserVoucherRequest()
        {
            chargeId = "Default Charge Id",
            finalPrice = 2.50M,
            voucherId = 9
        };

        await userVoucherService.Invoking(x => x.AddUserVoucher(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Message == "User Voucher needs a user");
    }

    [Fact]
    public async void CreatingUserVoucherWithoutVoucherIdFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        AddUserVoucherRequest request = new AddUserVoucherRequest()
        {
            chargeId = "Default Charge Id",
            finalPrice = 2.50M,
            userId = 1,
        };

        await userVoucherService.Invoking(x => x.AddUserVoucher(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Message == "Voucher could not be found");
    }

    [Fact]
    public async void DeletingUserVoucherSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        AddUserVoucherRequest request = new AddUserVoucherRequest()
        {
            chargeId = "Default Charge Id",
            finalPrice = 2.50M,
            userId = 1,
            voucherId = 9
        };

        var userVoucher = await userVoucherService.AddUserVoucher(request);

        var result = await userVoucherService.DeleteUserVoucher(userVoucher.Id);
        result.Should().BeTrue();
    }

    [Fact]
    public async void DeletingUserVoucherThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var result = await userVoucherService.DeleteUserVoucher(100000);
        result.Should().BeFalse();
    }

    [Fact]
    public async void GettingAllUserVoucherByBusinessSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var allUserVouchers = await userVoucherService.GetAllByBusiness("codebras2023@gmail.com");
        allUserVouchers.Should().HaveCount(2);
        allUserVouchers[0].Promo_Code.Should().Be("WOGO");
        allUserVouchers[1].Promo_Code.Should().Be("BOGO");
    }

    [Fact]
    public async void GettingAllUserVoucherByBusinessThatDoesntExistReturnsNothingTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var allUserVouchers = await userVoucherService.GetAllByBusiness("BadTestEmail@gmail.com");
        allUserVouchers.Should().HaveCount(0);
    }

    [Fact]
    public async void GettingAllUserVoucherByUserSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var allUserVouchers = await userVoucherService.GetAllByUser(1);
        allUserVouchers.Should().HaveCount(2);
        allUserVouchers[0].Promo_Code.Should().Be("WOGO");
        allUserVouchers[1].Promo_Code.Should().Be("BOGO");
    }

    [Fact]
    public async void GettingAllUserVoucherByUserThatDoesntExistReturnsNothingTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var allUserVouchers = await userVoucherService.GetAllByUser(100000);
        allUserVouchers.Should().HaveCount(0);
    }

    [Fact]
    public async void GettingAllUserVoucherByVoucherSuccessfullyTest()
    {

        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var allUserVouchers = await userVoucherService.GetAllByVoucher(20);
        allUserVouchers.Should().HaveCount(2);
        allUserVouchers[0].Promo_Code.Should().Be("BOGO");
        allUserVouchers[1].Promo_Code.Should().Be("WOGO");
    }

    [Fact]
    public async void GettingAllUserVoucherByVoucherThatDoesntExistReturnsNothingTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var allUserVouchers = await userVoucherService.GetAllByVoucher(100000);
        allUserVouchers.Should().HaveCount(0);
    }

    [Fact]
    public async void GettingAllUserVoucherSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var allUserVouchers = await userVoucherService.GetAllUserVouchers();
        allUserVouchers.Should().HaveCount(2);
        allUserVouchers[0].Promo_Code.Should().Be("WOGO");
        allUserVouchers[1].Promo_Code.Should().Be("BOGO");
    }

    [Fact]
    public async void GetUserVoucherSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var userVoucher = await userVoucherService.GetById(1);
        userVoucher.Should().NotBeNull();
        userVoucher.Promo_Code.Should().Be("BOGO");
    }

    [Fact]
    public async void GetUserVoucherDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        await userVoucherService.Invoking(x => x.GetById(100000))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Message == "User Voucher not found");
    }

    [Fact]
    public async void UpdatingUserVoucherSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        var userVoucher = await userVoucherService.GetById(1);
        userVoucher.Is_Used = true;

        var result = await userVoucherService.UpdateUserVoucher(userVoucher);

        result.Is_Used.Should().BeTrue();
    }

    [Fact]
    public async void UpdatingUserVoucherThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserVoucherService userVoucherService = scope.ServiceProvider.GetRequiredService<IUserVoucherService>();

        await userVoucherService.Invoking(x => x.GetById(100000))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Message == "User Voucher not found");
    }
}
