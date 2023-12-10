using Microsoft.EntityFrameworkCore;
using webApi.Data.Models;

namespace webAPI.Data;

public partial class webAPIDbContext : DbContext
{
    public webAPIDbContext(DbContextOptions<webAPIDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityDataModel> ActivityDataModels { get; set; }

    public virtual DbSet<ActivityRecommendationModel> ActivityRecommendationModels { get; set; }

    public virtual DbSet<HealthDataModel> HealthDataModels { get; set; }

    public virtual DbSet<HealthRecommendationModel> HealthRecommendationModels { get; set; }

    public virtual DbSet<UserModel> UserModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseModel && e.State == EntityState.Added);

        foreach (var entityEntry in entries)
        {
            ((BaseModel) entityEntry.Entity).CreatedDate = DateTime.Now;
        }

        return base.SaveChanges();
    }

}