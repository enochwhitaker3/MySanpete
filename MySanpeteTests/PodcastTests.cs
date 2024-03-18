using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests;

public class PodcastTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }

    public PodcastTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void GetPodcastPasses()
    {
        // Arrange
        IPodcastService service = createService();
        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            URL = "https://www.google.com/",
            Commentable = true,
            PodcastName = "name"
        };
        var podcastCreated = await service.AddPodcast(podcastRequest);

        // Act
        var podcastFromService = await service.GetPodcast(podcastCreated.Id);

        // Assert
        podcastFromService.Name.Should().Be(podcastCreated.Name);
        podcastFromService.URL.Should().Be(podcastCreated.URL);
    }

    [Fact]
    public async void AddAPodcastWithAllRequiredFieldsPasses()
    {
        // Arrange
        IPodcastService service = createService();
        var podcasts = await service.GetAllPodcasts();
        int countBefore = podcasts.Count;
        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            URL = "https://www.google.com/",
            Commentable = true,
            PodcastName = "name"
        };

        // Act
        await service.AddPodcast(podcastRequest);
        podcasts = await service.GetAllPodcasts();
        int countAfter = podcasts.Count;

        // Assert
        countAfter.Should().BeGreaterThan(countBefore);
    }

    [Fact]
    public async void AddAPodcastWithoutURLFieldFails()
    {
        IPodcastService service = createService();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            Commentable = true,
            PodcastName = "name"
        };

        await service.Invoking(vs => vs.AddPodcast(podcastRequest))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Podcasts requires a valid URL");
    }

    [Fact]
    public async void AddAPodcastWithInvalidURLFieldFails()
    {
        IPodcastService service = createService();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            URL = "NOT_A_REAL_URL",
            Commentable = true,
            PodcastName = "name"
        };

        await service.Invoking(vs => vs.AddPodcast(podcastRequest))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "Podcasts requires a valid URL");
    }

    [Fact]
    public async void DeleteAPodcastPass()
    {
        // Arrange
        IPodcastService service = createService();
        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            URL = "https://www.google.com/",
            Commentable = true,
            PodcastName = "name"
        };
        var podcastAdded = await service.AddPodcast(podcastRequest);

        // Act
        bool success = await service.DeletePodcast(podcastAdded.Id);

        // Assert
        success.Should().BeTrue();
    }

    public IPodcastService createService()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        return scope.ServiceProvider.GetService<IPodcastService>()!;
    }
}
