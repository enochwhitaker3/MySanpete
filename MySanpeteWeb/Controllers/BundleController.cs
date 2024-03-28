using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BundleController : Controller
{
    private readonly IBundleService bundleService;

    public BundleController(IBundleService bundleService)
    {
        this.bundleService = bundleService;
    }

    [HttpGet()]
    public async Task<List<BundleDTO>> GetAll()
    {
        var allBundles = await bundleService.GetAllBundles();
        return allBundles;
    }

    [HttpGet("{id}")]
    public async Task<BundleDTO?> GetById(int id)
    {
        var bundle = await bundleService.GetBundle(id);
        if (bundle is null)
        {
            return null;
        }
        return bundle;
    }
}
