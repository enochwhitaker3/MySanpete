using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Mozilla;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests
{
    public class BlogTests : IClassFixture<MySanpeteFactory>
    {
        public MySanpeteFactory mySanpeteFactory { get; set; }
        public BlogTests(MySanpeteFactory factory)
        {
            factory.CreateDefaultClient();
            mySanpeteFactory = factory;
        }

        [Fact]
        public async void AddingBlogSuccessfullyTest()
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

            var response = await blogService.AddBlog(request);

            response!.Id.Should().NotBe(0);
        }

        [Fact]
        public async void AddingBlogWithNoTitleFailsTest()
        {
            using var scope = mySanpeteFactory.Services.CreateScope();
            IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

            AddBlogRequest request = new AddBlogRequest()
            {
                AuthorId = 1,
                Commentable = true,
                Content = "Blah blah blah",
            };

            await blogService.Invoking(blogService => blogService.AddBlog(request))
                .Should()
                .ThrowAsync<Exception>()
                .Where(x => x.Message == "Blog cannot be made with no title");
        }

        [Fact]
        public async void AddingBlogWithNoContentFailsTest()
        {
            using var scope = mySanpeteFactory.Services.CreateScope();
            IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

            AddBlogRequest request = new AddBlogRequest()
            {
                AuthorId = 1,
                Commentable = true,
                Title = "Title",
            };

            await blogService.Invoking(vs => vs.AddBlog(request))
                .Should()
                .ThrowAsync<Exception>()
                .Where(x => x.Message == "Blog cannot be made with no content");
        }

        [Fact]
        public async void AddingBlogWithNoAuthorFailsTest()
        {
            using var scope = mySanpeteFactory.Services.CreateScope();
            IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

            AddBlogRequest request = new AddBlogRequest()
            {
                Commentable = true,
                Content = "Blah blah blah",
                Title = "Title",
            };

            await blogService.Invoking(vs => vs.AddBlog(request))
               .Should()
               .ThrowAsync<Exception>()
               .Where(x => x.Message == "The author either doesn't exist or doesn't have permissions");
        }

        [Fact]
        public async void DeletingBlogSuccessfullyTest()
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

            var blog = await blogService.AddBlog(request);

            var result = await blogService.DeleteBlog(blog!.Id);

            result.Should().BeTrue();
        }

        [Fact]
        public async void DeletingBlogThatDoesntExistFailsTest()
        {
            using var scope = mySanpeteFactory.Services.CreateScope();
            IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

            var result = await blogService.DeleteBlog(1000000);

            result.Should().BeFalse();
        }

        [Fact]
        public async void UpdatingBlogSuccessfullyTest()
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

            var blog = await blogService.AddBlog(request);

            blog!.Content = "Woah new content";

            var result = await blogService.EditBlog(blog);

            result.Should().NotBeNull();

            result!.Content.Should().Be("Woah new content");
            result.Commentable.Should().BeTrue();
            result.Title.Should().Be("Title");
        }

        [Fact]
        public async void GettingBlogSuccessfullyTest()
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

            var blog = await blogService.AddBlog(request);

            var result = await blogService.GetBlog(blog!.Id);

            result.Should().NotBeNull();
            result!.Commentable.Should().BeTrue();
            result!.Title.Should().Be("Title");
            result.Content.Should().Be("Blah blah blah");
        }

        [Fact]
        public async void GettingBlogThatDoesntExistFailsTest()
        {
            using var scope = mySanpeteFactory.Services.CreateScope();
            IBlogService blogService = scope.ServiceProvider.GetRequiredService<IBlogService>();

            var result = await blogService.GetBlog(1000000);

            result.Should().BeNull();
        }
    }
}
