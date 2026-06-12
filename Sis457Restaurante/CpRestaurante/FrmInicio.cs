using ClnRestaurante;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpRestaurante
{
    public partial class FrmInicio : Form
    {
        public FrmInicio()
        {
            InitializeComponent();

            // Optimizamos el renderizado para evitar molestos parpadeos visuales
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            // Configuración del Timer para el Reloj
            tmrReloj.Enabled = true;
            tmrReloj.Interval = 1000;
            tmrReloj.Tick += Timer_Tick;

            // Estilizado base del diseño moderno
            this.BackColor = Color.FromArgb(241, 245, 249);
            lblReloj.Font = new Font("Segoe UI", 20, FontStyle.Bold);

            this.Load += FrmInicio_Load;
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            CargarUsuarioActivo();
            CargarProductosMasVendidos();
        }

        private void CargarUsuarioActivo()
        {
            // Validamos que el objeto de sesión global no esté vacío
            if (Util.usuario != null && !string.IsNullOrEmpty(Util.usuario.usuario1))
            {
                lblUsuario.Text = Util.usuario.usuario1;
            }
            else
            {
                lblUsuario.Text = "INVITADO";
            }
        }

        public void CargarProductosMasVendidos()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            try
            {
                var topProductos = ProductoCln.obtenerTopMasVendidos();
                flpContenedor.Controls.Clear(); // Limpiamos el contenedor principal

                if (topProductos == null || topProductos.Count == 0) return;

                // --- CONFIGURACIÓN PARA 3 COLUMNAS CENTRADAS ---
                int columnas = 3;
                int margenCard = 10; // Margen alrededor de cada tarjeta (Padding de la card)
                int anchoCard = 260; // Ancho fijo ideal para mantener la estética limpia
                int altoCard = 240;

                // Calculamos cuánto espacio ocupa una fila completa de tarjetas con sus márgenes
                int anchoFilaTotal = (anchoCard + (margenCard * 2)) * columnas;

                // Calculamos el espacio sobrante en el contenedor principal
                int espacioSobrante = flpContenedor.Width - anchoFilaTotal;

                // Si hay espacio sobrante, dividimos entre 2 para centrar el bloque de tarjetas
                if (espacioSobrante > 0)
                {
                    // Dejamos un pequeño margen a la derecha para la barra de desplazamiento si fuera necesaria
                    flpContenedor.Padding = new Padding(espacioSobrante / 2, 15, 0, 15);
                }
                else
                {
                    flpContenedor.Padding = new Padding(15, 15, 15, 15);
                }
                // -----------------------------------------------------

                foreach (var prod in topProductos)
                {
                    // 1. Tarjeta Blanca contenedora
                    Panel card = new Panel();
                    card.Size = new Size(anchoCard, altoCard);
                    card.BackColor = Color.White;
                    card.Margin = new Padding(margenCard); // Margen simétrico

                    // 2. PictureBox para la Imagen del Plato
                    PictureBox picPlato = new PictureBox();
                    picPlato.Size = new Size(anchoCard, 135);
                    picPlato.Dock = DockStyle.Top;
                    picPlato.SizeMode = PictureBoxSizeMode.Zoom;
                    picPlato.BackColor = Color.FromArgb(241, 245, 249);

                    // --- LÓGICA DE CARGA DE IMÁGENES SEGURO (Mismo criterio de FrmVenta) ---
                    try
                    {
                        var baseDir = Path.Combine(Application.StartupPath, "ImagesProductos");
                        string[] extensiones = { ".jpg", ".png", ".jpeg" };

                        if (!Directory.Exists(baseDir)) Directory.CreateDirectory(baseDir);

                        string rutaLocal = null;

                        // 1) Buscar por ID
                        rutaLocal = extensiones
                            .Select(ext => Path.Combine(baseDir, prod.id.ToString() + ext))
                            .FirstOrDefault(File.Exists);

                        // 2) Buscar por campo URL/Ruta
                        if (rutaLocal == null && !string.IsNullOrEmpty(prod.imagenUrl))
                        {
                            if (Uri.IsWellFormedUriString(prod.imagenUrl, UriKind.Absolute))
                            {
                                picPlato.LoadAsync(prod.imagenUrl);
                            }
                            else
                            {
                                string rutaCombinada = Path.Combine(baseDir, prod.imagenUrl);
                                if (File.Exists(rutaCombinada)) rutaLocal = rutaCombinada;
                                else if (File.Exists(prod.imagenUrl)) rutaLocal = prod.imagenUrl;
                            }
                        }

                        // 3) Imagen por defecto
                        if (rutaLocal == null && picPlato.Image == null)
                        {
                            rutaLocal = extensiones
                                .Select(ext => Path.Combine(baseDir, "default" + ext))
                                .FirstOrDefault(File.Exists);
                        }

                        if (rutaLocal != null)
                        {
                            using (var tmp = Image.FromFile(rutaLocal))
                            {
                                picPlato.Image = new Bitmap(tmp);
                            }
                            picPlato.BackColor = Color.White;
                        }
                    }
                    catch (Exception imgEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error cargando imagen: {imgEx.Message}");
                    }

                    // 3. Label para el Nombre y cantidad
                    Label lblNombre = new Label();
                    lblNombre.Text = $"{prod.nombre}\n({Convert.ToInt32(prod.totalVendido)} und.)";
                    lblNombre.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    lblNombre.ForeColor = Color.FromArgb(30, 41, 59);
                    lblNombre.Location = new Point(12, 148);
                    lblNombre.Size = new Size(anchoCard - 24, 38);
                    lblNombre.AutoSize = false;

                    // 4. Label de Estado "Disponible"
                    Label lblEstado = new Label();
                    lblEstado.Text = "Disponible";
                    lblEstado.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                    lblEstado.ForeColor = Color.FromArgb(22, 163, 74);
                    lblEstado.BackColor = Color.FromArgb(220, 252, 231);
                    lblEstado.Size = new Size(80, 22);
                    lblEstado.TextAlign = ContentAlignment.MiddleCenter;
                    lblEstado.Location = new Point(12, 198);

                    // Estructuración de la tarjeta
                    card.Controls.Add(lblEstado);
                    card.Controls.Add(lblNombre);
                    card.Controls.Add(picPlato);

                    // Borde sutil moderno
                    card.Paint += (s, e) => {
                        ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                            Color.FromArgb(226, 232, 240), ButtonBorderStyle.Solid);
                    };

                    flpContenedor.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Dashboard: {ex.Message}");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblReloj.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
    }
}
