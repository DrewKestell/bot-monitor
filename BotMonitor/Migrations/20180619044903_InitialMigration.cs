﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BotMonitor.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false),
                    Email = table.Column<string>(maxLength: 32, nullable: false),
                    HashedPassword = table.Column<string>(maxLength: 128, nullable: false),
                    ProfilePrivate = table.Column<bool>(nullable: false),
                    Username = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false),
                    CurrentState = table.Column<string>(maxLength: 32, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Level = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(maxLength: 16, nullable: true),
                    UserId = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bots_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false),
                    BotId = table.Column<byte>(nullable: false),
                    Command = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bots_UserId",
                table: "Bots",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_BotId",
                table: "Instructions",
                column: "BotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Bots");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
