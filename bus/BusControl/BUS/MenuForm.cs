using System;
using System.Windows.Forms;

namespace BusControl.Presentation
{
    public class MenuForm : Form
    {
        private Button btnChoferes;
        private Button btnBuses;
        private Button btnUsuarios;
        private Button btnRutas;

        public MenuForm()
        {
            this.Text = "Menú Principal - BusControl";
            this.Width = 400;
            this.Height = 300;
            this.StartPosition = FormStartPosition.CenterScreen;

            btnChoferes = new Button();
            btnChoferes.Text = "Gestión de Choferes";
            btnChoferes.Top = 40;
            btnChoferes.Left = 100;
            btnChoferes.Width = 200;
            btnChoferes.Click += BtnChoferes_Click;
            this.Controls.Add(btnChoferes);

            btnBuses = new Button();
            btnBuses.Text = "Gestión de Buses";
            btnBuses.Top = 90;
            btnBuses.Left = 100;
            btnBuses.Width = 200;
            btnBuses.Click += BtnBuses_Click;
            this.Controls.Add(btnBuses);

            btnUsuarios = new Button();
            btnUsuarios.Text = "Gestión de Usuarios";
            btnUsuarios.Top = 140;
            btnUsuarios.Left = 100;
            btnUsuarios.Width = 200;
            btnUsuarios.Click += BtnUsuarios_Click;
            this.Controls.Add(btnUsuarios);

            btnRutas = new Button();
            btnRutas.Text = "Gestión de Rutas";
            btnRutas.Top = 190;
            btnRutas.Left = 100;
            btnRutas.Width = 200;
            btnRutas.Click += BtnRutas_Click;
            this.Controls.Add(btnRutas);
        }

        private void BtnChoferes_Click(object sender, EventArgs e)
        {
            ChoferForm form = new ChoferForm();
            form.ShowDialog();
        }

        private void BtnBuses_Click(object sender, EventArgs e)
        {
            BusForm form = new BusForm();
            form.ShowDialog();
        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            UserForm form = new UserForm();
            form.ShowDialog();
        }

        private void BtnRutas_Click(object sender, EventArgs e)
        {
            RutaForm form = new RutaForm();
            form.ShowDialog();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MenuForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "MenuForm";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.ResumeLayout(false);

        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
