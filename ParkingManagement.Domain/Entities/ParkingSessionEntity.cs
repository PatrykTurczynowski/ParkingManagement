using ParkingManagement.Domain.Common;
using ParkingManagement.Domain.Enums;

namespace ParkingManagement.Domain.Entities;

public class ParkingSessionEntity : BaseEntity
{
    public Guid VehicleId { get; private set; }
    public Guid ParkingSpaceId { get; private set; }
    public string? VehicleRegistration { get; private set; }
    public VehicleType VehicleType { get; private set; }
    public int SpaceNumber { get; private set; }
    public DateTimeOffset TimeIn { get; private set; }
    public DateTimeOffset? TimeOut { get; private set; }
    public decimal? FinalCharge { get; private set; }
    public bool IsActive { get; private set; }

    public static ParkingSessionEntity Create(Guid vehicleId, Guid parkingSpaceId, string vehicleRegistration, VehicleType vehicleSize, int spaceNumber)
    {
        return new ParkingSessionEntity
        {
            CreatedAt = DateTimeOffset.UtcNow,
            VehicleId = vehicleId,
            ParkingSpaceId = parkingSpaceId,
            VehicleRegistration = vehicleRegistration,
            VehicleType = vehicleSize,
            SpaceNumber = spaceNumber,
            TimeIn = DateTimeOffset.UtcNow,
            TimeOut = null,
            FinalCharge = null,
            IsActive = true,
        };
    }

    public void EndSession(DateTimeOffset timeOut, decimal charge)
    {
        EditedAt = DateTimeOffset.UtcNow;
        TimeOut = timeOut;
        FinalCharge = charge;
        IsActive = false;
    }
}
