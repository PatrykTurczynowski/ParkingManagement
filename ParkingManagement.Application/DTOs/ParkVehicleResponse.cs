namespace ParkingManagement.Application.DTOs;

public record class ParkVehicleResponse(string VehicleRegistration, int SpotNumber, DateTimeOffset TimeIn);
