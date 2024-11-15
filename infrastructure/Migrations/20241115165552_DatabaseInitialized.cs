using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseInitialized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingSummaries",
                columns: table => new
                {
                    SummaryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalParked = table.Column<int>(type: "int", nullable: false),
                    EmptySlots = table.Column<int>(type: "int", nullable: false),
                    CarCount = table.Column<int>(type: "int", nullable: false),
                    BikeCount = table.Column<int>(type: "int", nullable: false),
                    TruckCount = table.Column<int>(type: "int", nullable: false),
                    MicrobusCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSummaries", x => x.SummaryDate);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehicleType = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParkingCharge = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LicenseNumber",
                table: "Vehicles",
                column: "LicenseNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSummaries");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
