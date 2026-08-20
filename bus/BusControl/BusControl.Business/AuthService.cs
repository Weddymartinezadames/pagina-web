using BusControl.Data;

namespace BusControl.Business
{
    public class AuthService
    {
        public string Login(string usuario, string password)
        {
            // Aquí deberías consultar la base de datos con tu repositorio.
            // Por ahora lo dejamos simulado para probar el flujo.

            if (usuario == "admin" && password == "1234")
                return "Admin";

            if (usuario == "user" && password == "1234")
                return "User";

            return null; // credenciales inválidas
        }
    }
}
