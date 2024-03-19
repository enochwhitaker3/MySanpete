using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests;

public class ReactionTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public ReactionTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }


    [Fact]
    public async Task AddBlogReactionPasses()
    {
        // Arrange
        IReactionService service = createService();
        IUserService userService = createUserService();
        IBlogService blogService = createBlogService();
        Guid userGuid = new("dc43835d-1738-1738-1738-ce90cc1209e3");    // Admin guid
        var user = await userService.GetUser(userGuid);
        byte[] photo = new byte[1];
        AddBlogRequest blogRequest = new() { AuthorId = user.Id, Commentable = true, Content="<p>Hello</p>", Photo= photo, Title="Title" };
        var addedBlog = await blogService.AddBlog(blogRequest);
        AddReactionRequest request = new()
        {
            ContentId = addedBlog.Id,
            Unicode = "U+1F44D",
            UserGuid = userGuid,
        };

        // Act
        var newBlog = await service.AddBlogReaction(request);

        // Assert
        newBlog.BlogId.Should().Be(addedBlog.Id);
        newBlog.UserId.Should().Be(user.Id);  
    }

    [Fact]
    public async Task RemoveBlogReactionPasses()
    {
        // Arrange
        IReactionService service = createService();
        IUserService userService = createUserService();
        IBlogService blogService = createBlogService();
        Guid userGuid = new("dc43835d-1738-1738-1738-ce90cc1209e3");    // Admin guid
        var user = await userService.GetUser(userGuid);
        byte[] photo = new byte[1];
        AddBlogRequest blogRequest = new() { AuthorId = user.Id, Commentable = true, Content = "<p>Hello</p>", Photo = photo, Title = "Title" };
        var addedBlog = await blogService.AddBlog(blogRequest);
        AddReactionRequest request = new()
        {
            ContentId = addedBlog.Id,
            Unicode = "U+1F44D",
            UserGuid = userGuid,
        };
        var newBlogReaction = await service.AddBlogReaction(request);

        // Act
        var result = await service.DeleteBlogReaction(newBlogReaction.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddPodcastReactionPasses()
    {
        // Arrange
        IReactionService service = createService();
        IUserService userService = createUserService();
        IPodcastService podcastService = createPodcastService();
        Guid userGuid = new("dc43835d-1738-1738-1738-ce90cc1209e3");
        var user = await userService.GetUser(userGuid);
        AddPodcastRequest podRequest = new() { URL = "https://www.google.com/", Commentable = true, PodcastName = "Name" };
        var addedPodcast = await podcastService.AddPodcast(podRequest);
        AddReactionRequest request = new()
        {
            ContentId = addedPodcast.Id,
            Unicode = "U+1F44D",
            UserGuid = userGuid,
        };

        // Act
        var newPod = await service.AddPodReaction(request);

        // Assert
        newPod.PodcastId.Should().Be(addedPodcast.Id);
        newPod.UserId.Should().Be(user.Id);
    }

    [Fact]
    public async Task RemovePodcastReactionPasses()
    {
        // Arrange
        IReactionService service = createService();
        IUserService userService = createUserService();
        IPodcastService podService = createPodcastService();
        Guid userGuid = new("dc43835d-1738-1738-1738-ce90cc1209e3");    // Admin guid
        var user = await userService.GetUser(userGuid);
        AddPodcastRequest podRequest = new() { URL = "https://www.google.com/", Commentable = true, PodcastName = "Name" };
        var addedPodcast = await podService.AddPodcast(podRequest);
        AddReactionRequest request = new()
        {
            ContentId = addedPodcast.Id,
            Unicode = "U+1F44D",
            UserGuid = userGuid,
        };
        var newPodReaction = await service.AddPodReaction(request);

        // Act
        var result = await service.DeletePodReaction(newPodReaction.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeletePodReactionInvalidIdFails()
    {
        // Arrange
        IReactionService service = createService();
        IUserService userService = createUserService();
        IPodcastService podService = createPodcastService();
        Guid userGuid = new("dc43835d-1738-1738-1738-ce90cc1209e3");    // Admin guid
        var user = await userService.GetUser(userGuid);
        AddPodcastRequest podRequest = new() { URL = "https://www.google.com/", Commentable = true, PodcastName = "Name" };
        var addedPodcast = await podService.AddPodcast(podRequest);
        AddReactionRequest request = new()
        {
            ContentId = addedPodcast.Id, // Invalid id
            Unicode = "U+1F44D",
            UserGuid = userGuid,
        };
        var newPodReaction = await service.AddPodReaction(request);

        // Act
        var result = await service.DeletePodReaction(-1);

        // Assert
        result.Should().BeFalse();    
    }

    public IReactionService createService()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        return scope.ServiceProvider.GetService<IReactionService>()!;
    }

    public IUserService createUserService()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        return scope.ServiceProvider.GetService<IUserService>()!;
    }

    public IBlogService createBlogService()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        return scope.ServiceProvider.GetService<IBlogService>()!;
    }

    public IPodcastService createPodcastService()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        return scope.ServiceProvider.GetService<IPodcastService>()!;
    }
}
