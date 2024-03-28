using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RazorClassLibrary.Data;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebBusinessService : IBusinessService
{
    private IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebBusinessService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }
    public async Task<Business?> AddBusiness(AddBusinessRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        Business business = new Business
        {
            BusinessName = request.Name,
            Address = request.Address,
            Logo = request.Logo,
            Website = request.WebURL,
            PhoneNumber = request.PhoneNum
        };

        if (business.Address == "")
        {
            throw new Exception("Can't make a business without an address");
        }
        if (business.BusinessName == "")
        {
            throw new Exception("Can't make a business without a name");
        }
        if (business.PhoneNumber == "")
        {
            throw new Exception("Can't make a business without a phone number");
        }
        if (business.Website == "")
        {
            throw new Exception("Can't make a business without a website");
        }

        await context.Businesses.AddAsync(business);
        await context.SaveChangesAsync();

        return business;
    }

    public async Task<bool?> DeleteBusiness(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var bud = await context.Businesses.Where(b => b.Id == id).FirstOrDefaultAsync();

        if (bud is null)
        {
            return false;
        }

        context.Businesses.Remove(bud);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Business>?> GetAllBusinesses()
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var businesses = await context.Businesses.ToListAsync();

        return businesses;
    }

    public async Task<Business?> GetBusiness(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var business = await context.Businesses.Where(b => b.Id == id).FirstOrDefaultAsync();

        if (business is null)
        {
            throw new Exception("No businesses found with given ID");
        }

        return business;
    }

    public async Task<Business?> GetBusiness(string email)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var business = await context.Businesses
            .Include(b => b.Vouchers)
            .ThenInclude(v => v.UserVouchers)
            .ThenInclude(uv => uv.User)
            .Where(b => b.Email == email)
            .FirstOrDefaultAsync();

        if (business is null)
        {
            throw new Exception("No businesses found with given email");
        }

        return business;
    }

    public async Task<Business?> UpdateBusiness(Business business)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var buc = await context.Businesses.Where(b => b.Id == business.Id).FirstOrDefaultAsync();

        if (buc is null)
        {
            throw new Exception("No business found with given ID");
        }

        buc.BusinessName = business.BusinessName;
        buc.Address = business.Address;
        buc.Logo = business.Logo;

        context.Update(buc);
        await context.SaveChangesAsync();

        return buc;
    }
}
