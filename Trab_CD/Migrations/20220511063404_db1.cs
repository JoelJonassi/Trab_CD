using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobShopAPI.Migrations
{
    public partial class db1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Operations_IdOperation",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "IdOperation",
                table: "Jobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Operations_IdOperation",
                table: "Jobs",
                column: "IdOperation",
                principalTable: "Operations",
                principalColumn: "IdOperation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Operations_IdOperation",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "IdOperation",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Operations_IdOperation",
                table: "Jobs",
                column: "IdOperation",
                principalTable: "Operations",
                principalColumn: "IdOperation",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
