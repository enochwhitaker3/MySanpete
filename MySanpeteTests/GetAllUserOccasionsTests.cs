using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using MySanpeteWeb.Services;
using RazorClassLibrary.Data;

namespace MySanpeteTests;

public class GetAllUserOccasionsTests : IClassFixture<MySanpeteFactory>
{
    private static bool alreadyAdded = false;
    public MySanpeteFactory mySanpeteFactory { get; set; }

    public GetAllUserOccasionsTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
        Task.Run(async () => await FillDatabase()).Wait();
    }

    internal async Task FillDatabase()
    {
        if (alreadyAdded) return;
        alreadyAdded = true;
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        AddUserOccasionRequest request = new AddUserOccasionRequest()
        {
            OccasionId = 2,
            UserId = 1,
        };

        AddUserOccasionRequest request2 = new AddUserOccasionRequest()
        {
            OccasionId = 2,
            UserId = 1,
        };

        await userOccasionService.AddNewUserOccasion(request);
        await userOccasionService.AddNewUserOccasion(request2);
    }


    [Fact]
    public async void GettingAllUserOccasionsSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        var allUserOccasions = await userOccasionService.GetAllUserOccasions();
        allUserOccasions.Should().NotBeEmpty();
        allUserOccasions.Count.Should().Be(2);
    }

    [Fact]
    public async void GettingAllUserOccasionsByOccasionSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        var allUserOccasions = await userOccasionService.GetAllUserOccasionsByOccasion(2);
        allUserOccasions.Should().NotBeEmpty();
        allUserOccasions.Count.Should().Be(2);
    }

    [Fact]
    public async void GettingAllUserOccasionsByUserSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IUserOccasionService userOccasionService = scope.ServiceProvider.GetRequiredService<IUserOccasionService>();

        var allUserOccasions = await userOccasionService.GetAllUserOccasionsByUser(new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"));
        allUserOccasions.Should().NotBeEmpty();
        allUserOccasions.Count.Should().Be(2);
    }
}
