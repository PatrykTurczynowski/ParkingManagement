using ParkingManagement.Application.Common;
using ParkingManagement.Application.Contracts;
using ParkingManagement.Application.DTOs;
using ParkingManagement.Application.Helpers;
using ParkingManagement.Domain.Contracts;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Application.Services;

public class ParkingService(IUnitOfWork unitOfWork, IParkingRepository parkingRepository) : IParkingService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IParkingRepository _parkingRepository = parkingRepository;

    public async Task<Result<ParkingStatusResponse>> GetParkingStatusAsync()
    {
        var availableCount = await _parkingRepository.GetAvailableSpacesAsync();
        var occupiedCount = await _parkingRepository.GetOccupiedSpacesAsync();

        return Result<ParkingStatusResponse>.Success(new ParkingStatusResponse(availableCount, occupiedCount));
    }

    public async Task<Result<ParkVehicleResponse>> ParkVehicleAsync(ParkVehicleRequest request)
    {
        var session = await _parkingRepository.GetActiveSessionByRegistrationAsync(request.VehicleRegistration);
        if (session is not null)
        {
            return Result<ParkVehicleResponse>.BadRequest($"Vehicle {request.VehicleRegistration} is already parked in space {session.SpaceNumber}.");
        }

        var vehicle = await _parkingRepository.GetVehicleByRegistrationAsync(request.VehicleRegistration);
        if (vehicle is null)
        {
            vehicle = VehicleEntity.Create(request.VehicleRegistration, request.VehicleType);
            await _parkingRepository.AddVehicleAsync(vehicle);
        }

        var space = await _parkingRepository.GetFirstAvailableSpaceAsync();
        if (space is null)
        {
            return Result<ParkVehicleResponse>.NotFound("No available parking spaces.");
        }

        space.Allocate(vehicle);
        await _parkingRepository.UpdateParkingSpaceAsync(space);

        session = ParkingSessionEntity.Create(vehicle.Id, space.Id, vehicle.Registration, vehicle.Type, space.Number);
        await _parkingRepository.AddParkingSessionAsync(session);

        await _unitOfWork.SaveChangesAsync();

        return Result<ParkVehicleResponse>.Success(new ParkVehicleResponse(vehicle.Registration, space.Number, DateTimeOffset.UtcNow));
    }

    public async Task<Result<ExitVehicleResponse>> ExitVehicleAsync(ExitVehicleRequest request)
    {
        var session = await _parkingRepository.GetActiveSessionByRegistrationAsync(request.VehicleRegistration);
        if (session is null)
        {
            return Result<ExitVehicleResponse>.NotFound("Vehicle not found or not parked.");
        }

        var space = await _parkingRepository.GetParkingSpaceByIdAsync(session.ParkingSpaceId);
        if (space is null)
        {
            return Result<ExitVehicleResponse>.NotFound("Parking space not found.");
        }

        var timeOut = DateTimeOffset.UtcNow;
        var totalCharge = ParkingChargeHelper.CalculateCharge(session.VehicleType, session.TimeIn, timeOut);

        session.EndSession(timeOut, totalCharge);
        space.Deallocate();

        await _parkingRepository.UpdateParkingSessionAsync(session);
        await _parkingRepository.UpdateParkingSpaceAsync(space);

        await _unitOfWork.SaveChangesAsync();

        return Result<ExitVehicleResponse>.Success(new ExitVehicleResponse(request.VehicleRegistration, totalCharge, session.TimeIn, timeOut));
    }
}
