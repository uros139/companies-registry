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

app.UseDynamicPort();

app.Run();

namespace CompaniesRegistry.Api
{
    public partial class Program { }
}
