using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebBusinessService : IBusinessService
{
    private IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    private IGoogleApiService googleApiService;
    public WebBusinessService(IDbContextFactory<MySanpeteDbContext> dbContextFactory, IGoogleApiService googleApiService)
    {
        this.dbContextFactory = dbContextFactory;
        this.googleApiService = googleApiService;
    }
    public async Task<BusinessDTO?> AddBusiness(AddBusinessRequest request)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        Business business = new Business
        {
            BusinessName = request.Name,
            Address = request.Address,
            Logo = request.Logo,
            Website = request.WebURL,
            PhoneNumber = request.PhoneNum,
            Email = request.Email ?? ""
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
        if (business.Email == "" || business.Email is null)
        {
            throw new Exception("Can't make a business without an email");
        }
        var coords = await googleApiService.GetCoordsOfAddress(business.Address);

        if (coords is not null)
        {
            business.XCoordinate = coords.X;
            business.YCoordinate = coords.Y;
        }

        await context.Businesses.AddAsync(business);
        await context.SaveChangesAsync();

        return business.ToDto();
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

    public async Task<List<BusinessDTO>> GetAllBusinesses()
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var businesses = await context.Businesses
            .Include(x => x.Vouchers)
            .Select(x => x.ToDto())
            .ToListAsync();

        return businesses!;
    }

    public async Task<BusinessDTO?> GetBusiness(int id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var business = await context.Businesses
            .Include(x => x.Vouchers)
                .ThenInclude(x => x.UserVouchers)
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();

        if (business is null)
        {
            throw new Exception("No businesses found with given ID");
        }

        return business.ToDto();
    }

    public async Task<BusinessDTO?> GetBusiness(string email)
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

        return business.ToDto();
    }

    public async Task<BusinessDTO?> UpdateBusiness(UpdateBusinessRequest businessRequest)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var buc = await context.Businesses.Where(b => b.Id == businessRequest.Id).FirstOrDefaultAsync();

        if (buc is null)
        {
            throw new Exception("No business found with given ID");
        }

        buc.BusinessName = businessRequest.BusinessName!;
        buc.Address = businessRequest.Address!;
        buc.PhoneNumber = businessRequest.PhoneNumber!;
        buc.Website = businessRequest.WebsiteURL;
        buc.Email = businessRequest.Email!;
        buc.Description = businessRequest.Description!;

        if (businessRequest.Logo is not null)
        {
            buc.Logo = businessRequest.Logo;
        }

        context.Update(buc);
        await context.SaveChangesAsync();

        return buc.ToDto();
    }
}
