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
    public async Task<List<OccasionDTO>> GetAll()
    {
        List<OccasionDTO> allOccasions = await occasionService.GetAllOcassions();
        return allOccasions;
    }

    [HttpGet("{id}")]
    public async Task<OccasionDTO?> GetById(int id)
    {
        OccasionDTO occasion = await occasionService.GetOccasion(id);
        if (occasion is null)
        {
            return null;
        }
        return occasion;
    }
}
