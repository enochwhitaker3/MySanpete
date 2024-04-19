using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Net.Http.Json;

namespace MySanpeteMobile.Services;

public class MobileVoucherService : IVoucherService
{
    private readonly HttpClient httpClient;
    public MobileVoucherService(HttpClient HttpClient)
    {
        this.httpClient = HttpClient;
    }
    public Task<VoucherDTO> AddVoucher(AddVoucherRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ClaimVoucher(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteVoucher(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<VoucherDTO>> GetAllBusinessVouchers(int businessId)
    {
        var result = await httpClient.GetFromJsonAsync<List<VoucherDTO>>($"/api/voucher/businessvouchers/{businessId}");
        return result!;
    }

    public async Task<List<VoucherDTO>> GetAllVouchers()
    {
        var result = await httpClient.GetFromJsonAsync<List<VoucherDTO>>("/api/voucher/");
        return result!;
    }

    public async Task<VoucherDTO?> GetVoucher(int id)
    {
        var result = await httpClient.GetFromJsonAsync<VoucherDTO>($"/api/voucher/{id}");
        return result!;
    }

    public Task<VoucherDTO?> UpdateVoucher(VoucherDTO voucher)
    {
        throw new NotImplementedException();
    }
}
