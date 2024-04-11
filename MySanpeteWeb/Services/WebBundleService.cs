using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Pages;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System.Runtime.InteropServices;
using System.Threading.RateLimiting;

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

        if (request.Name is null || request.StartDate is null || request.EndDate is null || request.Vouchers is null)
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
                                    VoucherId = x.Id,
                                })
                                .ToList();

        foreach (var newBundleVoucher in bundleVouchers)
        {
            context.BundleVouchers.Add(newBundleVoucher);
        }
        await context.SaveChangesAsync();
        var bundle = await context.Bundles
            .Include(x => x.BundleVouchers)
                .ThenInclude(x => x.Voucher)
                    .ThenInclude(x => x!.Business)
            .FirstOrDefaultAsync(x => x.Id == newBundle.Id);
        return bundle!.ToDto();
    }

    public async Task<bool> DeleteBundle(int bundleId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var bundleToDelete = await context.Bundles
                                .Include(x => x.BundleVouchers)
                                    .ThenInclude(x => x.Voucher)
                                .FirstOrDefaultAsync(x => x.Id == bundleId);

        if (bundleToDelete is null)
        {
            throw new Exception("Can't delete bundle that doesn't exist");
        }

        foreach (var bundleVoucher in bundleToDelete.BundleVouchers)
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
                        .Include(x => x.BundleVouchers)
                            .ThenInclude(x => x.Voucher)
                                .ThenInclude(x => x!.Business)
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
                                .ThenInclude(x => x.Business)
                        .FirstOrDefaultAsync(x => x.Id == bundleId);

        if (bundle is null)
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
                                     .ThenInclude(b => b!.UserVouchers)
                                   .FirstOrDefaultAsync(b => b.Id == request.BundleId);

        if (bundleToPurchase is null)
        {
            throw new Exception("Cannot purchase bundle that doesn't exist");
        }

        var userToPurchase = await context.EndUsers.FirstOrDefaultAsync(u => u.Guid == request.UserId);

        if (userToPurchase is null)
        {
            throw new Exception("Cannot purchase a bundle for a user that doesn't exist");
        }

        //TODO: Ping stripe and make sure the database has a new. 

        var purchases = bundleToPurchase.BundleVouchers.Select(bv => new UserVoucher()
        {
            ChargeId = request!.ChargeId!,
            FinalPrice = bv.DiscountPrice,
            PurchaseDate = DateTime.Now.ToUniversalTime(),
            TotalReclaimable = bv.Voucher!.TotalReclaimable ?? 0,
            UserId = userToPurchase.Id,
            TimesClaimed = 0,
            VoucherId = bv.VoucherId ?? throw new Exception("The voucher id was not found")

        });

        foreach (var purchase in purchases)
        {
            context.UserVouchers.Add(purchase);
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<BundleDTO> UpdateBundle(BundleDTO bundle)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        if (bundle.EndDate is null)
        {
            throw new Exception("End date for the occasion needs a value");
        }

        if (bundle.StartDate is null)
        {
            throw new Exception("Start date for the occasion needs a value");
        }

        if (bundle.EndDate < bundle.StartDate)
        {
            throw new Exception("End date needs to be after the start date");
        }

        if (bundle.Name is null || bundle.Name == "")
        {
            throw new Exception("Name needs a value for the occasion");
        }

        var buc = new Bundle()
        {
            EndDate = bundle.EndDate,
            StartDate = bundle.StartDate,
            BundleName = bundle.Name,
            Id = bundle.Id,
        };

        context.Bundles.Update(buc);

        var rangeToDelete = await context.BundleVouchers.Where(x => x.BundleId == buc.Id).ToListAsync();

        context.BundleVouchers.RemoveRange(rangeToDelete);

        if (bundle.Vouchers is not null)
        {
            var bundleVouchers = bundle.Vouchers
                                   .Select(x => new BundleVoucher()
                                   {
                                       BundleId = buc.Id,
                                       DiscountPrice = x.RetailPrice,
                                       VoucherId = x.Id,
                                   })
                                   .ToList();

            foreach (var newBundleVoucher in bundleVouchers)
            {
                context.BundleVouchers.Add(newBundleVoucher);
            }

            await context.SaveChangesAsync();
        }

        var newBundle = await context.Bundles
            .Include(x => x.BundleVouchers)
                .ThenInclude(x => x.Voucher)
                    .ThenInclude(x => x!.Business)
            .FirstOrDefaultAsync(x => x.Id == buc.Id);

        return newBundle!.ToDto();
    }
}
