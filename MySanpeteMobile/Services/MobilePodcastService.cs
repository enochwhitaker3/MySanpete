using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteMobile.Services;

public class MobilePodcastService : IPodcastService
{
    private readonly HttpClient httpClient;
    public MobilePodcastService(HttpClient HttpClient)
    {
        httpClient = HttpClient;
    }
    public Task<PodcastDTO> AddPodcast(AddPodcastRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletePodcast(int podcastId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PodcastDTO>> GetAllPodcasts()
    {
        var result = await httpClient.GetFromJsonAsync<List<PodcastDTO>>("/api/podcast/");
        return result!;
    }

    public async Task<PodcastDTO> GetPodcast(int podcastId)
    {
        var result = await httpClient.GetFromJsonAsync<PodcastDTO>($"/api/podcast/{podcastId}");
        return result!;
    }
}
