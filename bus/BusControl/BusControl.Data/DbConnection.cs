using System.Data.SqlClient;

namespace BusControl.Data
{
    public class DbConnection
    {
        private static DbConnection instance;
        private SqlConnection connection;

        // Constructor privado (patrón Singleton)
        private DbConnection()
        {
            // Por ahora la cadena de conexión es simulada.
            // Cuando hagamos la base de datos real, la cambiamos.
            string connectionString = "Server=.;Database=BusControlDB;Trusted_Connection=True;";
            connection = new SqlConnection(connectionString);
        }

        // Propiedad estática para obtener la única instancia
        public static DbConnection Instance
        {
            get
            {
                if (instance == null)
                    instance = new DbConnection();
                return instance;
            }
        }

        // Método para obtener la conexión
        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
