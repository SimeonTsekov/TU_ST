using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
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

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>().HasData(
            new UserModel
            {
                Id = 1, // Set the primary key
                Email = "admin@gmail.com",
                Username = "admin",
                Password = "admin", // In a real-world scenario, this should be hashed
                Age = 20,
                Height = 180,
                Sex = SexEnum.Male,
                CreatedDate = DateTime.Now,
            });

        // Assuming Role has an Id property that serves as the primary key
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1, // Set the primary key for the Role
                UserId = 1, // This should match the UserId of the user you want to link this role to
                Roles = RolesEnum.Admin
            }
        );
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseModel && e.State == EntityState.Added);

        foreach (var entityEntry in entries)
        {
            ((BaseModel)entityEntry.Entity).CreatedDate = DateTime.Now;
        }

        return base.SaveChanges();
    }

}