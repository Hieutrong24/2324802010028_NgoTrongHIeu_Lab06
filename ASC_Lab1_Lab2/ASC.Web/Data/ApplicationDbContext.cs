using ASC.Model.Contracts;
using ASC.Model.Entities;
using ASC.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASC.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<MasterDataKey> MasterDataKeys => Set<MasterDataKey>();
    public DbSet<MasterDataValue> MasterDataValues => Set<MasterDataValue>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<MasterDataKey>(entity =>
        {
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.DisplayName).IsRequired();
        });

        builder.Entity<MasterDataValue>(entity =>
        {
            entity.HasIndex(x => new { x.MasterDataKeyId, x.KeyCode }).IsUnique();
            entity.HasOne(x => x.MasterDataKey)
                .WithMany(x => x.Values)
                .HasForeignKey(x => x.MasterDataKeyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Product>(entity =>
        {
            entity.HasIndex(x => x.Sku).IsUnique();
            entity.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
        });

        builder.Entity<ServiceRequest>(entity =>
        {
            entity.HasIndex(x => x.RequestNumber).IsUnique();
            entity.Property(x => x.EstimatedAmount).HasColumnType("decimal(18,2)");
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        TrackAuditColumns();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        TrackAuditColumns();
        return base.SaveChanges();
    }

    private void TrackAuditColumns()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IAuditTracker>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOnUtc = now;
                entry.Entity.LastModifiedOnUtc = now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedOnUtc = now;
            }
        }
    }
}
