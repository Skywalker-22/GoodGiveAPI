using GoodGive.DAL.Contexts;
using GoodGive.DAL.Seeders.Implementations;
using GoodGive.DAL.Seeders.Interfaces;
using GoodGive.DAL.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodGive.DAL.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUserSeeder, UserSeeder>();
        services.AddScoped<ICharitySeeder, CharitySeeder>();
        services.AddScoped<IDonationSeeder, DonationSeeder>();
        services.AddScoped<IDonationGoalSeeder, DonationGoalSeeder>();
        services.AddScoped<SeedOrchestrator>();

        return services;
    }
}
