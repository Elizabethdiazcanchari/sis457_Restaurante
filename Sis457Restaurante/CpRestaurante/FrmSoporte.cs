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

        // 5. Simulación de envío del formulario técnico
        private void btnEnviarReporte_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Por favor, describa el incidente técnico antes de enviar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Aquí conectarías con tu capa lógica ClnRestaurante si deseas guardar el ticket en BD
            MessageBox.Show("El informe técnico ha sido registrado y enviado al equipo de soporte en Sucre con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtDescripcion.Clear();
        }
    }
}
