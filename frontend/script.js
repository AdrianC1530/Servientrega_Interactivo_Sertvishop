const API_URL = 'http://localhost:5250/api/Envios'; 
let timerInterval;
let mapInterval;
let currentEnvioId = 0;
let carrito = [];
let totalCarrito = 0;

// Variables de Mapa
let map;
let truckMarker;
let routeLine;
let originLatLng = [-0.1807, -78.4678]; // Quito (Matriz)
const cities = {
    "Guayaquil": [-2.1894, -79.8891],
    "Cuenca": [-2.9001, -79.0059]
};

// Inicialización
document.addEventListener('DOMContentLoaded', cargarProductos);

async function cargarProductos() {
    try {
        const response = await fetch(`${API_URL}/productos`);
        const productos = await response.json();
        
        const container = document.getElementById('products-container');
        container.innerHTML = '';
        
        productos.forEach(p => {
            container.innerHTML += `
                <div class="product-card">
                    <i class="${p.imagenUrl} product-icon"></i>
                    <div class="product-title">${p.nombre}</div>
                    <div class="product-desc">${p.descripcion}</div>
                    <div class="product-price">$${p.precio.toFixed(2)}</div>
                    <button class="btn-secondary" style="width:100%" onclick="agregarAlCarrito('${p.nombre}', ${p.precio})">
                        Añadir <i class="ri-shopping-bag-line"></i>
                    </button>
                </div>
            `;
        });
    } catch (e) {
        console.error("Error cargando productos. ¿Está el backend encendido?");
    }
}

function agregarAlCarrito(nombre, precio) {
    carrito.push({ nombre, precio });
    actualizarCarritoUI();
}

function actualizarCarritoUI() {
    document.getElementById('cart-count').innerText = carrito.length;
    
    const container = document.getElementById('cart-items-container');
    container.innerHTML = '';
    totalCarrito = 0;

    carrito.forEach(item => {
        totalCarrito += item.precio;
        container.innerHTML += `
            <div class="cart-item">
                <span>${item.nombre}</span>
                <strong>$${item.precio.toFixed(2)}</strong>
            </div>
        `;
    });
    
    document.getElementById('lbl-total').innerText = `$${totalCarrito.toFixed(2)}`;
}

function showView(viewId) {
    if (viewId === 'view-checkout' && carrito.length === 0) {
        alert("El carrito está vacío.");
        return;
    }
    document.querySelectorAll('.view').forEach(v => v.classList.remove('active-view'));
    document.getElementById(viewId).classList.add('active-view');

    if(viewId === 'view-tracking' && !map) {
        initMap();
    }
}

function initMap() {
    map = L.map('map').setView([-1.8312, -78.1834], 6); // Centro de Ecuador
    L.tileLayer('https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}{r}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    const truckIcon = L.divIcon({
        html: '<div style="background:var(--primary);color:white;width:30px;height:30px;border-radius:50%;display:flex;align-items:center;justify-content:center;box-shadow:0 0 10px rgba(0,0,0,0.3);font-size:16px;"><i class="ri-truck-line"></i></div>',
        className: '',
        iconSize: [30, 30],
        iconAnchor: [15, 15]
    });

    truckMarker = L.marker(originLatLng, { icon: truckIcon }).addTo(map);
}

