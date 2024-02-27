using Microsoft.EntityFrameworkCore;
using MySanpeteWeb.Data;
using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using RazorClassLibrary.DTOs;

namespace MySanpeteWeb.Services;

public class WebVoucherService : IVoucherService
{
    private readonly IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebVoucherService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task AddVoucher(AddVoucherRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        Voucher newVoucher = new Voucher(){
            BusinessId = request.BusinessId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PromoCode = request.PromoCode,
            PromoDescription = request.PromoDescription,
            PromoName = request.PromoName,
            PromoStock = request.PromoStock,
            RetailPrice = request.RetailPrice,
            TotalReclaimable = request.TotalReclaimable
        };
        await context.Vouchers.AddAsync(newVoucher);
    }

    public Task<bool> ClaimVoucher(int id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteVoucher(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Voucher>> GetAllBusinessVouchers(int businessId)
    {
        throw new NotImplementedException();
    }

    public Task<List<VoucherDTO>> GetAllVouchers()
    {
        throw new NotImplementedException();
    }

    public Task<VoucherDTO> GetVoucher(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateVoucher(Voucher voucher)
    {
        throw new NotImplementedException();
    }
}
