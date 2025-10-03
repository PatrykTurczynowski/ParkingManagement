using ParkingManagement.Application.Common;
using ParkingManagement.Application.DTOs;

namespace ParkingManagement.Domain.Contracts;

public interface IParkingService
{
    Task<Result<ParkingStatusResponse>> GetParkingStatusAsync();
    Task<Result<ParkVehicleResponse>> ParkVehicleAsync(ParkVehicleRequest request);
    Task<Result<ExitVehicleResponse>> ExitVehicleAsync(ExitVehicleRequest request);
}
