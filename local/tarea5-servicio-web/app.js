const express = require('express');
const cors = require('cors');

const app = express();
const PORT = process.env.PORT || 3000;

// URL base de la agenda proporcionada por Raydelto
const API_AGENDA_URL = 'http://www.raydelto.org/agenda.php';

// Middlewares
app.use(cors());
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

/**
 * 1. Operación de Listar Contactos
 * Ruta: GET /contactos
 * Descripción: Obtiene la lista de contactos desde la API externa y los devuelve en formato JSON.
 */
app.get('/contactos', async (req, res) => {
    try {
        const response = await fetch(API_AGENDA_URL);
        
        if (!response.ok) {
            throw new Error('Error al conectar con la API externa de contactos.');
        }

        const contactos = await response.json();
        res.json({
            success: true,
            data: contactos
        });
    } catch (error) {
        console.error('Error en GET /contactos:', error.message);
        res.status(500).json({
            success: false,
            message: 'No se pudieron obtener los contactos.',
            error: error.message
        });
    }
});

/**
 * 2. Operación de Almacenar Contactos
 * Ruta: POST /contactos
 * Descripción: Recibe un contacto (nombre, apellido, teléfono) y lo envía a la API externa para almacenarlo.
 */
app.post('/contactos', async (req, res) => {
    try {
        const { nombre, apellido, telefono } = req.body;

        // Validar que los campos requeridos estén presentes
        if (!nombre || !apellido || !telefono) {
            return res.status(400).json({
                success: false,
                message: 'Faltan campos obligatorios: nombre, apellido y teléfono son requeridos.'
            });
        }

        const nuevoContacto = { nombre, apellido, telefono };

        // Enviar el contacto mediante POST a la API externa
        const response = await fetch(API_AGENDA_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nuevoContacto)
        });

        // La API externa suele responder con el objeto creado o texto plano
        const resultadoTexto = await response.text();
        
        let resultadoData;
        try {
            resultadoData = JSON.parse(resultadoTexto);
        } catch {
            resultadoData = { mensaje: resultadoTexto };
        }

        res.status(201).json({
            success: true,
            message: 'Contacto almacenado exitosamente.',
            data: resultadoData
        });

    } catch (error) {
        console.error('Error en POST /contactos:', error.message);
        res.status(500).json({
            success: false,
            message: 'No se pudo almacenar el contacto.',
            error: error.message
        });
    }
});

// Ruta de bienvenida / prueba
app.get('/', (req, res) => {
    res.send('Servicio Web de Agenda Activo. Utilice /contactos para listar o almacenar.');
});

// Iniciar el servidor
app.listen(PORT, () => {
    console.log(`Servidor corriendo exitosamente en el puerto ${PORT}`);
});