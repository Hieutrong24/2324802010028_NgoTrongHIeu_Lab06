namespace ASC.Web.Configuration;

public class ApplicationSettings
{
    public const string SectionName = "ApplicationSettings";

    public string ApplicationTitle { get; set; } = "Automobile Service Center";
    public string CompanyName { get; set; } = "ASC Auto Care";
    public string SupportEmail { get; set; } = "support@asc.local";
    public string TagLine { get; set; } = "Modern automobile service management";
    public string DashboardWelcomeMessage { get; set; } = "Manage customers, work orders, products, and master data from one place.";
}
