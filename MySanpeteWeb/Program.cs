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

DotNetEnv.Env.TraversePath().Load();

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

builder.Services.AddMudServices();

builder.Services.AddAntiforgery(options => { 
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

builder.Services.AddLazyCache(serviceProvider => {
    var cache = new CachingService(CachingService.DefaultCacheProvider);
    cache.DefaultCachePolicy.DefaultCacheDurationSeconds = 60 * 60 * 24; //1m -> 1hr -> 1d
    return cache;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Domain"] ?? throw new Exception("Auth0 domain missing");
    options.ClientId = builder.Configuration["ClientId"] ?? throw new Exception("Auth0 clientid is missing");
    options.AccessDeniedPath = "/";
});

builder.Services.AddControllersWithViews();

builder.Configuration
.AddDotNetEnv(".env", LoadOptions.TraversePath())
.AddEnvironmentVariables();

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


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapRazorPages();

app.UseStatusCodePagesWithRedirects("/404");


app.Run();

public partial class Program { }