using CapaDatos;
using CapaNegocio;
using E_Contacto = CapaEntidad.E_Contacto;


namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        private N_Contacto negocio = new N_Contacto();
        private bool icsEditando = false;
        private int idContactoSeleccionado = 0;

        private DataGridView fdataGridView1;
        private TextBox txtNombre;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private TextBox txtDireccion;
        private TextBox txtBuscar;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnCancelar;
        private Label lblTitulo;

        public Form1()
        {
            this.Controls.Clear();
            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Agenda de Contactos - Multicapa";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Icon = SystemIcons.Application;

            // Título
            lblTitulo = new Label()
            {
                Text = "📋 AGENDA DE CONTACTOS",
                Left = 30,
                Top = 15,
                Width = 1000,
                Height = 35,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 97, 141),
                AutoSize = false
            };
            this.Controls.Add(lblTitulo);

            Font fuenteTexto = new Font("Segoe UI", 10, FontStyle.Regular);
            Font fuenteLabel = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fuenteBold = new Font("Segoe UI", 11, FontStyle.Bold);

            // Panel Tabla
            Panel panelTabla = new Panel()
            {
                Left = 30,
                Top = 60,
                Width = 580,
                Height = 540,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            panelTabla.Paint += (s, e) => {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(31, 97, 141), 2), 0, 0, panelTabla.Width - 1, panelTabla.Height - 1);
            };
            this.Controls.Add(panelTabla);

            fdataGridView1 = new DataGridView()
            {
                Left = 15,
                Top = 15,
                Width = 550,
                Height = 510,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false
            };
            fdataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            fdataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 97, 141);
            fdataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            fdataGridView1.ColumnHeadersDefaultCellStyle.Font = fuenteBold;
            fdataGridView1.ColumnHeadersHeight = 35;
            fdataGridView1.RowTemplate.Height = 28;
            fdataGridView1.EnableHeadersVisualStyles = false;
            fdataGridView1.DefaultCellStyle.Font = fuenteTexto;
            fdataGridView1.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);
            panelTabla.Controls.Add(fdataGridView1);

            // Panel Controles
            Panel panelControles = new Panel()
            {
                Left = 630,
                Top = 60,
                Width = 420,
                Height = 540,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            panelControles.Paint += (s, e) => {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(31, 97, 141), 2), 0, 0, panelControles.Width - 1, panelControles.Height - 1);
            };
            this.Controls.Add(panelControles);

            int labelX = 25;
            int textX = 140;
            int startY = 25;
            int spacing = 65;

            Label CrearLabel(string texto, int top)
            {
                return new Label()
                {
                    Text = texto,
                    Left = labelX,
                    Top = top + 3,
                    Width = 110,
                    Font = fuenteLabel,
                    ForeColor = Color.FromArgb(41, 128, 185),
                    AutoSize = false
                };
            }

            TextBox CrearTextBox(int top)
            {
                TextBox txt = new TextBox()
                {
                    Left = textX,
                    Top = top,
                    Width = 250,
                    Height = 32,
                    Font = fuenteTexto,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.FromArgb(248, 249, 250)
                };
                txt.MouseEnter += (s, e) => txt.BackColor = Color.FromArgb(240, 244, 248);
                txt.MouseLeave += (s, e) => txt.BackColor = Color.FromArgb(248, 249, 250);
                txt.Focus += (s, e) => txt.BackColor = Color.White;
                return txt;
            }

            // Campos
            panelControles.Controls.Add(CrearLabel("Nombre:", startY));
            txtNombre = CrearTextBox(startY);
            panelControles.Controls.Add(txtNombre);

            panelControles.Controls.Add(CrearLabel("Teléfono:", startY + spacing));
            txtTelefono = CrearTextBox(startY + spacing);
            panelControles.Controls.Add(txtTelefono);

            panelControles.Controls.Add(CrearLabel("Correo:", startY + (spacing * 2)));
            txtCorreo = CrearTextBox(startY + (spacing * 2));
            panelControles.Controls.Add(txtCorreo);

            panelControles.Controls.Add(CrearLabel("Dirección:", startY + (spacing * 3)));
            txtDireccion = CrearTextBox(startY + (spacing * 3));
            panelControles.Controls.Add(txtDireccion);

            // Separador
            Panel separador = new Panel()
            {
                Left = 25,
                Top = 280,
                Width = 365,
                Height = 1,
                BackColor = Color.FromArgb(200, 200, 200)
            };
            panelControles.Controls.Add(separador);

            panelControles.Controls.Add(CrearLabel("Buscar:", 300));
            txtBuscar = CrearTextBox(300);
            txtBuscar.TextChanged += new EventHandler(this.txtBuscar_TextChanged);
            panelControles.Controls.Add(txtBuscar);

            // Botones
            int btnY = 370;
            int btnW = 85;
            int btnH = 40;

            btnNuevo = CrearBoton("➕ Nuevo", 25, btnY, btnW, btnH, Color.FromArgb(39, 174, 96), Color.White);
            btnNuevo.Click += new EventHandler(this.btnNuevo_Click);
            panelControles.Controls.Add(btnNuevo);

            btnEditar = CrearBoton("✏️ Editar", 125, btnY, btnW, btnH, Color.FromArgb(41, 128, 185), Color.White);
            btnEditar.Click += new EventHandler(this.btnEditar_Click);
            panelControles.Controls.Add(btnEditar);

            btnEliminar = CrearBoton("🗑️ Eliminar", 225, btnY, btnW + 20, btnH, Color.FromArgb(192, 57, 43), Color.White);
            btnEliminar.Click += new EventHandler(this.btnEliminar_Click);
            panelControles.Controls.Add(btnEliminar);

            btnGuardar = CrearBoton("💾 Guardar", 55, btnY + 60, 130, btnH, Color.FromArgb(39, 174, 96), Color.White);
            btnGuardar.Click += new EventHandler(this.btnGuardar_Click);
            panelControles.Controls.Add(btnGuardar);

            btnCancelar = CrearBoton("❌ Cancelar", 210, btnY + 60, 130, btnH, Color.FromArgb(127, 140, 141), Color.White);
            btnCancelar.Click += new EventHandler(this.btnCancelar_Click);
            panelControles.Controls.Add(btnCancelar);

            this.Load += new EventHandler(this.Form1_Load);
        }

        private Button CrearBoton(string texto, int left, int top, int width, int height, Color colorFondo, Color colorTexto)
        {
            Button btn = new Button()
            {
                Text = texto,
                Left = left,
                Top = top,
                Width = width,
                Height = height,
                BackColor = colorFondo,
                ForeColor = colorTexto,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };

            btn.MouseEnter += (s, e) => {
                btn.BackColor = AjustarBrillo(colorFondo, 1.15f);
            };
            btn.MouseLeave += (s, e) => {
                btn.BackColor = colorFondo;
            };

            return btn;
        }

        private Color AjustarBrillo(Color color, float factor)
        {
            return Color.FromArgb(
                Math.Min(255, (int)(color.R * factor)),
                Math.Min(255, (int)(color.G * factor)),
                Math.Min(255, (int)(color.B * factor))
            );
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                MostrarContactos("");
            }
            catch
            {
                MessageBox.Show("No se pudo conectar a la base de datos. Verifica la conexión.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ConfigurarControles(false);
        }

        private void MostrarContactos(string buscar)
        {
            fdataGridView1.DataSource = negocio.ListandoContactos(buscar);
        }

        private void ConfigurarControles(bool estado)
        {
            txtNombre.Enabled = estado;
            txtTelefono.Enabled = estado;
            txtCorreo.Enabled = estado;
            txtDireccion.Enabled = estado;

            btnGuardar.Enabled = estado;
            btnCancelar.Enabled = estado;

            btnNuevo.Enabled = !estado;
            btnEditar.Enabled = !estado;
            btnEliminar.Enabled = !estado;
        }

        private void LimpiarCajas()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtBuscar.Clear();
            idContactoSeleccionado = 0;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCajas();
            icsEditando = false;
            ConfigurarControles(true);
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (fdataGridView1.SelectedRows.Count > 0)
            {
                icsEditando = true;
                idContactoSeleccionado = Convert.ToInt32(fdataGridView1.CurrentRow.Cells["IdContacto"].Value);
                txtNombre.Text = fdataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                txtTelefono.Text = fdataGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
                txtCorreo.Text = fdataGridView1.CurrentRow.Cells["Correo"].Value.ToString();
                txtDireccion.Text = fdataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();

                ConfigurarControles(true);
                txtNombre.Focus();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un registro de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El Nombre y el Teléfono son obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                E_Contacto contacto = new E_Contacto()
                {
                    Nombre = txtNombre.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                if (!icsEditando)
                {
                    negocio.InsertandoContacto(contacto);
                    MessageBox.Show("✅ Contacto guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    contacto.IdContacto = idContactoSeleccionado;
                    negocio.EditandoContacto(contacto);
                    MessageBox.Show("✅ Contacto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                MostrarContactos("");
                LimpiarCajas();
                ConfigurarControles(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (fdataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("¿Está seguro de eliminar este contacto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(fdataGridView1.CurrentRow.Cells["IdContacto"].Value);
                        negocio.EliminandoContacto(id);
                        MessageBox.Show("✅ Contacto eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MostrarContactos("");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione el contacto que desea eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCajas();
            ConfigurarControles(false);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            MostrarContactos(txtBuscar.Text.Trim());
        }
    }
}
