using Microsoft.EntityFrameworkCore;
using webApi.Data.Models;

namespace webAPI.Data
{
	public class webAPIDbContext : DbContext
	{
		public webAPIDbContext(DbContextOptions<webAPIDbContext> options) 
			:base(options)
		{
				
		}

		public DbSet<UserModel> UserModels { get; set; } = null!;

		public DbSet<HealthDataModel> HealthDataModels { get; set; } = null!;

		public DbSet<ActivityDataModel> ActivityDataModels { get; set; } = null!;

		public DbSet<ActivityRecommendationModel> ActivityRecommendationModels { get; set; } = null!;

		public DbSet<HealthRecommendationModel> HealthRecommendationModels { get; set; } = null!;


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}