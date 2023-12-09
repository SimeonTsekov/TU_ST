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
}