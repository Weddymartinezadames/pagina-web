using System;
using System.Windows.Forms;
using BusControl.Business;

namespace BusControl.Presentation
{
    public class BusForm : Form
    {
        private DataGridView dgvBuses;
        private Button btnCargar, btnAgregar, btnEditar, btnEliminar;
        private BusDAO busDAO = new BusDAO();

        public BusForm()
        {
            this.Text = "Gestión de Buses";
            this.Width = 600;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvBuses = new DataGridView { Top = 20, Left = 20, Width = 540, Height = 250, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            this.Controls.Add(dgvBuses);

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
            try { dgvBuses.DataSource = busDAO.ObtenerBuses(); } catch { }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string placa = InputHelper.PedirTexto("Ingrese placa:", "Agregar Bus");
            string modelo = InputHelper.PedirTexto("Ingrese modelo:", "Agregar Bus");
            string capacidad = InputHelper.PedirTexto("Ingrese capacidad:", "Agregar Bus");

            if (!string.IsNullOrWhiteSpace(placa))
            {
                busDAO.InsertarBus(placa, modelo, capacidad);
                BtnCargar_Click(null, null);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvBuses.CurrentRow != null)
            {
                int id = (int)dgvBuses.CurrentRow.Cells["Id"].Value;
                string placa = InputHelper.PedirTexto("Editar placa:", "Editar Bus");
                string modelo = InputHelper.PedirTexto("Editar modelo:", "Editar Bus");
                string capacidad = InputHelper.PedirTexto("Editar capacidad:", "Editar Bus");

                busDAO.EditarBus(id, placa, modelo, capacidad);
                BtnCargar_Click(null, null);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvBuses.CurrentRow != null)
            {
                int id = (int)dgvBuses.CurrentRow.Cells["Id"].Value;
                string placa = dgvBuses.CurrentRow.Cells["Placa"].Value.ToString();

                if (MessageBox.Show($"¿Eliminar bus {placa}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    busDAO.EliminarBus(id);
                    BtnCargar_Click(null, null);
                }
            }
        }
    }
}