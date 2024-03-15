using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using RazorClassLibrary.DTOs;
using System.Diagnostics;

namespace MySanpeteWeb.Services;

public class WebVoucherService : IVoucherService
{
    private readonly IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebVoucherService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<VoucherDTO> AddVoucher(AddVoucherRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        Voucher newVoucher = new Voucher()
        {
            BusinessId = request.BusinessId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            PromoCode = request.PromoCode ?? "",
            PromoDescription = request.PromoDescription ?? "",
            PromoName = request.PromoName ?? "",
            PromoStock = request.PromoStock,
            RetailPrice = request.RetailPrice,
            TotalReclaimable = request.TotalReclaimable,
        };
        if (newVoucher is not null)
        {
            if (newVoucher.EndDate < newVoucher.StartDate)
            {
                throw new Exception("Voucher end date is before start date");
            }

            var businessExists = await context.Businesses.AnyAsync(b => b.Id == request.BusinessId);
            if (!businessExists)
            {
                throw new Exception("Business doesn't exist");
            }

            await context.Vouchers.AddAsync(newVoucher);
            await context.SaveChangesAsync();

            var newestVoucher = await context.Vouchers.Include(v => v.Business).FirstOrDefaultAsync(v => v.Id == newVoucher.Id);

            if (newestVoucher is null)
            {
                throw new Exception("Voucher was created UNsuccesfully");
            }

            return newestVoucher.ToDto();
        }

        throw new Exception("Voucher was null and couldn't be created");
    }

    public async Task<bool> ClaimVoucher(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var userVoucher = await context.UserVouchers.Where(x => x.VoucherId == id).FirstOrDefaultAsync();

        if (userVoucher != null)
        {
            userVoucher.Isused = true;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteVoucher(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        
        var voucher = await context.Vouchers.Include(v => v.Business).Where(x => x.Id == id).FirstOrDefaultAsync();
        if (voucher != null) 
        { 
            context.Vouchers.Remove(voucher);
            await context.SaveChangesAsync();
            return true;
        }

        throw new Exception("Couldn't delete voucher");
    }

    public async Task<List<VoucherDTO>> GetAllBusinessVouchers(int businessId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var vouchers = await context.Vouchers.Include(v => v.Business).Where(x => x.BusinessId == businessId).ToListAsync();
        return vouchers.Select(x => x.ToDto()).ToList();
    }

    public async Task<List<VoucherDTO>> GetAllVouchers()
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var vouchers = await context.Vouchers.Include(v => v.Business).ToListAsync();
        return vouchers.Select(x => x.ToDto()).ToList();
    }

    public async Task<VoucherDTO?> GetVoucher(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var voucher = await context.Vouchers.Include(v => v.Business).Where(x => x.Id == id).FirstOrDefaultAsync();
        if (voucher is not null)
        {
            return voucher.ToDto();
        }
        return null;
    }

    public async Task<VoucherDTO?> UpdateVoucher(Voucher voucher)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var voucherUnderChange = await context.Vouchers.Include(v => v.Business).Where(x => x.Id == voucher.Id).FirstOrDefaultAsync();

        if (voucherUnderChange is not null)
        {
            voucherUnderChange.StartDate = voucher.StartDate;
            voucherUnderChange.EndDate = voucher.EndDate;
            voucherUnderChange.PromoCode = voucher.PromoCode;
            voucherUnderChange.PromoName = voucher.PromoName;
            voucherUnderChange.PromoDescription = voucher.PromoDescription;
            voucherUnderChange.PromoStock = voucher.PromoStock;
            voucherUnderChange.RetailPrice = voucher.RetailPrice;
            voucherUnderChange.TotalReclaimable = voucher.TotalReclaimable;
        }
        else { return null; }

        context.Vouchers.Update(voucherUnderChange);
        await context.SaveChangesAsync();
        return voucherUnderChange.ToDto();
    }
}
