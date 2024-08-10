using LeaveTimes.Infrastructure;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    logger.Information("Server booting up...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((_, config) =>
    {
        // Write logs to Console
        config.WriteTo.Console().ReadFrom.Configuration(builder.Configuration);
    });

    builder.AddInfrastructure();

    var app = builder.Build();
    app.UseInfrastructure();
    app.SeedDatabase();
    app.Run();
}
// https://stackoverflow.com/questions/70247187/microsoft-extensions-hosting-hostfactoryresolverhostinglistenerstopthehostexce
catch (Exception ex) when (ex is not HostAbortedException)
{
    logger.Fatal(ex, "Unhandled exception.");
}
finally
{
    logger.Information("Server shutting down...");
}

// Need to make this partial class to be able to reference it in the LeaveTimes.FunctionalTests project.
// It's required only in case of Top-level statements.
namespace LeaveTimes.API
{
    public partial class Program { }
}