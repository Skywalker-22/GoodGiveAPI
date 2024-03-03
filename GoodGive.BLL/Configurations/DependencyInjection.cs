using GoodGive.BLL.Services.Implementations;
using GoodGive.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GoodGive.BLL.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddBLLServices(this IServiceCollection services)
    {
        services.AddTransient<IUserManagementService, UserManagementService>();
        services.AddTransient<IDonationService, DonationService>();

        return services;
    }
}
