using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;

namespace RazorClassLibrary.Services;

public interface IVoucherService
{
    public Task<List<VoucherDTO>> GetAllVouchers();

    public Task<VoucherDTO?> GetVoucher(int id);

    public Task<List<VoucherDTO>> GetAllBusinessVouchers(int businessId);

    public Task<VoucherDTO> AddVoucher(AddVoucherRequest request);

    public Task<VoucherDTO?> UpdateVoucher(VoucherDTO voucher);

    public Task<bool> DeleteVoucher(int id);

    public Task<bool> ClaimVoucher(int id);

}
