namespace ASC.Web.Seed;

public interface IApplicationDataSeed
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
