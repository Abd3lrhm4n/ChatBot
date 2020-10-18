using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatbot.Data.Migrations
{
    public partial class addedsenderboolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SenderIsBot",
                table: "Messages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderIsBot",
                table: "Messages");
        }
    }
}
