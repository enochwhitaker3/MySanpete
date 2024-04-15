using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace MySanpeteMobile.Services;

public class MobileUserVoucherService : IUserVoucherService
{
    private readonly HttpClient httpClient;
    public MobileUserVoucherService(HttpClient HttpClient)
    {
        this.httpClient = HttpClient;
    }
    public Task<UserVoucherDTO> AddUserVoucher(AddUserVoucherRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserVoucher(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserVoucherDTO>> GetAllByBusiness(string businessEmail)
    {
        var result = await httpClient.GetFromJsonAsync<List<UserVoucherDTO>>($"/api/uservoucher/uservoucherbusiness/{businessEmail}");
        return result!;
    }

    public async Task<List<UserVoucherDTO>> GetAllByUser(int userId)
    {
        var result = await httpClient.GetFromJsonAsync<List<UserVoucherDTO>>($"/api/uservoucher/uservoucheruser/{userId}");
        return result!;
    }

    public async Task<List<UserVoucherDTO>> GetAllByVoucher(int voucherId)
    {
        var result = await httpClient.GetFromJsonAsync<List<UserVoucherDTO>>($"/api/uservoucher/uservouchervoucher/{voucherId}");
        return result!;
    }

    public async Task<List<UserVoucherDTO>> GetAllUserVouchers()
    {
        var result = await httpClient.GetFromJsonAsync<List<UserVoucherDTO>>($"/api/uservoucher/");
        return result!;
    }

    public async Task<UserVoucherDTO> GetById(int id)
    {
        var result = await httpClient.GetFromJsonAsync<UserVoucherDTO>($"/api/uservoucher/uservoucher/{id}");
        return result!;
    }

    public Task<UserVoucherDTO> UpdateUserVoucher(UserVoucherDTO userVoucher)
    {
        throw new NotImplementedException();
    }
}
