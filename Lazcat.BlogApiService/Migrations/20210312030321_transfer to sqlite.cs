using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lazcat.BlogApiService.Migrations
{
    public partial class transfertosqlite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Categories",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Categories", x => x.Id); });

            migrationBuilder.CreateTable(
                "HashTags",
                table => new
                {
                    Id = table.Column<Guid>("TEXT", nullable: false),
                    Name = table.Column<string>("TEXT", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_HashTags", x => x.Id); });

            migrationBuilder.CreateTable(
                "Articles",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>("TEXT", maxLength: 50, nullable: true),
                    Content = table.Column<string>("TEXT", nullable: true),
                    CategoryId = table.Column<int>("INTEGER", nullable: false),
                    Description = table.Column<string>("TEXT", maxLength: 100, nullable: true),
                    CreateTime = table.Column<DateTime>("TEXT", nullable: false),
                    EditTime = table.Column<DateTime>("TEXT", nullable: false),
                    IsPublished = table.Column<bool>("INTEGER", nullable: false),
                    PublishTime = table.Column<DateTime>("TEXT", nullable: true),
                    Cover = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        "FK_Articles_Categories_CategoryId",
                        x => x.CategoryId,
                        "Categories",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ArticleTags",
                table => new
                {
                    ArticleId = table.Column<int>("INTEGER", nullable: false),
                    HashTagId = table.Column<Guid>("TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => new {x.ArticleId, x.HashTagId});
                    table.ForeignKey(
                        "FK_ArticleTags_Articles_ArticleId",
                        x => x.ArticleId,
                        "Articles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ArticleTags_HashTags_HashTagId",
                        x => x.HashTagId,
                        "HashTags",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Messages",
                table => new
                {
                    Id = table.Column<Guid>("TEXT", nullable: false, defaultValueSql: "newid()"),
                    Sender = table.Column<string>("TEXT", nullable: false),
                    Content = table.Column<string>("TEXT", nullable: true),
                    ArticleId = table.Column<int>("INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>("TEXT", nullable: false),
                    ReplyId = table.Column<Guid>("TEXT", nullable: true),
                    IsDeleted = table.Column<bool>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        "FK_Messages_Articles_ArticleId",
                        x => x.ArticleId,
                        "Articles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Articles_CategoryId",
                "Articles",
                "CategoryId");

            migrationBuilder.CreateIndex(
                "IX_Articles_Title",
                "Articles",
                "Title");

            migrationBuilder.CreateIndex(
                "IX_ArticleTags_HashTagId",
                "ArticleTags",
                "HashTagId");

            migrationBuilder.CreateIndex(
                "IX_Messages_ArticleId",
                "Messages",
                "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ArticleTags");

            migrationBuilder.DropTable(
                "Messages");

            migrationBuilder.DropTable(
                "HashTags");

            migrationBuilder.DropTable(
                "Articles");

            migrationBuilder.DropTable(
                "Categories");
        }
    }
}