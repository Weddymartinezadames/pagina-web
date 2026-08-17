// src/Login.js
import React, { useState } from "react";
import { auth } from "./firebase";
import { signInWithEmailAndPassword, signOut } from "firebase/auth"; // 👈 Importar signOut
import { useNavigate } from "react-router-dom";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const userCredential = await signInWithEmailAndPassword(auth, email, password);
      alert("Inicio de sesión exitoso ✅");
      console.log("Usuario:", userCredential.user);

      navigate("/muro"); // 👈 Redirigir al muro
    } catch (error) {
      console.error("Error al iniciar sesión:", error);
      alert("Correo o contraseña incorrectos ❌");
    }
  };

  // 👇 Nueva función para cerrar sesión
  const handleLogout = async () => {
    try {
      await signOut(auth);
      alert("Sesión cerrada ✅");
      navigate("/"); // 👈 Volver al login
    } catch (error) {
      console.error("Error al cerrar sesión:", error);
      alert("Error al cerrar sesión ❌");
    }
  };

  return (
    <div>
      <h2>Iniciar Sesión</h2>
      <form onSubmit={handleLogin}>
        <input
          type="email"
          placeholder="Correo electrónico"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <input
          type="password"
          placeholder="Contraseña"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit">Entrar</button>
      </form>

      {/* 👇 Botón extra para cerrar sesión */}
      <button onClick={handleLogout} style={{ marginTop: "10px" }}>
        Cerrar sesión
      </button>
    </div>
  );
}

export default Login;
