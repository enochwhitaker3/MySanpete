using Auth0.OidcClient;
using Microsoft.Extensions.Configuration;
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
            builder.Configuration.AddUserSecrets("147a395d-350b-47ec-852a-d7e5339b58dc");

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "dev-013fwxix4dwe1jea.us.auth0.com",
                ClientId = "w7WZdACmKqbjV3Q5uIwvItVgJyUv4aQU",
                RedirectUri = "myapp://callback",
                PostLogoutRedirectUri = "myapp://callback",
                Scope = "openid profile email",
            }));

            return builder.Build();
        }
    }
}
