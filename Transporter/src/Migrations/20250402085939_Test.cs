using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transporter.Api.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transporters_Locations_Location_ID",
                table: "Transporters");

            migrationBuilder.AlterColumn<long>(
                name: "Location_ID",
                table: "Transporters",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Transporter_ID",
                table: "Locations",
                column: "Transporter_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Transporters_Transporter_ID",
                table: "Locations",
                column: "Transporter_ID",
                principalTable: "Transporters",
                principalColumn: "Transporter_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transporters_Locations_Location_ID",
                table: "Transporters",
                column: "Location_ID",
                principalTable: "Locations",
                principalColumn: "Location_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Transporters_Transporter_ID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Transporters_Locations_Location_ID",
                table: "Transporters");

            migrationBuilder.DropIndex(
                name: "IX_Locations_Transporter_ID",
                table: "Locations");

            migrationBuilder.AlterColumn<long>(
                name: "Location_ID",
                table: "Transporters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transporters_Locations_Location_ID",
                table: "Transporters",
                column: "Location_ID",
                principalTable: "Locations",
                principalColumn: "Location_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
