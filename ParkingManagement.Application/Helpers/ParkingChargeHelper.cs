using ParkingManagement.Domain.Enums;

namespace ParkingManagement.Application.Helpers;

public static class ParkingChargeHelper
{
    private static readonly Dictionary<VehicleType, decimal> _chargeRates = new()
    {
        { VehicleType.Small, 0.10m },  // £0.10/minute
        { VehicleType.Medium, 0.20m }, // £0.20/minute
        { VehicleType.Large, 0.40m }   // £0.40/minute
    };

    public static decimal CalculateCharge(VehicleType vehicleType, DateTimeOffset timeIn, DateTimeOffset timeOut)
    {
        int minutes = (int)Math.Ceiling((timeOut - timeIn).TotalMinutes);
        decimal rate = _chargeRates[vehicleType];
        decimal baseCharge = minutes * rate;
        decimal additionalCharge = (minutes / 5) * 1m; // £1 every 5 min
        return baseCharge + additionalCharge;
    }
}
