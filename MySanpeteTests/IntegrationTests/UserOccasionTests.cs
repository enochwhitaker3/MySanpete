using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using MySanpeteWeb.Services;
using RazorClassLibrary.Data;

namespace MySanpeteTests;

public class UserOccasionTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }

    public UserOccasionTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void CreateUserOccasionSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        AddUserOccasionRequest request = new AddUserOccasionRequest()
        {
            OccasionId = 2,
            UserId = 1,
        };

        var userOccasion = await userOccasionService.AddNewUserOccasion(request);
        userOccasion.Should().NotBeNull();
        userOccasion!.Occasion!.Business!.BusinessName.Should().Be("McDonalds");
        userOccasion!.User!.Username.Should().Be("[Deleted]");
    }

    [Fact]
    public async void CreateUserOccasionWithoutUserFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        AddUserOccasionRequest request = new AddUserOccasionRequest()
        {
            OccasionId = 2,
        };

        await userOccasionService.Invoking(x => x.AddNewUserOccasion(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Message == "User was not found");
    }

    [Fact]
    public async void CreateUserOccasionWithoutOccasionFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        AddUserOccasionRequest request = new AddUserOccasionRequest()
        {
            UserId = 1
        };

        await userOccasionService.Invoking(x => x.AddNewUserOccasion(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Message == "Occasion was not found");
    }

    [Fact]
    public async void DeleteUserOccasionSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        AddUserOccasionRequest request = new AddUserOccasionRequest()
        {
            OccasionId = 2,
            UserId = 1,
        };

        var userOccasion = await userOccasionService.AddNewUserOccasion(request);

        var result = await userOccasionService.DeleteUserOccasion(userOccasion!.Id);
        result.Should().BeTrue();
    }

    [Fact]
    public async void DeleteUserOccasionThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        var result = await userOccasionService.DeleteUserOccasion(100000);
        result.Should().BeFalse();
    }

    [Fact]
    public async void DeleteUserOccasionWithNegativeIdFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        await userOccasionService.Invoking(x => x.DeleteUserOccasion(-1))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Message == "user occasion id was less than or equal to 0 which doesn't exist");
    }

    [Fact]
    public async void GettingUserOccasionSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        AddUserOccasionRequest request = new AddUserOccasionRequest()
        {
            OccasionId = 2,
            UserId = 1,
        };

        var userOccasion = await userOccasionService.AddNewUserOccasion(request);

        var result = await userOccasionService.GetUserOccasion(userOccasion!.Id);
        result.Should().NotBeNull();
        result!.Occasion!.Business!.BusinessName.Should().Be("McDonalds");
        result!.User!.Username.Should().Be("[Deleted]");
    }

    [Fact]
    public async void GettingUserOccasionThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        await userOccasionService.Invoking(x => x.GetUserOccasion(100000))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Message == "User occasion wasn't found");
    }
}
