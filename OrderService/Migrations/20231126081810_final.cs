using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BorderOption",
                table: "OrderItems",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "SizeOption",
                table: "OrderItems",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorderOption",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SizeOption",
                table: "OrderItems");
        }
    }
}
