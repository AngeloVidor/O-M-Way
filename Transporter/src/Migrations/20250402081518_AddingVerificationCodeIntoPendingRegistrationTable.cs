using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transporter.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddingVerificationCodeIntoPendingRegistrationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationCode_ID",
                table: "TransporterPreRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "TransporterPreRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "TransporterPreRegistrations");

            migrationBuilder.AddColumn<long>(
                name: "VerificationCode_ID",
                table: "TransporterPreRegistrations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
