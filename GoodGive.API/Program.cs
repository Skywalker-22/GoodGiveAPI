using GoodGive.API.Configurations;
using GoodGive.API.Middleware;
using GoodGive.BLL.Configurations;
using GoodGive.DAL.Configurations;
using GoodGive.DAL.Seeders;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Starting Application");

try
{
    var builder = WebApplication.CreateBuilder(args);

    IWebHostEnvironment environment = builder.Environment;
    IConfiguration configuration = new ApiConfigurationBuilder(environment).Configuration;

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddAPIServices(configuration);
    builder.Services.AddBLLServices();
    builder.Services.AddDALServices(configuration);

    string[]? enabledCORS = null;

    if (environment.IsDevelopment())
    {
        enabledCORS = builder.Configuration.GetSection("Clients")?.GetChildren()?.Select(x => x.Value)?.ToArray();
        if (enabledCORS != null && enabledCORS.Length > 0)
        {
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    corsBuilder =>
                    {
                        corsBuilder.WithOrigins(enabledCORS)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed(host => true);
                    });
            });
        }

    }

    var app = builder.Build();

    if (!environment.IsDevelopment())
    {
        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        using IServiceScope scope = scopeFactory.CreateScope();

        var seedOrchestrator = scope.ServiceProvider.GetRequiredService<SeedOrchestrator>();
        await seedOrchestrator.SeedAllAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.EnableFilter();
    });

    if (enabledCORS != null && enabledCORS.Length > 0)
    {
        app.UseCors();
    }

    app.UseMiddleware<RequestResponseLoggingMiddleware>();

    app.UseRouting();
    app.MapControllers();

    if (environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseHttpsRedirection();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "An error occurred at startup.");
}
finally
{
    LogManager.Shutdown();
}
