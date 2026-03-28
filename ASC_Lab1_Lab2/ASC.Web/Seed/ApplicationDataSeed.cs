using ASC.Model.Entities;
using ASC.Web.Configuration;
using ASC.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ASC.Web.Seed;

public class ApplicationDataSeed : IApplicationDataSeed
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IOptions<SeedDataOptions> _seedDataOptions;

    public ApplicationDataSeed(ApplicationDbContext dbContext, IOptions<SeedDataOptions> seedDataOptions)
    {
        _dbContext = dbContext;
        _seedDataOptions = seedDataOptions;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var options = _seedDataOptions.Value;

        if (!await _dbContext.MasterDataKeys.AnyAsync(cancellationToken))
        {
            foreach (var group in options.MasterData)
            {
                var key = new MasterDataKey
                {
                    Name = group.Name,
                    DisplayName = group.DisplayName,
                    Description = group.Description,
                    IsActive = true,
                    CreatedBy = "seed"
                };

                foreach (var item in group.Values.OrderBy(x => x.DisplaySequence))
                {
                    key.Values.Add(new MasterDataValue
                    {
                        KeyCode = item.KeyCode,
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        DisplaySequence = item.DisplaySequence,
                        IsActive = true,
                        CreatedBy = "seed"
                    });
                }

                _dbContext.MasterDataKeys.Add(key);
            }
        }

        if (!await _dbContext.Products.AnyAsync(cancellationToken))
        {
            foreach (var product in options.Products)
            {
                _dbContext.Products.Add(new Product
                {
                    Name = product.Name,
                    Sku = product.Sku,
                    Category = product.Category,
                    Description = product.Description,
                    UnitPrice = product.UnitPrice,
                    IsActive = true,
                    CreatedBy = "seed"
                });
            }
        }

        if (!await _dbContext.ServiceRequests.AnyAsync(cancellationToken))
        {
            var counter = 1;
            foreach (var request in options.ServiceRequests)
            {
                _dbContext.ServiceRequests.Add(new ServiceRequest
                {
                    RequestNumber = string.IsNullOrWhiteSpace(request.RequestNumber)
                        ? $"SR-SEED-{counter:000}"
                        : request.RequestNumber,
                    CustomerName = request.CustomerName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    VehicleRegistrationNumber = request.VehicleRegistrationNumber,
                    CarModel = request.CarModel,
                    ServiceType = request.ServiceType,
                    Status = request.Status,
                    AssignedEngineerName = request.AssignedEngineerName,
                    BranchName = request.BranchName,
                    ComplaintDescription = request.ComplaintDescription,
                    AppointmentDateUtc = request.AppointmentDateUtc,
                    EstimatedAmount = request.EstimatedAmount,
                    CreatedBy = "seed"
                });

                counter++;
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
