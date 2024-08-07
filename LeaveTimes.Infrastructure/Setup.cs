using FluentValidation;
using LeaveTimes.Application.Services.Serializer;
using LeaveTimes.Application.Validation;
using LeaveTimes.Domain.Repositories;
using LeaveTimes.Infrastructure.Context;
using LeaveTimes.Infrastructure.Middlewares;
using LeaveTimes.Infrastructure.Repositories;
using LeaveTimes.Infrastructure.ServicesImpl;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LeaveTimes.Infrastructure;

public static class Setup
{
    public const string AllowAllOrigins = "AllowAll";

    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseCors(AllowAllOrigins);
        app.UseMiddleware<ExceptionMiddleware>();
        //app.UseAuthentication();
        //app.UseAuthorization();
        app.MapControllers();
    }

    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        var applicationAssembly = typeof(Application.Application).Assembly;

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        builder.Services.AddValidatorsFromAssembly(applicationAssembly);
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        TypeAdapterConfig.GlobalSettings.Scan(applicationAssembly);

        builder.Services.AddPersistance();
        builder.Services.AddControllers();

        builder.Services.AddScoped<ExceptionMiddleware>();
        builder.Services.AddSingleton<ISerializerService, NewtonSoftSerializerService>();
    }

    private static void AddPersistance(this IServiceCollection services)
    {
        services.AddScoped<ILeaveTimeRepository, LeaveTimeRepository>();

        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(nameof(DatabaseSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<AppDbContext>((p, m) =>
        {
            var databaseSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            m.UseInMemoryDatabase(databaseSettings.ConnectionString);
            //m.UseDatabase(databaseSettings.DBProvider, databaseSettings.ConnectionString);
        });
    }
}
