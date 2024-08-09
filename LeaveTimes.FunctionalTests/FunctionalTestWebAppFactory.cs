using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LeaveTimes.Infrastructure.Context;
using LeaveTimes.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LeaveTimes.FunctionalTests;

public class FunctionalTestWebAppFactory : WebApplicationFactory<API.Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        var host = builder.Build();
        host.Start();

        // Get service provider.
        var serviceProvider = host.Services;

        // Create a scope to obtain a reference to the database
        // context (AppDbContext).
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<FunctionalTestWebAppFactory>>();

            try
            {
                SeedData.PopulateData(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {exceptionMessage}", ex.Message);
            }
        }

        return host;
    }

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            string inMemoryCollectionName = Guid.NewGuid().ToString();

            // Add ApplicationDbContext using an in-memory database for testing.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(inMemoryCollectionName);
            });
        });
    }
}