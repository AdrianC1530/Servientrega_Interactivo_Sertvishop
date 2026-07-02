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

## 🚀 Requisitos de Instalación (Para ejecutarlo en otra PC)

Si quieres pasarle este proyecto a un amigo, **solo pasarle la carpeta no es suficiente**, ya que requiere herramientas instaladas en su computadora. Tu amigo deberá instalar lo siguiente:

1. **[.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0):** Necesario para compilar y ejecutar el servidor Backend en C#.
2. **[MySQL Server](https://dev.mysql.com/downloads/installer/):** Necesario para la base de datos (se recomienda usar MySQL Workbench para visualizarla).
3. **[Node.js (Opcional)](https://nodejs.org/):** Solo si quieres usar un servidor en vivo (Live Server) para el Frontend.

---

## ⚙️ Instrucciones de Ejecución

Sigue estos pasos para arrancar el proyecto de cero en una computadora nueva:

### 1. Configurar la Base de Datos
1. Abre tu gestor de MySQL (ej. MySQL Workbench) y asegúrate de que el servidor esté corriendo en el puerto `3306` con el usuario `root` y la contraseña `root` (o ajusta esto en el archivo `backend/appsettings.json`).
2. Abre una terminal dentro de la carpeta `backend` y ejecuta los comandos para crear la base de datos y sus tablas automáticamente:
   ```bash
   dotnet ef database update
   ```

### 2. Arrancar el Servidor Backend (C#)
Dentro de la misma terminal de la carpeta `backend`, ejecuta:
```bash
dotnet run
```
El servidor empezará a escuchar peticiones en `http://localhost:5250`. **¡No cierres esta terminal!** El backend debe quedarse corriendo.

### 3. Abrir el Frontend
No necesitas un servidor complejo para el frontend. Simplemente abre el archivo `index.html` (dentro de la carpeta `frontend`) en cualquier navegador web moderno, como Google Chrome o Microsoft Edge.

¡Listo! Ya puedes empezar a simular compras y rastrear camiones de Servientrega en el mapa. 🚚📦
