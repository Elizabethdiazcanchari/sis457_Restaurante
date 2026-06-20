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
    public partial class FrmReporte : Form
    {
        public FrmReporte()
        {
            InitializeComponent();
        }

        // Se ejecuta una sola vez al levantar la ventana de forma segura
        private void FrmReporte_Load(object sender, EventArgs e)
        {
            ConfigurarColumnasGrid();

            // Configurar los Labels del gráfico una sola vez al cargar el formulario
            ConfigurarFormatoLabelsGrafico();

            // Seteamos las fechas por defecto (DESDE: Ayer, HASTA: Hoy)
            dtpDesde.Value = DateTime.Now.AddDays(-1);
            dtpHasta.Value = DateTime.Now;

            // Forzamos la actualización automática al abrirse el formulario
            btnProcesar_Click(this, EventArgs.Empty);
        }

        // Inicializa o restablece la estructura de columnas del DataGridView de forma segura.
        private void ConfigurarColumnasGrid()
        {
            dgvReporte.Columns.Clear();
            dgvReporte.Columns.Add("colTransaccion", "Nro. Transacción");
            dgvReporte.Columns.Add("colFecha", "Fecha");
            dgvReporte.Columns.Add("colCliente", "Cliente");
            dgvReporte.Columns.Add("colTipo", "Tipo Pedido");
            dgvReporte.Columns.Add("colEstado", "Estado");
            dgvReporte.Columns.Add("colTotal", "Total Neto");

            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Aplica las propiedades de truncado seguro a las etiquetas del gráfico.
        private void ConfigurarFormatoLabelsGrafico()
        {
            Label[] labelsProductos = { lblProd1, lblProd2, lblProd3 };

            foreach (var lbl in labelsProductos)
            {
                if (lbl != null)
                {
                    lbl.AutoSize = false;       // Permite definir un tamaño fijo explícito
                    lbl.Width = 115;            // Ancho límite seguro antes de que inicien tus paneles de barra
                    lbl.AutoEllipsis = true;    // Agrega automáticamente los "..." si el texto no entra en su caja
                }
            }
        }

        // Función auxiliar para recortar nombres de productos muy largos y asegurar los puntos suspensivos.
        private string TruncarTexto(string texto, int maxCaracteres)
        {
            if (string.IsNullOrEmpty(texto)) return "Sin datos";
            return texto.Length > maxCaracteres ? texto.Substring(0, maxCaracteres - 3) + "..." : texto;
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime inicio = dtpDesde.Value;
                DateTime fin = dtpHasta.Value;

                var listaReporte = VentaCln.ObtenerReporteFinanciero("", inicio, fin);

                // Prevención: Si por ciclo de vida MDI se perdieron las columnas, las remaqueta antes de limpiar filas
                if (dgvReporte.Columns.Count == 0)
                {
                    ConfigurarColumnasGrid();
                }
                dgvReporte.Rows.Clear();

                int totalOrdenes = 0;
                decimal totalIngresosNetos = 0;

                foreach (var fila in listaReporte)
                {
                    totalOrdenes++;

                    string transaccion = fila.numeroTransaccion;
                    string fecha = fila.fechaRegistro.ToString("dd/MM/yyyy HH:mm");
                    string cliente = fila.cliente;
                    string tipoPedido = fila.tipoPedido;
                    string estado = fila.estado.ToString() == "1" ? "Completado" : "Anulado";

                    decimal netoFila = Convert.ToDecimal(fila.totalNeto);
                    totalIngresosNetos += netoFila;

                    dgvReporte.Rows.Add(transaccion, fecha, cliente, tipoPedido, estado, $"Bs. {netoFila:F2}");
                }

                lblKpiOrdenes.Text = $"{totalOrdenes} Órdenes";
                lblKpiIngresos.Text = $"Bs. {totalIngresosNetos:F2}";

                decimal ticketPromedio = totalOrdenes > 0 ? (totalIngresosNetos / totalOrdenes) : 0;
                lblKpiTicket.Text = $"Bs. {ticketPromedio:F2}";

                int totalRowIndex = dgvReporte.Rows.Add("TOTALES", totalOrdenes.ToString(), "", "", "", $"Bs. {totalIngresosNetos:F2}");
                dgvReporte.Rows[totalRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(234, 237, 209);
                dgvReporte.Rows[totalRowIndex].DefaultCellStyle.Font = new Font(dgvReporte.Font, FontStyle.Bold);

                ActualizarGraficoEstadistico();
                CalcularEficienciaOperativa();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar los datos financieros: {ex.Message}", "Error Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarGraficoEstadistico()
        {
            try
            {
                var listaTop = VentaCln.ObtenerTopProductosGerencial();

                lblProd1.Text = "Sin datos"; valP1.Text = "0"; panelBarra1.Width = 0;
                lblProd2.Text = "Sin datos"; valP2.Text = "0"; panelBarra2.Width = 0;
                lblProd3.Text = "Sin datos"; valP3.Text = "0"; panelBarra3.Width = 0;

                // Límite de caracteres idóneo antes de chocar con tus paneles (Modifica el 13 si requieres más o menos espacio)
                int limiteLetras = 13;

                if (listaTop.Count >= 1)
                {
                    lblProd1.Text = TruncarTexto(listaTop[0].nombre, limiteLetras);
                    int cant1 = Convert.ToInt32(listaTop[0].totalVendido);
                    valP1.Text = cant1.ToString();
                    panelBarra1.Width = Math.Min(cant1 * 8, 174);
                }

                if (listaTop.Count >= 2)
                {
                    lblProd2.Text = TruncarTexto(listaTop[1].nombre, limiteLetras);
                    int cant2 = Convert.ToInt32(listaTop[1].totalVendido);
                    valP2.Text = cant2.ToString();
                    panelBarra2.Width = Math.Min(cant2 * 8, 174);
                }

                if (listaTop.Count >= 3)
                {
                    lblProd3.Text = TruncarTexto(listaTop[2].nombre, limiteLetras);
                    int cant3 = Convert.ToInt32(listaTop[2].totalVendido);
                    valP3.Text = cant3.ToString();
                    panelBarra3.Width = Math.Min(cant3 * 8, 174);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el gráfico estadístico: {ex.Message}", "Error de Gráfico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CalcularEficienciaOperativa()
        {
            try
            {
                var listaPedido = VentaCln.ObtenerReporteTipoPedido();

                foreach (var fila in listaPedido)
                {
                    string tipo = fila.tipoPedido;
                    decimal total = Convert.ToDecimal(fila.totalIngresos);
                    int ordenes = Convert.ToInt32(fila.cantidadOrdenes);

                    // Búsqueda dinámica limpia para evitar el error CS0103 de controles inexistentes en compilación
                    var lblMesa = Controls.Find("lblMesaInfo", true).FirstOrDefault() as Label;
                    if (tipo == "MESA" && lblMesa != null)
                        lblMesa.Text = $"En Salón (Mesas): Bs. {total:F2} ({ordenes} Pedidos)";

                    var lblDelivery = Controls.Find("lblDeliveryInfo", true).FirstOrDefault() as Label;
                    if (tipo == "DELIVERY" && lblDelivery != null)
                        lblDelivery.Text = $"Delivery / Envíos: Bs. {total:F2} ({ordenes} Pedidos)";

                    var lblLlevar = Controls.Find("lblLlevarInfo", true).FirstOrDefault() as Label;
                    if (tipo == "LLEVAR" && lblLlevar != null)
                        lblLlevar.Text = $"Para Llevar: Bs. {total:F2} ({ordenes} Pedidos)";
                }
            }
            catch
            {
                // Tratamiento silencioso seguro
            }
        }

        private void FrmReporte_VisibleChanged(object sender, EventArgs e)
        {
            // Si el formulario hijo se vuelve a hacer visible dentro del contenedor, actualiza
            if (this.Visible)
            {
                btnProcesar_Click(this, EventArgs.Empty);
            }
        }
    }
}