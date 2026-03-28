using System.ComponentModel.DataAnnotations;

namespace ASC.Model.Entities;

public class ServiceRequest : BaseEntity
{
    [Required]
    [MaxLength(30)]
    public string RequestNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(120)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? PhoneNumber { get; set; }

    [Required]
    [MaxLength(30)]
    public string VehicleRegistrationNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(120)]
    public string CarModel { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string ServiceType { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Status { get; set; } = "New";

    [MaxLength(120)]
    public string? AssignedEngineerName { get; set; }

    [MaxLength(120)]
    public string? BranchName { get; set; }

    [MaxLength(1000)]
    public string? ComplaintDescription { get; set; }

    public DateTime AppointmentDateUtc { get; set; }

    public decimal? EstimatedAmount { get; set; }
}
