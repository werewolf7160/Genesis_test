using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genesis_test.Migrations
{
    /// <inheritdoc />
    public partial class order_and_orderline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Beers_BeerId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_WholeSalers_WholesalerId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_BeerId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_WholesalerId",
                table: "Stocks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BeerId",
                table: "Stocks",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_WholesalerId",
                table: "Stocks",
                column: "WholesalerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Beers_BeerId",
                table: "Stocks",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_WholeSalers_WholesalerId",
                table: "Stocks",
                column: "WholesalerId",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
