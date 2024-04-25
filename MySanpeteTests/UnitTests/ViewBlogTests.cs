using FluentAssertions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests.UnitTests
{
    public class ViewBlogTests
    {
        [Fact]
        public void HidingBlogWorks()
        {
            var page = SetUpPage();

            var result = page.HideEmail("email@gmail.com");

            result.Should().Be("e***l@gmail.com");
        }

        [Fact]
        public void RegularUsernameShouldNotBeObfuscated()
        {
            var page = SetUpPage();

            var result = page.HideEmail("First Last");

            result.Should().Be("First Last");
        }

        [Fact]
        public void UsernameWithAtSymbolTest()
        {
            var page = SetUpPage();

            var result = page.HideEmail("First@Last");

            result.Should().Be("F***t@Last");
        }

        [Fact]
        public void UsernameWithAtSymbolAndSpacesTest()
        {
            var page = SetUpPage();

            var result = page.HideEmail("F2@Last");

            result.Should().Be("F2@Last");
        }

        [Fact]
        public void UsernameWithTwoAtSymbols()
        {
            var page = SetUpPage();

            var result = page.HideEmail("First@Last@");

            result.Should().Be("F***t@L**t@");
        }

        public ViewBlog SetUpPage()
        {
            var page = new ViewBlog();
            var Blog = new BlogDTO()
            {

                Title = "Blog Title",
                Content = "<h1>Title</h1><p>blah blahh</p>",
                AuthorName = "Dusty Shaw",
                PublishDate = DateTime.Now,
                Commentable = true,
                PhotoURL = "/MySanpete.jpg"

            };

            page.blog = Blog;
            return page;

        }
    }
}
