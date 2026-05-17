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

namespace CpRestaurante
{
    public partial class FrmPrincipal : Form
    {
        private Form activeForm;
        FrmAutenticacion frmAutenticacion;
        public FrmPrincipal()
        {
            InitializeComponent();
            tmrReloj.Enabled = true;
            tmrReloj.Interval = 1000;
            tmrReloj.Tick += Timer_Tick;
            this.frmAutenticacion = new FrmAutenticacion();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblReloj.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void AbrirFormulario(Form formulario)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            this.pnContenedor.Controls.Add(formulario);
            this.pnContenedor.Tag = formulario;
            formulario.BringToFront();
            formulario.Show();
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
        private void btnHome_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm = null;
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmProducto());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmReporte());
        }

        private void btnDetalleVenta_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmVenta());
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmEmpleado());
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAutenticacion.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            paBarraTitulo.BackColor = Color.FromArgb(15, 23, 42);
            AbrirFormulario(new FrmClientes());
        }
    }
}
