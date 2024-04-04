using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

public class MobileBusinessService : IBusinessService
{
    private readonly HttpClient httpClient;
    public MobileBusinessService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public Task<Business?> AddBusiness(AddBusinessRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> DeleteBusiness(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Business>> GetAllBusinesses()
    {
        var result = await httpClient.GetFromJsonAsync<List<Business>>("/api/business/");
        return result!;
    }

    public async Task<Business?> GetBusiness(int id)
    {
        var result = await httpClient.GetFromJsonAsync<Business>($"/api/business/byid/{id}");
        return result!;
    }

    public async Task<Business?> GetBusiness(string email)
    {
        var result = await httpClient.GetFromJsonAsync<Business>($"/api/business/byemail/{email}");
        return result!;
    }

    public Task<Business?> UpdateBusiness(Business business)
    {
        throw new NotImplementedException();
    }
}
