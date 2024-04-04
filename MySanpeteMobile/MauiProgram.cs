using Auth0.OidcClient;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using MySanpeteMobile.Services;
using RazorClassLibrary.Services;

namespace MySanpeteMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

         
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "dev-013fwxix4dwe1jea.us.auth0.com",
                ClientId = "w7WZdACmKqbjV3Q5uIwvItVgJyUv4aQU",
                RedirectUri = "myapp://callback",
                PostLogoutRedirectUri = "myapp://callback",
                Scope = "openid profile email",
            }));

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IUserState, MauiUserState>();
            builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();

            return builder.Build();
        }
    }
}
