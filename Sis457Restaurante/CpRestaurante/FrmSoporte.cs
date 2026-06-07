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

namespace CpRestaurante
{
    public partial class FrmSoporte : Form
    {
        public DetalleVenta DetalleSeleccionado { get; private set; }
        public FrmSoporte()
        {
            InitializeComponent();
            tbcSoporte.SelectedIndexChanged += tbcSoporte_SelectedIndexChanged;
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
            Process.Start(new ProcessStartInfo
            {
                FileName = urlWhatsapp,
                UseShellExecute = true
            });
        }

        // 2. Interacción para abrir el gestor de correo electrónico predeterminado
        private void lnkCorreo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string mailto = "mailto:soporte@restaurant.com?subject=Soporte%20Sistema%20Restaurante";
            Process.Start(new ProcessStartInfo
            {
                FileName = mailto,
                UseShellExecute = true
            });
        }

        // 3. Interacción para ejecutar la llamada telefónica
        private void lnkTelefono_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "tel:+59175647380",
                UseShellExecute = true
            });
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
        private async void btnEnviarReporte_Click(object sender, EventArgs e)
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

            // Captura de datos listos
            string moduloAfectado = cbModulo.SelectedItem.ToString();
            string prioridadTickets = cbPrioridad.SelectedItem.ToString();
            string descripcionProblema = txtDescripcion.Text.Trim();

            // --- CONFIGURACIÓN Y ENVÍO DE EMAIL CON MAILKIT ---

            // Cambiar el cursor a "Espera" para avisar al usuario que se está procesando
            Cursor = Cursors.WaitCursor;

            var mensaje = new MimeMessage();
            // Remitente (El correo que envía, idealmente una cuenta del sistema)
            mensaje.From.Add(new MailboxAddress("Sistema Restaurante POS", "notificaciones.sistema.pos@gmail.com"));
            // Destinatario (Tu correo de soporte que se ve en la barra lateral izquierda)
            mensaje.To.Add(new MailboxAddress("Soporte Técnico", "soporte@restaurant.com"));

            // Asunto dinámico basado en lo que seleccionó el usuario
            mensaje.Subject = $"[INCIDENCIA] Módulo: {moduloAfectado} - Prioridad: {prioridadTickets}";

            // Cuerpo del correo formateado elegantemente en HTML
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

            using (var clienteSmtp = new SmtpClient())
            {
                try
                {
                    // Conexión al servidor SMTP (Ejemplo con Gmail, puerto 587)
                    await clienteSmtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    // Autenticación: Recuerda usar una "Contraseña de aplicación" si es Gmail o Outlook
                    await clienteSmtp.AuthenticateAsync("notificaciones.sistema.pos@gmail.com", "tu_contraseña_o_token_aqui");

                    // Enviar de forma asíncrona
                    await clienteSmtp.SendAsync(mensaje);
                    await clienteSmtp.DisconnectAsync(true);

                    // Si todo sale bien, mostramos el mensaje de éxito original
                    MessageBox.Show("El informe técnico ha sido registrado y enviado al equipo de soporte en Sucre con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar el formulario y regresar a los estados por defecto
                    txtDescripcion.Clear();
                    cbModulo.SelectedIndex = 0;
                    cbPrioridad.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    // Si el servidor SMTP falla, le avisamos al usuario sin tumbar la app
                    MessageBox.Show($"No se pudo enviar el correo de soporte automáticamente.\nDetalles del error: {ex.Message}", "Error de Conexión SMTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Devolver el cursor a su estado normal pase lo que pase
                    Cursor = Cursors.Default;
                }
            }
        }

        private void tbcSoporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Evaluamos el índice de la pestaña seleccionada actualmente
            switch (tbcSoporte.SelectedIndex)
            {
                case 0:
                    // LADO: REPORTAR INCIDENCIA (Índice 0)
                    // Aquí puedes poner la lógica que desees cuando entren a este lado
                    cbModulo.Focus(); // Por ejemplo, mandar el foco al primer combobox
                    break;

                case 1:
                    // LADO: PREGUNTAS FRECUENTES (Índice 1)
                    // Aquí puedes limpiar o reestablecer el estado de las FAQ
                    txtRespuestaFAQ.Text = "Seleccione una pregunta para ver la solución detallada.";
                    break;
            }
        }
    }
}
