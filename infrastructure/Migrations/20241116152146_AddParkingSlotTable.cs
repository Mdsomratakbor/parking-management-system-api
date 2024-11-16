using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParkingSlotTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSummaries");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Vehicles",
                newName: "ParkingSlotId");

            migrationBuilder.CreateTable(
                name: "ParkingSlots",
                columns: table => new
                {
                    ParkingSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlotNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    OccupiedFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OccupiedUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSlots", x => x.ParkingSlotId);
                });

            migrationBuilder.InsertData(
                table: "ParkingSlots",
                columns: new[] { "ParkingSlotId", "IsOccupied", "OccupiedFrom", "OccupiedUntil", "SlotNumber" },
                values: new object[,]
                {
                    { 1, false, null, null, "Slot-001" },
                    { 2, false, null, null, "Slot-002" },
                    { 3, false, null, null, "Slot-003" },
                    { 4, false, null, null, "Slot-004" },
                    { 5, false, null, null, "Slot-005" },
                    { 6, false, null, null, "Slot-006" },
                    { 7, false, null, null, "Slot-007" },
                    { 8, false, null, null, "Slot-008" },
                    { 9, false, null, null, "Slot-009" },
                    { 10, false, null, null, "Slot-010" },
                    { 11, false, null, null, "Slot-011" },
                    { 12, false, null, null, "Slot-012" },
                    { 13, false, null, null, "Slot-013" },
                    { 14, false, null, null, "Slot-014" },
                    { 15, false, null, null, "Slot-015" },
                    { 16, false, null, null, "Slot-016" },
                    { 17, false, null, null, "Slot-017" },
                    { 18, false, null, null, "Slot-018" },
                    { 19, false, null, null, "Slot-019" },
                    { 20, false, null, null, "Slot-020" },
                    { 21, false, null, null, "Slot-021" },
                    { 22, false, null, null, "Slot-022" },
                    { 23, false, null, null, "Slot-023" },
                    { 24, false, null, null, "Slot-024" },
                    { 25, false, null, null, "Slot-025" },
                    { 26, false, null, null, "Slot-026" },
                    { 27, false, null, null, "Slot-027" },
                    { 28, false, null, null, "Slot-028" },
                    { 29, false, null, null, "Slot-029" },
                    { 30, false, null, null, "Slot-030" },
                    { 31, false, null, null, "Slot-031" },
                    { 32, false, null, null, "Slot-032" },
                    { 33, false, null, null, "Slot-033" },
                    { 34, false, null, null, "Slot-034" },
                    { 35, false, null, null, "Slot-035" },
                    { 36, false, null, null, "Slot-036" },
                    { 37, false, null, null, "Slot-037" },
                    { 38, false, null, null, "Slot-038" },
                    { 39, false, null, null, "Slot-039" },
                    { 40, false, null, null, "Slot-040" },
                    { 41, false, null, null, "Slot-041" },
                    { 42, false, null, null, "Slot-042" },
                    { 43, false, null, null, "Slot-043" },
                    { 44, false, null, null, "Slot-044" },
                    { 45, false, null, null, "Slot-045" },
                    { 46, false, null, null, "Slot-046" },
                    { 47, false, null, null, "Slot-047" },
                    { 48, false, null, null, "Slot-048" },
                    { 49, false, null, null, "Slot-049" },
                    { 50, false, null, null, "Slot-050" },
                    { 51, false, null, null, "Slot-051" },
                    { 52, false, null, null, "Slot-052" },
                    { 53, false, null, null, "Slot-053" },
                    { 54, false, null, null, "Slot-054" },
                    { 55, false, null, null, "Slot-055" },
                    { 56, false, null, null, "Slot-056" },
                    { 57, false, null, null, "Slot-057" },
                    { 58, false, null, null, "Slot-058" },
                    { 59, false, null, null, "Slot-059" },
                    { 60, false, null, null, "Slot-060" },
                    { 61, false, null, null, "Slot-061" },
                    { 62, false, null, null, "Slot-062" },
                    { 63, false, null, null, "Slot-063" },
                    { 64, false, null, null, "Slot-064" },
                    { 65, false, null, null, "Slot-065" },
                    { 66, false, null, null, "Slot-066" },
                    { 67, false, null, null, "Slot-067" },
                    { 68, false, null, null, "Slot-068" },
                    { 69, false, null, null, "Slot-069" },
                    { 70, false, null, null, "Slot-070" },
                    { 71, false, null, null, "Slot-071" },
                    { 72, false, null, null, "Slot-072" },
                    { 73, false, null, null, "Slot-073" },
                    { 74, false, null, null, "Slot-074" },
                    { 75, false, null, null, "Slot-075" },
                    { 76, false, null, null, "Slot-076" },
                    { 77, false, null, null, "Slot-077" },
                    { 78, false, null, null, "Slot-078" },
                    { 79, false, null, null, "Slot-079" },
                    { 80, false, null, null, "Slot-080" },
                    { 81, false, null, null, "Slot-081" },
                    { 82, false, null, null, "Slot-082" },
                    { 83, false, null, null, "Slot-083" },
                    { 84, false, null, null, "Slot-084" },
                    { 85, false, null, null, "Slot-085" },
                    { 86, false, null, null, "Slot-086" },
                    { 87, false, null, null, "Slot-087" },
                    { 88, false, null, null, "Slot-088" },
                    { 89, false, null, null, "Slot-089" },
                    { 90, false, null, null, "Slot-090" },
                    { 91, false, null, null, "Slot-091" },
                    { 92, false, null, null, "Slot-092" },
                    { 93, false, null, null, "Slot-093" },
                    { 94, false, null, null, "Slot-094" },
                    { 95, false, null, null, "Slot-095" },
                    { 96, false, null, null, "Slot-096" },
                    { 97, false, null, null, "Slot-097" },
                    { 98, false, null, null, "Slot-098" },
                    { 99, false, null, null, "Slot-099" },
                    { 100, false, null, null, "Slot-100" },
                    { 101, false, null, null, "Slot-101" },
                    { 102, false, null, null, "Slot-102" },
                    { 103, false, null, null, "Slot-103" },
                    { 104, false, null, null, "Slot-104" },
                    { 105, false, null, null, "Slot-105" },
                    { 106, false, null, null, "Slot-106" },
                    { 107, false, null, null, "Slot-107" },
                    { 108, false, null, null, "Slot-108" },
                    { 109, false, null, null, "Slot-109" },
                    { 110, false, null, null, "Slot-110" },
                    { 111, false, null, null, "Slot-111" },
                    { 112, false, null, null, "Slot-112" },
                    { 113, false, null, null, "Slot-113" },
                    { 114, false, null, null, "Slot-114" },
                    { 115, false, null, null, "Slot-115" },
                    { 116, false, null, null, "Slot-116" },
                    { 117, false, null, null, "Slot-117" },
                    { 118, false, null, null, "Slot-118" },
                    { 119, false, null, null, "Slot-119" },
                    { 120, false, null, null, "Slot-120" },
                    { 121, false, null, null, "Slot-121" },
                    { 122, false, null, null, "Slot-122" },
                    { 123, false, null, null, "Slot-123" },
                    { 124, false, null, null, "Slot-124" },
                    { 125, false, null, null, "Slot-125" },
                    { 126, false, null, null, "Slot-126" },
                    { 127, false, null, null, "Slot-127" },
                    { 128, false, null, null, "Slot-128" },
                    { 129, false, null, null, "Slot-129" },
                    { 130, false, null, null, "Slot-130" },
                    { 131, false, null, null, "Slot-131" },
                    { 132, false, null, null, "Slot-132" },
                    { 133, false, null, null, "Slot-133" },
                    { 134, false, null, null, "Slot-134" },
                    { 135, false, null, null, "Slot-135" },
                    { 136, false, null, null, "Slot-136" },
                    { 137, false, null, null, "Slot-137" },
                    { 138, false, null, null, "Slot-138" },
                    { 139, false, null, null, "Slot-139" },
                    { 140, false, null, null, "Slot-140" },
                    { 141, false, null, null, "Slot-141" },
                    { 142, false, null, null, "Slot-142" },
                    { 143, false, null, null, "Slot-143" },
                    { 144, false, null, null, "Slot-144" },
                    { 145, false, null, null, "Slot-145" },
                    { 146, false, null, null, "Slot-146" },
                    { 147, false, null, null, "Slot-147" },
                    { 148, false, null, null, "Slot-148" },
                    { 149, false, null, null, "Slot-149" },
                    { 150, false, null, null, "Slot-150" },
                    { 151, false, null, null, "Slot-151" },
                    { 152, false, null, null, "Slot-152" },
                    { 153, false, null, null, "Slot-153" },
                    { 154, false, null, null, "Slot-154" },
                    { 155, false, null, null, "Slot-155" },
                    { 156, false, null, null, "Slot-156" },
                    { 157, false, null, null, "Slot-157" },
                    { 158, false, null, null, "Slot-158" },
                    { 159, false, null, null, "Slot-159" },
                    { 160, false, null, null, "Slot-160" },
                    { 161, false, null, null, "Slot-161" },
                    { 162, false, null, null, "Slot-162" },
                    { 163, false, null, null, "Slot-163" },
                    { 164, false, null, null, "Slot-164" },
                    { 165, false, null, null, "Slot-165" },
                    { 166, false, null, null, "Slot-166" },
                    { 167, false, null, null, "Slot-167" },
                    { 168, false, null, null, "Slot-168" },
                    { 169, false, null, null, "Slot-169" },
                    { 170, false, null, null, "Slot-170" },
                    { 171, false, null, null, "Slot-171" },
                    { 172, false, null, null, "Slot-172" },
                    { 173, false, null, null, "Slot-173" },
                    { 174, false, null, null, "Slot-174" },
                    { 175, false, null, null, "Slot-175" },
                    { 176, false, null, null, "Slot-176" },
                    { 177, false, null, null, "Slot-177" },
                    { 178, false, null, null, "Slot-178" },
                    { 179, false, null, null, "Slot-179" },
                    { 180, false, null, null, "Slot-180" },
                    { 181, false, null, null, "Slot-181" },
                    { 182, false, null, null, "Slot-182" },
                    { 183, false, null, null, "Slot-183" },
                    { 184, false, null, null, "Slot-184" },
                    { 185, false, null, null, "Slot-185" },
                    { 186, false, null, null, "Slot-186" },
                    { 187, false, null, null, "Slot-187" },
                    { 188, false, null, null, "Slot-188" },
                    { 189, false, null, null, "Slot-189" },
                    { 190, false, null, null, "Slot-190" },
                    { 191, false, null, null, "Slot-191" },
                    { 192, false, null, null, "Slot-192" },
                    { 193, false, null, null, "Slot-193" },
                    { 194, false, null, null, "Slot-194" },
                    { 195, false, null, null, "Slot-195" },
                    { 196, false, null, null, "Slot-196" },
                    { 197, false, null, null, "Slot-197" },
                    { 198, false, null, null, "Slot-198" },
                    { 199, false, null, null, "Slot-199" },
                    { 200, false, null, null, "Slot-200" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ParkingSlotId",
                table: "Vehicles",
                column: "ParkingSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ParkingSlots_ParkingSlotId",
                table: "Vehicles",
                column: "ParkingSlotId",
                principalTable: "ParkingSlots",
                principalColumn: "ParkingSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ParkingSlots_ParkingSlotId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "ParkingSlots");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ParkingSlotId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "ParkingSlotId",
                table: "Vehicles",
                newName: "Duration");

            migrationBuilder.CreateTable(
                name: "ParkingSummaries",
                columns: table => new
                {
                    SummaryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BikeCount = table.Column<int>(type: "int", nullable: false),
                    CarCount = table.Column<int>(type: "int", nullable: false),
                    EmptySlots = table.Column<int>(type: "int", nullable: false),
                    MicrobusCount = table.Column<int>(type: "int", nullable: false),
                    TotalParked = table.Column<int>(type: "int", nullable: false),
                    TruckCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSummaries", x => x.SummaryDate);
                });
        }
    }
}
