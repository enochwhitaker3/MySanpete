using MySanpeteWeb.Components.Pages.AdminPages;
using RazorClassLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests.UnitTests
{
    public class AdminEditBlogTests
    {
        [Fact]
        public void CanCallMethod()
        {
            var page = SetUpPage();

            var NewBlog = new BlogDTO()
            {

                Title = "Blog Title",
                Content = "<h1>Title</h1><p>blah blahh</p>",
                AuthorName = "Dusty Shaw",
                PublishDate = DateTime.Now,
                Commentable = true,
                PhotoURL = "/MySanpete.jpg"

            };

            page.SelectBlog(NewBlog);


        }

        public AdminEditBlog SetUpPage() 
        {
            var page = new AdminEditBlog();

            var Blog = new BlogDTO()
            {

                Title = "Blog Title",
                Content = "<h1>Title</h1><p>blah blahh</p>",
                AuthorName = "Dusty Shaw",
                PublishDate = DateTime.Now,
                Commentable = true,
                PhotoURL = "/MySanpete.jpg"

            };
            page.SelectedBlog = Blog;
            return page;
        }
    }
}
