using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> PurchaseBundle(PurchaseBundleRequest request)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        //Get the bundle
        var bundleToPurchase = await context.Bundles
                                   .Include(b => b.BundleVouchers)
                                    .ThenInclude(b => b.Voucher)
                                     .ThenInclude(b => b.UserVouchers)
                                   .FirstOrDefaultAsync(b => b.Id == request.BundleId);

        if(bundleToPurchase is null)
        {
            throw new Exception("Cannot purcahse bundle that doesn't exist");
        }

        var userToPurchase = await context.EndUsers.FirstOrDefaultAsync(u => u.Guid == request.UserId);

        if(userToPurchase is null) 
        {
            throw new Exception("Cannot purchase a bundle for a user that doesn't exist");
        }

        //Ping stripe and make sure the database has a new. 

        var purchases = bundleToPurchase.BundleVouchers.Select(bv => new UserVoucher()
        {
            ChargeId = request.ChargeId,
            FinalPrice = bv.DiscountPrice,
            PurchaseDate = DateTime.Now.ToUniversalTime(),
            TotalReclaimable = bv.Voucher!.TotalReclaimable,
            UserId = userToPurchase.Id,
            TimesClaimd = 0,
            VoucherId = bv.VoucherId ?? throw new Exception("The voucher id was not found")
            
        });

        foreach(var purchase in purchases)
        {
            context.UserVouchers.Add(purchase);
        }

        await context.SaveChangesAsync();
        return true;
    }

    public Task<BundleDTO> UpdateBundle(BundleDTO bundle)
    {
        throw new NotImplementedException();
    }
}
