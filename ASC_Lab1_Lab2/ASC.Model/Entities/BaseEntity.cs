using ASC.Model.Contracts;

namespace ASC.Model.Entities;

public abstract class BaseEntity : IAuditTracker
{
    public int Id { get; set; }
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public string? LastModifiedBy { get; set; }
}
