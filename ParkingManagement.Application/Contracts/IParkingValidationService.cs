namespace ParkingManagement.Application.Contracts;

public interface IParkingValidationService
{
    (bool IsValid, string Message) ValidateVehicleRegistration(string registration);
}
