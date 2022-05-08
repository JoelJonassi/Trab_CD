using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobShopAPI.Migrations
{
    public partial class SimulationToDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Time",
                columns: table => new
                {
                    IdTime = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<int>(type: "int", nullable: false),
                    JobIdJob = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.IdTime);
                    table.ForeignKey(
                        name: "FK_Time_Job_JobIdJob",
                        column: x => x.JobIdJob,
                        principalTable: "Job",
                        principalColumn: "IdJob");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Time_JobIdJob",
                table: "Time",
                column: "JobIdJob");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Time");
        }
    }
}
