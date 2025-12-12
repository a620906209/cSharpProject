using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleApi.Migrations
{
    public partial class AddUserRelationToHelloMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HelloMessages",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_HelloMessages_UserId",
                table: "HelloMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelloMessages_Users_UserId",
                table: "HelloMessages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelloMessages_Users_UserId",
                table: "HelloMessages");

            migrationBuilder.DropIndex(
                name: "IX_HelloMessages_UserId",
                table: "HelloMessages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HelloMessages");
        }
    }
}
