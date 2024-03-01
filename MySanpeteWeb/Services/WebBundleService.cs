using Microsoft.EntityFrameworkCore;
using MySanpeteWeb.Data;
using RazorClassLibrary.DTOs;
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

    public Task<BundleDTO> AddNewBundle(AddBundleRequest request)
    {
        throw new NotImplementedException();
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

    }

    public Task<BundleDTO> GetBundle(int bundleId)
    {
        throw new NotImplementedException();
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
