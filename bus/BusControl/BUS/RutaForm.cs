using System;
using System.Windows.Forms;
using BusControl.Business;

namespace BusControl.Presentation
{
    public class RutaForm : Form
    {
        private DataGridView dgvRutas;
        private Button btnCargar, btnAgregar, btnEditar, btnEliminar;
        private RutaDAO rutaDAO = new RutaDAO();

        public RutaForm()
        {
            this.Text = "Gestión de Rutas";
            this.Width = 600;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvRutas = new DataGridView { Top = 20, Left = 20, Width = 540, Height = 250, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            this.Controls.Add(dgvRutas);

            btnCargar = new Button { Text = "Cargar", Top = 300, Left = 20 };
            btnCargar.Click += BtnCargar_Click;
            this.Controls.Add(btnCargar);

            btnAgregar = new Button { Text = "Agregar", Top = 300, Left = 120 };
            btnAgregar.Click += BtnAgregar_Click;
            this.Controls.Add(btnAgregar);

            btnEditar = new Button { Text = "Editar", Top = 300, Left = 220 };
            btnEditar.Click += BtnEditar_Click;
            this.Controls.Add(btnEditar);

            btnEliminar = new Button { Text = "Eliminar", Top = 300, Left = 320 };
            btnEliminar.Click += BtnEliminar_Click;
            this.Controls.Add(btnEliminar);

            this.Load += (s, e) => BtnCargar_Click(null, null);
        }

        private void BtnCargar_Click(object sender, EventArgs e)
        {
            try { dgvRutas.DataSource = rutaDAO.ObtenerRutas(); } catch { }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string origen = InputHelper.PedirTexto("Ingrese origen:", "Agregar Ruta");
            string destino = InputHelper.PedirTexto("Ingrese destino:", "Agregar Ruta");

            if (!string.IsNullOrWhiteSpace(origen))
            {
                rutaDAO.InsertarRuta(origen, destino);
                BtnCargar_Click(null, null);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvRutas.CurrentRow != null)
            {
                int id = (int)dgvRutas.CurrentRow.Cells["Id"].Value;
                string origen = InputHelper.PedirTexto("Editar origen:", "Editar Ruta");
                string destino = InputHelper.PedirTexto("Editar destino:", "Editar Ruta");

                rutaDAO.EditarRuta(id, origen, destino);
                BtnCargar_Click(null, null);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvRutas.CurrentRow != null)
            {
                int id = (int)dgvRutas.CurrentRow.Cells["Id"].Value;
                if (MessageBox.Show("¿Deseas eliminar esta ruta?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    rutaDAO.EliminarRuta(id);
                    BtnCargar_Click(null, null);
                }
            }
        }
    }
}