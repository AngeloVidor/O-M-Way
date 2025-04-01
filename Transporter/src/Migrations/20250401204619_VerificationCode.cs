using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transporter.Api.Migrations
{
    /// <inheritdoc />
    public partial class VerificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VerificationCode_ID",
                table: "Transporters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "VerificationCodes",
                columns: table => new
                {
                    VerificationCode_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Transporter_ID = table.Column<long>(type: "bigint", nullable: false),
                    TransporterCompanyTransporter_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationCodes", x => x.VerificationCode_ID);
                    table.ForeignKey(
                        name: "FK_VerificationCodes_Transporters_TransporterCompanyTransporter_ID",
                        column: x => x.TransporterCompanyTransporter_ID,
                        principalTable: "Transporters",
                        principalColumn: "Transporter_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCodes_TransporterCompanyTransporter_ID",
                table: "VerificationCodes",
                column: "TransporterCompanyTransporter_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "VerificationCode_ID",
                table: "Transporters");
        }
    }
}
