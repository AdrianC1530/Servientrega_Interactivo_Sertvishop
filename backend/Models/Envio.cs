using System;

namespace Servientrega.Api.Models
{
    public class Envio
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        
        public DateTime HoraSalida { get; set; }
        public DateTime HoraEntregaEstimada { get; set; }
        public DateTime? HoraEntregaReal { get; set; }
        public string Estado { get; set; } = "En Transito"; // "En Transito", "Entregado"
        
        // Navigation properties
        public Orden? Orden { get; set; }
        public Valoracion? Valoracion { get; set; }
        
        public TimeSpan TiempoRestante 
        {
            get 
            {
                if (HoraEntregaReal == null && HoraEntregaEstimada > DateTime.Now)
                    return HoraEntregaEstimada - DateTime.Now;
                return TimeSpan.Zero;
            }
        }
    }
}
