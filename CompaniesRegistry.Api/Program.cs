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


app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

app.UseAuthorization();

app.MapControllers();

// Get Render-assigned port or fallback
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

// Listen on all network interfaces (0.0.0.0)
app.Urls.Add($"http://0.0.0.0:{port}");
app.Run();

namespace CompaniesRegistry.Api
{
    public partial class Program { }
}
