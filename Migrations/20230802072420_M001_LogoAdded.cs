using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualShop.Migrations
{
    /// <inheritdoc />
    public partial class M001_LogoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Categories");
        }
    }
}
