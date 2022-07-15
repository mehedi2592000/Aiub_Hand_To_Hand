using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aiub_Hand_To_Hand_MVC.Migrations
{
    public partial class jobbase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Logins_LoginId",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "LoginId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Logins_LoginId",
                table: "Jobs",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Logins_LoginId",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "LoginId",
                table: "Jobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Logins_LoginId",
                table: "Jobs",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id");
        }
    }
}
