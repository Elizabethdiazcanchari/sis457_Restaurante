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
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Configuration;

namespace CpRestaurante
{
    public partial class FrmSoporte : Form
    {
        public DetalleVenta DetalleSeleccionado { get; private set; }
        public FrmSoporte()
        {
            InitializeComponent();
            tbcSoporte.SelectedIndexChanged += tbcSoporte_SelectedIndexChanged;
            lstPreguntas.SelectedIndexChanged += LstPreguntas_SelectedIndexChanged;
            cbPrioridad.SelectedIndexChanged += CbPrioridad_SelectedIndexChanged;

            ConfigurarComboBoxes();
        }

        private void ConfigurarComboBoxes()
        {
            cbModulo.Items.Clear();
            cbModulo.Items.Add("--- Seleccionar Módulo ---");
            cbModulo.Items.Add("Inicio de sesión (Autenticación / Permisos)");
            cbModulo.Items.Add("Venta (POS / Registro de Pedidos)");
            cbModulo.Items.Add("Productos (Platos / Categorías / Stock)");
            cbModulo.Items.Add("Empleados (Roles / Turnos / Personal)");
            cbModulo.Items.Add("Clientes (Historial / Datos de Facturación)");
            cbModulo.Items.Add("Reportes (Estadísticas / Cierres de Caja)");
            cbModulo.SelectedIndex = 0;

            cbPrioridad.Items.Clear();
            cbPrioridad.Items.Add("--- Seleccionar Prioridad ---");
            cbPrioridad.Items.Add("Alta (Caja Inoperable / Bloqueante)");
            cbPrioridad.Items.Add("Media (Falla intermitente en el flujo)");
            cbPrioridad.Items.Add("Baja (Consulta técnica / Duda general)");
            cbPrioridad.SelectedIndex = 0;
        }

