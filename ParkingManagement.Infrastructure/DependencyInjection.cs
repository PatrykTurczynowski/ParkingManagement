using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.Application.Contracts;
using ParkingManagement.Domain.Contracts;
using ParkingManagement.Infrastructure.Persistance;
using ParkingManagement.Infrastructure.Persistance.Repositories;

namespace ParkingManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IParkingRepository, EfParkingRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ParkingDbContext>());

        return services;
    }
}
