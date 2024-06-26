﻿using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using RazorClassLibrary.DTOs;
using System.Diagnostics;
using Stripe;

namespace MySanpeteWeb.Services;

public class WebVoucherService : IVoucherService
{
    private readonly IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    private IStripeService stripeService;

    public WebVoucherService(IDbContextFactory<MySanpeteDbContext> dbContextFactory, IStripeService stripeService)
    {
        this.dbContextFactory = dbContextFactory;
        this.stripeService = stripeService;
    }

    public async Task<VoucherDTO> AddVoucher(AddVoucherRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        bool isInStripe = stripeService.ValidateStripeId(request.StripeId!);
        if (!isInStripe) { stripeService.AddProductToStripe(request); }

        var promoCodeTwins = await context.Vouchers.AnyAsync(x => x.StartDate <= DateTime.UtcNow && x.EndDate >= DateTime.UtcNow && x.BusinessId == request.BusinessId && x.PromoCode == request.PromoCode && x.IsActive == true);
        if (promoCodeTwins)
        {
            throw new Exception("An active voucher for this business already has this promo code. Create a unique promo code.");
        }

        Voucher newVoucher = new()
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
            StripeId = request.StripeId,
            PriceId = request.PriceId,
            IsActive = request.IsActive,
            IsBundle = request.IsBundle
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

            var newestVoucher = await context.Vouchers.Include(v => v.Business).FirstOrDefaultAsync(v => v.Id == newVoucher.Id) ?? throw new Exception("Voucher was created UNsuccesfully");

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

    public async Task<VoucherDTO?> UpdateVoucher(VoucherDTO voucher)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var voucherUnderChange = await context.Vouchers.Include(v => v.Business).Where(x => x.Id == voucher.Id).FirstOrDefaultAsync();

        if (voucherUnderChange is not null)
        {
            voucherUnderChange.StartDate = voucher.StartDate;
            voucherUnderChange.EndDate = voucher.EndDate;
            voucherUnderChange.PromoCode = voucher.PromoCode ?? "";
            voucherUnderChange.PromoName = voucher.PromoName ?? "";
            voucherUnderChange.PromoDescription = voucher.PromoDescription ?? "";
            voucherUnderChange.PromoStock = voucher.Stock;
            voucherUnderChange.RetailPrice = voucher.RetailPrice;
            voucherUnderChange.TotalReclaimable = voucher.AmmountReclaimable;
            voucherUnderChange.IsBundle = voucher.IsBundle;
            voucherUnderChange.IsActive = voucher.IsActive;
            voucherUnderChange.PriceId = voucher.PriceId;
        }
        else { return null; }

        try
        {
            context.Vouchers.Update(voucherUnderChange);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong with updating this voucher. Please check all fields. {ex}");
        }
        return voucherUnderChange.ToDto();
    }
}
