// src/Muro.js
import React, { useState, useEffect } from "react";
import { db, auth } from "./firebase";
import { collection, addDoc, query, orderBy, onSnapshot, deleteDoc, doc, updateDoc } from "firebase/firestore";
import { signOut } from "firebase/auth";
import { useNavigate } from "react-router-dom";

function Muro() {
  const [texto, setTexto] = useState("");
  const [posts, setPosts] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const q = query(collection(db, "posts"), orderBy("fecha", "desc"));
    const unsubscribe = onSnapshot(q, (snapshot) => {
      setPosts(snapshot.docs.map(doc => ({ id: doc.id, ...doc.data() })));
    });
    return () => unsubscribe();
  }, []);

  // Crear post (funciona con Enter y botón)
  const handlePost = async (e) => {
    e.preventDefault(); // 👈 evita que el form recargue la página
    if (!texto.trim()) return;

    try {
      await addDoc(collection(db, "posts"), {
        texto,
        autor: auth.currentUser.email,
        autorUid: auth.currentUser.uid, // 👈 clave para las reglas
        fecha: new Date()
      });
      setTexto("");
    } catch (error) {
      console.error("Error al publicar:", error);
      alert("Error: " + error.message);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteDoc(doc(db, "posts", id));
    } catch (error) {
      console.error("Error al borrar:", error);
      alert("Error: " + error.message);
    }
  };

  const handleEdit = async (id, nuevoTexto) => {
    try {
      await updateDoc(doc(db, "posts", id), { texto: nuevoTexto });
    } catch (error) {
      console.error("Error al editar:", error);
      alert("Error: " + error.message);
    }
  };

  const handleLogout = async () => {
    try {
      await signOut(auth);
      alert("Sesión cerrada ✅");
      navigate("/");
    } catch (error) {
      console.error("Error al cerrar sesión:", error);
      alert("Error al cerrar sesión ❌");
    }
  };

  return (
    <div>
      <h2>Muro Interactivo</h2>

      <button onClick={handleLogout} style={{ marginBottom: "15px" }}>
        Cerrar sesión
      </button>

      {/* 👇 ahora el form usa onSubmit */}
      <form onSubmit={handlePost}>
        <input
          type="text"
          placeholder="Escribe tu post..."
          value={texto}
          onChange={(e) => setTexto(e.target.value)}
        />
        <button type="submit">Publicar</button>
      </form>

      <div>
        {posts.map((post) => (
          <div key={post.id} className="post-card">
            <p><strong>{post.autor}</strong></p>
            <p>{post.texto}</p>
            <small>
              {post.fecha.toDate ? post.fecha.toDate().toLocaleString() : post.fecha.toString()}
            </small>

            {auth.currentUser && auth.currentUser.uid === post.autorUid && (
              <div style={{ marginTop: "10px" }}>
                <button onClick={() => handleDelete(post.id)}>Borrar</button>
                <EditForm post={post} onEdit={handleEdit} />
              </div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
}

function EditForm({ post, onEdit }) {
  const [nuevoTexto, setNuevoTexto] = useState(post.texto);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!nuevoTexto.trim()) return;
    onEdit(post.id, nuevoTexto);
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginTop: "5px" }}>
      <input
        type="text"
        value={nuevoTexto}
        onChange={(e) => setNuevoTexto(e.target.value)}
      />
      <button type="submit">Guardar</button>
    </form>
  );
}

export default Muro;
