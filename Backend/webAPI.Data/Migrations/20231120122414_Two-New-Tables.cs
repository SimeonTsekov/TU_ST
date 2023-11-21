using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class TwoNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityRecommendationModels",
                columns: table => new
                {
                    ActivityRecommendationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutRecommendations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityGoals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomActivityAdvice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityRecommendationModels", x => x.ActivityRecommendationId);
                    table.ForeignKey(
                        name: "FK_ActivityRecommendationModels_UserModels_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModels",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthRecommendationModels",
                columns: table => new
                {
                    HealthRecommendationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DietaryAdvice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SleepAdvice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralHealthAdvice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecommendationModels", x => x.HealthRecommendationId);
                    table.ForeignKey(
                        name: "FK_HealthRecommendationModels_UserModels_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModels",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityRecommendationModels_UserId",
                table: "ActivityRecommendationModels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecommendationModels_UserId",
                table: "HealthRecommendationModels",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityRecommendationModels");

            migrationBuilder.DropTable(
                name: "HealthRecommendationModels");
        }
    }
}
