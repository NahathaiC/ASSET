using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxIdToAssetDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaxInvoiceId",
                table: "AssetDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssetDetails_AssetId",
                table: "AssetDetails",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDetails_TaxInvoiceId",
                table: "AssetDetails",
                column: "TaxInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetDetails_Assets_AssetId",
                table: "AssetDetails",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetDetails_TaxInvoices_TaxInvoiceId",
                table: "AssetDetails",
                column: "TaxInvoiceId",
                principalTable: "TaxInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetDetails_Assets_AssetId",
                table: "AssetDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetDetails_TaxInvoices_TaxInvoiceId",
                table: "AssetDetails");

            migrationBuilder.DropIndex(
                name: "IX_AssetDetails_AssetId",
                table: "AssetDetails");

            migrationBuilder.DropIndex(
                name: "IX_AssetDetails_TaxInvoiceId",
                table: "AssetDetails");

            migrationBuilder.DropColumn(
                name: "TaxInvoiceId",
                table: "AssetDetails");
        }
    }
}
