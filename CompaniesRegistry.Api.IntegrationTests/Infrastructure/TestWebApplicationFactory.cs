using CompaniesRegistry.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CompaniesRegistry.Api.IntegrationTests.Infrastructure;

public class TestApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var newServices = new ServiceCollection();

            foreach (var service in services)
            {
                if (!IsEntityFrameworkService(service))
                {
                    newServices.Add(service);
                }
            }

            services.Clear();
            foreach (var service in newServices)
            {
                services.Add(service);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            AddTestAuthentication(services);
        });
    }

    private static bool IsEntityFrameworkService(ServiceDescriptor service) =>
        service.ServiceType.Assembly?.GetName().Name?.StartsWith("Microsoft.EntityFrameworkCore", StringComparison.InvariantCulture) == true;

    private static void AddTestAuthentication(IServiceCollection services)
    {
        services.AddAuthentication("Test")
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });

        services.Configure<AuthorizationOptions>(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder("Test")
                .RequireAuthenticatedUser()
                .Build();
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            try
            {
                using var scope = Services.CreateScope();
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context?.Database.EnsureDeleted();
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
        base.Dispose(disposing);
    }
}
