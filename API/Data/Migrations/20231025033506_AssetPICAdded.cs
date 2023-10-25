using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssetPICAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixHistory",
                table: "PurchaseRequisitions");

            migrationBuilder.RenameColumn(
                name: "SuppAdress",
                table: "TaxInvoices",
                newName: "SuppAddress");

            migrationBuilder.AddColumn<int>(
                name: "PersonInChargeId",
                table: "Assets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_PersonInChargeId",
                table: "Assets",
                column: "PersonInChargeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AspNetUsers_PersonInChargeId",
                table: "Assets",
                column: "PersonInChargeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AspNetUsers_PersonInChargeId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_PersonInChargeId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PersonInChargeId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "SuppAddress",
                table: "TaxInvoices",
                newName: "SuppAdress");

            migrationBuilder.AddColumn<string>(
                name: "FixHistory",
                table: "PurchaseRequisitions",
                type: "TEXT",
                nullable: true);
        }
    }
}
