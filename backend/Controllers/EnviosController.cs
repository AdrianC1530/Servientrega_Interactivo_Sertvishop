using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servientrega.Api.Data;
using Servientrega.Api.Models;
using Servientrega.Api.Services;

namespace Servientrega.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnviosController : ControllerBase
    {
        private readonly ServientregaDbContext _context;
        private readonly RoutingService _routingService;

        public EnviosController(ServientregaDbContext context, RoutingService routingService)
        {
            _context = context;
            _routingService = routingService;
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _context.Catalogo.ToListAsync();
            return Ok(productos);
        }

        [HttpPost("simular-compra")]
        public async Task<IActionResult> SimularCompra([FromBody] CrearOrdenRequest request)
        {
            var orden = new Orden
            {
                NombreCliente = request.NombreCliente,
                TelefonoContacto = request.TelefonoContacto,
                DireccionEntrega = request.DireccionEntrega,
                UbicacionOrigen = request.Origen, // Siempre será "Matriz Principal (Quito)"
                UbicacionDestino = request.Destino,
                Productos = request.ProductosNombres.Select(n => new Producto { Nombre = n, Precio = 0m }).ToList()
            };

            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();

            // Usamos "Quito" como base para calcular la distancia en el RoutingService si el origen es la Matriz
            double distancia = _routingService.CalcularDistancia("Quito", orden.UbicacionDestino);
            TimeSpan tiempoEstimado = _routingService.CalcularTiempoEstimado(distancia);

            var envio = new Envio
            {
                OrdenId = orden.Id,
                HoraSalida = DateTime.Now,
                HoraEntregaEstimada = DateTime.Now.Add(tiempoEstimado),
                Estado = "En Transito"
            };

            _context.Envios.Add(envio);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Compra realizada y envío despachado", EnvioId = envio.Id, DistanciaKm = distancia });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEstadoEnvio(int id)
        {
            var envio = await _context.Envios
                .Include(e => e.Orden)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (envio == null) return NotFound("Envío no encontrado");

            return Ok(new
            {
                envio.Id,
                Cliente = envio.Orden?.NombreCliente,
                Telefono = envio.Orden?.TelefonoContacto,
                Direccion = envio.Orden?.DireccionEntrega,
                Origen = envio.Orden?.UbicacionOrigen,
                Destino = envio.Orden?.UbicacionDestino,
                envio.Estado,
                envio.HoraSalida,
                envio.HoraEntregaEstimada,
                envio.HoraEntregaReal,
                TiempoRestante = envio.TiempoRestante.ToString(@"hh\:mm\:ss")
            });
        }

        [HttpPost("{id}/entregar")]
        public async Task<IActionResult> EntregarEnvio(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null) return NotFound();

            envio.Estado = "Entregado";
            envio.HoraEntregaReal = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Envío marcado como entregado" });
        }

        [HttpPost("{id}/valorar")]
        public async Task<IActionResult> ValorarEnvio(int id, [FromBody] ValoracionRequest req)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null) return NotFound();

            var valoracion = new Valoracion
            {
                EnvioId = id,
                Puntuacion = req.Puntuacion,
                Comentario = req.Comentario
            };
            
            _context.Valoraciones.Add(valoracion);
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Valoración guardada" });
        }
    }

    public class CrearOrdenRequest
    {
        public string NombreCliente { get; set; } = string.Empty;
        public string TelefonoContacto { get; set; } = string.Empty;
        public string DireccionEntrega { get; set; } = string.Empty;
        public string Origen { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public List<string> ProductosNombres { get; set; } = new();
    }

    public class ValoracionRequest
    {
        public int Puntuacion { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}
