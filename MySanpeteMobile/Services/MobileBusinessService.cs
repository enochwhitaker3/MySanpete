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
    public Task<BusinessDTO?> AddBusiness(AddBusinessRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> DeleteBusiness(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<BusinessDTO>> GetAllBusinesses()
    {
        var result = await httpClient.GetFromJsonAsync<List<BusinessDTO>>($"/api/business/");
        return result!;
    }

    public async Task<BusinessDTO?> GetBusiness(int id)
    {
        var result = await httpClient.GetFromJsonAsync<BusinessDTO>($"/byid/{id}");
        return result!;
    }

    public async Task<BusinessDTO?> GetBusiness(string email)
    {
        var result = await httpClient.GetFromJsonAsync<BusinessDTO>($"/api/business/byemail/{email}");
        return result!;
    }

    public Task<BusinessDTO?> UpdateBusiness(UpdateBusinessRequest businessRequest)
    {
        throw new NotImplementedException();
    }
}
