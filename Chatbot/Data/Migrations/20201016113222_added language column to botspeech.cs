using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatbot.Data.Migrations
{
    public partial class addedlanguagecolumntobotspeech : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "BotSpeeches",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "BotSpeeches");
        }
    }
}
