using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobChat.ChatHubMicroservice.Infra.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectedUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ContextUserId = table.Column<string>(nullable: true),
                    ConnectionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectedUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfflineTextMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReceiverId = table.Column<Guid>(nullable: false),
                    SenderId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfflineTextMessage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectedUser");

            migrationBuilder.DropTable(
                name: "OfflineTextMessage");
        }
    }
}
