using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeQuotation : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
