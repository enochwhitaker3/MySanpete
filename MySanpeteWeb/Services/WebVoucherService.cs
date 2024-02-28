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
        Voucher newVoucher = new Voucher()
        {
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

    public async Task<bool> ClaimVoucher(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var userVoucher = await context.UserVouchers.Where(x => x.VoucherId == id).FirstOrDefaultAsync();

        if (userVoucher != null)
        {
            userVoucher.Isused = true;
            return true;
        }
        return false;
    }

    public async Task DeleteVoucher(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        
        var voucher = await context.UserVouchers.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (voucher != null) 
        { 
            context.Remove(voucher);
        }
    }

    public async Task<List<VoucherDTO>> GetAllBusinessVouchers(int businessId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var vouchers = await context.Vouchers.Where(x => x.BusinessId == businessId).ToListAsync();
        return vouchers.Select(x => x.ToDto()).ToList();
    }

    public async Task<List<VoucherDTO>> GetAllVouchers()
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var vouchers = await context.Vouchers.ToListAsync();
        return vouchers.Select(x => x.ToDto()).ToList();
    }

    public async Task<VoucherDTO> GetVoucher(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var voucher = await context.Vouchers.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (voucher is not null)
        {
            return voucher.ToDto();
        }
        return new VoucherDTO();
    }

    public async Task UpdateVoucher(Voucher voucher)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var voucherUnderChange = await context.Vouchers.Where(x => x.Id == voucher.Id).FirstOrDefaultAsync();

        if (voucherUnderChange is not null)
        {
            voucherUnderChange.BusinessId = voucher.BusinessId;
            voucherUnderChange.StartDate = voucher.StartDate;
            voucherUnderChange.EndDate = voucher.EndDate;
            voucherUnderChange.PromoCode = voucher.PromoCode;
            voucherUnderChange.PromoName = voucher.PromoName;
            voucherUnderChange.PromoDescription = voucher.PromoDescription;
            voucherUnderChange.PromoStock = voucher.PromoStock;
            voucherUnderChange.RetailPrice = voucher.RetailPrice;
            voucherUnderChange.TotalReclaimable = voucher.TotalReclaimable;
        }
        else { return; }

        context.Update(voucherUnderChange);
    }
}
