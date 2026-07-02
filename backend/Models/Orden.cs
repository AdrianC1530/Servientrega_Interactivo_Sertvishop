using System.Collections.Generic;

namespace Servientrega.Api.Models
{
    public class Orden
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string TelefonoContacto { get; set; } = string.Empty; // NUEVO
        public string DireccionEntrega { get; set; } = string.Empty; // NUEVO
        
        public List<Producto> Productos { get; set; } = new();
        public string UbicacionOrigen { get; set; } = string.Empty;
        public string UbicacionDestino { get; set; } = string.Empty;
        
        // Navigation properties
        public Envio? Envio { get; set; }
    }
}
