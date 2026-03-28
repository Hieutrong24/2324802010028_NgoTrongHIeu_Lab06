namespace ASC.Model.Contracts;

public interface IAuditTracker
{
    DateTime CreatedOnUtc { get; set; }
    string? CreatedBy { get; set; }
    DateTime? LastModifiedOnUtc { get; set; }
    string? LastModifiedBy { get; set; }
}
