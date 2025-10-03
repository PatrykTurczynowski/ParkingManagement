using Microsoft.EntityFrameworkCore;
using ParkingManagement.Domain.Contracts;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Infrastructure.Persistance.Repositories;

public sealed class EfParkingRepository(ParkingDbContext dbContext) : IParkingRepository
{
    private readonly ParkingDbContext _dbContext = dbContext;

    public async Task<int> GetAvailableSpacesAsync()
    {
        return await _dbContext.ParkingSpaceEntities
            .AsNoTracking()
            .CountAsync(x => x.IsOccupied == false);
    }

    public async Task<int> GetOccupiedSpacesAsync()
    {
        return await _dbContext.ParkingSpaceEntities
            .AsNoTracking()
            .CountAsync(x => x.IsOccupied == true);
    }

    public async Task<ParkingSpaceEntity?> GetFirstAvailableSpaceAsync()
    {
        return await _dbContext.ParkingSpaceEntities
            .AsNoTracking()
            .Where(x => x.IsOccupied == false)
            .OrderBy(x => x.Number)
            .FirstOrDefaultAsync();
    }

    public async Task<ParkingSpaceEntity?> GetParkingSpaceByIdAsync(Guid id)
    {
        return await _dbContext.ParkingSpaceEntities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<VehicleEntity?> GetVehicleByRegistrationAsync(string registration)
    {
        return await _dbContext.VehicleEntities
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Registration == registration);
    }

    public async Task<VehicleEntity?> GetVehicleByIdAsync(Guid id)
    {
        return await _dbContext.VehicleEntities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ParkingSessionEntity?> GetActiveSessionByRegistrationAsync(string registration)
    {
        return await _dbContext.ParkingSessionEntities
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.IsActive && x.VehicleRegistration == registration);
    }

    public async Task AddParkingSessionAsync(ParkingSessionEntity parkingSession)
    {
        await _dbContext.ParkingSessionEntities.AddAsync(parkingSession);
    }

    public async Task AddVehicleAsync(VehicleEntity vehicle)
    {
        await _dbContext.VehicleEntities.AddAsync(vehicle);
    }

    public async Task UpdateParkingSpaceAsync(ParkingSpaceEntity parkingSpace)
    {
        _dbContext.ParkingSpaceEntities.Update(parkingSpace);
    }

    public async Task UpdateParkingSessionAsync(ParkingSessionEntity parkingSession)
    {
        _dbContext.ParkingSessionEntities.Update(parkingSession);
    }
}
