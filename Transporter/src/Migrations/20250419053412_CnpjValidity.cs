using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transporter.Api.Migrations
{
    /// <inheritdoc />
    public partial class CnpjValidity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cnpj_Validated",
                table: "TransporterPreRegistrations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCnpj_Valid",
                table: "TransporterPreRegistrations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnpj_Validated",
                table: "TransporterPreRegistrations");

            migrationBuilder.DropColumn(
                name: "IsCnpj_Valid",
                table: "TransporterPreRegistrations");
        }
    }
}
