using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;
using Microsoft.EntityFrameworkCore;
using LazyCache;
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
        return await cache.GetOrAddAsync($"Business{id}",  () => GetImageAsync(id));
    }

    private async Task<IActionResult> GetImageAsync(int id)
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


}
