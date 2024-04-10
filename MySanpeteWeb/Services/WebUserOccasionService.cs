using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebUserOccasionService : IUserOccasionService
{
    private readonly IDbContextFactory<MySanpeteDbContext> dbContextFactory;

    public WebUserOccasionService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }
    public async Task<UserOccasionDTO?> AddNewUserOccasion(AddUserOccasionRequest userOccasionRequest)
    {
        if (userOccasionRequest is null)
        {
            throw new Exception("The user occasion request was null");
        }

        using var context = await dbContextFactory.CreateDbContextAsync();

        var user = await context.EndUsers.Where(x => x.Id == userOccasionRequest.UserId).FirstOrDefaultAsync();

        if (user is null)
        {
            throw new Exception("User was not found");
        }

        var occasion = await context.Occasions.Where(x => x.Id == userOccasionRequest.OccasionId).FirstOrDefaultAsync();

        if (occasion is null)
        {
            throw new Exception("Occasion was not found");
        }

        UserOccasion newUserOccasion = new()
        {
            OccasionId = userOccasionRequest.OccasionId,
            UserId = userOccasionRequest.UserId,
        };

        await context.UserOccasions.AddAsync(newUserOccasion);
        await context.SaveChangesAsync();

        return await GetUserOccasion(newUserOccasion.Id);
    }

    public async Task<bool> DeleteUserOccasion(int id)
    {
        if (id <= 0)
        {
            throw new Exception("user occasion id was less than or equal to 0 which doesn't exist");
        }

        using var context = await dbContextFactory.CreateDbContextAsync();

        var selectedUserOccasion = await context.UserOccasions.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (selectedUserOccasion is null)
        {
            return false;
        }

        context.UserOccasions.Remove(selectedUserOccasion);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<UserOccasionDTO?>> GetAllUserOccasions()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var allUserOccasions = await context.UserOccasions
            .Include(x => x.Occasion)
                .ThenInclude(x => x.Business)
            .Include(x => x.User)
            .Select(x => x.ToDto())
            .ToListAsync();

        return allUserOccasions!;
    }

    public async Task<List<UserOccasionDTO?>> GetAllUserOccasionsByOccasion(int occasionId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var allUserOccasions = await context.UserOccasions
            .Include(x => x.Occasion)
                .ThenInclude(x => x.Business)
            .Include(x => x.User)
            .Where(x => x.OccasionId == occasionId)
            .Select(x => x.ToDto())
            .ToListAsync();

        return allUserOccasions!;
    }

    public async Task<List<UserOccasionDTO?>> GetAllUserOccasionsByUser(Guid userGuid)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var allUserOccasions = await context.UserOccasions
            .Include(x => x.Occasion)
                .ThenInclude(x => x.Business)
            .Include(x => x.User)
            .Where(x => x.User.Guid == userGuid)
            .Select(x => x.ToDto())
            .ToListAsync();

        return allUserOccasions!;
    }

    public async Task<UserOccasionDTO?> GetUserOccasion(int id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var userOccasion = await context.UserOccasions
            .Include(x => x.Occasion)
                .ThenInclude(x => x.Business)
            .Include(x => x.User)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (userOccasion is null)
        {
            throw new Exception("User occasion wasn't found");
        }

        return userOccasion.ToDto();
    }

    public async Task<UserOccasionDTO?> UpdateUserOccasion(UserOccasionDTO userOccasionDTO)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var ucuc = await context.UserOccasions.Where(x => x.Id == userOccasionDTO.Id).FirstOrDefaultAsync();

        if (ucuc is null)
        {
            throw new Exception("User occasion wasn't found");
        }

        UserOccasionDTO user = new UserOccasionDTO()
        {
            Id = userOccasionDTO.Id,
            User = userOccasionDTO.User,
            Occasion = userOccasionDTO.Occasion
        };

        return user;
    }
}
