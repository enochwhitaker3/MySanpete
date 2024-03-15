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

public class GetAllBlogsTests : IClassFixture<MySanpeteFactory>
{
    public MySanpeteFactory mySanpeteFactory { get; set; }
    public GetAllBlogsTests(MySanpeteFactory factory)
    {
        factory.CreateDefaultClient();
        mySanpeteFactory = factory;
    }

    [Fact]
    public async void CanGetAllBlogsSuccessfullTest()
    {
        using var scope = mySanpeteFactory.Services.CreateScope();
        IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

        AddBlogRequest request = new AddBlogRequest()
        {
            AuthorId = 1,
            Commentable = true,
            Content = "Blah blah blah",
            Title = "Title",
        };

        AddBlogRequest request2 = new AddBlogRequest()
        {
            AuthorId = 1,
            Commentable = true,
            Content = "Blah blah blah",
            Title = "Title 2",
        };

        var blog = await blogService.AddBlog(request);
        var blog2 = await blogService.AddBlog(request2);

        var results = await blogService.GetAllBlogs();

        results.Count.Should().Be(2);

        results[0].Title.Should().Be("Title");
        results[1].Title.Should().Be("Title 2");
    }
}