        private void CbPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPrioridad.SelectedIndex == 1)
            {
                cbPrioridad.ForeColor = Color.FromArgb(239, 64, 64);
            }
            else
            {
                cbPrioridad.ForeColor = Color.Black;
            }
        }

        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            string urlWhatsapp = "https://wa.me/59171806340?text=Hola,%20necesito%20soporte%20con%20el%20sistema%20POS";
            Process.Start(new ProcessStartInfo
            {
                FileName = urlWhatsapp,
                UseShellExecute = true
            });
        }

        private void lnkCorreo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string mailto = "mailto:jhoselinfigueroacolque@gmail.com?subject=Soporte%20Sistema%20Restaurante";
            Process.Start(new ProcessStartInfo
            {
                FileName = mailto,
                UseShellExecute = true
            });
        }

        private void lnkTelefono_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "tel:+59171806340",
                UseShellExecute = true
            });
        }

        private void LstPreguntas_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lstPreguntas.SelectedIndex)
            {
                case 0:
                    txtRespuestaFAQ.Text = "SOLUCIÓN TICKETERA:\r\n\r\n1. Verifique que la luz verde de encendido esté fija.\r\n2. Revise que el cable USB esté conectado al puerto correcto.\r\n3. Reinicie la cola de impresión desde el Panel de Control de Windows.";
                    break;
                case 1:
                    txtRespuestaFAQ.Text = "ANULACIÓN DE PEDIDOS:\r\n\r\nPor motivos de auditoría, los cajeros no pueden borrar pedidos.\r\nSolicite al Administrador de Turno que ingrese con su clave al panel de 'Historial de Ventas' para autorizar la cancelación.";
                    break;
                case 2:
                    txtRespuestaFAQ.Text = "CÁLCULO DE CAMBIO:\r\n\r\nAsegúrese de escribir el monto con el que paga el cliente en la casilla 'Efectivo Recibido' antes de guardar. El sistema procesará el vuelto automáticamente.";
                    break;
                default:
                    txtRespuestaFAQ.Text = "Seleccione una pregunta para ver la solución detallada.";
                    break;
            }
        }

        // 5. Envío del formulario técnico optimizado
        private async void btnEnviarReporte_Click(object sender, EventArgs e)
        {
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

            string moduloAfectado = cbModulo.SelectedItem.ToString();
            string prioridadTickets = cbPrioridad.SelectedItem.ToString();
            string descripcionProblema = txtDescripcion.Text.Trim();

            Cursor = Cursors.WaitCursor;

            using (var clienteSmtp = new SmtpClient())
            {
                try
                {
                    // 1. Cargamos TODOS los parámetros de configuración de forma segura en el Try
                    string host = ConfigurationManager.AppSettings["SmtpHost"];
                    string puertoStr = ConfigurationManager.AppSettings["SmtpPort"];
                    string usuario = ConfigurationManager.AppSettings["SmtpUser"];
                    string contrasena = ConfigurationManager.AppSettings["SmtpPass"];
                    string ignoreCertStr = ConfigurationManager.AppSettings["SmtpIgnoreInvalidCert"];

                    if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena) || string.IsNullOrWhiteSpace(puertoStr))
                    {
                        throw new Exception("Faltan configuraciones del servidor de correo en el archivo config. Verifique SmtpHost, SmtpPort, SmtpUser y SmtpPass.");
                    }

                    if (!int.TryParse(puertoStr, out int puerto))
                    {
                        throw new Exception("El puerto SMTP configurado no es un número válido.");
                    }

                    bool ignoreInvalidCert = false;
                    if (!string.IsNullOrWhiteSpace(ignoreCertStr)) bool.TryParse(ignoreCertStr, out ignoreInvalidCert);

                    // 2. Ahora sí, construimos el mensaje con variables ya verificadas
                    var mensaje = new MimeMessage();
                    mensaje.From.Add(new MailboxAddress("Sistema Restaurante POS", usuario));
                    mensaje.To.Add(new MailboxAddress("Soporte Técnico", "jhoselinfigueroacolque@gmail.com"));
                    mensaje.Subject = $"[INCIDENCIA] Módulo: {moduloAfectado} - Prioridad: {prioridadTickets}";

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = $@"
                        <h2>Nuevo Informe de Soporte Técnico</h2>
                        <hr/>
                        <p><strong>Módulo Afectado:</strong> {moduloAfectado}</p>
                        <p><strong>Nivel de Prioridad:</strong> {prioridadTickets}</p>
                        <p><strong>Fecha/Hora del Reporte:</strong> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
                        <hr/>
                        <h3>Descripción del Problema:</h3>
                        <p style='background-color: #f4f4f4; padding: 15px; border-left: 4px solid #ef4040; font-family: sans-serif;'>
                            {descripcionProblema.Replace("\n", "<br/>")}
                        </p>
                        <br/>
                        <small>Este es un correo automático generado por el módulo de soporte desde Sucre, Bolivia.</small>";

                    mensaje.Body = bodyBuilder.ToMessageBody();

                    // 3. Selección de SecureSocketOptions según puerto (465 -> SSL, 587/25 -> STARTTLS)
                    SecureSocketOptions socketOption = SecureSocketOptions.Auto;
                    if (puerto == 465) socketOption = SecureSocketOptions.SslOnConnect;
                    else if (puerto == 587 || puerto == 25) socketOption = SecureSocketOptions.StartTls;

                    // 4. Validación de certificado opcional (solo si se configura explícitamente)
                    if (ignoreInvalidCert)
                    {
                        clienteSmtp.ServerCertificateValidationCallback = (s, c, h, certEx) => true; // Permite certificados inválidos (usar solo en desarrollo)
                    }

                    // 5. Conexión y autenticación separadas para diagnóstico más claro
                    try
                    {
                        await clienteSmtp.ConnectAsync(host, puerto, socketOption);
                    }
                    catch (Exception connEx)
                    {
                        throw new Exception($"Error al conectar con el servidor SMTP ({host}:{puerto}). {connEx.Message}", connEx);
                    }

                    try
                    {
                        await clienteSmtp.AuthenticateAsync(usuario, contrasena);
                    }
                    catch (Exception authEx)
                    {
                        throw new Exception($"Autenticación SMTP fallida para el usuario {usuario}. {authEx.Message}", authEx);
                    }

                    // 6. Envío
                    await clienteSmtp.SendAsync(mensaje);
                    await clienteSmtp.DisconnectAsync(true);

                    MessageBox.Show("El informe técnico ha sido registrado y enviado al equipo de soporte en Sucre con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtDescripcion.Clear();
                    cbModulo.SelectedIndex = 0;
                    cbPrioridad.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    // Al añadir ex.Message completo sabrás con precisión exacta qué credencial o parámetro rechaza tu proveedor de email
                    MessageBox.Show($"No se pudo enviar el correo de soporte automáticamente.\n\nDetalle del error técnico: {ex.Message}", "Error de Conexión SMTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void tbcSoporte_SelectedIndexChanged(object sender, EventArgs e)
        {
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
    }
}