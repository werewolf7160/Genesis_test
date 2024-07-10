using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Genesis_test.Migrations
{
    /// <inheritdoc />
    public partial class addstock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Beers_BeerId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_WholeSalers_WholeSalerId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_WholeSalerId_BeerId",
                table: "Stock");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_BeerId",
                table: "Stocks",
                newName: "IX_Stocks_BeerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_WholeSalerId",
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
                name: "FK_Stocks_WholeSalers_WholeSalerId",
                table: "Stocks",
                column: "WholesalerId",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Beers_BeerId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_WholeSalers_WholeSalerId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_WholeSalerId",
                table: "Stocks");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_BeerId",
                table: "Stock",
                newName: "IX_Stock_BeerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_WholeSalerId_BeerId",
                table: "Stock",
                columns: new[] { "WholesalerId", "BeerId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Beers_BeerId",
                table: "Stock",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_WholeSalers_WholeSalerId",
                table: "Stock",
                column: "WholesalerId",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
