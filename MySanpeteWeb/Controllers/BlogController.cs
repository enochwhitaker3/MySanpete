using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : Controller
{
    private readonly IBlogService blogService;

    public BlogController(IBlogService blogService)
    {
        this.blogService = blogService;
    }

    [HttpGet()]
    public async Task<List<BlogDTO>> GetAll()
    {
        var allBlogs = await blogService.GetAllBlogs();
        return allBlogs;
    }

    [HttpGet("{id}")]
    public async Task<BlogDTO?> GetById(int id)
    {
        var blog = await blogService.GetBlog(id);
        if (blog is null)
        {
            return null;
        }
        return blog;
    }
}
