using Microsoft.EntityFrameworkCore;
using ParkingManagement.Application.Contracts;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Infrastructure.Persistance;

public class ParkingDbContext : DbContext, IUnitOfWork
{
    private const string PARKING_SCHEMA = "PARKING";

    public DbSet<ParkingSpaceEntity> ParkingSpaceEntities { get; set; }
    public DbSet<VehicleEntity> VehicleEntities { get; set; }
    public DbSet<ParkingSessionEntity> ParkingSessionEntities { get; set; }

    public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParkingSpaceEntity>(m =>
        {
            m.ToTable(name: nameof(ParkingSpaceEntities), schema: PARKING_SCHEMA);
        });

        modelBuilder.Entity<VehicleEntity>(m =>
        {
            m.ToTable(name: nameof(VehicleEntities), schema: PARKING_SCHEMA);
        });

        modelBuilder.Entity<ParkingSessionEntity>(m =>
        {
            m.ToTable(name: nameof(ParkingSessionEntities), schema: PARKING_SCHEMA);
        });
    }
}
