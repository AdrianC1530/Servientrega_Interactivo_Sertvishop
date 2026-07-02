# ServiShop: Logística e E-Commerce (Simulación de Servientrega)

¡Bienvenido a **ServiShop**! Este proyecto es una simulación interactiva de una plataforma de comercio electrónico integrada con un sistema de logística avanzado (inspirado en Servientrega), donde los usuarios pueden comprar productos, generar una guía de despacho y rastrear sus paquetes en tiempo real sobre un mapa.

## ✨ Características Principales
- **Tienda (E-Commerce):** Catálogo de productos tecnológicos cargados desde una base de datos MySQL.
- **Carrito de Compras:** Añade múltiples productos y calcula totales dinámicamente.
- **Checkout:** Ingreso de datos del cliente (nombre, dirección, teléfono) con origen fijo en la Matriz.
- **Rastreo en Vivo (Tracking):** Integración con **Leaflet.js** para visualizar la ruta en un mapa real de Ecuador (Quito a Guayaquil o Cuenca), con un camión animado moviéndose en tiempo real.
- **Acelerador de Tiempo:** Botón para simular entregas instantáneas.
- **Sistema de Valoración:** Calificación de 1 a 5 estrellas con comentarios al finalizar la entrega.

## 🛠️ Tecnologías Utilizadas
- **Backend:** C# con ASP.NET Core 9 Web API
- **Base de Datos:** MySQL (usando Entity Framework Core y Pomelo)
- **Frontend:** HTML5, CSS3 (Glassmorphism), Vanilla JavaScript
- **Librerías Externas:** Leaflet.js (Mapas), Canvas Confetti (Efectos Visuales), Remix Icon (Iconografía)

---

## 🚀 Requisitos de Instalación e Implementación

Para ejecutar este proyecto en un entorno local, asegúrese de cumplir con los siguientes requisitos previos del sistema:

1. **[.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0):** Necesario para compilar y ejecutar el servidor Backend en C#.
2. **[MySQL Server](https://dev.mysql.com/downloads/installer/):** Necesario para alojar la base de datos relacional.
3. **[Node.js (Opcional)](https://nodejs.org/):** Para servidores de desarrollo front-end como Live Server.

---

## ⚙️ Instrucciones de Ejecución

Siga estos pasos para compilar y arrancar el proyecto:

### 1. Configurar la Base de Datos
1. Asegúrese de que el servicio de MySQL esté corriendo en el puerto `3306` con el usuario por defecto (o ajuste la cadena de conexión en el archivo `backend/appsettings.json`).
2. Abra una terminal en el directorio `backend` y ejecute los comandos de Entity Framework para desplegar las migraciones y la data semilla:
   ```bash
   dotnet ef database update
   ```

### 2. Arrancar el Servidor Backend (API)
Dentro del mismo directorio `backend`, ejecute el proyecto:
```bash
dotnet run
```
La API REST comenzará a escuchar peticiones en `http://localhost:5250`. (Mantenga esta terminal en ejecución).

### 3. Iniciar el Frontend
Abra el archivo `index.html` (ubicado en el directorio `frontend`) en cualquier navegador web moderno, o sírvalo mediante un servidor local (ej. extensión Live Server en VS Code).

¡La aplicación estará lista para procesar órdenes de prueba! 🚚📦
