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
    public async void GetAllPodcastsTest()
    {
        IPodcastService service = createService();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            URL = "myurl.com",
            Commentable = true,
            PodcastName = "name"
        };

        var allPodcasts = await service.GetAllPodcasts();

        allPodcasts.Count.Should().Be(1);
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
            URL = "myurl.com",
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
    public async void AddAPodcastWithoutURLFieldsFails()
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

    public IPodcastService createService()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        return scope.ServiceProvider.GetService<IPodcastService>()!;
    }
}
