import React from "react";

function ContactList({ contacts }) {
  return (
    <div>
      <h2>Listado de contactos</h2>
      <ul>
        {contacts.map((c, index) => (
          <li key={index}>
            {c.nombre} {c.apellido} - {c.telefono}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ContactList;
