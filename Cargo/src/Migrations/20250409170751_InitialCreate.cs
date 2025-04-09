using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Address_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Address_ID);
                });

            migrationBuilder.CreateTable(
                name: "Loads",
                columns: table => new
                {
                    Load_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Transporter_ID = table.Column<long>(type: "bigint", nullable: false),
                    Driver_ID = table.Column<long>(type: "bigint", nullable: false),
                    LoadingStartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoadingFinishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loads", x => x.Load_ID);
                });

            migrationBuilder.CreateTable(
                name: "LoadItems",
                columns: table => new
                {
                    Product_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Load_ID = table.Column<long>(type: "bigint", nullable: false),
                    Address_ID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientCPF = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadItems", x => x.Product_ID);
                    table.ForeignKey(
                        name: "FK_LoadItems_Addresses_Address_ID",
                        column: x => x.Address_ID,
                        principalTable: "Addresses",
                        principalColumn: "Address_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoadItems_Address_ID",
                table: "LoadItems",
                column: "Address_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoadItems");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
