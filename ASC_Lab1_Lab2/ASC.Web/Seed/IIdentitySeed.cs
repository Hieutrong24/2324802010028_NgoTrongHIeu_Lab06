namespace ASC.Web.Seed;

public interface IIdentitySeed
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
