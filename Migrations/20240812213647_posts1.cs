using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialClone.Migrations
{
    /// <inheritdoc />
    public partial class posts1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "content",
                table: "Posts",
                newName: "Content");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Posts",
                newName: "content");
        }
    }
}
