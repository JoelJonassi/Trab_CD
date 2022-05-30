using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobShopAPI.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "initialTime",
                table: "Plan",
                newName: "InitialTime");

            migrationBuilder.RenameColumn(
                name: "finalTime",
                table: "Plan",
                newName: "FinalTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialTime",
                table: "Plan",
                newName: "initialTime");

            migrationBuilder.RenameColumn(
                name: "FinalTime",
                table: "Plan",
                newName: "finalTime");
        }
    }
}
