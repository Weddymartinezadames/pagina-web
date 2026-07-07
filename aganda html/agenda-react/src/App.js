import React, { useState, useEffect } from "react";
import ContactList from "./ContactList";
import AddContact from "./AddContact";

const url = "http://www.raydelto.org/agenda.php";

function App() {
  const [contacts, setContacts] = useState([]);

  useEffect(() => {
    fetch(url)
      .then(res => res.json())
      .then(data => setContacts(data));
  }, []);

  const addContact = async (nuevo) => {
    await fetch(url, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(nuevo)
    });
    const res = await fetch(url);
    const data = await res.json();
    setContacts(data);
  };

  return (
    <div>
      <h1>Agenda React</h1>
      <AddContact onAdd={addContact} />
      <ContactList contacts={contacts} />
    </div>
  );
}

export default App;
