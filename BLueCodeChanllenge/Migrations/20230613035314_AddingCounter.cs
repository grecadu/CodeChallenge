using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLueCodeChanllenge.Migrations
{
    /// <inheritdoc />
    public partial class AddingCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Counter",
                table: "Urls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Counter",
                table: "Urls");
        }
    }
}
