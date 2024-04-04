using RazorClassLibrary.Data;
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
    public Task<Occasion> AddOccasion(AddOccasionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOccasion(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Occasion>> GetAllOcassions()
    {
        var result = await httpClient.GetFromJsonAsync<List<Occasion>>("/api/occasion/");
        return result!;
    }

    public async Task<Occasion> GetOccasion(int id)
    {
        var result = await httpClient.GetFromJsonAsync<Occasion>($"/api/occasion/{id}");
        return result!;
    }

    public Task<Occasion> UpdateOccasion(Occasion occasion)
    {
        throw new NotImplementedException();
    }
}
