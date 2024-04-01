using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using Xunit.Sdk;

namespace MySanpeteTests;

public class GetAllOccasionsTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public GetAllOccasionsTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void GettingAllOccasionsSuccessfullTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IOccasionService occasionService = scope.ServiceProvider.GetRequiredService<IOccasionService>();

        AddOccasionRequest request = new AddOccasionRequest()
        {
            Description = "Test Description",
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            BusinessId = 13,
            Title = "Test Title",
            XCoordinate = 39.359770M,
            YCoordinate = -111.584170M,
        };


        AddOccasionRequest request2 = new AddOccasionRequest()
        {
            Description = "Test Description 2",
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            BusinessId = 13,
            Title = "Test Title 2",
            XCoordinate = 39.359770M,
            YCoordinate = -111.584170M,
        };

        var occasion1 = await occasionService.AddOccasion(request);
        var occasion2 = await occasionService.AddOccasion(request2);

        var result = await occasionService.GetAllOcassions();

        result.Count.Should().Be(3);

        result[1].Description.Should().Be("Test Description");
        result[1].Title.Should().Be("Test Title");

        result[2].Description.Should().Be("Test Description 2");
        result[2].Title.Should().Be("Test Title 2");
    }
}
