using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MySanpeteWeb;
using MySanpeteWeb.Components;
using MySanpeteWeb.Services;
using RazorClassLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Auth0.AspNetCore.Authentication;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Stripe;
using LazyCache;
using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySanpeteWeb.Data;
using OpenTelemetry.Resources;
using OpenTelemetry.Logs;
using RazorClassLibrary.DTOs;
using Microsoft.AspNetCore.Html;
using OpenTelemetry.Metrics;
using MySanpeteWeb.Telemetry;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddDbContextFactory<MySanpeteDbContext>(config => config.UseNpgsql(builder.Configuration["MySanpeteDB"]));

var CheckSameSite = (CookieOptions options) =>
{
    if (options.SameSite == SameSiteMode.None && options.Secure == false)
    {
        options.SameSite = SameSiteMode.Unspecified;
    }
};

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
    options.OnAppendCookie = cookieContext => CheckSameSite(cookieContext.CookieOptions);
    options.OnDeleteCookie = cookieContext => CheckSameSite(cookieContext.CookieOptions);
});
StripeConfiguration.ApiKey = builder.Configuration["STRIPE_SECRET_KEY"];
//builder.Services.Configure<StripeOptions>

builder.Services.AddHealthChecks();

builder.Services.AddMudServices();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Expiration = TimeSpan.Zero;
    options.SuppressXFrameOptionsHeader = true;
});

builder.Services.AddRazorPages();
builder.Services.AddSingleton<IOccasionService, WebOccasionService>();
builder.Services.AddSingleton<IBlogService, WebBlogService>();
builder.Services.AddScoped<IVoucherService, WebVoucherService>();
builder.Services.AddSingleton<IBusinessService, WebBusinessService>();
builder.Services.AddSingleton<IUserService, WebUserService>();
builder.Services.AddSingleton<IPodcastService, WebPodcastService>();
builder.Services.AddSingleton<IReactionService, WebReactionService>();
builder.Services.AddSingleton<ICommentService, WebCommentService>();
builder.Services.AddSingleton<IBundleService, WebBundleService>();
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<IUserVoucherService, WebUserVoucherService>();
builder.Services.AddScoped<IUserState, WindowsUserState>();
builder.Services.AddSingleton<IUserOccasionService, WebUserOccasionService>();
builder.Services.AddSingleton<IGoogleApiService, GoogleApiService>();

builder.Services.AddLazyCache(serviceProvider =>
{
    var cache = new CachingService(CachingService.DefaultCacheProvider);
    cache.DefaultCachePolicy.DefaultCacheDurationSeconds = 60 * 60 * 24; //1m -> 1hr -> 1d
    return cache;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Telemetry

const string serviceName = "MySanpete";
const string otelUrl = "http://otel-collector:4317";

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri(otelUrl);
        })
       //.AddConsoleExporter()
       ;
});

builder.Services.AddOpenTelemetry()
     .ConfigureResource(resource => resource.AddService(serviceName))
     //.WithTracing(tracing => tracing
     //    .AddSource(EthanTraces.Name)
     //    .AddAspNetCoreInstrumentation()
     ////.AddConsoleExporter()
     //    .AddOtlpExporter(o =>
     //      o.Endpoint = new Uri(otelUrl)))
     .WithMetrics(metrics => metrics
         .AddAspNetCoreInstrumentation()
         .AddMeter(MySanpeteMetrics.Meter.Name)
     // .AddConsoleExporter()
         .AddOtlpExporter(o =>
           o.Endpoint = new Uri(otelUrl)))
           //.ConfigureResource(res => res.AddService("NewOne"))
           //.WithTracing(t => t
           //     .AddSource(EthanSecondTraces.Name)
           //     .AddAspNetCoreInstrumentation()
           ////.AddConsoleExporter()
           //     .AddOtlpExporter(o =>
           //       o.Endpoint = new Uri(otelUrl)))
           ;

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = "dev-013fwxix4dwe1jea.us.auth0.com";
    options.ClientId = "m0vdCWKGqEoU8BxauGZ73jR6y6qMEFaT";
    options.AccessDeniedPath = "/";
});

builder.Services.AddControllersWithViews();

builder.Configuration
.AddDotNetEnv(".env", LoadOptions.TraversePath())
.AddEnvironmentVariables();

FeatureFlag.IsAvailable = builder.Configuration.GetValue<string>(FeatureFlag.FeatureFlagName) == "true";

DtoConverter.websiteUrl = builder.Configuration.GetValue<string>("WebsiteUrl") ?? "https://mysanpete.azurewebsites.net";

var app = builder.Build();

app.UseRouting();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseAntiforgery();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
});

app.MapGet("/Account/Login", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(redirectUri)
            .Build();

    await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext, string redirectUri = "/") =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(redirectUri)
            .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});


app.MapHealthChecks("/healthCheck", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
        {
            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            [HealthStatus.Degraded] = StatusCodes.Status200OK,
            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
        }
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapRazorPages();

app.UseStatusCodePagesWithRedirects("/404");


app.Run();

public partial class Program { }