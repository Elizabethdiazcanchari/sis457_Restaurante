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
    public partial class FrmDetalleVenta : Form
    {
        private readonly int _idVenta;

        public FrmDetalleVenta(int value)
        {
            InitializeComponent();
            _idVenta = value;
            this.Load += FrmDetalleVenta_Load;
        }

        private void FrmDetalleVenta_Load(object sender, EventArgs e)
        {
            ConfigurarDgvDetalle();
            CargarDatosVenta(_idVenta);
        }

        private void ConfigurarDgvDetalle()
        {
            dgvDetalle.Columns.Clear();
            dgvDetalle.AutoGenerateColumns = false;

            // Estilos estéticos para el DataGridView en modo oscuro
            dgvDetalle.EnableHeadersVisualStyles = false;
            dgvDetalle.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 36, 56);
            dgvDetalle.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetalle.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            var colNombre = new DataGridViewTextBoxColumn
            {
                Name = "nombreProducto",
                HeaderText = "Producto / Ímportado",
                DataPropertyName = "nombreProducto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var colCantidad = new DataGridViewTextBoxColumn
            {
                Name = "cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "cantidad",
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            var colPrecio = new DataGridViewTextBoxColumn
            {
                Name = "precioUnitario",
                HeaderText = "P. Unitario",
                DataPropertyName = "precioUnitario",
                Width = 100,
                DefaultCellStyle = { Format = "0.00", Alignment = DataGridViewContentAlignment.MiddleRight }
            };

            var colTotal = new DataGridViewTextBoxColumn
            {
                Name = "total",
                HeaderText = "Subtotal",
                DataPropertyName = "total",
                Width = 110,
                DefaultCellStyle = { Format = "0.00", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 9, FontStyle.Bold) }
            };

            dgvDetalle.Columns.AddRange(colNombre, colCantidad, colPrecio, colTotal);
        }

        private void CargarDatosVenta(int idVenta)
        {
            var pedido = VentaCln.obtenerUno(idVenta);
            if (pedido == null)
            {
                MessageBox.Show("No se encontró la cabecera del pedido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            var cliente = ClienteCln.obtenerUno(pedido.idCliente);
            var detalles = DetalleVentaCln.listarPorVenta(idVenta);

            // Cabecera (Asegúrate de que estos nombres coincidan con tus Labels del diseño)
            lblTransaccion.Text = pedido.numeroTransaccion;
            lblCliente.Text = cliente != null
                ? $"{cliente.razonSocial} ({cliente.ciNit})"
                : "(Cliente no disponible)";
            
            lblUsuario.Text = pedido.usuarioRegistro;
            lblFecha.Text = pedido.fechaRegistro.ToString("dd/MM/yyyy HH:mm");

            // Detalle: Ajustamos los nombres de las propiedades del objeto anónimo
            // para que coincidan exactamente con el DataPropertyName configurado arriba
            var filas = detalles.Select(d =>
            {
                var prod = ProductoCln.obtenerUno(d.idProducto);
                var nombre = prod != null ? prod.nombre : $"#{d.idProducto}";
                return new
                {
                    nombreProducto = nombre,  // Corregido
                    cantidad = d.cantidad,      // Corregido
                    precioUnitario = d.precioUnitario, // Corregido
                    total = d.total             // Corregido
                };
            }).ToList();

            dgvDetalle.DataSource = filas;

            // Formateamos el total para que muestre decimales limpios en la interfaz
            var total = detalles.Sum(x => x.total);
            lblTotal.Text = total.ToString();
        }

        private void btnSalirCate_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
