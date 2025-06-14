using CompaniesRegistry.Api.Infrastructure;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Application.Behaviors;
using CompaniesRegistry.Application.Features.Companies.Create;
using CompaniesRegistry.Infrastructure.Api.Init;
using CompaniesRegistry.Infrastructure.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(AssemblyFinder.ApplicationAssembly);
});
builder.Services.AddAutoMapper(AssemblyFinder.ApplicationAssembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssembly(AssemblyFinder.ApplicationAssembly);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();
app.UseAuthorization();

app.MapControllers();

app.Run();
