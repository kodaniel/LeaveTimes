using FluentValidation;
using LeaveTimes.Application.Services.Serializer;
using LeaveTimes.Application.Validation;
using LeaveTimes.Domain.Repositories;
using LeaveTimes.Infrastructure.Endpoints.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;
using Asp.Versioning;
using Asp.Versioning.Conventions;

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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //app.MapEndpoints(); // Minimap API, remove controllers to be able to use it
        app.MapControllers(); // Classic Web API

        SeedDatabase(app);
    }

    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        var applicationAssembly = typeof(Application.Application).Assembly;

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        builder.Services.AddValidatorsFromAssembly(applicationAssembly);
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        builder.Services.AddSwagger();
        builder.Services.AddPersistance(builder.Configuration);
        builder.Services.AddControllers().AddJsonOptions(cfg =>
        {
            cfg.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            cfg.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            cfg.JsonSerializerOptions.WriteIndented = true;
            cfg.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(AllowAllOrigins, pbuilder =>
            {
                pbuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        builder.Services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
        });

        builder.Services.AddScoped<ExceptionMiddleware>();
        builder.Services.AddSingleton<ISerializerService, JsonSerializerService>();
    }

    private static void MapEndpoints(this WebApplication app)
    {
        var versions = app.NewApiVersionSet()
            .HasApiVersion(1)
            .ReportApiVersions()
            .Build();

        var leaveTimeGroup = app.MapGroup("leave-times")
            .WithApiVersionSet(versions);

        leaveTimeGroup.MapLeaveTimeSearchEndpoint();
        leaveTimeGroup.MapLeaveTimeCreationEndpoint();
        leaveTimeGroup.MapLeaveTimeUpdateEndpoint();
        leaveTimeGroup.MapLeaveTimeDeleteEndpoint();
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LeaveTimes.API.xml"));
        });
    }

    private static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        // Register repositories
        services.AddScoped<ILeaveTimeRepository, LeaveTimeRepository>();

        services.AddOptions<DatabaseSettings>()
            .BindConfiguration("DatabaseSettings")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<AppDbContext>((p, m) =>
        {
            m.UseDatabase(p, configuration);
        });
    }

    private static void UseDatabase(this DbContextOptionsBuilder dbContextBuilder, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var dbSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>();

        switch (dbSettings.Value.DbProvider)
        {
            default:
            case DatabaseSettings.InMemory:
                dbContextBuilder.UseInMemoryDatabase(dbSettings.Value.InMemoryOptions!.Name);
                break;
            case DatabaseSettings.Sqlite:
                // TODO: add Sqlite
                break;
        }
    }

    private static void SeedDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        SeedData.PopulateData(context);
    }
}
