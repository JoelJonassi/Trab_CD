using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobShopAPI.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    IdJob = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameJob = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.IdJob);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    IdMachine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.IdMachine);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    IdOperation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.IdOperation);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOperation",
                columns: table => new
                {
                    IdJob = table.Column<int>(type: "int", nullable: false),
                    IdOperation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOperation", x => new { x.IdJob, x.IdOperation });
                    table.ForeignKey(
                        name: "FK_JobOperation_Jobs_IdJob",
                        column: x => x.IdJob,
                        principalTable: "Jobs",
                        principalColumn: "IdJob",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOperation_Operations_IdOperation",
                        column: x => x.IdOperation,
                        principalTable: "Operations",
                        principalColumn: "IdOperation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineOperation",
                columns: table => new
                {
                    IdMachine = table.Column<int>(type: "int", nullable: false),
                    IdOperation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineOperation", x => new { x.IdMachine, x.IdOperation });
                    table.ForeignKey(
                        name: "FK_MachineOperation_Machines_IdMachine",
                        column: x => x.IdMachine,
                        principalTable: "Machines",
                        principalColumn: "IdMachine",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineOperation_Operations_IdOperation",
                        column: x => x.IdOperation,
                        principalTable: "Operations",
                        principalColumn: "IdOperation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Simulations",
                columns: table => new
                {
                    IdSimulation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSimulation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.IdSimulation);
                    table.ForeignKey(
                        name: "FK_Simulations_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSimulation",
                columns: table => new
                {
                    IdJob = table.Column<int>(type: "int", nullable: false),
                    IdSimulation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSimulation", x => new { x.IdJob, x.IdSimulation });
                    table.ForeignKey(
                        name: "FK_JobSimulation_Jobs_IdJob",
                        column: x => x.IdJob,
                        principalTable: "Jobs",
                        principalColumn: "IdJob",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSimulation_Simulations_IdSimulation",
                        column: x => x.IdSimulation,
                        principalTable: "Simulations",
                        principalColumn: "IdSimulation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOperation_IdOperation",
                table: "JobOperation",
                column: "IdOperation");

            migrationBuilder.CreateIndex(
                name: "IX_JobSimulation_IdSimulation",
                table: "JobSimulation",
                column: "IdSimulation");

            migrationBuilder.CreateIndex(
                name: "IX_MachineOperation_IdOperation",
                table: "MachineOperation",
                column: "IdOperation");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_Id",
                table: "Simulations",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobOperation");

            migrationBuilder.DropTable(
                name: "JobSimulation");

            migrationBuilder.DropTable(
                name: "MachineOperation");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Simulations");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
