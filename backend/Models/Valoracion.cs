namespace Servientrega.Api.Models
{
    public class Valoracion
    {
        public int Id { get; set; }
        public int EnvioId { get; set; }
        public int Puntuacion { get; set; }
        public string Comentario { get; set; } = string.Empty;
        
        // Navigation
        public Envio? Envio { get; set; }
    }
}
