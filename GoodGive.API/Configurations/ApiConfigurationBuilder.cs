namespace GoodGive.API.Configurations;

public class ApiConfigurationBuilder
{
    public IConfiguration Configuration { get; set; }

    public ApiConfigurationBuilder(IWebHostEnvironment environment)
    {
        IConfigurationBuilder configBuilder = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        Configuration = configBuilder.Build();
    }
}