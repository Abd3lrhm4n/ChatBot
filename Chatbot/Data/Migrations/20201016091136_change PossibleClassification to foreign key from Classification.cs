using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatbot.Data.Migrations
{
    public partial class changePossibleClassificationtoforeignkeyfromClassification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "BotSpeechClassesIndex",
                table: "BotSpeeches");

            migrationBuilder.DropColumn(
                name: "PossibleClasses",
                table: "BotSpeeches");

            migrationBuilder.AddColumn<int>(
                name: "ClassificationId",
                table: "BotSpeeches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BotSpeeches_ClassificationId",
                table: "BotSpeeches",
                column: "ClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BotSpeeches_Classifications_ClassificationId",
                table: "BotSpeeches",
                column: "ClassificationId",
                principalTable: "Classifications",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotSpeeches_Classifications_ClassificationId",
                table: "BotSpeeches");

            migrationBuilder.DropIndex(
                name: "IX_BotSpeeches_ClassificationId",
                table: "BotSpeeches");

            migrationBuilder.DropColumn(
                name: "ClassificationId",
                table: "BotSpeeches");

            migrationBuilder.AddColumn<string>(
                name: "PossibleClasses",
                table: "BotSpeeches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "BotSpeechClassesIndex",
                table: "BotSpeeches",
                column: "PossibleClasses")
                .Annotation("SqlServer:Include", new[] { "Phrase" });
        }
    }
}
