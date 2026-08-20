using System;
using System.Windows.Forms;
using BusControl.Business;

namespace BusControl.Presentation
{
    public class UserForm : Form
    {
        private DataGridView dgvUsuarios;
        private Button btnCargar, btnAgregar, btnEliminar;
        private UsuarioDAO usuarioDAO = new UsuarioDAO();

        public UserForm()
        {
            this.Text = "Gestión de Usuarios";
            this.Width = 600;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvUsuarios = new DataGridView { Top = 20, Left = 20, Width = 540, Height = 250, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            this.Controls.Add(dgvUsuarios);

            btnCargar = new Button { Text = "Cargar", Top = 300, Left = 20 };
            btnCargar.Click += BtnCargar_Click;
            this.Controls.Add(btnCargar);

            btnAgregar = new Button { Text = "Agregar", Top = 300, Left = 120 };
            btnAgregar.Click += BtnAgregar_Click;
            this.Controls.Add(btnAgregar);

            btnEliminar = new Button { Text = "Eliminar", Top = 300, Left = 220 };
            btnEliminar.Click += BtnEliminar_Click;
            this.Controls.Add(btnEliminar);

            this.Load += (s, e) => BtnCargar_Click(null, null);
        }

        private void BtnCargar_Click(object sender, EventArgs e)
        {
            try { dgvUsuarios.DataSource = usuarioDAO.ObtenerUsuarios(); } catch { }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string user = InputHelper.PedirTexto("Ingrese usuario:", "Agregar Usuario");
            string pass = InputHelper.PedirTexto("Ingrese contraseña:", "Agregar Usuario");
            string rol = InputHelper.PedirTexto("Ingrese rol (Admin/User):", "Agregar Usuario");

            if (!string.IsNullOrWhiteSpace(user))
            {
                usuarioDAO.InsertarUsuario(user, pass, rol);
                BtnCargar_Click(null, null);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                int id = (int)dgvUsuarios.CurrentRow.Cells["Id"].Value;
                if (MessageBox.Show("¿Deseas eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    usuarioDAO.EliminarUsuario(id);
                    BtnCargar_Click(null, null);
                }
            }
        }
    }
}