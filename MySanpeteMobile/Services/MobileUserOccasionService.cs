using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

public class MobileUserOccasionService : IUserOccasionService
{
    private readonly HttpClient httpClient;

    public MobileUserOccasionService(HttpClient HttpClient)
    {
        this.httpClient = HttpClient;
    }

    public async Task<UserOccasionDTO?> AddNewUserOccasion(AddUserOccasionRequest userOccasionRequest)
    {
        var result = await httpClient.PostAsJsonAsync($"/api/useroccasion/add/{userOccasionRequest}", userOccasionRequest);
        return await result.Content.ReadFromJsonAsync<UserOccasionDTO>();
    }

    public Task<bool> DeleteUserOccasion(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserOccasionDTO?>> GetAllUserOccasions()
    {
        var result = await httpClient.GetFromJsonAsync<List<UserOccasionDTO>>($"/api/useroccasion/");
        return result!;
    }

    public async Task<List<UserOccasionDTO?>> GetAllUserOccasionsByOccasion(int occasionId)
    {
        var result = await httpClient.GetFromJsonAsync<List<UserOccasionDTO>>($"/api/useroccasion/getallbyoccasion/{occasionId}");
        return result!;
    }

    public async Task<List<UserOccasionDTO?>> GetAllUserOccasionsByUser(Guid? userId)
    {
        var guidString = userId.ToString();
        var result = await httpClient.GetFromJsonAsync<List<UserOccasionDTO>>($"/api/useroccasion/getallbyuser/{guidString}");
        return result!;
    }

    public async Task<UserOccasionDTO?> GetUserOccasion(int id)
    {
        var result = await httpClient.GetFromJsonAsync<UserOccasionDTO>($"/api/useroccasion/getbyid/{id}");
        return result!;
    }

    public Task<UserOccasionDTO?> UpdateUserOccasion(UserOccasionDTO userOccasionDTO)
    {
        throw new NotImplementedException();
    }
}
