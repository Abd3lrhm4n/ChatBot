using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatbot.Data.Migrations
{
    public partial class addedlanguagecolumntoWordClassifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "WordClassifications",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "WordClassifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "WordClassifications");

            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "WordClassifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
