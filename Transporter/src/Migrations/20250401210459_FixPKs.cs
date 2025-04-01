using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transporter.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixPKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationCodes_Transporters_TransporterCompanyTransporter_ID",
                table: "VerificationCodes");

            migrationBuilder.DropIndex(
                name: "IX_VerificationCodes_TransporterCompanyTransporter_ID",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "TransporterCompanyTransporter_ID",
                table: "VerificationCodes");

            migrationBuilder.AddColumn<long>(
                name: "Transporter_ID",
                table: "Location",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCodes_Transporter_ID",
                table: "VerificationCodes",
                column: "Transporter_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transporters_VerificationCode_ID",
                table: "Transporters",
                column: "VerificationCode_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transporters_VerificationCodes_VerificationCode_ID",
                table: "Transporters",
                column: "VerificationCode_ID",
                principalTable: "VerificationCodes",
                principalColumn: "VerificationCode_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationCodes_Transporters_Transporter_ID",
                table: "VerificationCodes",
                column: "Transporter_ID",
                principalTable: "Transporters",
                principalColumn: "Transporter_ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transporters_VerificationCodes_VerificationCode_ID",
                table: "Transporters");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationCodes_Transporters_Transporter_ID",
                table: "VerificationCodes");

            migrationBuilder.DropIndex(
                name: "IX_VerificationCodes_Transporter_ID",
                table: "VerificationCodes");

            migrationBuilder.DropIndex(
                name: "IX_Transporters_VerificationCode_ID",
                table: "Transporters");

            migrationBuilder.DropColumn(
                name: "Transporter_ID",
                table: "Location");

            migrationBuilder.AddColumn<long>(
                name: "TransporterCompanyTransporter_ID",
                table: "VerificationCodes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCodes_TransporterCompanyTransporter_ID",
                table: "VerificationCodes",
                column: "TransporterCompanyTransporter_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationCodes_Transporters_TransporterCompanyTransporter_ID",
                table: "VerificationCodes",
                column: "TransporterCompanyTransporter_ID",
                principalTable: "Transporters",
                principalColumn: "Transporter_ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
