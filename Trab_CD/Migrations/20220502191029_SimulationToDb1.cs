using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobShopAPI.Migrations
{
    public partial class SimulationToDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    IdMachine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineÑame = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.IdMachine);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    IdJob = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameJob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SimulationIdSimulation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.IdJob);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    IdOperation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMachine = table.Column<int>(type: "int", nullable: false),
                    JobIdJob = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.IdOperation);
                    table.ForeignKey(
                        name: "FK_Operation_Job_JobIdJob",
                        column: x => x.JobIdJob,
                        principalTable: "Job",
                        principalColumn: "IdJob");
                });

            migrationBuilder.CreateTable(
                name: "Simulations",
                columns: table => new
                {
                    IdSimulation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineIdMachine = table.Column<int>(type: "int", nullable: false),
                    OperationIdOperation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.IdSimulation);
                    table.ForeignKey(
                        name: "FK_Simulations_Machine_MachineIdMachine",
                        column: x => x.MachineIdMachine,
                        principalTable: "Machine",
                        principalColumn: "IdMachine",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Simulations_Operation_OperationIdOperation",
                        column: x => x.OperationIdOperation,
                        principalTable: "Operation",
                        principalColumn: "IdOperation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_SimulationIdSimulation",
                table: "Job",
                column: "SimulationIdSimulation");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_JobIdJob",
                table: "Operation",
                column: "JobIdJob");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_MachineIdMachine",
                table: "Simulations",
                column: "MachineIdMachine");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_OperationIdOperation",
                table: "Simulations",
                column: "OperationIdOperation");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Simulations_SimulationIdSimulation",
                table: "Job",
                column: "SimulationIdSimulation",
                principalTable: "Simulations",
                principalColumn: "IdSimulation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Simulations_SimulationIdSimulation",
                table: "Job");

            migrationBuilder.DropTable(
                name: "Simulations");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "Job");
        }
    }
}
