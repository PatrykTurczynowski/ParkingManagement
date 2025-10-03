# Car Park Management API
A .NET Core API for managing a car park, built using Clean Architecture principles.

## Architecture Overview
- **Domain**: Core entities (ParkingSpaceEntity, VehicleEntity, ParkingSessionEntity) and business rules.
- **Application**: Use cases via IParkingService and DTOs for API communication.
- **Infrastructure**:  Entity Framework Core with InMemory database (extensible to MSSQL) and repository pattern.
- **Presentation**: REST API with Swagger documentation and custom exception handling.

## Setup
- Install .NET 8 SDK.
- Navigate to `ParkingManagement/ParkingManagement.Api`.
- Run with `dotnet run`.
- API will be available at `https://localhost:5001`.

## Database Configuration Options
Option 1: Pure InMemory Repository (Singleton):
- Fastest, no EF Core dependency `builder.AddInMemoryRepository();`
- Fully in-memory implementation.
- Singleton lifetime - data persists until app restart.
- No database dependencies.

Option 2: EF Core InMemory Database (Default):
- Default configuration `builder.AddEFInMemoryDatabase();`
- EF Core with in-memory provider.
- Automatically seeded with 60 parking spaces.
- Good for development and integration testing.

Option 3: EF Core with SQL Server:
- Production-ready `builder.AddEFDatabase();`
- Real SQL Server database
- Requires connection string in configuration
- Requires initial migration

## Endpoints
- GET /parking: Response `{ "AvailableSpaces": 55, "OccupiedSpaces": 5 }`.
- POST /parking: Parks a vehicle in the first available space. Body: `{ "VehicleRegistration": "ABC1234", "VehicleType": "Small" }`. Response: `{ "VehicleRegistration": "ABC1234", "SpotNumber": 1, "TimeIn": "2025-10-01T10:30:00Z" }`.
- POST /parking/exit: Frees a vehicle's space and calculates charges. Body: `{ "VehicleRegistration": "ABC1234" }`. Response: `{ "VehicleRegistration": "ABC1234", "VehicleCharge": 9.00, "TimeIn": "2025-10-01T10:30:00Z", "TimeOut": "2025-10-01T11:00:00Z" }`.

## Key Features
- Clean Architecture with proper separation of concerns.
- Repository Pattern with both EF Core and InMemory implementations.
- Unit of Work pattern for transaction management.
- Comprehensive error handling with custom exception middleware.
- Vehicle tracking with active parking sessions.
- Automatic space allocation (first available).

## Assumptions
- Parking lot capacity is fixed at 60 spaces (configurable in seeding).
- Vehicle types: Small, Medium, Large with fixed rates.
- Charges calculated per minute with extra £1 every 5 minutes.
- Time calculations use UTC throughout the system.

## Tests
- Navigate to `ParkingManagement/ParkingManagement.Tests`.
- Run `dotnet test` to execute unit and integration tests.
- Unit Tests: Charge calculator, business logic.
- Integration Tests: Full API endpoint testing with WebApplicationFactory.
