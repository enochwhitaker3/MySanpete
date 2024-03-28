using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OccasionController : ControllerBase
{
    private readonly IOccasionService occasionService;

    public OccasionController(IOccasionService occasionService)
    {
        this.occasionService = occasionService;
    }

    [HttpGet()]
    public async Task<List<Occasion>> GetAll()
    {
        List<Occasion> allOccasions = await occasionService.GetAllOcassions();
        return allOccasions;
    }

    [HttpGet("{id}")]
    public async Task<Occasion?> GetById(int id)
    {
        Occasion occasion = await occasionService.GetOccasion(id);
        if (occasion is null)
        {
            return null;
        }
        return occasion;
    }
}
