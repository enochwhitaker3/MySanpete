using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorClassLibrary.Services;
using System.Security.Claims;

namespace MySanpeteWeb.Services;

public class WindowsUserState : IUserState
{
    private readonly NavigationManager navManager;
    private readonly AuthenticationStateProvider authenticationStateProvider;

    public WindowsUserState(NavigationManager navManager, AuthenticationStateProvider authenticationStateProvider)
    {
        this.navManager = navManager;
        this.authenticationStateProvider = authenticationStateProvider;
        Task.Run(async () =>
        {
            AuthenticationState authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            user = authState.User;
        }).Wait();
    }

    private ClaimsPrincipal user;
    public ClaimsPrincipal User { get => user; }


    public async Task Login()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        if(authState.User == null || authState.User.Identity == null || authState.User.Identity.IsAuthenticated == false)
        {
            navManager.NavigateTo($"Account/Login?redirectUri={navManager.Uri}", true);
            return;
        }
        user = authState.User;
    }

    public async Task Logout()
    {
        navManager.NavigateTo($"Account/Logout", true);
        await Task.CompletedTask;
    }
}
