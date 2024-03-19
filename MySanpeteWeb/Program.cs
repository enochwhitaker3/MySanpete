using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MySanpeteWeb;
using MySanpeteWeb.Components;
using MySanpeteWeb.Services;
using RazorClassLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<MySanpeteDbContext>(config => config.UseNpgsql(builder.Configuration["MySanpeteDB"]));

builder.Services.AddMudServices();

builder.Services.AddSingleton<IOccasionService, WebOccasionService>();
builder.Services.AddSingleton<IBlogService, WebBlogService>();
builder.Services.AddSingleton<IVoucherService, WebVoucherService>();
builder.Services.AddSingleton<IBusinessService, WebBusinessService>();
builder.Services.AddSingleton<IUserService, WebUserService>();
builder.Services.AddSingleton<IPodcastService, WebPodcastService>();
builder.Services.AddSingleton<IRoleService, WebRoleService>();
builder.Services.AddSingleton<IReactionService, WebReactionService>();
builder.Services.AddSingleton<ICommentService, WebCommentService>();
builder.Services.AddSingleton<IBundleService, WebBundleService>();  

//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = builder.Configuration["GoogleClientId"] ?? throw new Exception("BWAH");
//    googleOptions.ClientSecret = builder.Configuration["GoogleClientSecret"] ?? throw new Exception("BWAH");
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();

public partial class Program { }