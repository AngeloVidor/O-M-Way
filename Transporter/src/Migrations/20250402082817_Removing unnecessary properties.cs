using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transporter.Api.Migrations
{
    /// <inheritdoc />
    public partial class Removingunnecessaryproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transporters_VerificationCodes_VerificationCode_ID",
                table: "Transporters");

            migrationBuilder.DropIndex(
                name: "IX_Transporters_VerificationCode_ID",
                table: "Transporters");

            migrationBuilder.DropColumn(
                name: "VerificationCode_ID",
                table: "Transporters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VerificationCode_ID",
                table: "Transporters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
