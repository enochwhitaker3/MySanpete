using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests;

public class GetAllPodcastTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public GetAllPodcastTests(MySanpeteFactory factory)
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
            URL = "https://www.google.com/",
            Commentable = true,
            PodcastName = "name"
        };
        await service.AddPodcast(podcastRequest);

        var allPodcasts = await service.GetAllPodcasts();

        allPodcasts.Count.Should().Be(28);
    }

    public IPodcastService createService()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        return scope.ServiceProvider.GetService<IPodcastService>()!;
    }
}
