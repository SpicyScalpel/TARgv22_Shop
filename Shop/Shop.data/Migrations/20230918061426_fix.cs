using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shapeship",
                table: "Shapeship");

            migrationBuilder.RenameTable(
                name: "Shapeship",
                newName: "Spaceship");

            migrationBuilder.AlterColumn<string>(
                name: "Company",
                table: "Spaceship",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spaceship",
                table: "Spaceship",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Spaceship",
                table: "Spaceship");

            migrationBuilder.RenameTable(
                name: "Spaceship",
                newName: "Shapeship");

            migrationBuilder.AlterColumn<int>(
                name: "Company",
                table: "Shapeship",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shapeship",
                table: "Shapeship",
                column: "Id");
        }
    }
}
