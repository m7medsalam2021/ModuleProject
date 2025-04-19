using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Tech" },
                    { 2, null, "Prog" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedAt", "Image", "Title" },
                values: new object[,]
                {
                    { 1, 1, "New Content", new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "x.jpg", "Post 1" },
                    { 2, 2, "New Content 2", new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "y.jpg", "Post 2" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "PostId", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Content 1", 1, "User 1" },
                    { 2, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Content 2", 2, "User 2" },
                    { 3, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Content 3", 2, "User 3" },
                    { 4, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Content 4", 1, "User 4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
