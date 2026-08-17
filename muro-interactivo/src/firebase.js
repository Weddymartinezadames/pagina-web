// src/firebase.js
import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";
import { getFirestore } from "firebase/firestore";
import { getStorage } from "firebase/storage"; // 👈 Importar Storage

// ⚠️ Credenciales reales de tu proyecto Firebase
const firebaseConfig = {
  apiKey: "AIzaSyDBwQoXIZCwbHuAcCjH-mBcY__G820JUyU",
  authDomain: "muro-interactivo-3fe10.firebaseapp.com",
  projectId: "muro-interactivo-3fe10",
  storageBucket: "muro-interactivo-3fe10.appspot.com", // 👈 corregido: debe terminar en .appspot.com
  messagingSenderId: "959553474536",
  appId: "1:959553474536:web:2b9c262536d508b4ad5e5d"
};

// Inicializar Firebase
const app = initializeApp(firebaseConfig);

// Exportar servicios
export const auth = getAuth(app);
export const db = getFirestore(app);
export const storage = getStorage(app); // 👈 Exportar Storage
