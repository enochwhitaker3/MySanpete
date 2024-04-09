using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;

namespace MySanpeteTests;

public class UserTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public UserTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    private IUserService CreateUserService()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<IUserService>();
    }

    [Fact]
    public async void DeleteUserTest()
    {
        // Arrange
        var userService = CreateUserService();
        int countBefore = (await userService.GetAllUsers()).Count();
        Guid usersGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e4");

        // Act
        await userService.DeleteUser(usersGuid);
        int countAfter = (await userService.GetAllUsers()).Count();

        // Assert
        countAfter.Should().Be(countBefore - 1);
    }

    [Fact]
    public async void DeleteUserWhenUserDNETest()
    {
        // Arrange
        var userService = CreateUserService();
        int countBefore = (await userService.GetAllUsers()).Count();
        Guid usersGuid = Guid.NewGuid(); // invalid id

        // Act
        await userService.DeleteUser(usersGuid);
        int countAfter = (await userService.GetAllUsers()).Count();

        // Assert
        countAfter.Should().Be(countBefore);
    }

    [Fact]
    public async void GetAllUsersTest()
    {
        var userService = CreateUserService();

        var result = await userService.GetAllUsers();

        result.Should().NotBeEmpty();
    }

    [Fact]
    public async void GetUserByGUIDTest()
    {
        var userService = CreateUserService();
        Guid usersGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3");

        var result = await userService.GetUser(usersGuid);

        result.Should().NotBeNull();
    }

    [Fact]
    public async void GetUserWhereUserGUIDDNETest()
    {
        var userService = CreateUserService();
        Guid userGuid = Guid.NewGuid();

        var result = await userService.GetUser(userGuid);

        result.Should().BeNull();
    }

    [Fact]
    public async void GetUserByEmailTest()
    {
        var userService = CreateUserService();
        string userEmail = "codebras2023@gmail.com";

        var result = await userService.GetUser(userEmail);

        result.Should().NotBeNull();
    }

    [Fact]
    public async void GetUserByEmailThatDNETest()
    {
        var userService = CreateUserService();
        string userEmail = "BadEmail";

        var result = await userService.GetUser(userEmail);

        result.Should().BeNull();
    }

    [Fact]
    public async void PatchUserEmailValidTest()
    {
        // Arrange
        var userService = CreateUserService();
        var users = await userService.GetAllUsers();
        var userUnderTest = users.FirstOrDefault();
        string newEmail = "myEmail@gmail.com";

        if (userUnderTest == null)
        {
            throw new Exception("User was not found.");
        }

        if (userUnderTest.Username is null)
        {
            userUnderTest.UserEmail = newEmail;
        }

        UpdateUserRequest userRequest = new()
        {
            UserEmail = userUnderTest.UserEmail,
            Guid = userUnderTest.Guid,
            Id = userUnderTest.Id,
            Username = userUnderTest.Username
        };

        userUnderTest.UserEmail = newEmail;
        await userService.PatchUser(userRequest);
        var userChanged = await userService.GetUser(userUnderTest.UserEmail);

        userChanged?.UserEmail.Should().Be(newEmail);
    }

    [Fact]
    public async void PatchUserEmailInvalidTest()
    {
        // Arrange
        var userService = CreateUserService();
        var users = await userService.GetAllUsers();
        var userUnderTest = users.FirstOrDefault();
        string newEmail = "BadEmail";

        if (userUnderTest == null)
        {
            throw new Exception("User was not found.");
        }

        userUnderTest.UserEmail = newEmail;

        UpdateUserRequest userRequest = new()
        {
            UserEmail = userUnderTest.UserEmail,
            Guid = userUnderTest.Guid,
            Id = userUnderTest.Id,
            Username = userUnderTest.Username
        };

        try
        {
            await userService.PatchUser(userRequest);
        }
        catch
        {
            return;
        }
        throw new Exception("Invalid email was saved.");
    }

    [Fact]
    public async void PatchUserNullEmailTest()
    {
        // Arrange
        var userService = CreateUserService();
        var users = await userService.GetAllUsers();
        var userUnderTest = users.FirstOrDefault();
        string? newEmail = null;

        if (userUnderTest == null)
        {
            throw new Exception("User was not found.");
        }

        userUnderTest.UserEmail = newEmail;

        UpdateUserRequest userRequest = new()
        {
            UserEmail = userUnderTest.UserEmail,
            Guid = userUnderTest.Guid,
            Id = userUnderTest.Id,
            Username = userUnderTest.Username
        };

        try
        {
            await userService.PatchUser(userRequest);
        }
        catch
        {
            return;
        }
        throw new Exception("Null email was saved.");
    }

    [Fact]
    public async void PatchUserNullUsernameTest()
    {
        // Arrange
        var userService = CreateUserService();
        var users = await userService.GetAllUsers();
        var userUnderTest = users.FirstOrDefault();

        if (userUnderTest == null)
        {
            throw new Exception("User was not found.");
        }

        userUnderTest.Username = null;

        UpdateUserRequest userRequest = new()
        {
            UserEmail = userUnderTest.UserEmail,
            Guid = userUnderTest.Guid,
            Id = userUnderTest.Id,
            Username = userUnderTest.Username
        };

        try
        {
            await userService.PatchUser(userRequest);
        }
        catch
        {
            return;
        }
        throw new Exception("Null username was saved.");
    }

    [Fact]
    public async void PatchUserTooLongUsernameTest()
    {
        // Arrange
        var userService = CreateUserService();
        var users = await userService.GetAllUsers();
        var userUnderTest = users.FirstOrDefault();

        if (userUnderTest == null)
        {
            throw new Exception("User was not found.");
        }

        userUnderTest.Username = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";

        UpdateUserRequest userRequest = new()
        {
            UserEmail = userUnderTest.UserEmail,
            Guid = userUnderTest.Guid,
            Id = userUnderTest.Id,
            Username = userUnderTest.Username
        };

        try
        {
            await userService.PatchUser(userRequest);
        }
        catch
        {
            return;
        }
        throw new Exception("Long username was saved.");
    }
}
