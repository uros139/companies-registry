using CompaniesRegistry.Api;
using CompaniesRegistry.Api.Extensions;
using CompaniesRegistry.Application;
using CompaniesRegistry.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseStatusCodePages();
app.UseHttpsRedirection();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT");
if (!String.IsNullOrEmpty(port))
{
    // Only override when PORT is set (Render sets this)
    app.Urls.Add($"http://0.0.0.0:{port}");
}

app.Run();

namespace CompaniesRegistry.Api
{
    public partial class Program { }
}
