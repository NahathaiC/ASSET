using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAssetDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AssetId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PersonInChargeId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssetPic = table.Column<string>(type: "TEXT", nullable: true),
                    Classifier = table.Column<string>(type: "TEXT", nullable: true),
                    SerialNo = table.Column<string>(type: "TEXT", nullable: true),
                    Supplier = table.Column<string>(type: "TEXT", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Vat = table.Column<decimal>(type: "TEXT", nullable: false),
                    DepreciationRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    GrandAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    UsedMonths = table.Column<decimal>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: true),
                    Section = table.Column<string>(type: "TEXT", nullable: true),
                    LocateAt = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetDetails_AspNetUsers_PersonInChargeId",
                        column: x => x.PersonInChargeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetDetails_PersonInChargeId",
                table: "AssetDetails",
                column: "PersonInChargeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetDetails");
        }
    }
}
