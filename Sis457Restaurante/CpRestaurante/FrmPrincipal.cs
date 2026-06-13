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
        private Button botonActivo;

        public FrmPrincipal()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            this.frmAutenticacion = new FrmAutenticacion();
            pnContenedor.BackColor = Color.FromArgb(241, 245, 249);

            this.Load += FrmPrincipal_Load;

            // TRUCO CLAVE: Escuchar activamente cuando la ventana cambia de tamaño (Maximizar / Restaurar)
            this.SizeChanged += FrmPrincipal_SizeChanged;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            MostrarInicio(btnInicio);
        }

        private void FrmPrincipal_SizeChanged(object sender, EventArgs e)
        {
            // Si hay un formulario cargado actualmente en el contenedor
            if (activeForm != null && !activeForm.IsDisposed)
            {
                // Forzamos al contenedor principal a reacomodar sus dimensiones internas
                pnContenedor.PerformLayout();

                // Si el formulario que está viendo el usuario es el de Inicio...
                if (activeForm is FrmInicio frmInicio)
                {
                    // Volvemos a disparar el método para que recalcule el espacio sobrante y centre los platos
                    frmInicio.CargarProductosMasVendidos();
                }
            }
        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void ActivarBotonMenu(object botonRemitente)
        {
            if (botonRemitente != null)
            {
                if (botonActivo != (Button)botonRemitente)
                {
                    DesactivarBotonMenu();
                    botonActivo = (Button)botonRemitente;
                    botonActivo.BackColor = Color.FromArgb(202, 138, 4);
                    botonActivo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                }
            }
        }

        private void DesactivarBotonMenu()
        {
            if (botonActivo != null)
            {
                botonActivo.BackColor = Color.FromArgb(15, 23, 42);
                botonActivo.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            }
        }

        private void AbrirFormulario(Form formulario, object botonSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            ActivarBotonMenu(botonSender);

            activeForm = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill; // Garantiza que el formulario hijo llene el pnContenedor

            this.pnContenedor.Controls.Add(formulario);
            this.pnContenedor.Tag = formulario;

            formulario.BringToFront();
            formulario.Show();

            pnContenedor.PerformLayout();
            formulario.Update();
        }

        private void MostrarInicio(object sender)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmInicio(), sender);
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

        private void btnHome_Click(object sender, EventArgs e) => MostrarInicio(sender);
        private void btnInicio_Click(object sender, EventArgs e) => MostrarInicio(sender);

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