using RazorClassLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MySanpeteWeb;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Data;

namespace MySanpeteTests;

public class OccasionTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public OccasionTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void CreateOccasionSuccessfullyTest()
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

        var occasion = await occasionService.AddOccasion(request);
        occasion.Should().NotBeNull();
        occasion.Description.Should().Be("Test Description");
        occasion.EndDate.Should().Be(new DateTime(2024, 3, 12).ToUniversalTime());
        occasion.StartDate.Should().Be(new DateTime(2024, 3, 10).ToUniversalTime());
        occasion.BusinessId.Should().Be(13);
        occasion.Title.Should().Be("Test Title");
        occasion.XCoordinate.Should().Be(39.359770M);
        occasion.YCoordinate.Should().Be(-111.584170M);
    }

    [Fact]
    public async void CreateOccasionWithoutBusinessFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IOccasionService occasionService = scope.ServiceProvider.GetRequiredService<IOccasionService>();

        AddOccasionRequest request = new AddOccasionRequest()
        {
            Description = "Test Description",
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Title = "Test Title",
            XCoordinate = 39.359770M,
            YCoordinate = -111.584170M,
        };

        await occasionService.Invoking(vs => vs.AddOccasion(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Occasions need a business");
    }

    [Fact]
    public async void CreateOccasionWithoutTitleFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IOccasionService occasionService = scope.ServiceProvider.GetRequiredService<IOccasionService>();

        AddOccasionRequest request = new AddOccasionRequest()
        {
            Description = "Test Description",
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            BusinessId = 1,
            XCoordinate = 39.359770M,
            YCoordinate = -111.584170M,
        };

        await occasionService.Invoking(vs => vs.AddOccasion(request))
         .Should()
         .ThrowAsync<Exception>()
         .Where(e => e.Message == "Occasions need a title");
    }

    [Fact]
    public async void CreateOccasionWithoutDescriptionFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IOccasionService occasionService = scope.ServiceProvider.GetRequiredService<IOccasionService>();

        AddOccasionRequest request = new AddOccasionRequest()
        {
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            Title = "Test Title",
            XCoordinate = 39.359770M,
            YCoordinate = -111.584170M,
            BusinessId = 1
        };

        await occasionService.Invoking(vs => vs.AddOccasion(request))
          .Should()
          .ThrowAsync<Exception>()
          .Where(e => e.Message == "Occasions need a description");
    }

    [Fact]
    public async void CreateOccasionWithEndDateBeforeStartDateFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IOccasionService occasionService = scope.ServiceProvider.GetRequiredService<IOccasionService>();

        AddOccasionRequest request = new AddOccasionRequest()
        {
            Description = "Test Description",
            EndDate = new DateTime(2024, 3, 9).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            BusinessId = 1,
            Title = "Test Title",
            XCoordinate = 39.359770M,
            YCoordinate = -111.584170M,
        };

        await occasionService.Invoking(vs => vs.AddOccasion(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "The end date of the even needs to be after the start date");
    }

    [Fact]
    public async void CreateOccasionWithoutXCoordinateFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IOccasionService occasionService = scope.ServiceProvider.GetRequiredService<IOccasionService>();

        AddOccasionRequest request = new AddOccasionRequest()
        {
            Description = "Test Description",
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            BusinessId = 1,
            Title = "Test Title",
            YCoordinate = -111.584170M,
        };

        await occasionService.Invoking(vs => vs.AddOccasion(request))
          .Should()
          .ThrowAsync<Exception>()
          .Where(e => e.Message == "The occasion needs an X coordinate");
    }

    [Fact]
    public async void CreateOccasionWithoutYCoordinateFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IOccasionService occasionService = scope.ServiceProvider.GetRequiredService<IOccasionService>();

        AddOccasionRequest request = new AddOccasionRequest()
        {
            Description = "Test Description",
            EndDate = new DateTime(2024, 3, 12).ToUniversalTime(),
            StartDate = new DateTime(2024, 3, 10).ToUniversalTime(),
            BusinessId = 1,
            Title = "Test Title",
            XCoordinate = 39.359770M,
        };

        await occasionService.Invoking(vs => vs.AddOccasion(request))
          .Should()
          .ThrowAsync<Exception>()
          .Where(e => e.Message == "The occasion needs an Y coordinate");
    }

    [Fact]
    public async void DeleteOccasionSuccessfullTest()
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

        var occasion = await occasionService.AddOccasion(request);

        var result = await occasionService.DeleteOccasion(occasion.Id);

        result.Should().BeTrue();
    }

    [Fact]
    public async void DeleteOccasionThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IOccasionService occasionService = scope.ServiceProvider.GetRequiredService<IOccasionService>();

        await occasionService.Invoking(vs => vs.DeleteOccasion(100000))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "No occasion found with given ID");
    }

    [Fact]
    public async void GettingOccasionSuccessfullTest()
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

        var occasion = await occasionService.AddOccasion(request);

        var result = await occasionService.GetOccasion(occasion.Id);

        result.Should().NotBeNull();
        result.Description.Should().Be("Test Description");
        result.BusinessId.Should().Be(13);
        result.Title.Should().Be("Test Title");
        result.XCoordinate.Should().Be(39.359770M);
        result.YCoordinate.Should().Be(-111.584170M);
        result.EndDate.Should().Be(new DateTime(2024, 3, 12).ToUniversalTime());
        result.StartDate.Should().Be(new DateTime(2024, 3, 10).ToUniversalTime());
    }

    [Fact]
    public async void GettingOccasionThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IOccasionService occasionService = scope.ServiceProvider.GetRequiredService<IOccasionService>();

        await occasionService.Invoking(vs => vs.GetOccasion(100000))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "No occasion was found with given ID");
    }

    [Fact]
    public async void UpdatingOccasionSuccessfullTest()
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

        var occasion = await occasionService.AddOccasion(request);


        occasion.Description = "New Test Description";

        UpdateOccasionRequest occasionRequest = new UpdateOccasionRequest()
        { 
            Id = occasion.Id,
            Title = occasion.Title,
            XCoordinate= occasion.XCoordinate,
            YCoordinate= occasion.YCoordinate,
            StartDate = occasion.StartDate,
            EndDate = occasion.EndDate,
            BusinessId = occasion.BusinessId,
            Description = occasion.Description,
            //Photo = occasion.PhotoURL
        };



        var result = await occasionService.UpdateOccasion(occasionRequest);

        result.Description.Should().Be("New Test Description");
    }
}
