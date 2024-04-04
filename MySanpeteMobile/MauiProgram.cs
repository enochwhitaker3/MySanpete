using Auth0.OidcClient;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

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
                Domain = builder.Configuration["Auth0:Domain"],
                ClientId = builder.Configuration["Auth0:ClientId"],
                RedirectUri = "myapp://callback",
                PostLogoutRedirectUri = "myapp://callback",
                Scope = "openid profile email",
            }));

            

            return builder.Build();
        }
    }
}
