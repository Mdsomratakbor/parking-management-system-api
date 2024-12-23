﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using infrastructure;

#nullable disable

namespace infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241115181101_DatabaseInitialized3")]
    partial class DatabaseInitialized3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("domain.entities.ParkingSummary", b =>
                {
                    b.Property<DateTime>("SummaryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BikeCount")
                        .HasColumnType("int");

                    b.Property<int>("CarCount")
                        .HasColumnType("int");

                    b.Property<int>("EmptySlots")
                        .HasColumnType("int");

                    b.Property<int>("MicrobusCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalParked")
                        .HasColumnType("int");

                    b.Property<int>("TruckCount")
                        .HasColumnType("int");

                    b.HasKey("SummaryDate");

                    b.ToTable("ParkingSummaries");
                });

            modelBuilder.Entity("domain.entities.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<int?>("Duration")
                        .HasColumnType("int");

                    b.Property<DateTime>("EntryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExitTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("OwnerAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OwnerPhone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("ParkingCharge")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleId");

                    b.HasIndex("LicenseNumber")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
