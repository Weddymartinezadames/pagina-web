using System;
using System.Windows.Forms;
using BusControl.Business;

namespace BusControl.Presentation
{
    public partial class LoginForm : Form
    {
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnSalir;

        public LoginForm()
        {
            InitializeComponent(); // 👈 ahora sí, se conecta con el Designer

            // Configuración del formulario
            this.Text = "Login";
            this.Width = 300;
            this.Height = 200;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Label Usuario
            lblUsuario = new Label();
            lblUsuario.Text = "Usuario:";
            lblUsuario.Top = 20;
            lblUsuario.Left = 20;
            this.Controls.Add(lblUsuario);

            // TextBox Usuario
            txtUsuario = new TextBox();
            txtUsuario.Top = 20;
            txtUsuario.Left = 100;
            txtUsuario.Width = 150;
            this.Controls.Add(txtUsuario);

            // Label Contraseña
            lblPassword = new Label();
            lblPassword.Text = "Contraseña:";
            lblPassword.Top = 60;
            lblPassword.Left = 20;
            this.Controls.Add(lblPassword);

            // TextBox Contraseña
            txtPassword = new TextBox();
            txtPassword.Top = 60;
            txtPassword.Left = 100;
            txtPassword.Width = 150;
            txtPassword.UseSystemPasswordChar = true;
            this.Controls.Add(txtPassword);

            // Botón Login
            btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Top = 100;
            btnLogin.Left = 100;
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            // Botón Salir
            btnSalir = new Button();
            btnSalir.Text = "Salir";
            btnSalir.Top = 100;
            btnSalir.Left = 180;
            btnSalir.Click += BtnSalir_Click;
            this.Controls.Add(btnSalir);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string password = txtPassword.Text;

            AuthService auth = new AuthService();
            var rol = auth.Login(usuario, password);

            if (rol == "Admin" || rol == "User")
            {
                this.Hide(); // Ocultamos el login
                MainForm main = new MainForm(rol); // Abrimos el menú principal
                main.ShowDialog();
                this.Close(); // Cerramos el login cuando se cierre el MainForm
            }
            else
            {
                MessageBox.Show("Credenciales inválidas");
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Aquí puedes poner lógica al cargar el formulario si lo necesitas
        }
    }
}
