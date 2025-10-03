using Microsoft.AspNetCore.Mvc.Testing;
using ParkingManagement.Application.DTOs;
using ParkingManagement.Domain.Enums;
using System.Net;
using System.Net.Http.Json;

namespace ParkingManagement.Tests.IntegrationTests;

public class ParkingControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ParkingControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ParkingStatus_ShouldReturnAvailableAndOccupiedSpaces()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/parking");

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ParkingStatusResponse>();

        Assert.NotNull(result);
        Assert.True(result.AvailableSpaces >= 0);
        Assert.True(result.OccupiedSpaces >= 0);
    }

    [Fact]
    public async Task Post_CreateParking_ShouldReturnAllocatedSpace()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new ParkVehicleRequest("ABC1234", VehicleType.Small);

        // Act
        var response = await client.PostAsJsonAsync("/parking", request);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ParkVehicleResponse>();

        Assert.NotNull(result);
        Assert.Equal("ABC1234", result.VehicleRegistration);
        Assert.True(result.SpotNumber > 0);
    }

    [Fact]
    public async Task Post_ExitParking_ShouldReturnFinalCharge()
    {
        // Arrange:
        var client = _factory.CreateClient();
        var createRequest = new ParkVehicleRequest("XYZ1234", VehicleType.Medium);
        var createResponse = await client.PostAsJsonAsync("/parking", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<ParkVehicleResponse>();

        var exitRequest = new ExitVehicleRequest("XYZ1234");

        // Act
        var exitResponse = await client.PostAsJsonAsync("/parking/exit", exitRequest);

        // Assert
        Assert.True(exitResponse.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, exitResponse.StatusCode);

        var exitResult = await exitResponse.Content.ReadFromJsonAsync<ExitVehicleResponse>();

        Assert.NotNull(exitResult);
        Assert.Equal("XYZ1234", exitResult.VehicleRegistration);
        Assert.True(exitResult.VehicleCharge > 0);
    }
}
