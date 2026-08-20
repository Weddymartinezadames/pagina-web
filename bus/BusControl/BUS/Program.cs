using System;
using System.Windows.Forms;

namespace BusControl.Presentation
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Configuración inicial de la aplicación
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Aquí arrancamos con el LoginForm
            Application.Run(new LoginForm());
        }
    }
}
