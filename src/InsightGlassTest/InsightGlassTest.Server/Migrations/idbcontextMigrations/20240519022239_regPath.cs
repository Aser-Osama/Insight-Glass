using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsightGlassTest.Server.Migrations.idbcontextMigrations
{
    /// <inheritdoc />
    public partial class regPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LoggedInBefore",
                table: "aspnetusers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "aspnetusers",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedInBefore",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "aspnetusers");
        }
    }
}
