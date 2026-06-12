using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClnRestaurante;

namespace CpRestaurante
{
    public partial class FrmPrincipal : Form
    {
        private Form activeForm;
        private FrmAutenticacion frmAutenticacion;

        // 1. Declaramos una variable global para rastrear el botón que está marcado actualmente
        private Button botonActivo;

        public FrmPrincipal()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            this.frmAutenticacion = new FrmAutenticacion();

            pnContenedor.BackColor = Color.FromArgb(241, 245, 249);

            this.Load += FrmPrincipal_Load;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // Al iniciar pasamos 'btnInicio' (o btnHome) para que empiece marcado por defecto
            MostrarInicio(btnInicio);
        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        // 2. Método centralizado para cambiar los estados visuales de los botones del menú
        private void ActivarBotonMenu(object botonRemitente)
        {
            if (botonRemitente != null)
            {
                // Si ya había un botón activo anterior, lo regresamos a su diseño normal oscuro
                if (botonActivo != (Button)botonRemitente)
                {
                    DesactivarBotonMenu();

                    botonActivo = (Button)botonRemitente;

                    // --- ESTILO DEL BOTÓN SELECCIONADO ---
                    // Cambiamos el color de fondo a uno más claro para resaltar
                    botonActivo.BackColor = Color.FromArgb(30, 41, 59); // Azul grisáceo pizarra oscuro
                    botonActivo.Font = new Font("Segoe UI", 11F, FontStyle.Bold); // Texto un poco más grande y negrita
                }
            }
        }

        // 3. Método para limpiar el diseño de cualquier botón previamente activo
        private void DesactivarBotonMenu()
        {
            if (botonActivo != null)
            {
                // --- REGRESAR AL ESTILO ORIGINAL ---
                // Reemplaza por el color exacto que tengan tus botones en el diseñador (el azul marino oscuro)
                botonActivo.BackColor = Color.FromArgb(15, 23, 42);
                botonActivo.Font = new Font("Segoe UI", 10F, FontStyle.Regular); // Fuente normal
            }
        }

        private void AbrirFormulario(Form formulario, object botonSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            // Ejecutamos el marcado visual del botón correspondiente
            ActivarBotonMenu(botonSender);

            activeForm = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            this.pnContenedor.Controls.Add(formulario);
            this.pnContenedor.Tag = formulario;

            formulario.BringToFront();
            formulario.Show();

            pnContenedor.PerformLayout();
            formulario.Update();
        }

        // Adaptamos el método para recibir qué botón disparó la acción de Inicio
        private void MostrarInicio(object botonSender)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmInicio(), botonSender);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnRestaurar.Visible = true;
            btnMaximizar.Visible = false;
        }

        private void paBarraTitulo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnRestaurar.Visible = true;
                btnMaximizar.Visible = false;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnRestaurar.Visible = false;
                btnMaximizar.Visible = true;
            }
        }

        private void paBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        // --- SECCIÓN DE EVENTOS CLICK ACTUALIZADA CON 'SENDER' ---

        private void btnHome_Click(object sender, EventArgs e)
        {
            // Pasamos 'sender' (que representa al botón clicleado)
            MostrarInicio(sender);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            MostrarInicio(sender);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmProducto(), sender);
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmReporte(), sender);
        }

        private void btnDetalleVenta_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmVenta(), sender);
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmEmpleado(), sender);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmClientes(), sender);
        }

        private void btnSoporte_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmSoporte(), sender);
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Util.usuario = null;
            this.Hide();
            frmAutenticacion.Show();
        }
    }
}