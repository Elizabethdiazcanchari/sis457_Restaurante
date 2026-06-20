using CadRestaurante;
using ClnRestaurante;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

            // NOTA: Si estos eventos ya están enlazados desde el Diseñador (Designer.cs), 
            // puedes borrar estas líneas para evitar doble ejecución.
            tbcSoporte.SelectedIndexChanged += tbcSoporte_SelectedIndexChanged;
            tbcSoporte.DrawItem += tbcSoporte_DrawItem;

            lstPreguntas.SelectedIndexChanged += LstPreguntas_SelectedIndexChanged;
            cbPrioridad.SelectedIndexChanged += CbPrioridad_SelectedIndexChanged;

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
            cbModulo.SelectedIndex = 0;

            // 2. Configuración del ComboBox de Prioridad
            cbPrioridad.Items.Clear();
            cbPrioridad.Items.Add("--- Seleccionar Prioridad ---"); // Índice 0
            cbPrioridad.Items.Add("Alta (Caja Inoperable / Bloqueante)");
            cbPrioridad.Items.Add("Media (Falla intermitente en el flujo)");
            cbPrioridad.Items.Add("Baja (Consulta técnica / Duda general)");
            cbPrioridad.SelectedIndex = 0;
        }

        private void CbPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cambia el color del texto si seleccionan la prioridad Alta (Índice 1)
            cbPrioridad.ForeColor = cbPrioridad.SelectedIndex == 1
                ? Color.FromArgb(239, 64, 64)
                : Color.Black;
        }

        // Método auxiliar seguro para abrir URLs/Protocolos en cualquier versión de .NET
        private void AbrirEnlaceSeguro(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Requerido para URLs y protocolos como mailto/tel
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir el enlace de manera automática.\nDetalles: {ex.Message}",
                                "Error al abrir enlace", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            string urlWhatsapp = "https://wa.me/59171806340?text=Hola,%20necesito%20soporte%20con%20el%20sistema%20POS";
            AbrirEnlaceSeguro(urlWhatsapp);
        }

        private void lnkCorreo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string mailto = "mailto:jhoselinfigueroacolque@gmail.com?subject=Soporte%20Sistema%20Restaurante";
            AbrirEnlaceSeguro(mailto);
        }

        private void lnkTelefono_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirEnlaceSeguro("tel:+59171806340");
        }

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

        private async void btnEnviarReporte_Click(object sender, EventArgs e)
        {
            // 1. Validaciones de la Interfaz de Usuario
            if (cbModulo.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor, seleccione el módulo afectado por la incidencia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbModulo.Focus();
                return;
            }

            if (cbPrioridad.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor, seleccione el nivel de prioridad de la operación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbPrioridad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor, describa el incidente técnico antes de enviar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            // 2. Leer Configuración Externa desde el App.config
            string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            string smtpPortStr = ConfigurationManager.AppSettings["SmtpPort"];
            string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];

            if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass))
            {
                MessageBox.Show("Faltan parámetros de configuración de correo (SmtpHost, SmtpUser o SmtpPass) en el archivo App.config.", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int smtpPort = int.TryParse(smtpPortStr, out int port) ? port : 587;

            string moduloAfectado = cbModulo.SelectedItem.ToString();
            string prioridadTickets = cbPrioridad.SelectedItem.ToString();

            // CORRECCIÓN MENOR: Nos aseguramos de limpiar saltos de línea crudos de Windows (\r\n) para el HTML
            string descripcionProblema = txtDescripcion.Text.Trim().Replace("\r\n", "\n").Replace("\n", "<br/>");

            // 3. Cambiar estado de la interfaz (Feedback visual)
            Cursor = Cursors.WaitCursor;

            try
            {
                // 4. Construcción del Mensaje con MimeKit
                var mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress("Sistema Restaurante POS", smtpUser));

                // NOTA: Cambiar a 'elizabethdiazcanchari@gmail.com' si corresponde a la otra rama
                mensaje.To.Add(new MailboxAddress("Soporte Técnico", "jhoselinfigueroacolque@gmail.com"));

                mensaje.Subject = $"[INCIDENCIA] Módulo: {moduloAfectado} - Prioridad: {prioridadTickets}";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                    <h2>Nuevo Informe de Soporte Técnico</h2>
                    <hr/>
                    <p><strong>Módulo Afectado:</strong> {moduloAfectado}</p>
                    <p><strong>Nivel de Prioridad:</strong> {prioridadTickets}</p>
                    <p><strong>Fecha/Hora del Reporte:</strong> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
                    <hr/>
                    <h3>Descripción del Problema:</h3>
                    <p style='background-color: #f4f4f4; padding: 15px; border-left: 4px solid #ef4040; font-family: sans-serif; white-space: pre-wrap;'>
                        {descripcionProblema}
                    </p>
                    <br/>
                    <small>Este es un correo automático generado por el módulo de soporte desde Sucre, Bolivia.</small>"
                };

                mensaje.Body = bodyBuilder.ToMessageBody();

                // 5. Envío Asíncrono con MailKit
                using (var clienteSmtp = new SmtpClient())
                {
                    await clienteSmtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                    await clienteSmtp.AuthenticateAsync(smtpUser, smtpPass);
                    await clienteSmtp.SendAsync(mensaje);
                    await clienteSmtp.DisconnectAsync(true);

                    MessageBox.Show("El informe técnico ha sido registrado y enviado al equipo de soporte con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 6. Limpieza de Controles tras el éxito
                    txtDescripcion.Clear();
                    cbModulo.SelectedIndex = 0;
                    cbPrioridad.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo enviar el correo de soporte automáticamente.\nDetalles del error: {ex.Message}", "Error de Conexión SMTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restablecer el cursor pase lo que pase
                Cursor = Cursors.Default;
            }
        }

        private void tbcSoporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbcSoporte.Invalidate();

            switch (tbcSoporte.SelectedIndex)
            {
                case 0:
                    cbModulo.Focus();
                    break;

                case 1:
                    txtRespuestaFAQ.Text = "Seleccione una pregunta para ver la solución detallada.";
                    break;
            }
        }

        // CORRECCIÓN: Uso de bloques 'using' para asegurar la liberación de recursos gráficos (GDI+)
        private void tbcSoporte_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle tabPageArea = tbcSoporte.GetTabRect(e.Index);
            TabPage page = tbcSoporte.TabPages[e.Index];

            Color backColorSelected = Color.FromArgb(26, 34, 54);
            Color backColorUnselected = Color.FromArgb(15, 23, 42);

            Color textColorSelected = Color.FromArgb(56, 189, 248);
            Color textColorUnselected = Color.FromArgb(148, 163, 184);

            bool isSelected = tbcSoporte.SelectedIndex == e.Index;

            using (Brush backBrush = new SolidBrush(isSelected ? backColorSelected : backColorUnselected))
            using (Brush textBrush = new SolidBrush(isSelected ? textColorSelected : textColorUnselected))
            using (Font fontTab = new Font("Segoe UI", 10, isSelected ? FontStyle.Bold : FontStyle.Regular))
            {
                // Pintamos el fondo de la pestaña actual
                e.Graphics.FillRectangle(backBrush, tabPageArea);

                using (StringFormat stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    // Dibujamos el texto centrado
                    e.Graphics.DrawString(page.Text, fontTab, textBrush, tabPageArea, stringFormat);
                }
            } // Todos los recursos gráficos se liberan automáticamente aquí de forma segura
        }
    }
}