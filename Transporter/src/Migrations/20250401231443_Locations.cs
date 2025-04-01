using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transporter.Api.Migrations
{
    /// <inheritdoc />
    public partial class Locations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transporters_Location_Location_ID",
                table: "Transporters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Location_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transporters_Locations_Location_ID",
                table: "Transporters",
                column: "Location_ID",
                principalTable: "Locations",
                principalColumn: "Location_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transporters_Locations_Location_ID",
                table: "Transporters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Location_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transporters_Location_Location_ID",
                table: "Transporters",
                column: "Location_ID",
                principalTable: "Location",
                principalColumn: "Location_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
