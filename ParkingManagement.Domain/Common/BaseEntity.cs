using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset? EditedAt { get; protected set; }
}
