using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;
using Microsoft.EntityFrameworkCore;
namespace MySanpeteWeb.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ImageController : Controller
{
    private readonly IDbContextFactory<MySanpeteDbContext> factory;

    public ImageController(IDbContextFactory<MySanpeteDbContext> factory)
    {
        this.factory = factory;
    }

    [HttpGet("/business/{id}")]
    public async Task<IActionResult> GetImage(int id)
    {
        using var context = await factory.CreateDbContextAsync();
        var business = await context.Businesses.FirstOrDefaultAsync(i => i.Id == id);

        //if (business == null)
        //{
        //    return NotFound();
        //}

        var image = business.Logo;

        //if(image == null)
        //{
        //    return NotFound();
        //}

        return File(image, "image/jpeg");
    }
}
