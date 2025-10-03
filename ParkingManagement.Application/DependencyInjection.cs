using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.Application.Services;
using ParkingManagement.Domain.Contracts;

namespace ParkingManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IParkingService, ParkingService>();
        return services;
    }
}
