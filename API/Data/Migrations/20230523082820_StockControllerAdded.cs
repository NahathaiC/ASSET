using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockControllerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AssetDetails_AssetId",
                table: "AssetDetails",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetDetails_Assets_AssetId",
                table: "AssetDetails",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetDetails_Assets_AssetId",
                table: "AssetDetails");

            migrationBuilder.DropIndex(
                name: "IX_AssetDetails_AssetId",
                table: "AssetDetails");
        }
    }
}
