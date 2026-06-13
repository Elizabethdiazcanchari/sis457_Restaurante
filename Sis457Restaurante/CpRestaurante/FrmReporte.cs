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

                // 1. Extraemos las fechas de los controles ignorando las horas (solo la fecha pura)
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date;

                // 2. Invocamos al nuevo método de la CLN pasándole el criterio de texto y el rango temporal
                var lista = VentaCln.listarPa(criterio, fechaInicio, fechaFin);

                dgvReporte.DataSource = lista;

                // Verificamos si hay columnas para evitar errores de índice
                if (dgvReporte.Columns.Count > 0)
                {
                    if (dgvReporte.Columns.Contains("id")) dgvReporte.Columns["id"].Visible = false;
                    if (dgvReporte.Columns.Contains("estado")) dgvReporte.Columns["estado"].Visible = false;
                    if (dgvReporte.Columns.Contains("mesa")) dgvReporte.Columns["mesa"].Visible = false;
                    if (dgvReporte.Columns.Contains("tipoPedido")) dgvReporte.Columns["tipoPedido"].Visible = false;

                    if (dgvReporte.Columns.Contains("numeroTransaccion")) dgvReporte.Columns["numeroTransaccion"].HeaderText = "Nro. Transacción";
                    if (dgvReporte.Columns.Contains("cliente")) dgvReporte.Columns["cliente"].HeaderText = "Cliente";
                    if (dgvReporte.Columns.Contains("Usuario")) dgvReporte.Columns["Usuario"].HeaderText = "Empleado";
                    if (dgvReporte.Columns.Contains("fechaRegistro")) dgvReporte.Columns["fechaRegistro"].HeaderText = "Fecha";
                    if (dgvReporte.Columns.Contains("usuarioRegistro")) dgvReporte.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";

                    // Ajustar automáticamente las columnas para que llenen la pantalla limpiamente
                    dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.MultiSelect = false;

            // 3. Inicialización inteligente: Mostrar por defecto las ventas del mes actual
            DateTime hoy = DateTime.Now;
            dtpFechaInicio.Value = new DateTime(hoy.Year, hoy.Month, 1);
            dtpFechaFin.Value = hoy;

            listar();

            txtBuscar.TextChanged += txtBuscar_TextChanged;

            // 4. Enlazamos el evento ValueChanged para que el filtro sea automático al cambiar las fechas
            dtpFechaInicio.ValueChanged += DtpFechas_ValueChanged;
            dtpFechaFin.ValueChanged += DtpFechas_ValueChanged;

            dgvReporte.CellDoubleClick += dgvReporte_CellDoubleClick;
        }

        // 5. Manejador de eventos reactivo para cambios de fecha
        private void DtpFechas_ValueChanged(object sender, EventArgs e)
        {
            // Validación lógica básica: si el rango está al revés, no consultamos la base de datos
            if (dtpFechaInicio.Value.Date > dtpFechaFin.Value.Date)
            {
                return;
            }

            listar();
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
            if (dgvReporte.SelectedRows.Count == 0) return null;

            var filaSeleccionada = dgvReporte.SelectedRows[0];
            if (!dgvReporte.Columns.Contains("id")) return null;

            var cell = filaSeleccionada.Cells["id"];
            if (cell == null || cell.Value == null) return null;

            return Convert.ToInt32(cell.Value);
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            var id = GetIdPedidoSeleccionado();
            if (id == null)
            {
                MessageBox.Show("Por favor, seleccione una venta de la lista para ver su detalle.", "Aviso",
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
            if (e.RowIndex >= 0)
            {
                btnVerDetalle_Click(sender, EventArgs.Empty);
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            var id = GetIdPedidoSeleccionado();
            if (id == null)
            {
                MessageBox.Show("Por favor, seleccione la venta que desea anular.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Capturamos el código exacto de la fila seleccionada por el usuario
            string nroTransaccion = dgvReporte.SelectedRows[0].Cells["numeroTransaccion"].Value?.ToString() ?? "Seleccionada";

            DialogResult result = MessageBox.Show(
                $"¿Está completamente seguro de que desea ANULAR la transacción {nroTransaccion}?\n\nEsta acción revertirá los estados asociados.",
                "Confirmación de Auditoría",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int filasAfectadas = VentaCln.eliminar(id.Value, "admin");

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show($"La venta {nroTransaccion} ha sido anulada con éxito.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        listar(); // Recarga la grilla limpia
                    }
                    else
                    {
                        MessageBox.Show("No se pudo realizar la operación. Verifique las restricciones del registro.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error en la capa de datos al intentar anular: {ex.Message}", "Error Crítico",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Limpiamos el cuadro de texto de búsqueda
                txtBuscar.Text = string.Empty;

                // 2. Restablecemos el rango de fechas al mes actual (igual que en el FrmReporte_Load)
                DateTime hoy = DateTime.Now;
                dtpFechaInicio.Value = new DateTime(hoy.Year, hoy.Month, 1);
                dtpFechaFin.Value = hoy;

                // 3. Forzamos la recarga de la grilla con los datos limpios
                listar();

                txtBuscar.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al recargar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
