using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aiub_Hand_To_Hand_MVC.Migrations
{
    public partial class jobbb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Depertment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jobtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateNow = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Exparied = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_LoginId",
                table: "Jobs",
                column: "LoginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
