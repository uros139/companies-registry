using System.Text;
using CompaniesRegistry.Application.Abstractions.Authentication;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Infrastructure.Authentication;
using CompaniesRegistry.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CompaniesRegistry.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration) =>
    services
        .AddDatabase(configuration)
        .AddRepositories()
        .AddAuthenticationInternal(configuration);

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database")));
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) => services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    private static IServiceCollection AddAuthenticationInternal(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<ITokenProvider, TokenProvider>();

        return services;
    }
}
