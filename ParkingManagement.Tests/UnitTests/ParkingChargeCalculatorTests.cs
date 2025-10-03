using ParkingManagement.Domain.Enums;
using ParkingManagement.Application.Helpers;

namespace ParkingManagement.Tests.UnitTests;

public class ParkingChargeCalculatorTests
{
    [Theory]
    [InlineData(VehicleType.Small, 0, 5, 0.5 + 1)]   // 5 min, £0.10/min + £1
    [InlineData(VehicleType.Medium, 0, 10, 2 + 2)]   // 10 min, £0.20/min + £2
    [InlineData(VehicleType.Large, 0, 3, 1.2 + 0)]   // 3 min, £0.40/min + £0
    public void CalculateCharge_ShouldReturnExpectedCharge(VehicleType type, int startMin, int endMin, decimal expected)
    {
        // Arrange
        var timeIn = DateTimeOffset.UtcNow.AddMinutes(startMin * -1);
        var timeOut = timeIn.AddMinutes(endMin);

        // Act
        var charge = ParkingChargeHelper.CalculateCharge(type, timeIn, timeOut);

        // Assert
        Assert.Equal(expected, charge);
    }

    [Fact]
    public void CalculateCharge_RoundsUpMinutes()
    {
        // Arrange
        var timeIn = DateTimeOffset.UtcNow;
        var timeOut = timeIn.AddSeconds(61);

        // Act
        var charge = ParkingChargeHelper.CalculateCharge(VehicleType.Small, timeIn, timeOut);

        // Assert
        Assert.Equal(0.10m * 2, charge);
    }

    [Fact]
    public void CalculateCharge_ShouldAddAdditionalChargeEvery5Minutes()
    {
        // Arrange
        var timeIn = DateTimeOffset.UtcNow;
        var timeOut = timeIn.AddMinutes(12);
        var vehicleType = VehicleType.Medium;

        // Act
        var charge = ParkingChargeHelper.CalculateCharge(vehicleType, timeIn, timeOut);
        var expected = 2.4m + 2m;

        // Assert
        Assert.Equal(expected, charge);
    }

    [Fact]
    public void CalculateCharge_ShouldReturnZero_When_DurationIsZero()
    {
        // Arrange
        var timeIn = DateTimeOffset.UtcNow;
        var timeOut = timeIn.AddMinutes(0);

        // Act
        var charge = ParkingChargeHelper.CalculateCharge(VehicleType.Large, timeIn, timeOut);

        // Assert
        Assert.Equal(0m, charge);
    }
}
