using ParkingManagement.Application.Contracts;

namespace ParkingManagement.Application.Services;

public class ParkingValidationService : IParkingValidationService
{
    public (bool IsValid, string Message) ValidateVehicleRegistration(string registration)
    {
        if (string.IsNullOrWhiteSpace(registration))
        {
            return (false, "Registration is required.");
        }
        if (registration != registration.ToUpper())
        {
            return (false, "Registration must contain only uppercase letters.");
        }

        return (true, string.Empty);
    }
}
