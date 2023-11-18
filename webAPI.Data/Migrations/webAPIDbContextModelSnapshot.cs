﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webAPI.Data;

#nullable disable

namespace webAPI.Data.Migrations
{
    [DbContext(typeof(webAPIDbContext))]
    partial class webAPIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("webApi.Data.Models.ActivityDataModel", b =>
                {
                    b.Property<int>("ActivityDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityDataId"));

                    b.Property<float>("DailyDistance")
                        .HasColumnType("real");

                    b.Property<float>("DailyEnergyBurned")
                        .HasColumnType("real");

                    b.Property<int>("DailySteps")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Workouts")
                        .HasColumnType("int");

                    b.HasKey("ActivityDataId");

                    b.HasIndex("UserId");

                    b.ToTable("ActivityDataModels");
                });

            modelBuilder.Entity("webApi.Data.Models.HealthDataModel", b =>
                {
                    b.Property<int>("HealthDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HealthDataId"));

                    b.Property<float>("BMI")
                        .HasColumnType("real");

                    b.Property<float>("BodyFat")
                        .HasColumnType("real");

                    b.Property<float>("BodyMass")
                        .HasColumnType("real");

                    b.Property<float>("LeanBodyMass")
                        .HasColumnType("real");

                    b.Property<string>("SleepAnalysis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("HealthDataId");

                    b.HasIndex("UserId");

                    b.ToTable("HealthDataModels");
                });

            modelBuilder.Entity("webApi.Data.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Passwords")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserModels");
                });

            modelBuilder.Entity("webApi.Data.Models.ActivityDataModel", b =>
                {
                    b.HasOne("webApi.Data.Models.UserModel", "UserModel")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("webApi.Data.Models.HealthDataModel", b =>
                {
                    b.HasOne("webApi.Data.Models.UserModel", "UserModel")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserModel");
                });
#pragma warning restore 612, 618
        }
    }
}