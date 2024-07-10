using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genesis_test.Migrations
{
    /// <inheritdoc />
    public partial class orderLine_add_totalht : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalHt",
                table: "Orders",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalHt",
                table: "OrderLine",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalHt",
                table: "OrderLine");
        }
    }
}
