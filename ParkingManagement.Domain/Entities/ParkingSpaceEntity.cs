using ParkingManagement.Domain.Common;

namespace ParkingManagement.Domain.Entities;

public class ParkingSpaceEntity : BaseEntity
{
    public int Number { get; private set; }
    public bool IsOccupied { get; private set; }

    public static ParkingSpaceEntity Create(int number, bool isOccupied)
    {
        return new ParkingSpaceEntity
        {
            CreatedAt = DateTimeOffset.UtcNow,
            Number = number,
            IsOccupied = isOccupied,
        };
    }

    public void Allocate(VehicleEntity vehicle)
    {
        EditedAt = DateTimeOffset.UtcNow;
        IsOccupied = true;
    }

    public void Deallocate()
    {
        EditedAt = DateTimeOffset.UtcNow;
        IsOccupied = false;
    }
}
