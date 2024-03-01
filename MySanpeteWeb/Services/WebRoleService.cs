using Microsoft.EntityFrameworkCore;
using RazorClassLibrary.Data;
using RazorClassLibrary.Services;

namespace MySanpeteWeb.Services;

public class WebRoleService : IRoleService
{
    private IDbContextFactory<MySanpeteDbContext> dbContextFactory { get; set; }
    public WebRoleService(IDbContextFactory<MySanpeteDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<List<UserRole>> GetAllRoles()
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var roles = await context.UserRoles.ToListAsync();

        return roles;
    }
}
