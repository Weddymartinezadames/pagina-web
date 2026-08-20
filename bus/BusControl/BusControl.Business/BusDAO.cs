using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace BusControl.Business
{
    public class BusDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BusControlDB"].ConnectionString;

        public List<Bus> ObtenerBuses()
        {
            var lista = new List<Bus>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Placa, Modelo, Capacidad FROM Buses";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Bus
                    {
                        Id = reader.GetInt32(0),
                        Placa = reader.GetString(1),
                        Modelo = reader.GetString(2),
                        Capacidad = reader.IsDBNull(3) ? "" : reader.GetString(3)
                    });
                }
            }
            return lista;
        }

        public void InsertarBus(string placa, string modelo, string capacidad)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Buses (Placa, Modelo, Capacidad) VALUES (@Placa, @Modelo, @Capacidad)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Placa", placa);
                cmd.Parameters.AddWithValue("@Modelo", modelo);
                cmd.Parameters.AddWithValue("@Capacidad", capacidad);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarBus(int id, string placa, string modelo, string capacidad)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE Buses SET Placa=@Placa, Modelo=@Modelo, Capacidad=@Capacidad WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Placa", placa);
                cmd.Parameters.AddWithValue("@Modelo", modelo);
                cmd.Parameters.AddWithValue("@Capacidad", capacidad);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarBus(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Buses WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
