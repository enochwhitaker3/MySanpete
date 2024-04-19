using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;
using Microsoft.EntityFrameworkCore;
using LazyCache;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Diagnostics;
using MySanpeteWeb.Telemetry;
using Sprache;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MySanpeteWeb.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ImageController(IDbContextFactory<MySanpeteDbContext> factory, IAppCache cache) : Controller
{
    private readonly IDbContextFactory<MySanpeteDbContext> factory = factory;
    private readonly IAppCache cache = cache;

    [HttpGet("business/{id}")]
    public async Task<IActionResult> GetImage(int id)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = await cache.GetOrAddAsync($"Business{id}", () => GetBusinessImageAsync(id));
        stopwatch.Stop();
        MySanpeteMetrics.ImageRetrieval.Record(stopwatch.ElapsedMilliseconds);
        return result;
    }

    [HttpGet("blogs/{id}")]
    public async Task<IActionResult> GetBlogImage(int id)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = await cache.GetOrAddAsync($"Blogs{id}", () => GetBlogImageAsync(id));
        stopwatch.Stop();
        MySanpeteMetrics.ImageRetrieval.Record(stopwatch.ElapsedMilliseconds);
        return result;
    }

    [HttpGet("occasion/{id}")]
    public async Task<IActionResult> GetOccasionImage(int id)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = await cache.GetOrAddAsync($"Occasion{id}", () => GetOccasionImageAsync(id));
        stopwatch.Stop();
        MySanpeteMetrics.ImageRetrieval.Record(stopwatch.ElapsedMilliseconds);
        return result;
    }


    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserImage(int userId)
    {
        using var context = await factory.CreateDbContextAsync();
        var user = await context.EndUsers.FirstOrDefaultAsync(i => i.Id == userId) ?? throw new NullReferenceException("The user does not exist");

        var image = user?.Photo ?? System.IO.File.ReadAllBytes("wwwroot/Images/default_avatar.jpg");
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
            // If the image is null, return a default image from your project files
            string imagePath = "wwwroot/Images/My_Sanpete.png"; //Path.Combine(environment.WebRootPath, "images", "default.jpg");
            image = System.IO.File.ReadAllBytes(imagePath);
            return File(image, "image/jpeg");
        }

        return File(image, "image/jpeg");
    }

    private async Task<IActionResult> GetOccasionImageAsync(int id)
    {
        using var context = await factory.CreateDbContextAsync();
        var occasion = await context.Occasions.FirstOrDefaultAsync(i => i.Id == id);

        if (occasion == null)
        {
            return NotFound();
        }

        var image = occasion.Photo;

        if (image == null)
        {
            return NotFound();
        }

        return File(image, "image/jpeg");
    }


}
