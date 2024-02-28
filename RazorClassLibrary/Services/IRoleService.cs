using RazorClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IRoleService
{
    public Task<List<UserRole>> GetAllRoles();
}
