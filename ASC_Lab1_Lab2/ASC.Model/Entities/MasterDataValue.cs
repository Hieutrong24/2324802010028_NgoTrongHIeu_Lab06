using System.ComponentModel.DataAnnotations;

namespace ASC.Model.Entities;

public class MasterDataValue : BaseEntity
{
    public int MasterDataKeyId { get; set; }

    [Required]
    [MaxLength(50)]
    public string KeyCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string DisplayName { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Value { get; set; } = string.Empty;

    public int DisplaySequence { get; set; }

    public bool IsActive { get; set; } = true;

    public MasterDataKey? MasterDataKey { get; set; }
}
