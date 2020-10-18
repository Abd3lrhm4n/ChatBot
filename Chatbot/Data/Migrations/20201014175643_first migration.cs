using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatbot.Data.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BotSpeeches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phrase = table.Column<string>(nullable: true),
                    PossibleClasses = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotSpeeches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ClassificationNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MessageBody = table.Column<string>(nullable: false),
                    SentAt = table.Column<DateTime>(nullable: false),
                    HumanId = table.Column<string>(nullable: false),
                    BotId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_BotId",
                        column: x => x.BotId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_HumanId",
                        column: x => x.HumanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18, 3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(nullable: true),
                    ClassificationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordClassifications_Classifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "BotSpeechClassesIndex",
                table: "BotSpeeches",
                column: "PossibleClasses")
                .Annotation("SqlServer:Include", new[] { "Phrase" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_BotId",
                table: "Messages",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "HumanIdIndex",
                table: "Messages",
                column: "HumanId");

            migrationBuilder.CreateIndex(
                name: "MessageBodyIndex",
                table: "Messages",
                column: "MessageBody");

            migrationBuilder.CreateIndex(
                name: "ProductNameArIndex",
                table: "Products",
                column: "NameAr");

            migrationBuilder.CreateIndex(
                name: "ProductNameEnIndex",
                table: "Products",
                column: "NameEn");

            migrationBuilder.CreateIndex(
                name: "ProductPriceIndex",
                table: "Products",
                column: "Price")
                .Annotation("SqlServer:Include", new[] { "NameAr", "NameEn" });

            migrationBuilder.CreateIndex(
                name: "WordClassificationIdIndex",
                table: "WordClassifications",
                column: "ClassificationId")
                .Annotation("SqlServer:Include", new[] { "Word" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotSpeeches");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "WordClassifications");

            migrationBuilder.DropTable(
                name: "Classifications");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");
        }
    }
}
