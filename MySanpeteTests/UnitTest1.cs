using RazorClassLibrary.Services;
using FluentAssertions;
using  Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;

namespace MySanpeteTests;

public class UserTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public UserTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void CreateUserTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var user = await userService.AddUser("testUser@gmail.com");

        user.Should().NotBeNull();
    }
}
