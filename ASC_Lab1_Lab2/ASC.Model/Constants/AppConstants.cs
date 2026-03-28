namespace ASC.Model.Constants;

public static class AppConstants
{
    public const string RoleAdmin = "Admin";
    public const string RoleEngineer = "Engineer";
    public const string RoleCustomer = "Customer";

    public static readonly IReadOnlyList<string> Roles =
    [
        RoleAdmin,
        RoleEngineer,
        RoleCustomer
    ];
}
