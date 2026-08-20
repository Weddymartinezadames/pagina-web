namespace BusControl.Business
{
    public class Chofer
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Licencia { get; set; }
        public string Telefono { get; set; }
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
    }

    public class Bus
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Capacidad { get; set; }
    }

    public class Ruta
    {
        public int Id { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Duracion { get; set; }
    }
}
