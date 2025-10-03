namespace ParkingManagement.Application.DTOs;

public record class ParkingStatusResponse(int AvailableSpaces, int OccupiedSpaces);