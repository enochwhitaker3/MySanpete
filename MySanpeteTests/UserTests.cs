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
    public async void CreateUserTest()
    {
        var userService = CreateUserService();
        var user = await userService.AddUser("testUser@gmail.com");

        user.Should().NotBeNull();
    }

    [Fact]
    public async void CreateUserWithInvalidEmail()
    {
        var userService = CreateUserService();
        var user = await userService.AddUser("blah blah");

        user.Should().BeNull();
    }

    [Fact]
    public async void DeleteUserTest()
    {
        // Arrange
        var userService = CreateUserService();
        int countBefore = (await userService.GetAllUsers()).Count();
        Guid usersGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3");

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

        userUnderTest.UserEmail = newEmail;
        await userService.PatchUser(userUnderTest);
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

        try
        {
            await userService.PatchUser(userUnderTest);
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

        try
        {
            await userService.PatchUser(userUnderTest);
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

        try
        {
            await userService.PatchUser(userUnderTest);
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

        try
        {
            await userService.PatchUser(userUnderTest);
        }
        catch
        {
            return;
        }
        throw new Exception("Long username was saved.");
    }

    [Fact]
    public async void SetRoleWithInvalidGUIDTest()
    {
        var userService = CreateUserService();
        SetRoleRequest request = new SetRoleRequest()
        {
            UserId = new Guid("dc34835d-1738-1738-1738-ce90dc1209e3"),
            AuthString = "DefaultAuthString",
            RoleId = 2
        };

        //Act
        try
        {
            var result = await userService.SetRole(request);
        }
        catch
        {
            return;
        }

        //Assert
        throw new Exception("Role was set");

    }

    [Fact]
    public async void SetRoleWithValidEverythingTest()
    {
        var userService = CreateUserService();
        SetRoleRequest request = new SetRoleRequest()
        {
            UserId = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            AuthString = "DefaultAuthString",
            RoleId = 2
        };

        var result = await userService.SetRole(request);

        result.Should().Be(true);
    }

    [Fact]
    public async void SetRoleWithInvalidAuthString()
    {
        var userService = CreateUserService();
        SetRoleRequest request = new SetRoleRequest()
        {
            UserId = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            AuthString = "Incorrect Auth String",
            RoleId = 2
        };

        //Act
        try
        {
            var result = await userService.SetRole(request);
        }
        catch
        {
            return;
        }

        //Assert
        throw new Exception("Role was set");
    }

    [Fact]
    public async void SetRoleWithInvalidRoleId()
    {
        var userService = CreateUserService();
        SetRoleRequest request = new SetRoleRequest()
        {
            UserId = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            AuthString = "DefaultAuthString",
            RoleId = 4
        };

        //Act
        try
        {
            var result = await userService.SetRole(request);
        }
        catch
        {
            return;
        }

        //Assert
        throw new Exception("Role was set");
    }
}
