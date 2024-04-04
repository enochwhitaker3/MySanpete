using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteMobile.Services;

public class MauiUserState : IUserState
{
    public ClaimsPrincipal User => throw new NotImplementedException();

    public Task Login()
    {
        throw new NotImplementedException();
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}
