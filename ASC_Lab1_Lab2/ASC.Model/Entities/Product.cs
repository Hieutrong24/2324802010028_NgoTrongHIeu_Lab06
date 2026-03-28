using System.ComponentModel.DataAnnotations;

namespace ASC.Model.Entities;

public class Product : BaseEntity
{
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(60)]
    public string Sku { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Category { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public decimal UnitPrice { get; set; }

    public bool IsActive { get; set; } = true;
}
