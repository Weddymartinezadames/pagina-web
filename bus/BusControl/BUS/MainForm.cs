using System;
using System.Windows.Forms;

namespace BusControl.Presentation
{
    public class MainForm : Form
    {
        private MenuStrip menu;
        private ToolStripMenuItem choferesMenu;
        private ToolStripMenuItem busesMenu;
        private ToolStripMenuItem rutasMenu;
        private ToolStripMenuItem usuariosMenu;
        private ToolStripMenuItem salirMenu;

        public MainForm(string rol)
        {
            this.Text = "BusControl - Menú Principal";
            this.Width = 600;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterScreen;

            menu = new MenuStrip();

            choferesMenu = new ToolStripMenuItem("Choferes");
            busesMenu = new ToolStripMenuItem("Buses");
            rutasMenu = new ToolStripMenuItem("Rutas");
            usuariosMenu = new ToolStripMenuItem("Usuarios");
            salirMenu = new ToolStripMenuItem("Salir");

            menu.Items.Add(choferesMenu);
            menu.Items.Add(busesMenu);
            menu.Items.Add(rutasMenu);

            // Solo Admin puede ver Usuarios
            if (rol == "Admin")
                menu.Items.Add(usuariosMenu);

            menu.Items.Add(salirMenu);

            this.MainMenuStrip = menu;
            this.Controls.Add(menu);

            // Eventos → ahora abren formularios reales
            choferesMenu.Click += (s, e) =>
            {
                ChoferForm form = new ChoferForm();
                form.ShowDialog();
            };

            busesMenu.Click += (s, e) =>
            {
                BusForm form = new BusForm();
                form.ShowDialog();
            };

            rutasMenu.Click += (s, e) =>
            {
                RutaForm form = new RutaForm();
                form.ShowDialog();
            };

            usuariosMenu.Click += (s, e) =>
            {
                UserForm form = new UserForm();
                form.ShowDialog();
            };

            salirMenu.Click += (s, e) => Application.Exit();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
