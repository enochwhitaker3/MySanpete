using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IUserState
{
    public ClaimsPrincipal User { get;}
    public Task Login();
    public Task Logout();
}
