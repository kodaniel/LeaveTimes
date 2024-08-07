using LeaveTimes.Infrastructure;
using Serilog;

/*
 * Possible improvements:
 * - Specification pattern
 * - Page-based or Cursor-based pagination
 * - Authentication and authorization
 * - Caching
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

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseInfrastructure();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

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
