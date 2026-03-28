using System.ComponentModel.DataAnnotations;

namespace ASC.Model.Entities;

public class MasterDataKey : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string DisplayName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<MasterDataValue> Values { get; set; } = new List<MasterDataValue>();
}
