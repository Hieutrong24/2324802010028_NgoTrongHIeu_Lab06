using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASC.Web.Models;

public class ServiceRequestCreateViewModel
{
    [Required]
    [Display(Name = "Customer name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; set; }

    [Required]
    [Display(Name = "Vehicle registration")]
    public string VehicleRegistrationNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Car model")]
    public string CarModel { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Service type")]
    public string ServiceType { get; set; } = string.Empty;

    [Display(Name = "Branch")]
    public string? BranchName { get; set; }

    [Display(Name = "Assigned engineer")]
    public string? AssignedEngineerName { get; set; }

    [Display(Name = "Complaint description")]
    public string? ComplaintDescription { get; set; }

    [Display(Name = "Appointment date (UTC)")]
    public DateTime AppointmentDateUtc { get; set; } = DateTime.UtcNow.AddDays(1);

    [Display(Name = "Estimated amount")]
    public decimal? EstimatedAmount { get; set; }

    public List<SelectListItem> ServiceTypes { get; set; } = [];
}
