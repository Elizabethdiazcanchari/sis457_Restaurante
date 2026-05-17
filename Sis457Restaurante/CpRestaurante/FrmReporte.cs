using ClnRestaurante;
using CadRestaurante;
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
    public partial class FrmReporte : Form
    {
        private System.Threading.Timer searchTimer;
        private const int SearchDelay = 500;
        public FrmReporte()
        {
            InitializeComponent();
            this.Load += FrmReporte_Load;
            this.FormClosing += FrmReporte_FormClosing;
        }

        private void listar()
        {
            try
            {
                // Obtenemos el texto de forma segura
                string criterio = txtBuscar.Text.Trim();
                var lista = VentaCln.listarPa(criterio);

                dgvReporte.DataSource = lista;

                // Verificamos si hay columnas para evitar errores de índice
                if (dgvReporte.Columns.Count > 0)
                {
                    if (dgvReporte.Columns.Contains("id")) dgvReporte.Columns["id"].Visible = false;
                    if (dgvReporte.Columns.Contains("estado")) dgvReporte.Columns["estado"].Visible = false;
                    if (dgvReporte.Columns.Contains("numeroTransaccion")) dgvReporte.Columns["numeroTransaccion"].HeaderText = "Nro. Transacción";
                    if (dgvReporte.Columns.Contains("Cliente")) dgvReporte.Columns["Cliente"].HeaderText = "Cliente";
                    if (dgvReporte.Columns.Contains("Empleado")) dgvReporte.Columns["Empleado"].HeaderText = "Empleado";
                    if (dgvReporte.Columns.Contains("fechaRegistro")) dgvReporte.Columns["fechaRegistro"].HeaderText = "Fecha";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            listar();
            txtBuscar.TextChanged += txtBuscar_TextChanged;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            searchTimer?.Dispose();

            // Capturamos el texto aquí (en el hilo de la UI) antes de entrar al Timer
            string textoBuscar = txtBuscar.Text;

            searchTimer = new System.Threading.Timer(_ =>
            {
                // Ahora invocamos a la UI de manera segura pasando el texto ya capturado
                BeginInvoke((MethodInvoker)delegate
                {
                    listar();
                });
            }, null, SearchDelay, System.Threading.Timeout.Infinite);
        }

        private void FrmReporte_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchTimer?.Dispose();
        }

        private int? GetIdPedidoSeleccionado()
        {
            if (dgvReporte.CurrentRow == null) return null;

            // Validación robusta de la existencia de la celda "id"
            if (!dgvReporte.Columns.Contains("id")) return null;

            var cell = dgvReporte.CurrentRow.Cells["id"];
            if (cell == null || cell.Value == null) return null;

            return Convert.ToInt32(cell.Value);
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            var id = GetIdPedidoSeleccionado();
            if (id == null)
            {
                MessageBox.Show("Seleccione un pedido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var frm = new FrmDetalleVenta(id.Value))
            {
                frm.ShowDialog(this);
            }
        }

        private void dgvReporte_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnVerDetalle_Click(sender, EventArgs.Empty);
        }
    }
}
