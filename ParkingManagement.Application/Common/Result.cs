using System.Net;

namespace ParkingManagement.Application.Common;

public sealed class Result<T>
{
    public Result(bool isSuccess, T data, HttpStatusCode statusCode, string message)
    {
        IsSuccess = isSuccess;
        Data = data;
        HttpStatus = statusCode;
        Message = message;
    }

    public bool IsSuccess { get; }
    public T Data { get; }
    public HttpStatusCode HttpStatus { get; }
    public int StatusCode => (int)HttpStatus;
    public string Message { get; }

    public static Result<T> Success(T data) => new Result<T>(true, data, HttpStatusCode.OK, null);
    public static Result<T> Error(HttpStatusCode statusCode, string message) => new(false, default, statusCode, message);
    public static Result<T> NotFound(string message) => new(false, default, HttpStatusCode.NotFound, message);
    public static Result<T> BadRequest(string message) => new(false, default, HttpStatusCode.BadRequest, message);
    public static Result<T> Unauthorized(string message) => new(false, default, HttpStatusCode.Unauthorized, message);
    public static Result<T> Forbidden(string message) => new(false, default, HttpStatusCode.Forbidden, message);
    public static Result<T> Conflict(string message) => new(false, default, HttpStatusCode.Conflict, message);
}
