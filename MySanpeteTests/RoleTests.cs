using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using MySanpeteWeb.Services;
using RazorClassLibrary.Data;

namespace MySanpeteTests;

public class RoleTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public RoleTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void GetAllRoleSuccessfullTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IRoleService roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();

        var result = await roleService.GetAllRoles();

        result.Count.Should().Be(2);
    }
}
