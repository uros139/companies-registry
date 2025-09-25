using CompaniesRegistry.Api.Infrastructure;
using CompaniesRegistry.Api.Infrastructure.ExceptionHandling;

namespace CompaniesRegistry.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(
                    "http://localhost:4200",
                    "https://companies-registry.onrender.com/")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        services.AddSwaggerGen();

        services.AddControllers();

        services.Scan(scan => scan
            .FromAssemblyOf<IExceptionHandlerStrategy>()
            .AddClasses(classes => classes.AssignableTo<IExceptionHandlerStrategy>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        return services;
    }
}
