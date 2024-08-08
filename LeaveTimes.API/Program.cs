using LeaveTimes.Infrastructure;
using Serilog;

/*
 * Possible improvements:
 * - Specification pattern
 * - Page-based or Cursor-based pagination, although only one month of leave times are queried
 * - Authentication and authorization
 * - Caching
 * - UnitOfWork pattern
 * - HATEOAS to make FE devs' lives easier
 * - Functional tests with Postman
 */

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
