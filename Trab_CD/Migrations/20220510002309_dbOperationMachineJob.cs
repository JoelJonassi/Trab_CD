using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobShopAPI.Migrations
{
    public partial class dbOperationMachineJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    IdMachine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineÑame = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    OperationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMachine = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<int>(type: "int", nullable: false)
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
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    IdJob = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameJob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdOperation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.IdJob);
                    table.ForeignKey(
                        name: "FK_Jobs_Operations_IdOperation",
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
                        name: "FK_MachineOperation_Machines_IdOperation",
                        column: x => x.IdOperation,
                        principalTable: "Machines",
                        principalColumn: "IdMachine",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineOperation_Operations_IdMachine",
                        column: x => x.IdMachine,
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
                    IdJob = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.IdSimulation);
                    table.ForeignKey(
                        name: "FK_Simulations_Jobs_IdJob",
                        column: x => x.IdJob,
                        principalTable: "Jobs",
                        principalColumn: "IdJob",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_IdOperation",
                table: "Jobs",
                column: "IdOperation");

            migrationBuilder.CreateIndex(
                name: "IX_MachineOperation_IdOperation",
                table: "MachineOperation",
                column: "IdOperation");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_IdJob",
                table: "Simulations",
                column: "IdJob");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineOperation");

            migrationBuilder.DropTable(
                name: "Simulations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Operations");
        }
    }
}
