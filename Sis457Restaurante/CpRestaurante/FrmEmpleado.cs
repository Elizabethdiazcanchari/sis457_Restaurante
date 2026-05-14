using CadRestaurante;
using ClnRestaurante;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpRestaurante
{
    public partial class FrmEmpleado : Form
    {
        private bool modoEdicion = false;
        private System.Threading.Timer searchTimer;
        private const int SearchDelay = 500;
        public FrmEmpleado()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var lista = EmpleadoCln.listarPa(txtBuscar.Text.Trim());
            dgvEmpleados.DataSource = lista;
            dgvEmpleados.Columns["id"].Visible = false;
            dgvEmpleados.Columns["idUsuario"].Visible = false;
            dgvEmpleados.Columns["estado"].Visible = false;
            dgvEmpleados.Columns["usuario"].HeaderText = "Usuario";
            dgvEmpleados.Columns["cedulaIdentidad"].HeaderText = "Cédula de Identidad";
            dgvEmpleados.Columns["nombres"].HeaderText = "Nombres";
            dgvEmpleados.Columns["primerApellido"].HeaderText = "Primer Apellido";
            dgvEmpleados.Columns["segundoApellido"].HeaderText = "Segundo Apellido";
            dgvEmpleados.Columns["fechaNacimiento"].HeaderText = "Fecha de Nacimiento";
            dgvEmpleados.Columns["direccion"].HeaderText = "Dirección";
            dgvEmpleados.Columns["celular"].HeaderText = "Celular";
            dgvEmpleados.Columns["cargo"].HeaderText = "Cargo";
            dgvEmpleados.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvEmpleados.Columns["fechaRegistro"].HeaderText = "Fecha Registro";
            if (lista.Count > 0) dgvEmpleados.CurrentCell = dgvEmpleados.Rows[0].Cells["cedulaIdentidad"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void FrmEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchTimer?.Dispose();
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            listar();
            pnlAgregar.Visible = false;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            txtCelular.KeyPress += Util.onlyNumbers;
            txtNombres.KeyPress += Util.onlyLetters;
            txtPrimerApellido.KeyPress += Util.onlyLetters;
            txtSegundoApellido.KeyPress += Util.onlyLetters;
        }
        private void limpiar()
        {
            txtCedulaIdentidad.Clear();
            txtNombres.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
            txtUsuario.Clear();
            cbxCargo.SelectedIndex = -1;
        }
        private void mostrarPanelAgregar()
        {
            pnlAgregar.Visible = true;
            pnlAgregar.BringToFront();

            dgvEmpleados.Enabled = false;
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            txtBuscar.Enabled = false;
        }
        private void ocultarPanelAgregar()
        {
            pnlAgregar.Visible = false;

            dgvEmpleados.Enabled = true;
            btnAgregar.Enabled = true;
            btnEditar.Enabled = dgvEmpleados.Rows.Count > 0;
            btnEliminar.Enabled = dgvEmpleados.Rows.Count > 0;
            txtBuscar.Enabled = true;

            txtBuscar.Focus();
        }
        private bool validar()
        {
            bool esValido = true;
            erpCedulaIdentidad.SetError(txtCedulaIdentidad, "");
            erpNombres.SetError(txtNombres, "");
            erpApellidos.SetError(txtPrimerApellido, "");
            erpDireccion.SetError(txtDireccion, "");
            erpCelular.SetError(txtCelular, "");
            erpCargo.SetError(cbxCargo, "");

            if (string.IsNullOrEmpty(txtCedulaIdentidad.Text))
            {
                erpCedulaIdentidad.SetError(txtCedulaIdentidad, "El campo CI es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtNombres.Text))
            {
                erpNombres.SetError(txtNombres, "El campo Nombres es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtPrimerApellido.Text) && string.IsNullOrEmpty(txtSegundoApellido.Text))
            {
                erpApellidos.SetError(txtPrimerApellido, "Debe introducir al menos un apellido");
                erpApellidos.SetError(txtSegundoApellido, "Debe introducir al menos un apellido");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                erpDireccion.SetError(txtDireccion, "El campo Dirección es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtCelular.Text) || txtCelular.Text.Length != 8)
            {
                erpCelular.SetError(txtCelular, "El campo Celular es obligatorio o no tiene la longitud correcta");
                esValido = false;
            }
            if (string.IsNullOrEmpty(cbxCargo.Text))
            {
                erpCargo.SetError(cbxCargo, "El campo Cargo es obligatorio");
                esValido = false;
            }

            return esValido;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modoEdicion = false;
            limpiar();
            mostrarPanelAgregar();
            txtCedulaIdentidad.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var empleado = new Empleado();
                empleado.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
                empleado.nombres = txtNombres.Text.Trim();
                empleado.primerApellido = txtPrimerApellido.Text.Trim();
                empleado.segundoApellido = txtSegundoApellido.Text.Trim();
                empleado.direccion = txtDireccion.Text.Trim();
                empleado.celular = Convert.ToInt64(txtCelular.Text);
                empleado.cargo = cbxCargo.Text;
                empleado.usuarioRegistro = Util.usuario.usuario1;

                Usuario usuario = null;
                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    usuario = new Usuario();
                    usuario.usuario1 = txtUsuario.Text.Trim();
                    usuario.clave = Util.Encrypt("hola123");
                }

                if (!modoEdicion)
                {
                    empleado.fechaRegistro = DateTime.Now;
                    empleado.estado = 1;
                    EmpleadoCln.crear(empleado, usuario);
                }
                else
                {
                    int index = dgvEmpleados.CurrentCell.RowIndex;
                    empleado.id = Convert.ToInt32(dgvEmpleados.Rows[index].Cells["id"].Value);
                    EmpleadoCln.actualizar(empleado, txtUsuario.Text.Trim(), Util.Encrypt("hola123"));
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Registro guardado correctamente", "::: Restaurant - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int index = dgvEmpleados.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvEmpleados.Rows[index].Cells["id"].Value);
            var empleado = EmpleadoCln.obtenerUno(id);
            var usuario = empleado.Usuario.Count > 0 ? empleado.Usuario.First().usuario1 : "";
            txtCedulaIdentidad.Text = empleado.cedulaIdentidad;
            txtNombres.Text = empleado.nombres;
            txtPrimerApellido.Text = empleado.primerApellido;
            txtSegundoApellido.Text = empleado.segundoApellido;
            txtDireccion.Text = empleado.direccion;
            txtCelular.Text = empleado.celular.ToString();
            txtUsuario.Text = usuario;
            cbxCargo.Text = empleado.cargo;
            mostrarPanelAgregar();
            txtCedulaIdentidad.Focus();
            modoEdicion = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvEmpleados.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvEmpleados.Rows[index].Cells["id"].Value);
            string ci = dgvEmpleados.Rows[index].Cells["cedulaIdentidad"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar al Empleado con CI {ci}?",
                "::: Restaurant - Mensaje :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                EmpleadoCln.eliminar(id, Util.usuario.usuario1);
                listar();
                MessageBox.Show("El Empleado se ha eliminado correctamente", "::: Restaurant - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ocultarPanelAgregar();
        }

        private void btnCerrarAgregar_Click(object sender, EventArgs e)
        {
            ocultarPanelAgregar();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            searchTimer?.Dispose();

            searchTimer = new System.Threading.Timer(_ =>
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    if (!string.IsNullOrWhiteSpace(txtBuscar.Text) || txtBuscar.Text == "")
                    {
                        listar();
                    }
                });
            }, null, SearchDelay, System.Threading.Timeout.Infinite);
        }
    }
}
