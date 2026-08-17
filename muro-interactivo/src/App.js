// src/App.js
import React, { useEffect } from "react";
import { db } from "./firebase";
import { collection, getDocs } from "firebase/firestore";
import Register from "./Register";
import Login from "./Login";
import Muro from "./Muro"; 
import "./App.css";

// Importar React Router
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  useEffect(() => {
    const testFirestore = async () => {
      const querySnapshot = await getDocs(collection(db, "users"));
      console.log("Usuarios en Firestore:", querySnapshot.docs.map(doc => doc.data()));
    };
    testFirestore();
  }, []);

  return (
    <Router>
      <div className="App">
        <header className="App-header">
          <h1>Muro Interactivo</h1>
          {/* 👇 Eliminado el texto de Firebase */}
        </header>

        <Routes>
          {/* Ruta principal: muestra registro y login */}
          <Route
            path="/"
            element={
              <div>
                <Register />
                <Login />
              </div>
            }
          />

          {/* Ruta del muro */}
          <Route path="/muro" element={<Muro />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
