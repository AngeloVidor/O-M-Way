using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transporter.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationCodes_Transporters_Transporter_ID",
                table: "VerificationCodes");

            migrationBuilder.DropIndex(
                name: "IX_VerificationCodes_Transporter_ID",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "Transporter_ID",
                table: "VerificationCodes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Transporter_ID",
                table: "VerificationCodes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCodes_Transporter_ID",
                table: "VerificationCodes",
                column: "Transporter_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationCodes_Transporters_Transporter_ID",
                table: "VerificationCodes",
                column: "Transporter_ID",
                principalTable: "Transporters",
                principalColumn: "Transporter_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
