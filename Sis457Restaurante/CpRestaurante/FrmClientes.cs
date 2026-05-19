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
    public partial class FrmClientes : Form
    {
        private bool modoEdicion = false;
        private System.Threading.Timer searchTimer;
        private const int SearchDelay = 500;
        public FrmClientes()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var lista = ClienteCln.listarPa(txtBuscar.Text.Trim());
            dgvClientes.DataSource = lista;
            dgvClientes.Columns["id"].Visible = false;
            dgvClientes.Columns["estado"].Visible = false;
            dgvClientes.Columns["ciNit"].HeaderText = "CI/NIT";
            dgvClientes.Columns["razonSocial"].HeaderText = "Razón Social";
            dgvClientes.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvClientes.Columns["fechaRegistro"].HeaderText = "Fecha Registro";
            if (lista.Count > 0) dgvClientes.CurrentCell = dgvClientes.Rows[0].Cells["ciNit"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
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

        private void FrmClientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchTimer?.Dispose();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            listar();
            pnlAgregar.Visible = false;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            txtCiNit.KeyPress += Util.onlyNumbers;
            txtRazonSocial.KeyPress += Util.onlyLetters;

            // Botón Guardar (Cambia a un verde más oscuro al pasar el mouse)
            btnGuardar.MouseEnter += (s, ev) => btnGuardar.BackColor = Color.FromArgb(22, 163, 74);
            btnGuardar.MouseLeave += (s, ev) => btnGuardar.BackColor = Color.FromArgb(34, 197, 94);

            // Botón Cancelar (Cambia a un rojo más oscuro al pasar el mouse)
            btnCancelar.MouseEnter += (s, ev) => btnCancelar.BackColor = Color.FromArgb(220, 38, 38);
            btnCancelar.MouseLeave += (s, ev) => btnCancelar.BackColor = Color.FromArgb(239, 68, 68);
        }
        private void limpiar()
        {
            txtCiNit.Clear();
            txtRazonSocial.Clear();
        }
        private void mostrarPanelAgregar()
        {
            pnlAgregar.Visible = true;
            pnlAgregar.BringToFront();

            dgvClientes.Enabled = false;
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            txtBuscar.Enabled = false;
        }
        private void ocultarPanelAgregar()
        {
            pnlAgregar.Visible = false;

            dgvClientes.Enabled = true;
            btnAgregar.Enabled = true;
            btnEditar.Enabled = dgvClientes.Rows.Count > 0;
            btnEliminar.Enabled = dgvClientes.Rows.Count > 0;
            txtBuscar.Enabled = true;

            txtBuscar.Focus();
        }
        private bool validar()
        {
            bool esValido = true;
            erpCiNit.SetError(txtCiNit, "");
            erpRazonSocial.SetError(txtRazonSocial, "");

            if (string.IsNullOrEmpty(txtCiNit.Text))
            {
                erpCiNit.SetError(txtCiNit, "El campo CI es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtRazonSocial.Text))
            {
                erpRazonSocial.SetError(txtRazonSocial, "El campo Nombres es obligatorio");
                esValido = false;
            }
            return esValido;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modoEdicion = false; ;
            limpiar();
            mostrarPanelAgregar();
            txtCiNit.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var cliente = new Cliente();
                cliente.ciNit = txtCiNit.Text.Trim();
                cliente.razonSocial = txtRazonSocial.Text.Trim();
                cliente.usuarioRegistro = Util.usuario.usuario1;

                if (!modoEdicion)
                {
                    cliente.fechaRegistro = DateTime.Now;
                    cliente.estado = 1;
                    ClienteCln.crear(cliente);
                }
                else
                {
                    int index = dgvClientes.CurrentCell.RowIndex;
                    cliente.id = Convert.ToInt32(dgvClientes.Rows[index].Cells["id"].Value);
                    ClienteCln.actualizar(cliente);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Registro guardado correctamente", "::: Restaurant - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int index = dgvClientes.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvClientes.Rows[index].Cells["id"].Value);
            var cliente = ClienteCln.obtenerUno(id);
            txtCiNit.Text = cliente.ciNit;
            txtRazonSocial.Text = cliente.razonSocial;
            mostrarPanelAgregar();
            txtCiNit.Focus();
            modoEdicion = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvClientes.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvClientes.Rows[index].Cells["id"].Value);
            string razonSocial = dgvClientes.Rows[index].Cells["razonSocial"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar al Cliente con {razonSocial}?",
                "::: Restaurant - Mensaje :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ClienteCln.eliminar(id, Util.usuario.usuario1);
                listar();
                MessageBox.Show("El Cliente se ha eliminado correctamente", "::: Restaurant - Mensaje :::",
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
    }
}
