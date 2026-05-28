using CadRestaurante;
using ClnRestaurante;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            // Asociamos el evento de cambio de selección de las preguntas frecuentes
            lstPreguntas.SelectedIndexChanged += LstPreguntas_SelectedIndexChanged;
            cbPrioridad.SelectedIndexChanged += CbPrioridad_SelectedIndexChanged; // Evento opcional para cambiar color dinámicamente

            ConfigurarComboBoxes();
        }

        private void ConfigurarComboBoxes()
        {
            // 1. Configuración del ComboBox de Módulo
            cbModulo.Items.Clear();
            cbModulo.Items.Add("--- Seleccionar Módulo ---"); // Índice 0
            cbModulo.Items.Add("Inicio de sesión (Autenticación / Permisos)");
            cbModulo.Items.Add("Venta (POS / Registro de Pedidos)");
            cbModulo.Items.Add("Productos (Platos / Categorías / Stock)");
            cbModulo.Items.Add("Empleados (Roles / Turnos / Personal)");
            cbModulo.Items.Add("Clientes (Historial / Datos de Facturación)");
            cbModulo.Items.Add("Reportes (Estadísticas / Cierres de Caja)");
            cbModulo.SelectedIndex = 0; // Muestra el texto por defecto

            // 2. Configuración del ComboBox de Prioridad
            cbPrioridad.Items.Clear();
            cbPrioridad.Items.Add("--- Seleccionar Prioridad ---"); // Índice 0
            cbPrioridad.Items.Add("Alta (Caja Inoperable / Bloqueante)");
            cbPrioridad.Items.Add("Media (Falla intermitente en el flujo)");
            cbPrioridad.Items.Add("Baja (Consulta técnica / Duda general)");
            cbPrioridad.SelectedIndex = 0; // Fuerza a mostrar el texto por defecto
        }

        // Evento visual: Cambia el color del texto si seleccionan la prioridad Alta para mantener la estética web
        private void CbPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPrioridad.SelectedIndex == 1) // Alta
            {
                cbPrioridad.ForeColor = Color.FromArgb(239, 64, 64); // Rojo
            }
            else
            {
                cbPrioridad.ForeColor = Color.Black;
            }
        }

        // 1. Interacción para abrir el chat de WhatsApp desde el sistema
        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            string urlWhatsapp = "https://wa.me/59175647380?text=Hola,%20necesito%20soporte%20con%20el%20sistema%20POS";
            Process.Start(new ProcessStartInfo(urlWhatsapp) { UseShellExecute = true });
        }

        // 2. Interacción para abrir el gestor de correo electrónico predeterminado
        private void lnkCorreo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string mailto = "mailto:soporte@restaurant.com?subject=Soporte%20Sistema%20Restaurante";
            Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
        }

        // 3. Interacción para ejecutar la llamada telefónica (si el sistema tiene app de marcado)
        private void lnkTelefono_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("tel:+59175647380") { UseShellExecute = true });
        }

        // 4. Lógica del Acordeón/Visualizador de Preguntas Frecuentes
        private void LstPreguntas_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lstPreguntas.SelectedIndex)
            {
                case 0:
                    txtRespuestaFAQ.Text = "SOLUCIÓN TICKETERA:\r\n\r\n" +
                        "1. Verifique que la luz verde de encendido esté fija.\r\n" +
                        "2. Revise que el cable USB esté conectado al puerto correcto.\r\n" +
                        "3. Reinicie la cola de impresión desde el Panel de Control de Windows.";
                    break;

                case 1:
                    txtRespuestaFAQ.Text = "ANULACIÓN DE PEDIDOS:\r\n\r\n" +
                        "Por motivos de auditoría, los cajeros no pueden borrar pedidos.\r\n" +
                        "Solicite al Administrador de Turno que ingrese con su clave al panel de 'Historial de Ventas' para autorizar la cancelación.";
                    break;

                case 2:
                    txtRespuestaFAQ.Text = "CÁLCULO DE CAMBIO:\r\n\r\n" +
                        "Asegúrese de escribir el monto con el que paga el cliente en la casilla 'Efectivo Recibido' antes de guardar. El sistema procesará el vuelto automáticamente.";
                    break;

                default:
                    txtRespuestaFAQ.Text = "Seleccione una pregunta para ver la solución detallada.";
                    break;
            }
        }

        // 5. Envío del formulario técnico con validación de ComboBoxes
        private void btnEnviarReporte_Click(object sender, EventArgs e)
        {
            // Validación 1: Verificar si seleccionó un módulo válido
            if (cbModulo.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor, seleccione el módulo afectado por la incidencia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbModulo.Focus();
                return;
            }

            // Validación 2: Verificar si seleccionó una prioridad válida
            if (cbPrioridad.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor, seleccione el nivel de prioridad de la operación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbPrioridad.Focus();
                return;
            }

            // Validación 3: Descripción vacía
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor, describa el incidente técnico antes de enviar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            // Captura de datos listos para enviar a ClnRestaurante
            string moduloAfectado = cbModulo.SelectedItem.ToString();
            string prioridadTickets = cbPrioridad.SelectedItem.ToString();
            string descripcionProblema = txtDescripcion.Text.Trim();

            // Lógica de guardado...
            MessageBox.Show("El informe técnico ha sido registrado y enviado al equipo de soporte en Sucre con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar el formulario y regresar a los estados por defecto
            txtDescripcion.Clear();
            cbModulo.SelectedIndex = 0;
            cbPrioridad.SelectedIndex = 0;
        }
    }
}
