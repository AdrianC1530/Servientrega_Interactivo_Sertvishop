using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Servientrega.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Catalogo",
                columns: new[] { "Id", "Descripcion", "ImagenUrl", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 4, "Consola híbrida de 64GB", "ri-gamepad-line", "Nintendo Switch OLED", 349.99m },
                    { 5, "34 pulgadas, 144Hz, IPS", "ri-computer-line", "Monitor LG UltraWide", 450.00m },
                    { 6, "Cámara Mirrorless Full-Frame 33MP", "ri-camera-line", "Cámara Sony Alpha a7 IV", 2499.00m },
                    { 7, "Mecánico inalámbrico, RGB", "ri-keyboard-line", "Teclado Keychron K8 Pro", 110.00m },
                    { 8, "Ergonómico, sensor láser 8K DPI", "ri-mouse-line", "Mouse Logitech MX Master 3S", 99.99m },
                    { 9, "45mm, GPS, Midnight Aluminum", "ri-smart-watch-line", "Apple Watch Series 9", 399.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Catalogo",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Catalogo",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Catalogo",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Catalogo",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Catalogo",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Catalogo",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
