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

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>()
            .ToTable("UserRoles")
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(sc => sc.User)
            .WithMany(s => s.UserRoles)
            .HasForeignKey(sc => sc.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(sc => sc.Role)
            .WithMany(c => c.UserRoles)
            .HasForeignKey(sc => sc.RoleId);

        modelBuilder.Entity<Role>().HasData(Role.GetValues());

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = 1, RoleId = 1 },
            new UserRole { UserId = 1, RoleId = 2 });

        modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    Email = "admin@gmail.com",
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("password"),
                    Age = 20,
                    Height = 180,
                    Sex = Sex.Male,
                    CreatedDate = DateTime.Now
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