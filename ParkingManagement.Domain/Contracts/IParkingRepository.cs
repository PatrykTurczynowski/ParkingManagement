using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Domain.Contracts;

public interface IParkingRepository
{
    Task<int> GetAvailableSpacesAsync();
    Task<int> GetOccupiedSpacesAsync();
    Task<ParkingSpaceEntity?> GetFirstAvailableSpaceAsync();
    Task<ParkingSpaceEntity?> GetParkingSpaceByIdAsync(Guid id);
    Task<VehicleEntity?> GetVehicleByRegistrationAsync(string registration);
    Task<VehicleEntity?> GetVehicleByIdAsync(Guid id);
    Task<ParkingSessionEntity?> GetActiveSessionByRegistrationAsync(string registration);
    Task AddVehicleAsync(VehicleEntity vehicle);
    Task AddParkingSessionAsync(ParkingSessionEntity parkingSession);
    Task UpdateParkingSpaceAsync(ParkingSpaceEntity space);
    Task UpdateParkingSessionAsync(ParkingSessionEntity parkingSession);
}
