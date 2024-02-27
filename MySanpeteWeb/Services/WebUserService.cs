using Microsoft.EntityFrameworkCore;
using MySanpeteWeb.Data;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebUserService : IUserService
{
    private IDbContextFactory<MySanpeteDbContext> dbContextFactory;
    public WebUserService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }
    public async Task<UserDTO?> AddUser(string email)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        //Check if user doesn't exist
        var repeats = await context.EndUsers.Where(u => u.UserEmail == email).ToListAsync();
        if(repeats.Count() > 0)
        {
            return null;
        }

        var newUser = new EndUser()
        {
            UserEmail = email,
            UserRoleId = 1,
        };

        await context.EndUsers.AddAsync(newUser);
        return newUser.ToDto();

    }

    public Task<bool> DeleteUser(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserDTO>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetUser(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetUser(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> PatchUser(UserDTO user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetRole(SetRoleRequest request)
    {
        throw new NotImplementedException();
    }
}
