using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobShopAPI.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    IdPlan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSimulation = table.Column<int>(type: "int", nullable: false),
                    IdJob = table.Column<int>(type: "int", nullable: false),
                    IdOperation = table.Column<int>(type: "int", nullable: false),
                    IdMachine = table.Column<int>(type: "int", nullable: false),
                    initialTime = table.Column<int>(type: "int", nullable: false),
                    finalTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.IdPlan);
                    table.ForeignKey(
                        name: "FK_Plan_Jobs_IdJob",
                        column: x => x.IdJob,
                        principalTable: "Jobs",
                        principalColumn: "IdJob",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plan_Machines_IdMachine",
                        column: x => x.IdMachine,
                        principalTable: "Machines",
                        principalColumn: "IdMachine",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plan_Operations_IdOperation",
                        column: x => x.IdOperation,
                        principalTable: "Operations",
                        principalColumn: "IdOperation",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plan_Simulations_IdSimulation",
                        column: x => x.IdSimulation,
                        principalTable: "Simulations",
                        principalColumn: "IdSimulation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plan_IdJob",
                table: "Plan",
                column: "IdJob");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_IdMachine",
                table: "Plan",
                column: "IdMachine");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_IdOperation",
                table: "Plan",
                column: "IdOperation");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_IdSimulation",
                table: "Plan",
                column: "IdSimulation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plan");
        }
    }
}
