using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;

namespace RazorClassLibrary.Services;

public interface IVoucherService
{
    public Task<List<VoucherDTO>> GetAllVouchers();

    public Task<VoucherDTO> GetVoucher(int id);

    public Task<List<Voucher>> GetAllBusinessVouchers(int businessId);

    public Task AddVoucher(AddVoucherRequest request);

    public Task UpdateVoucher(Voucher voucher);

    public Task DeleteVoucher(int id);

    public Task<bool> ClaimVoucher(int id);

}
