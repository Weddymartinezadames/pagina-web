using System;
using System.Data;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class N_Contacto
    {
        private D_Contacto datos = new D_Contacto();

        public DataTable ListandoContactos(string buscar)
        {
            return datos.Listar(string.IsNullOrEmpty(buscar) ? "" : buscar);
        }

        public void InsertandoContacto(E_Contacto contacto)
        {
            datos.Insertar(contacto);
        }

        public void EditandoContacto(E_Contacto contacto)
        {
            datos.Editar(contacto);
        }

        public void EliminandoContacto(int id)
        {
            datos.Eliminar(id);
        }
    }
}
