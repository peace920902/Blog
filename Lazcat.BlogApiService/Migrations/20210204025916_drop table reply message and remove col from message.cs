using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lazcat.BlogApiService.Migrations
{
    public partial class droptablereplymessageandremovecolfrommessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyMessages");

            migrationBuilder.AddColumn<Guid>(
                name: "ReplyId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "Messages");

            migrationBuilder.CreateTable(
                name: "ReplyMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RepliedMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReplyMessages_Messages_RepliedMessageId",
                        column: x => x.RepliedMessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplyMessages_MessageId",
                table: "ReplyMessages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyMessages_RepliedMessageId",
                table: "ReplyMessages",
                column: "RepliedMessageId");
        }
    }
}
