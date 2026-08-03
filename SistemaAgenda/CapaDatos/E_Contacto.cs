using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class D_Contacto
    {
        private SqlConnection conexion = new SqlConnection("Server=DESKTOP-Q5V3F6I; Database=AgendaDB; Integrated Security=true");

        public DataTable Listar(string buscar)
        {
            using (SqlCommand cmd = new SqlCommand("sp_listar_contactos", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", string.IsNullOrEmpty(buscar) ? "" : buscar);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public void Insertar(E_Contacto contacto)
        {
            using (SqlCommand cmd = new SqlCommand("sp_insertar_contacto", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("@Correo", contacto.Correo);
                cmd.Parameters.AddWithValue("@Direccion", contacto.Direccion);

                if (conexion.State == ConnectionState.Open) conexion.Close();
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
        }

        public void Editar(E_Contacto contacto)
        {
            using (SqlCommand cmd = new SqlCommand("sp_editar_contacto", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdContacto", contacto.IdContacto);
                cmd.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("@Correo", contacto.Correo);
                cmd.Parameters.AddWithValue("@Direccion", contacto.Direccion);

                if (conexion.State == ConnectionState.Open) conexion.Close();
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
        }

        public void Eliminar(int id)
        {
            using (SqlCommand cmd = new SqlCommand("sp_eliminar_contacto", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdContacto", id);

                if (conexion.State == ConnectionState.Open) conexion.Close();
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
        }
    }
}