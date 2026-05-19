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
    public partial class FrmSoporte : Form
    {
        public DetalleVenta DetalleSeleccionado { get; private set; }
        public FrmSoporte()
        {
            InitializeComponent();
        }

        private void FrmSeleccionarProducto_Load(object sender, EventArgs e)
        {
            cbxProducto.DataSource = ProductoCln.listar();
            cbxProducto.ValueMember = "id";
            cbxProducto.DisplayMember = "nombre";
            nudCantidad.Value = 1;
            ActualizarPrecioYTotal();
        }
        private void cbxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarPrecioYTotal();
        }
        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            ActualizarPrecioYTotal();
        }
        private void ActualizarPrecioYTotal()
        {
            if (cbxProducto.SelectedItem is Producto producto)
            {
                txtPrecioUnitario.Text = producto.precioVenta.ToString("0.00");
                txtTotal.Text = (producto.precioVenta * nudCantidad.Value).ToString("0.00");
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cbxProducto.SelectedItem is Producto producto)
            {
                int cantidadSolicitada = (int)nudCantidad.Value;
                if (cantidadSolicitada > producto.stock)
                {
                    MessageBox.Show("No hay stock suficiente para este producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DetalleSeleccionado = new DetalleVenta
                {
                    idProducto = producto.id,
                    cantidad = cantidadSolicitada,
                    precioUnitario = producto.precioVenta,
                    total = producto.precioVenta * cantidadSolicitada,
                };
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
