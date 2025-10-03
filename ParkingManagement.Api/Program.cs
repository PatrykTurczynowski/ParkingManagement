using ParkingManagement.Api.Extensions;
using ParkingManagement.Api.Middleware;
using ParkingManagement.Application;
using ParkingManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1: Pure InMemory Repository (Singleton)
// builder.AddInMemoryRepository();

// 2: EF Core InMemory Db (default)
builder.AddEFInMemoryDatabase();

// 3: EF Core MSSQL Db
// builder.AddEFDatabase();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Use Seed for EF Db
app.UseSeed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// THIS IS NEEDED FOR INTEGRATION TESTS
public partial class Program { }