async function simularCompra() {
    if (carrito.length === 0) return;

    const btn = document.getElementById('btn-comprar');
    btn.innerHTML = '<div class="spinner" style="width:20px;height:20px;margin:0"></div>';
    
    const destinoStr = document.getElementById('destino').value; // Ej: "Guayaquil"
    
    const payload = {
        nombreCliente: document.getElementById('cliente').value,
        telefonoContacto: document.getElementById('telefono').value,
        direccionEntrega: document.getElementById('direccion').value,
        origen: "Matriz Principal (Quito)",
        destino: destinoStr,
        productosNombres: carrito.map(c => c.nombre)
    };

    try {
        const response = await fetch(`${API_URL}/simular-compra`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if(response.ok) {
            const data = await response.json();
            currentEnvioId = data.envioId;
            
            showView('view-tracking');
            setTimeout(() => { map.invalidateSize(); }, 300); // fix leaflet render in hidden div

            document.getElementById('lbl-distancia').innerText = `${data.distanciaKm} km`;
            document.getElementById('lbl-id').innerText = `#${data.envioId.toString().padStart(6, '0')}`;

            const totalSeconds = Math.max(10, Math.floor(data.distanciaKm / 10));
            iniciarConteo(totalSeconds, destinoStr); 
        }
    } catch (error) {
        console.error(error);
        alert("Error de conexión");
    } finally {
        btn.innerHTML = '<span>Pagar y Despachar</span><i class="ri-rocket-line"></i>';
    }
}

function iniciarConteo(seconds, destino) {
    let totalSeconds = seconds;
    const initialSeconds = seconds;
    const countdownEl = document.getElementById('lbl-countdown');

    const destLatLng = cities[destino];

    // Trazar linea
    if(routeLine) map.removeLayer(routeLine);
    routeLine = L.polyline([originLatLng, destLatLng], {color: 'var(--primary)', weight: 3, dashArray: '5, 5'}).addTo(map);
    map.fitBounds(routeLine.getBounds(), { padding: [30, 30] });

    // Animación de Mapa
    const latStep = (destLatLng[0] - originLatLng[0]) / seconds;
    const lngStep = (destLatLng[1] - originLatLng[1]) / seconds;
    truckMarker.setLatLng(originLatLng);

    clearInterval(timerInterval);
    clearInterval(mapInterval);

    timerInterval = setInterval(() => {
        totalSeconds--;
        
        // Mover camioncito
        const currentPos = truckMarker.getLatLng();
        truckMarker.setLatLng([currentPos.lat + latStep, currentPos.lng + lngStep]);

        if(totalSeconds <= 0) {
            truckMarker.setLatLng(destLatLng); // Asegurar que llegue
            llegadaAlDestino();
        } else {
            let m = Math.floor((totalSeconds % 3600) / 60);
            let s = totalSeconds % 60;
            countdownEl.innerText = `00:${m.toString().padStart(2, '0')}:${s.toString().padStart(2, '0')}`;
        }
    }, 1000);
}

function adelantarTiempo() {
    llegadaAlDestino();
}

function llegadaAlDestino() {
    clearInterval(timerInterval);
    
    // Forzar el camión al final
    if(routeLine) {
        const coords = routeLine.getLatLngs();
        truckMarker.setLatLng(coords[coords.length - 1]);
    }

    document.getElementById('lbl-countdown').innerText = "00:00:00";
    document.getElementById('lbl-tiempo-text').innerText = "¡EL PAQUETE HA LLEGADO!";
    
    // UI Updates
    document.getElementById('step-transit').classList.remove('pulse-step');
    document.getElementById('line-dest').classList.add('active');
    document.getElementById('step-dest').classList.add('active');
    
    const badge = document.getElementById('badge-status');
    badge.className = "badge-status badge-success";
    badge.innerHTML = `<i class="ri-check-line"></i> ENTREGADO`;

    document.getElementById('btn-fastforward').style.display = 'none';
    document.getElementById('btn-confirmar').style.display = 'inline-flex';
}

async function confirmarLlegada() {
    await fetch(`${API_URL}/${currentEnvioId}/entregar`, { method: 'POST' });
    confetti({ particleCount: 100, spread: 70, origin: { y: 0.6 } });
    
    setTimeout(() => {
        showView('view-rating');
    }, 1000);
}

// Estrellas
const stars = document.querySelectorAll('#star-rating i');
stars.forEach(star => {
    star.addEventListener('click', () => {
        const val = star.getAttribute('data-val');
        document.getElementById('rating-val').value = val;
        
        stars.forEach(s => s.classList.remove('active', 'ri-star-fill'));
        stars.forEach(s => s.classList.add('ri-star-line'));

        for(let i = 0; i < val; i++) {
            stars[i].classList.remove('ri-star-line');
            stars[i].classList.add('ri-star-fill', 'active');
        }
    });
});

async function enviarValoracion() {
    const puntuacion = document.getElementById('rating-val').value;
    const comentario = document.getElementById('rating-comment').value;

    if(puntuacion == 0) {
        alert("Por favor selecciona las estrellas.");
        return;
    }

    await fetch(`${API_URL}/${currentEnvioId}/valorar`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ puntuacion: parseInt(puntuacion), comentario })
    });

    alert("¡Gracias por tu valoración! Nos ayuda a mejorar.");
    
    // Resetear app
    carrito = [];
    actualizarCarritoUI();
    document.getElementById('btn-fastforward').style.display = 'inline-flex';
    document.getElementById('btn-confirmar').style.display = 'none';
    document.getElementById('cliente').value = '';
    document.getElementById('telefono').value = '';
    document.getElementById('direccion').value = '';
    
    // Reiniciar vista
    showView('view-store');
}
