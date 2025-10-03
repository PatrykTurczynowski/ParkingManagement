using ParkingManagement.Domain.Contracts;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Infrastructure.Persistance.Repositories;

public sealed class InMemoryParkingRepository : IParkingRepository
{
    private static readonly List<ParkingSpaceEntity> _spaces = new();
    private static readonly List<VehicleEntity> _vehicles = new();
    private static readonly List<ParkingSessionEntity> _sessions = new();

    public InMemoryParkingRepository()
    {
        // Initialize with 60 parking spaces
        if (!_spaces.Any())
        {
            for (int i = 1; i <= 60; i++)
            {
                _spaces.Add(ParkingSpaceEntity.Create(number: i, isOccupied: false));
            }
        }
    }

    public Task<int> GetAvailableSpacesAsync()
    {
        var availableCount = _spaces.Count(x => x.IsOccupied == false);
        return Task.FromResult(availableCount);
    }

    public Task<int> GetOccupiedSpacesAsync()
    {
        var occupiedCount = _spaces.Count(x => x.IsOccupied == true);
        return Task.FromResult(occupiedCount);
    }

    public Task<ParkingSpaceEntity?> GetFirstAvailableSpaceAsync()
    {
        var space = _spaces
            .Where(x => x.IsOccupied == false)
            .OrderBy(x => x.Number)
            .FirstOrDefault();

        return Task.FromResult(space);
    }

    public Task<ParkingSpaceEntity?> GetParkingSpaceByIdAsync(Guid id)
    {
        var space = _spaces.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(space);
    }

    public Task<VehicleEntity?> GetVehicleByRegistrationAsync(string registration)
    {
        var vehicle = _vehicles.FirstOrDefault(x => x.Registration == registration);
        return Task.FromResult(vehicle);
    }

    public Task<VehicleEntity?> GetVehicleByIdAsync(Guid id)
    {
        var vehicle = _vehicles.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(vehicle);
    }

    public Task<ParkingSessionEntity?> GetActiveSessionByRegistrationAsync(string registration)
    {
        var session = _sessions.SingleOrDefault(x => x.IsActive && x.VehicleRegistration == registration);
        return Task.FromResult(session);
    }

    public Task AddVehicleAsync(VehicleEntity vehicle)
    {
        _vehicles.Add(vehicle);
        return Task.CompletedTask;
    }

    public Task AddParkingSessionAsync(ParkingSessionEntity parkingSession)
    {
        _sessions.Add(parkingSession);
        return Task.CompletedTask;
    }

    public Task UpdateParkingSpaceAsync(ParkingSpaceEntity parkingSpace)
    {
        var space = _spaces.FirstOrDefault(x => x.Id == parkingSpace.Id);
        space = parkingSpace;
        return Task.CompletedTask;
    }

    public Task UpdateParkingSessionAsync(ParkingSessionEntity parkingSession)
    {
        var session = _sessions.FirstOrDefault(x => x.Id == parkingSession.Id);
        session = parkingSession;
        return Task.CompletedTask;
    }
}
