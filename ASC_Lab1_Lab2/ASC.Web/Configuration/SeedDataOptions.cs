namespace ASC.Web.Configuration;

public class SeedDataOptions
{
    public const string SectionName = "SeedData";

    public DefaultAdminOptions DefaultAdmin { get; set; } = new();
    public List<SeedUserOptions> SampleUsers { get; set; } = [];
    public List<MasterDataGroupOptions> MasterData { get; set; } = [];
    public List<ProductSeedOptions> Products { get; set; } = [];
    public List<ServiceRequestSeedOptions> ServiceRequests { get; set; } = [];
}

public class DefaultAdminOptions
{
    public string Email { get; set; } = "admin@asc.local";
    public string Password { get; set; } = "Admin@12345";
    public string FullName { get; set; } = "System Administrator";
    public string? PhoneNumber { get; set; }
}

public class SeedUserOptions
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public List<string> Roles { get; set; } = [];
}

public class MasterDataGroupOptions
{
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<MasterDataValueOptions> Values { get; set; } = [];
}

public class MasterDataValueOptions
{
    public string KeyCode { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int DisplaySequence { get; set; }
}

public class ProductSeedOptions
{
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
}

public class ServiceRequestSeedOptions
{
    public string? RequestNumber { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string VehicleRegistrationNumber { get; set; } = string.Empty;
    public string CarModel { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public string Status { get; set; } = "New";
    public string? AssignedEngineerName { get; set; }
    public string? BranchName { get; set; }
    public string? ComplaintDescription { get; set; }
    public DateTime AppointmentDateUtc { get; set; }
    public decimal? EstimatedAmount { get; set; }
}
