using MySanpeteWeb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using RazorClassLibrary.Services;
using MySanpeteTests.Dummies;

namespace MySanpeteTests;

public class MySanpeteFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    public MySanpeteFactory()
    {
        var whereAmI = Environment.CurrentDirectory;

        var backupFile = Directory.GetFiles("../../../../", "*.sql", SearchOption.AllDirectories)
            .Select(f => new FileInfo(f))
            .OrderByDescending(fi => fi.LastWriteTime)
            .First();

        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres")
            .WithPassword("Strong_password_123!")
            .WithBindMount(backupFile.FullName, "/docker-entrypoint-initdb.d/init.sql")
            .Build();

    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connection = _dbContainer.GetConnectionString();
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<MySanpeteDbContext>));
            services.RemoveAll(typeof(IStripeService));
            services.AddDbContextFactory<MySanpeteDbContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));
            services.AddSingleton<IStripeService, DummyStripeService>();
        });
    }
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }


    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

}