using Auth0.OidcClient;
using Microsoft.AspNetCore.Components.Authorization;
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
    private readonly AuthenticationStateProvider authProvider;
    public MauiUserState(AuthenticationStateProvider authProvider)
    {
        this.authProvider = authProvider;
        user = new ClaimsPrincipal();
    }
    private ClaimsPrincipal user; 

    public ClaimsPrincipal User => user;

    public async Task Login()
    {
        await ((Auth0AuthenticationStateProvider)authProvider).LogInAsync();
    }

    public async Task Logout()
    {
        await Task.CompletedTask;
        ((Auth0AuthenticationStateProvider)authProvider).LogOut();
    }
}
