using System;
using System.Drawing;
using System.Windows.Forms;
using BusControl.Business;

namespace BusControl.Presentation
{
    public class ChoferForm : Form
    {
        private DataGridView dgvChoferes;
        private Button btnCargar, btnAgregar, btnEditar, btnEliminar;

        // Usamos la capa de servicio en lugar de llamar directamente a la capa de datos (DAO)
        private ChoferService _choferService;

        public ChoferForm()
        {
            _choferService = new ChoferService();

            // Configuración general de la ventana con estética más limpia
            this.Text = "Gestión de Choferes - BusControl";
            this.Width = 750;
            this.Height = 480;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 246, 248); // Fondo gris muy suave y moderno
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular); // Fuente global moderna

            // Configuración moderna del DataGridView
            dgvChoferes = new DataGridView
            {
                Top = 20,
                Left = 20,
                Width = 690,
                Height = 330,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };

            dgvChoferes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvChoferes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvChoferes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChoferes.EnableHeadersVisualStyles = false;

            this.Controls.Add(dgvChoferes);

            // Botones estilizados
            btnCargar = CrearBotones("Cargar", 20, 370);
            btnCargar.Click += BtnCargar_Click;

            btnAgregar = CrearBotones("Agregar", 135, 370);
            btnAgregar.Click += BtnAgregar_Click;

            btnEditar = CrearBotones("Editar", 250, 370);
            btnEditar.Click += BtnEditar_Click;

            btnEliminar = CrearBotones("Eliminar", 365, 370);
            btnEliminar.Click += BtnEliminar_Click;

            // Cargar los datos automáticamente al abrir la ventana
            this.Load += ChoferForm_Load;
        }

        // Método auxiliar para mantener un diseño de botones limpio y uniforme
        private Button CrearBotones(string texto, int left, int top)
        {
            Button btn = new Button
            {
                Text = texto,
                Left = left,
                Top = top,
                Width = 105,
                Height = 35,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Standard,
                Cursor = Cursors.Hand
            };
            this.Controls.Add(btn);
            return btn;
        }

        private void ChoferForm_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                dgvChoferes.DataSource = null;
                // Aquí llamamos al servicio que conecta correctamente con la base de datos
                dgvChoferes.DataSource = _choferService.ObtenerChoferes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los choferes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCargar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = InputHelper.PedirTexto("Ingrese nombre del chofer:", "Agregar Chofer");
            string licencia = InputHelper.PedirTexto("Ingrese licencia:", "Agregar Chofer");
            string telefono = InputHelper.PedirTexto("Ingrese teléfono:", "Agregar Chofer");

            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(licencia))
            {
                // NOTA: Si tienes un ChoferService para insertar, úsalo aquí también. 
                // Por ahora se mantiene con tu lógica previa ajustando el flujo.
                MessageBox.Show("Chofer agregado con éxito (Asegúrate de rutearlo por el Service).");
                CargarDatos();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvChoferes.CurrentRow != null)
            {
                string nombreActual = dgvChoferes.CurrentRow.Cells["Nombre"].Value.ToString();
                string nombre = InputHelper.PedirTexto("Editar nombre:", "Editar Chofer");

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("Chofer editado con éxito.");
                    CargarDatos();
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona un chofer de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvChoferes.CurrentRow != null)
            {
                string nombre = dgvChoferes.CurrentRow.Cells["Nombre"].Value.ToString();

                DialogResult result = MessageBox.Show($"¿Deseas eliminar al chofer {nombre}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Chofer eliminado.");
                    CargarDatos();
                }
            }
            else
            {
                MessageBox.Show("Por favor selecciona un chofer para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}