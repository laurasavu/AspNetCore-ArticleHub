using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Writers",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1L, "john.doe@example.com", "John Doe" },
                    { 2L, "jane.smith@example.com", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Content", "Title", "WriterId" },
                values: new object[,]
                {
                    { 1L, "This is a beginner's guide to C# programming.", "Introduction to C#", 1L },
                    { 2L, "This article covers advanced EF Core topics.", "Advanced Entity Framework", 2L }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "Content", "WriterId" },
                values: new object[,]
                {
                    { 1L, 1L, "Great article!", 2L },
                    { 2L, 2L, "Very informative, thanks!", 1L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Writers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Writers",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
