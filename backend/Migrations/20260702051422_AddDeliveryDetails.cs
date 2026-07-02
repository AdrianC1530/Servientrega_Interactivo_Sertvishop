using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Servientrega.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DireccionEntrega",
                table: "Ordenes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TelefonoContacto",
                table: "Ordenes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DireccionEntrega",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "TelefonoContacto",
                table: "Ordenes");
        }
    }
}
