using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobShopAPI.Migrations
{
    public partial class db4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Simulations_Jobs_IdJob",
                table: "Simulations");

            migrationBuilder.DropForeignKey(
                name: "FK_Simulations_Machines_IdMachine",
                table: "Simulations");

            migrationBuilder.DropForeignKey(
                name: "FK_Simulations_Operations_IdOperation",
                table: "Simulations");

            migrationBuilder.AddForeignKey(
                name: "FK_Simulations_Jobs_IdJob",
                table: "Simulations",
                column: "IdJob",
                principalTable: "Jobs",
                principalColumn: "IdJob",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Simulations_Machines_IdMachine",
                table: "Simulations",
                column: "IdMachine",
                principalTable: "Machines",
                principalColumn: "IdMachine",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Simulations_Operations_IdOperation",
                table: "Simulations",
                column: "IdOperation",
                principalTable: "Operations",
                principalColumn: "IdOperation",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Simulations_Jobs_IdJob",
                table: "Simulations");

            migrationBuilder.DropForeignKey(
                name: "FK_Simulations_Machines_IdMachine",
                table: "Simulations");

            migrationBuilder.DropForeignKey(
                name: "FK_Simulations_Operations_IdOperation",
                table: "Simulations");

            migrationBuilder.AddForeignKey(
                name: "FK_Simulations_Jobs_IdJob",
                table: "Simulations",
                column: "IdJob",
                principalTable: "Jobs",
                principalColumn: "IdJob");

            migrationBuilder.AddForeignKey(
                name: "FK_Simulations_Machines_IdMachine",
                table: "Simulations",
                column: "IdMachine",
                principalTable: "Machines",
                principalColumn: "IdMachine");

            migrationBuilder.AddForeignKey(
                name: "FK_Simulations_Operations_IdOperation",
                table: "Simulations",
                column: "IdOperation",
                principalTable: "Operations",
                principalColumn: "IdOperation");
        }
    }
}
