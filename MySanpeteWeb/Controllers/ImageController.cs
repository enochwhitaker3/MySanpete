using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;
using Microsoft.EntityFrameworkCore;
using LazyCache;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MySanpeteWeb.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ImageController : Controller
{
    private readonly IDbContextFactory<MySanpeteDbContext> factory;
    private readonly IAppCache cache;

    public ImageController(IDbContextFactory<MySanpeteDbContext> factory, IAppCache cache)
    {
        this.factory = factory;
        this.cache = cache;
    }

    [HttpGet("business/{id}")]
    public async Task<IActionResult> GetImage(int id)
    {
        return await cache.GetOrAddAsync($"Business{id}", () => GetBusinessImageAsync(id));
    }

    [HttpGet("blogs/{id}")]
    public async Task<IActionResult> GetBlogImage(int id)
    {
        return await cache.GetOrAddAsync($"Blogs{id}", () => GetBlogImageAsync(id));
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserImage(int userId)
    {
        using var context = await factory.CreateDbContextAsync();
        var user = await context.EndUsers.FirstOrDefaultAsync(i => i.Id == userId);

        var image = user.Photo;

        return File(image, "image/jpeg");
    }

    private async Task<IActionResult> GetBusinessImageAsync(int id)
    {
        using var context = await factory.CreateDbContextAsync();
        var business = await context.Businesses.FirstOrDefaultAsync(i => i.Id == id);

        if (business == null)
        {
            return NotFound();
        }

        var image = business.Logo;

        if (image == null)
        {
            return NotFound();
        }

        return File(image, "image/jpeg");
    }

    private async Task<IActionResult> GetBlogImageAsync(int id)
    {
        using var context = await factory.CreateDbContextAsync();
        var blog = await context.Blogs.FirstOrDefaultAsync(i => i.Id == id);

        if (blog == null)
        {
            return NotFound();
        }

        var image = blog.Photo;

        if (image == null)
        {
            return NotFound();
        }

        return File(image, "image/jpeg");
    }


}
