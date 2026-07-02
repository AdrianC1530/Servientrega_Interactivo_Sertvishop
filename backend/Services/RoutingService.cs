using System;
using System.Collections.Generic;
using System.Linq;

namespace Servientrega.Api.Services
{
    public class RoutingService
    {
        // Simularemos un mapa con distancias en kilómetros
        private readonly Dictionary<string, Dictionary<string, double>> _mapa = new()
        {
            { "Quito", new Dictionary<string, double> { { "Guayaquil", 400 }, { "Cuenca", 450 } } },
            { "Guayaquil", new Dictionary<string, double> { { "Quito", 400 }, { "Cuenca", 200 } } },
            { "Cuenca", new Dictionary<string, double> { { "Quito", 450 }, { "Guayaquil", 200 } } }
        };

        // Algoritmo simple para simular Dijkstra o ruta más corta
        public double CalcularDistancia(string origen, string destino)
        {
            if (origen == destino) return 0;
            
            // En una app real, aquí se implementa Dijkstra iterando los nodos del grafo
            // Por simplicidad de la simulación, usaremos una tabla de distancias directas o fallback
            if (_mapa.ContainsKey(origen) && _mapa[origen].ContainsKey(destino))
            {
                return _mapa[origen][destino];
            }
            
            // Si no existe, simulamos una distancia aleatoria (solo para pruebas)
            return new Random().Next(100, 800);
        }

        // Calcula el tiempo estimado (horas) asumiendo una velocidad de 80 km/h
        public TimeSpan CalcularTiempoEstimado(double distanciaKm)
        {
            double horas = distanciaKm / 80.0;
            return TimeSpan.FromHours(horas);
        }
    }
}
