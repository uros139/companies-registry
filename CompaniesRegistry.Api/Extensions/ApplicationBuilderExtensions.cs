namespace CompaniesRegistry.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)//TODO
    {
        app.UseSwagger();

        return app;
    }
}
