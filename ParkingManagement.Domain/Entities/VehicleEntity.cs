using ParkingManagement.Domain.Common;
using ParkingManagement.Domain.Enums;

namespace ParkingManagement.Domain.Entities;

public class VehicleEntity : BaseEntity
{
    public string Registration { get; private set; }
    public VehicleType Type { get; private set; }

    public static VehicleEntity Create(string registration, VehicleType type)
    {
        return new VehicleEntity
        {
            CreatedAt = DateTimeOffset.UtcNow,
            Registration = registration,
            Type = type,
        };
    }
}
