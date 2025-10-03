using ParkingManagement.Domain.Enums;
namespace ParkingManagement.Application.DTOs;

public record class ParkVehicleRequest(string VehicleRegistration, VehicleType VehicleType);
