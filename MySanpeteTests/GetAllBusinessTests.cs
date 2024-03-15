using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using MySanpeteWeb.Services;
using RazorClassLibrary.Data;

namespace MySanpeteTests;

public class GetAllBusinessTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public GetAllBusinessTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void CanGetAllBusinessTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBusinessService businessService = scope.ServiceProvider.GetRequiredService<IBusinessService>();

        var result = await businessService.GetAllBusinesses();
        result.Should().NotBeNull();
        result!.Count.Should().Be(2);
    }
}
