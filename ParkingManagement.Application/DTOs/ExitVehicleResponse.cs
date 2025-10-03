namespace ParkingManagement.Application.DTOs;

public record class ExitVehicleResponse(string VehicleRegistration, decimal VehicleCharge, DateTimeOffset TimeIn, DateTimeOffset TimeOut);