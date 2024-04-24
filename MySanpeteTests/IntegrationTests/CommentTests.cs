using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Mozilla;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests;

public class CommentTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public CommentTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void AddingBlogCommentSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

        AddBlogRequest blogRequest = new AddBlogRequest()
        {
            AuthorId = 1,
            Commentable = true,
            Content = "Test Blog Content",
            Title = "Test Blog Title",
        };

        var blog = await blogService.AddBlog(blogRequest);

        AddCommentRequest request = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
        };

        var result = await commentService.AddBlogComment(request);

        result.Should().BeTrue();
    }

    [Fact]
    public async void AddingPodcastCommentSuccessfullyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IPodcastService podcastService = scope.ServiceProvider.GetRequiredService<IPodcastService>();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            Commentable = true,
            PodcastName = "Test Podcast Name",
            URL = "https://www.google.com/"
        };

        var podcast = await podcastService.AddPodcast(podcastRequest);

        AddCommentRequest request = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
        };

        var result = await commentService.AddPodcastComment(request);

        result.Should().BeTrue();
    }

    [Fact]
    public async void AddingBlogCommentSuccessfullyWithReplyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

        AddBlogRequest blogRequest = new AddBlogRequest()
        {
            AuthorId = 1,
            Commentable = true,
            Content = "Test Blog Content",
            Title = "Test Blog Title",
        };

        var blog = await blogService.AddBlog(blogRequest);

        AddCommentRequest request1 = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
        };

        AddCommentRequest request2 = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            replyId = 1
        };

        var result1 = await commentService.AddBlogComment(request1);
        var result2 = await commentService.AddBlogComment(request2);

        result1.Should().BeTrue();
        result2.Should().BeTrue();
    }

    [Fact]
    public async void AddingPodcastCommentSuccessfullyWithReplyTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IPodcastService podcastService = scope.ServiceProvider.GetRequiredService<IPodcastService>();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            Commentable = true,
            PodcastName = "Test Podcast Name",
            URL = "https://www.google.com/"
        };

        var podcast = await podcastService.AddPodcast(podcastRequest);

        AddCommentRequest request1 = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
        };


        AddCommentRequest request2 = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            replyId = 1
        };

        var result1 = await commentService.AddPodcastComment(request1);
        var result2 = await commentService.AddPodcastComment(request2);

        result1.Should().BeTrue();
        result2.Should().BeTrue();
    }

    [Fact]
    public async void ReplyingToCommentThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IPodcastService podcastService = scope.ServiceProvider.GetRequiredService<IPodcastService>();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            Commentable = true,
            PodcastName = "Test Podcast Name",
            URL = "https://www.google.com/"
        };

        var podcast = await podcastService.AddPodcast(podcastRequest);

        AddCommentRequest request1 = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
        };


        AddCommentRequest request2 = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            replyId = 10000000
        };

        var result1 = await commentService.AddPodcastComment(request1);
        var result2 = await commentService.AddPodcastComment(request2);

        result1.Should().BeTrue();
        result2.Should().BeFalse();
    }

    [Fact]
    public async void ReplyingToPodcastCommentThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IPodcastService podcastService = scope.ServiceProvider.GetRequiredService<IPodcastService>();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            Commentable = true,
            PodcastName = "Test Podcast Name",
            URL = "https://www.google.com/"
        };

        var podcast = await podcastService.AddPodcast(podcastRequest);

        AddCommentRequest request1 = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
        };


        AddCommentRequest request2 = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            replyId = 100000000
        };

        var result1 = await commentService.AddPodcastComment(request1);
        var result2 = await commentService.AddPodcastComment(request2);

        result1.Should().BeTrue();
        result2.Should().BeFalse();
    }

    [Fact]
    public async void AddingBlogCommentWithoutContentFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

        AddBlogRequest blogRequest = new AddBlogRequest()
        {
            AuthorId = 1,
            Commentable = true,
            Content = "Test Blog Content",
            Title = "Test Blog Title",
        };

        var blog = await blogService.AddBlog(blogRequest);

        AddCommentRequest request = new AddCommentRequest()
        {
            contentId = 1,
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
        };

        await commentService.Invoking(vs => vs.AddBlogComment(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "A comment needs content");
    }

    [Fact]
    public async void AddingPodcastCommentWithoutContentFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IPodcastService podcastService = scope.ServiceProvider.GetRequiredService<IPodcastService>();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            Commentable = true,
            PodcastName = "Test Podcast Name",
            URL = "https://www.google.com/"
        };

        var podcast = await podcastService.AddPodcast(podcastRequest);

        AddCommentRequest request = new AddCommentRequest()
        {
            contentId = 1,
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
        };

        await commentService.Invoking(vs => vs.AddPodcastComment(request))
            .Should()
            .ThrowAsync<Exception>()
            .Where(e => e.Message == "A comment needs content");
    }

    [Fact]
    public async void DeletingBlogCommentSuccessfulTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

        AddBlogRequest blogRequest = new AddBlogRequest()
        {
            AuthorId = 1,
            Commentable = true,
            Content = "Test Blog Content",
            Title = "Test Blog Title",
        };

        var blog = await blogService.AddBlog(blogRequest);

        AddCommentRequest request = new AddCommentRequest()
        {
            contentId = 1,
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
            content = "Test Comment Content"
        };

        var comment = await commentService.AddBlogComment(request);

        var result = await commentService.DeleteBlogComment(1);
        result.Should().BeTrue();
    }

    [Fact]
    public async void DeletingPodcastCommentSuccessfulTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IPodcastService podcastService = scope.ServiceProvider.GetRequiredService<IPodcastService>();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            Commentable = true,
            PodcastName = "Test Podcast Name",
            URL = "https://www.google.com/"
        };

        var podcast = await podcastService.AddPodcast(podcastRequest);

        AddCommentRequest request = new AddCommentRequest()
        {
            contentId = 1,
            content = "Test Content",
            userGuid = new Guid("dc43835d-1738-1738-1738-ce90cc1209e3"),
        };

        var comment = await commentService.AddPodcastComment(request);

        var result = await commentService.DeletePodcastComment(1);

        result.Should().BeTrue();
    }

    [Fact]
    public async void DeletingBlogCommentThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

        AddBlogRequest blogRequest = new AddBlogRequest()
        {
            AuthorId = 1,
            Commentable = true,
            Content = "Test Blog Content",
            Title = "Test Blog Title",
        };

        var blog = await blogService.AddBlog(blogRequest);

        var result = await commentService.DeleteBlogComment(1000000);
        result.Should().BeFalse();
    }

    [Fact]
    public async void DeletingPodcastCommentThatDoesntExistFailsTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        ICommentService commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
        IPodcastService podcastService = scope.ServiceProvider.GetRequiredService<IPodcastService>();

        AddPodcastRequest podcastRequest = new AddPodcastRequest()
        {
            Commentable = true,
            PodcastName = "Test Podcast Name",
            URL = "https://www.google.com/"
        };

        var podcast = await podcastService.AddPodcast(podcastRequest);

        var result = await commentService.DeletePodcastComment(100000000);

        result.Should().BeFalse();
    }
}
