using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatbot.Data.Migrations
{
    public partial class addedWordCountUsesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordCountUses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Word = table.Column<string>(nullable: false),
                    Count = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordCountUses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "WordCountUseNameIndex",
                table: "WordCountUses",
                column: "Word")
                .Annotation("SqlServer:Include", new[] { "Count" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordCountUses");
        }
    }
}
