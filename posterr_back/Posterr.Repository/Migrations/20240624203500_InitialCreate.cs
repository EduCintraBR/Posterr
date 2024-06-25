using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Posterr.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reg_user",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reg_user", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "reg_post",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_user_owner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(777)", maxLength: 777, nullable: false),
                    reposts = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reg_post", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_reg_post_reg_user_id_user_owner",
                        column: x => x.id_user_owner,
                        principalTable: "reg_user",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reg_content",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    action = table.Column<int>(type: "int", nullable: false),
                    id_user_action = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_content_post = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reg_content", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_reg_content_reg_post_id_content_post",
                        column: x => x.id_content_post,
                        principalTable: "reg_post",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reg_content_reg_user_id_user_action",
                        column: x => x.id_user_action,
                        principalTable: "reg_user",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "reg_user",
                columns: new[] { "Identifier", "Name", "Username" },
                values: new object[,]
                {
                    { new Guid("13869682-3a4f-4d34-ac0a-821dbd9867c1"), "Test User 1", "test1" },
                    { new Guid("1d0ea5e7-f5a5-4fbf-a0b0-bede997ece85"), "Test User 5", "test5" },
                    { new Guid("86523495-0580-4ad6-866c-25c271ab80f7"), "Test User 2", "test2" },
                    { new Guid("b8dd8c55-f6e5-42df-9f13-e728572cc3b7"), "Test User 4", "test4" },
                    { new Guid("fd2e07f1-d191-40f0-97e2-59916b001caf"), "Test User 3", "test3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_reg_content_Date",
                table: "reg_content",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_reg_content_id_content_post",
                table: "reg_content",
                column: "id_content_post");

            migrationBuilder.CreateIndex(
                name: "IX_reg_content_id_user_action",
                table: "reg_content",
                column: "id_user_action");

            migrationBuilder.CreateIndex(
                name: "IX_reg_post_id_user_owner",
                table: "reg_post",
                column: "id_user_owner");

            migrationBuilder.CreateIndex(
                name: "IX_reg_user_Username",
                table: "reg_user",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reg_content");

            migrationBuilder.DropTable(
                name: "reg_post");

            migrationBuilder.DropTable(
                name: "reg_user");
        }
    }
}
