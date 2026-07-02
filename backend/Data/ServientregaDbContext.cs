using Microsoft.EntityFrameworkCore;
using Servientrega.Api.Models;

namespace Servientrega.Api.Data
{
    public class ServientregaDbContext : DbContext
    {
        public ServientregaDbContext(DbContextOptions<ServientregaDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Valoracion> Valoraciones { get; set; }
        public DbSet<ProductoCatalogo> Catalogo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Envio)
                .WithOne(e => e.Orden)
                .HasForeignKey<Envio>(e => e.OrdenId);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Valoracion)
                .WithOne(v => v.Envio)
                .HasForeignKey<Valoracion>(v => v.EnvioId);

            // Seed Data for E-Commerce (9 productos)
            modelBuilder.Entity<ProductoCatalogo>().HasData(
                new ProductoCatalogo { Id = 1, Nombre = "Laptop Gaming ASUS", Descripcion = "RTX 4060, 16GB RAM, 1TB SSD", Precio = 1200.00m, ImagenUrl = "ri-macbook-line" },
                new ProductoCatalogo { Id = 2, Nombre = "iPhone 15 Pro", Descripcion = "Titanium, 256GB, Color Natural", Precio = 999.00m, ImagenUrl = "ri-smartphone-line" },
                new ProductoCatalogo { Id = 3, Nombre = "Audífonos Sony WH-1000XM5", Descripcion = "Cancelación de ruido activa, Inalámbricos", Precio = 349.00m, ImagenUrl = "ri-headphone-line" },
                new ProductoCatalogo { Id = 4, Nombre = "Nintendo Switch OLED", Descripcion = "Consola híbrida de 64GB", Precio = 349.99m, ImagenUrl = "ri-gamepad-line" },
                new ProductoCatalogo { Id = 5, Nombre = "Monitor LG UltraWide", Descripcion = "34 pulgadas, 144Hz, IPS", Precio = 450.00m, ImagenUrl = "ri-computer-line" },
                new ProductoCatalogo { Id = 6, Nombre = "Cámara Sony Alpha a7 IV", Descripcion = "Cámara Mirrorless Full-Frame 33MP", Precio = 2499.00m, ImagenUrl = "ri-camera-line" },
                new ProductoCatalogo { Id = 7, Nombre = "Teclado Keychron K8 Pro", Descripcion = "Mecánico inalámbrico, RGB", Precio = 110.00m, ImagenUrl = "ri-keyboard-line" },
                new ProductoCatalogo { Id = 8, Nombre = "Mouse Logitech MX Master 3S", Descripcion = "Ergonómico, sensor láser 8K DPI", Precio = 99.99m, ImagenUrl = "ri-mouse-line" },
                new ProductoCatalogo { Id = 9, Nombre = "Apple Watch Series 9", Descripcion = "45mm, GPS, Midnight Aluminum", Precio = 399.00m, ImagenUrl = "ri-smart-watch-line" }
            );
        }
    }
}
