import React, { useState } from "react";

function AddContact({ onAdd }) {
  const [nombre, setNombre] = useState("");
  const [apellido, setApellido] = useState("");
  const [telefono, setTelefono] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    onAdd({ nombre, apellido, telefono });
    setNombre("");
    setApellido("");
    setTelefono("");
  };

  return (
    <form onSubmit={handleSubmit}>
      <input 
        type="text" 
        placeholder="Nombre" 
        value={nombre} 
        onChange={(e) => setNombre(e.target.value)} 
        required 
      />
      <input 
        type="text" 
        placeholder="Apellido" 
        value={apellido} 
        onChange={(e) => setApellido(e.target.value)} 
        required 
      />
      <input 
        type="text" 
        placeholder="Teléfono" 
        value={telefono} 
        onChange={(e) => setTelefono(e.target.value)} 
        required 
      />
      <button type="submit">Guardar</button>
    </form>
  );
}

export default AddContact;
