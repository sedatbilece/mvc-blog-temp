using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreMvcTemplate.Migrations
{
    /// <inheritdoc />
    public partial class add_product_to_DisplayOrer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Products");
        }
    }
}
