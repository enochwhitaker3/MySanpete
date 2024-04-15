using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

internal class MobileOccasionService : IOccasionService
{
    private readonly HttpClient httpClient;
    public MobileOccasionService(HttpClient HttpClient)
    {
        this.httpClient = HttpClient;
    }
    public Task<OccasionDTO> AddOccasion(AddOccasionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOccasion(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OccasionDTO>> GetAllOcassions()
    {
        var result = await httpClient.GetFromJsonAsync<List<OccasionDTO>>("/api/occasion/");
        return result!;
    }

    public async Task<OccasionDTO> GetOccasion(int id)
    {
        var result = await httpClient.GetFromJsonAsync<OccasionDTO>($"/api/occasion/{id}");
        return result!;
    }

    public Task<OccasionDTO> UpdateOccasion(UpdateOccasionRequest occasion)
    {
        throw new NotImplementedException();
    }
}
