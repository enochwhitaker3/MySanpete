using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;

namespace MySanpeteWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PodcastController : Controller
{
    private readonly IPodcastService podcastService;

    public PodcastController(IPodcastService podcastService)
    {
        this.podcastService = podcastService;
    }

    [HttpGet()]
    public async Task<List<PodcastDTO>> GetAll()
    {
        var allPodcasts = await podcastService.GetAllPodcasts();
        return allPodcasts;
    }

    [HttpGet("{id}")]
    public async Task<PodcastDTO?> GetById(int id)
    {
        var podcast = await podcastService.GetPodcast(id);
        if (podcast is null)
        {
            return null;
        }
        return podcast;
    }
}
