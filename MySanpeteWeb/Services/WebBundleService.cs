using Microsoft.EntityFrameworkCore;
using MySanpeteWeb.Data;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Pages;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Runtime.InteropServices;

namespace MySanpeteWeb.Services;

public class WebBundleService : IBundleService
{
    private IDbContextFactory<MySanpeteDbContext> dbContextFactory;

    public WebBundleService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<BundleDTO> AddNewBundle(AddBundleRequest request)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        if(request.Name is null || request.StartDate is null || request.EndDate is null || request.Vouchers is null )
        {
            throw new Exception("Insufficient Request");
        }

        var newBundle = new Bundle()
        {
            BundleName = request.Name,
            EndDate = request.EndDate,
            StartDate = request.StartDate,
        };

        await context.Bundles.AddAsync(newBundle);
        await context.SaveChangesAsync();

        var bundleVouchers = request.Vouchers
                                .Select(x => new BundleVoucher() 
                                {
                                    BundleId = newBundle.Id, 
                                    DiscountPrice = x.RetailPrice, 
                                    VoucherId = x.Id
                                })
                                .ToList();

        foreach(var newBundleVoucher in bundleVouchers)
        {
            context.BundleVouchers.Add(newBundleVoucher);
        }
        await context.SaveChangesAsync();
        return (await context.Bundles.FirstOrDefaultAsync(x => x.Id == newBundle.Id))!.ToDto();
    }

    public async Task<bool> DeleteBundle(int bundleId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var bundleToDelete = await context.Bundles
                                .Include(x => x.BundleVouchers)
                                    .ThenInclude(x => x.Voucher)
                                .FirstOrDefaultAsync(x => x.Id == bundleId);

        if(bundleToDelete is null)
        {
            throw new Exception("Can't delete bundle that doesn't exist");
        }

        foreach(var bundleVoucher in bundleToDelete.BundleVouchers)
        {
            context.BundleVouchers.Remove(bundleVoucher);
        }

        context.Bundles.Remove(bundleToDelete);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<BundleDTO>> GetAllBundles()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var bundles = await context.Bundles
                        .Include (x => x.BundleVouchers)
                            .ThenInclude (x => x.Voucher)
                        .Select(x => x.ToDto())
                        .ToListAsync();

        return bundles;

    }

    public async Task<BundleDTO> GetBundle(int bundleId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var bundle = await context.Bundles
                        .Include(x => x.BundleVouchers)
                            .ThenInclude(x => x.Voucher)
                        .FirstOrDefaultAsync(x => x.Id == bundleId);

        if(bundle is null)
        {
            throw new Exception("Can't find bundle");
        }

        return bundle.ToDto();

    }

    public Task<bool> PurchaseBundle(PurchaseBundleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<BundleDTO> UpdateBundle(BundleDTO bundle)
    {
        throw new NotImplementedException();
    }
}
