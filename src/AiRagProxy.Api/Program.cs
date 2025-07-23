using AiRagProxy.Api.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.RateLimiting;
using AiRagProxy.Api.Middlewares;
using AiRagProxy.Storage.Configuration;
using AiRagProxy.Storage.Services.Interfaces;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog-Konfiguration laden
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.ConfigureAuthentication(builder.Configuration, builder.Environment);
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureCors();

builder.Services.AddStorageServices(builder.Configuration);

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddHealthChecks();
builder.Services.ConfigureRateLimiting(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var storageService = scope.ServiceProvider.GetRequiredService<IAiRagProxyStorageService>();
    await storageService.MigrateDatabase();
}

// Auth middlewares
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();


app.MapControllers();

// Health Check Endpoint
app.MapHealthChecks("/health");

try
{
    Log.Information("Starting up");
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
    throw;
}
finally
{
    Log.CloseAndFlush();
}