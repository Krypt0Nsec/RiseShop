using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rise.Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddressIdInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "OrderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "OrderHeaders");
        }
    }
}
