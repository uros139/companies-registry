namespace CompaniesRegistry.Api.Extensions;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder UseSwaggerWithUI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    /// <summary>
    /// Binds the application to the port specified in the environment (e.g., Render) or does nothing if not set.
    /// </summary>
    public static WebApplication UseDynamicPort(this WebApplication app)
    {
        var port = Environment.GetEnvironmentVariable("PORT");
        if (!String.IsNullOrEmpty(port))
        {
            app.Urls.Add($"http://0.0.0.0:{port}");
            Console.WriteLine($"[Info] Dynamic port binding: 0.0.0.0:{port}");
        }

        return app;
    }
}
