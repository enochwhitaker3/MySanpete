using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

public class MobileUserService : IUserService
{
    private readonly HttpClient httpClient;
    public MobileUserService(HttpClient HttpClient)
    {
        httpClient = HttpClient;
    }
    public Task<UserDTO?> AddUser(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserDTO>> GetAllUsers()
    {
        var result = await httpClient.GetFromJsonAsync<List<UserDTO>>("/api/user/");
        return result!;
    }

    public async Task<UserDTO?> GetAuthUser(string authId)
    {
        var result = await httpClient.GetFromJsonAsync<UserDTO>($"/api/user/getauthuser/{authId}");
        return result!;
    }

    public async Task<UserDTO?> GetUser(string email)
    {
        var result = await httpClient.GetFromJsonAsync<UserDTO>($"/api/user/getuseremail/{email}");
        return result!;
    }

    public async Task<UserDTO?> GetUser(Guid guid)
    {
        var result = await httpClient.GetFromJsonAsync<UserDTO>($"/api/user/getuserguid/{guid}");
        return result!;
    }

    public Task<UserDTO?> PatchUser(UpdateUserRequest user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetRole(SetRoleRequest request)
    {
        throw new NotImplementedException();
    }
}
