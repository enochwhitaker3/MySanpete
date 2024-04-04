using RazorClassLibrary.DTOs;
using RazorClassLibrary.Services;
using RazorClassLibrary.Requests;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

public class MobileBundleService : IBundleService
{
    private readonly HttpClient httpClient;

    public MobileBundleService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public Task<BundleDTO> AddNewBundle(AddBundleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBundle(int bundleId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<BundleDTO>> GetAllBundles()
    {
        var result = await httpClient.GetFromJsonAsync<List<BundleDTO>>("/api/bundle");
        return result!;
    }

    public async Task<BundleDTO> GetBundle(int bundleId)
    {
        var result = await httpClient.GetFromJsonAsync<BundleDTO>($"/api/bundle/{bundleId}");
        return result!;
    }

    public Task<bool> PurchaseBundle(PurchaseBundleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<BundleDTO> UpdateBundle(BundleDTO bundle)
    {
        throw new NotImplementedException();
    }
}
