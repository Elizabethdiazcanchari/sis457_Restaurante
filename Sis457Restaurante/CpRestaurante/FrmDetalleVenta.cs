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
        private int idVenta;

        public FrmDetalleVenta(int value)
        {
            InitializeComponent();
            _idVenta = value;
            Load += FrmReporte_Load;
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            CargarPedido(_idVenta);
        }

        private void CargarPedido(int idVenta)
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

            // Cabecera
            lblTransaccion.Text = pedido.numeroTransaccion;
            lblCliente.Text = cliente != null
                ? $"{cliente.razonSocial} ({cliente.ciNit})"
                : "(Cliente no disponible)";
            // Muestra el usuario que registró en cabecera (evita cargar navegación)
            lblUsuario.Text = pedido.usuarioRegistro;
            lblFecha.Text = pedido.fechaRegistro.ToString("dd/MM/yyyy HH:mm");

            // Detalle
            var filas = detalles.Select(d =>
            {
                var prod = ProductoCln.obtenerUno(d.idProducto);
                var nombre = prod != null ? prod.nombre : $"#{d.idProducto}";
                return new
                {
                    Producto = nombre,
                    Cantidad = d.cantidad,
                    Precio = d.precioUnitario,
                    Importe = d.total
                };
            }).ToList();

            dgvDetalle.AutoGenerateColumns = true; // si ya definiste columnas, ponlo en false
            dgvDetalle.DataSource = filas;

            var total = detalles.Sum(x => x.total);
            lblTotal.Text = total.ToString();
        }

        private void btnSalirCate_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
