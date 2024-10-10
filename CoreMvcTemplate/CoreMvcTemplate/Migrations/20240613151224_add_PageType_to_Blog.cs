using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreMvcTemplate.Migrations
{
    /// <inheritdoc />
    public partial class add_PageType_to_Blog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PageType",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageType",
                table: "Blogs");
        }
    }
}
