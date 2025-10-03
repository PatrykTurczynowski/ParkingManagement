using Microsoft.EntityFrameworkCore;
using ParkingManagement.Domain.Contracts;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Infrastructure.Persistance;
using ParkingManagement.Infrastructure.Persistance.Repositories;

namespace ParkingManagement.Api.Extensions;

public static class ApplicationBuilderExtensions
{

    public static void AddInMemoryRepository(this WebApplicationBuilder builder)
    {
        // Pure InMemory Repository (Singleton)
        builder.Services.AddSingleton<IParkingRepository, InMemoryParkingRepository>();
    }

    public static void AddEFInMemoryDatabase(this WebApplicationBuilder builder)
    {
        // EF Core InMemory Db
        builder.Services.AddDbContext<ParkingDbContext>(opt =>
        {
            opt.UseInMemoryDatabase("ParkingDb");
        });
    }

    public static void AddEFDatabase(this WebApplicationBuilder builder)
    {
        // EF Core MSSQL Db
        var connectionString = builder.Configuration.GetConnectionString("ParkingDbConnection");
        builder.Services.AddDbContext<ParkingDbContext>((opt) =>
        {
            opt.UseSqlServer(connectionString,
                sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(60);
                });
        }, ServiceLifetime.Scoped, ServiceLifetime.Singleton);
    }

    public static void UseSeed(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ParkingDbContext>();

        if (!dbContext.ParkingSpaceEntities.Any())
        {
            var parkingSpaces = Enumerable.Range(1, 60)
                .Select(i => ParkingSpaceEntity.Create(i, false))
                .ToList();

            dbContext.ParkingSpaceEntities.AddRange(parkingSpaces);
            dbContext.SaveChanges();
        }
    }
}
