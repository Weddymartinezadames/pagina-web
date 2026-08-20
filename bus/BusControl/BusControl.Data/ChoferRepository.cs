using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BusControl.Data
{
    public class ChoferRepository
    {
        // Nota: Ya no guardamos la conexión como campo global para evitar que se quede abierta
        public ChoferRepository()
        {
        }

        public List<string> GetAll()
        {
            List<string> choferes = new List<string>();
            string query = "SELECT Nombre FROM Choferes";

            // Usamos 'using' para asegurar que la conexión y el comando se cierren y se liberen solos
            using (SqlConnection connection = DbConnection.Instance.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["Nombre"] != DBNull.Value)
                                {
                                    choferes.Add(reader["Nombre"].ToString());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Aquí puedes manejar el error o relanzarlo según prefieras
                        throw new Exception("Error al obtener los choferes: " + ex.Message);
                    }
                }
            }

            return choferes;
        }
    }
}