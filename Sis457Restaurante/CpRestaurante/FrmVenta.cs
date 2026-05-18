using CadRestaurante;
using ClnRestaurante;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpRestaurante
{
    public partial class FrmVenta : Form
    {
        private List<DetalleVenta> detalles = new List<DetalleVenta>();
        private Cliente clienteSeleccionado = null;
        public FrmVenta()
        {
            InitializeComponent();
            this.Load += FrmVenta_Load;
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            // Propiedades base del formulario (Línea clara corporativa)
            this.BackColor = Color.FromArgb(243, 244, 246); // Gris claro limpio

            dtpFecha.Value = DateTime.Now;
            txtCiNit.ReadOnly = false;
            txtRazonSocial.ReadOnly = false;
            txtTotal.ReadOnly = true;
            txtCambio.ReadOnly = true;

            dgvDetalleVenta.AutoGenerateColumns = false;
            ConfigurarDgvDetalle();
            LimpiarFormulario();
            ConstruirCatalogoProductos();

            // Eventos adicionales
            btnActualizar.Click += btnActualizar_Click;
            // recalcular cambio cuando cambie el efectivo
            txtEfectivo.TextChanged += (s, ev) => CalcularCambio();

            // Enlazar evento para capturar la eliminación de productos de la grilla
            dgvDetalleVenta.CellContentClick += dgvDetalleVenta_CellContentClick;
        }

        private void ConfigurarDgvDetalle()
        {
            dgvDetalleVenta.Columns.Clear();

            dgvDetalleVenta.AllowUserToAddRows = false;    // Evita la fila vacía al final que altera los índices
            dgvDetalleVenta.AllowUserToDeleteRows = false; // El borrado lo controlamos solo por código
            dgvDetalleVenta.ReadOnly = true;

            // --- ESTILO PROFESIONAL MODO CLARO ---
            dgvDetalleVenta.BackgroundColor = Color.FromArgb(249, 250, 251); // Fondo gris tierno
            dgvDetalleVenta.BorderStyle = BorderStyle.None;
            dgvDetalleVenta.RowHeadersVisible = false; // Remueve holgura izquierda
            dgvDetalleVenta.GridColor = Color.FromArgb(209, 213, 219); // Líneas sutiles
            dgvDetalleVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDetalleVenta.EnableHeadersVisualStyles = false;
            dgvDetalleVenta.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 65, 81); // Gris pizarra
            dgvDetalleVenta.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetalleVenta.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvDetalleVenta.ColumnHeadersHeight = 32;

            var colNombre = new DataGridViewTextBoxColumn
            {
                Name = "nombreProducto",
                HeaderText = "Producto",
                DataPropertyName = "nombreProducto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            var colCantidad = new DataGridViewTextBoxColumn
            {
                Name = "cantidad",
                HeaderText = "Cant.",
                DataPropertyName = "cantidad",
                Width = 55,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            var colPrecio = new DataGridViewTextBoxColumn
            {
                Name = "precioUnitario",
                HeaderText = "P. Unit",
                DataPropertyName = "precioUnitario",
                Width = 80,
                DefaultCellStyle = { Format = "0.00", Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            var colTotal = new DataGridViewTextBoxColumn
            {
                Name = "total",
                HeaderText = "Total",
                DataPropertyName = "total",
                Width = 85,
                DefaultCellStyle = { Format = "0.00", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 9, FontStyle.Bold) }
            };

            // Columna con botón dinámico para remover ítems del carrito
            var colEliminar = new DataGridViewButtonColumn
            {
                Name = "btnEliminar",
                HeaderText = "",
                Text = "×",
                UseColumnTextForButtonValue = true,
                Width = 35,
                DefaultCellStyle = {
                    ForeColor = Color.FromArgb(239, 68, 68), // Rojo peligro
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };

            dgvDetalleVenta.Columns.AddRange(colNombre, colCantidad, colPrecio, colTotal, colEliminar);
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string nit = txtCiNit.Text.Trim();
            clienteSeleccionado = ClienteCln.listar().FirstOrDefault(c => c.ciNit == nit);
            if (clienteSeleccionado != null)
                txtRazonSocial.Text = clienteSeleccionado.razonSocial;
            else
            {
                txtRazonSocial.Clear();
                MessageBox.Show("Cliente no encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAgregarVenta_Click(object sender, EventArgs e)
        {
            var frm = new FrmSeleccionarProducto();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var detalle = frm.DetalleSeleccionado;
                if (detalle != null)
                {
                    var existente = detalles.FirstOrDefault(d => d.idProducto == detalle.idProducto);
                    if (existente != null)
                    {
                        var producto = ProductoCln.obtenerUno(existente.idProducto);
                        double nuevaCantidad = (double)existente.cantidad + (double)detalle.cantidad;
                        if (nuevaCantidad > (double)producto.stock)
                        {
                            MessageBox.Show("La suma excede el stock disponible.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        existente.cantidad = (decimal)nuevaCantidad;
                        existente.total = existente.cantidad * existente.precioUnitario;
                    }
                    else
                    {
                        detalles.Add(detalle);
                    }
                    RefrescarDetalle();
                }
            }
        }
        private void RefrescarDetalle()
        {
            dgvDetalleVenta.DataSource = null;
            dgvDetalleVenta.Rows.Clear();
            dgvDetalleVenta.DataSource = detalles.Select(d => new
            {
                nombreProducto = d.Producto != null ? d.Producto.nombre : ProductoCln.obtenerUno(d.idProducto).nombre,
                cantidad = d.cantidad,
                precioUnitario = d.precioUnitario,
                total = d.total
            }).ToList();

            decimal sumaTotal = (decimal)detalles.Sum(d => d.total);
            txtTotal.Text = sumaTotal.ToString("0.00");
            CalcularCambio();
        }

        private void txtCambio_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
        }
        private void CalcularCambio()
        {
            decimal total = 0, efectivo = 0;
            decimal.TryParse(txtTotal.Text, out total);
            decimal.TryParse(txtEfectivo.Text, out efectivo);

            decimal cambio = efectivo - total;
            txtCambio.Text = cambio.ToString("0.00");

            // Feedback visual dinámico: si cubre el monto se pone verde tierno, sino negro
            txtCambio.ForeColor = cambio >= 0 && efectivo > 0 ? Color.FromArgb(16, 185, 129) : Color.Black;
        }

        // Evento que intercepta el click en la "×" de la fila para eliminar productos
        private void dgvDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Verificar que se hizo clic en la columna del botón eliminar y que no sea la cabecera
            if (dgvDetalleVenta.Columns.Contains("btnEliminar") &&
                e.ColumnIndex == dgvDetalleVenta.Columns["btnEliminar"].Index &&
                e.RowIndex >= 0)
            {
                // 2. Controlar que el índice visual sea válido para la lista de detalles
                if (e.RowIndex < detalles.Count)
                {
                    // 3. Remover el elemento de la lista real usando el índice de la fila
                    detalles.RemoveAt(e.RowIndex);

                    // 4. Refrescar la interfaz para recalcular totales y volver a pintar la tabla
                    RefrescarDetalle();
                }
            }
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            string ciNit = txtCiNit.Text.Trim();
            string razonSocial = txtRazonSocial.Text.Trim();

            if (string.IsNullOrWhiteSpace(ciNit) || string.IsNullOrWhiteSpace(razonSocial))
            {
                MessageBox.Show("Debe completar los campos del Cliente (CI/NIT y Razón Social).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (detalles.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal total = (decimal)detalles.Sum(d => d.total);
            decimal efectivo = 0;
            decimal.TryParse(txtEfectivo.Text, out efectivo);
            if (efectivo < total)
            {
                MessageBox.Show("El efectivo no puede ser menor al total.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si el cliente no fue buscado/seleccionado previamente, armamos un objeto Cliente provisional
            Cliente clienteNuevo = null;
            if (clienteSeleccionado == null)
            {
                clienteNuevo = new Cliente
                {
                    ciNit = ciNit,
                    razonSocial = razonSocial,
                    usuarioRegistro = Util.usuario.usuario1,
                    fechaRegistro = DateTime.Now,
                    estado = 1
                };
            }

            var venta = new Venta
            {
                idCliente = clienteSeleccionado != null ? clienteSeleccionado.id : 0,
                idEmpleado = Util.usuario.id,
                usuarioRegistro = Util.usuario.usuario1,
                fechaRegistro = DateTime.Now,
                estado = 1
            };

            try
            {
                long idVenta = VentaCln.crearConDetallesYCliente(venta, detalles, clienteNuevo);
                MessageBox.Show("Venta registrada correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                clienteSeleccionado = null;
                ConstruirCatalogoProductos(); // refresca el stock mostrado
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtCiNit.Clear();
            txtRazonSocial.Clear();
            detalles.Clear();
            RefrescarDetalle();
            txtEfectivo.Clear();
            txtCambio.Clear();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            clienteSeleccionado = null;
            ConstruirCatalogoProductos();
        }

        private void ConstruirCatalogoProductos()
        {
            if (flpCatalogoProductos == null) return;

            flpCatalogoProductos.SuspendLayout();
            flpCatalogoProductos.Controls.Clear();
            flpCatalogoProductos.BackColor = Color.FromArgb(229, 231, 235); // Enmarca las tarjetas con un gris de contraste

            // 1) Intento principal via EF
            var productos = ProductoCln.listar();

            // 2) Fallback: si no hay productos, intento vía SP y mapeo
            if (productos == null || productos.Count == 0)
            {
                var listaPa = ProductoCln.listarPa("");
                if (listaPa != null && listaPa.Count > 0)
                {
                    productos = listaPa
                        .Select(x => ProductoCln.obtenerUno(x.id))
                        .Where(x => x != null && x.estado != -1)
                        .ToList();
                }
            }

            if (productos != null && productos.Count > 0)
            {
                foreach (var p in productos.OrderBy(p => p.nombre))
                {
                    var card = CrearCardProducto(p);
                    flpCatalogoProductos.Controls.Add(card);
                }
            }
            else
            {
                var lbl = new Label
                {
                    Text = "No hay productos activos para mostrar.",
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    ForeColor = Color.DimGray
                };
                flpCatalogoProductos.Controls.Add(lbl);
            }

            flpCatalogoProductos.ResumeLayout();
        }

        private Control CrearCardProducto(Producto p)
        {
            // --- TARJETA DE PRODUCTO ESTILIZADA ---
            var panel = new Panel
            {
                Width = 165,
                Height = 235,
                BackColor = Color.White, // Fondo limpio para la card
                Margin = new Padding(8),
                BorderStyle = BorderStyle.None, // Quitamos el borde tosco antiguo
                Tag = p.id
            };

            var pb = new PictureBox
            {
                Width = 145,
                Height = 110,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(243, 244, 246)
            };
            CargarImagenProducto(p, pb);

            var lblNombre = new Label
            {
                Text = p.nombre,
                Location = new Point(10, 128),
                Width = 145,
                Height = 32, // Altura suficiente para dos líneas de texto
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                AutoSize = false
            };

            var lblStock = new Label
            {
                Text = $"Stock: {p.stock:0.00}",
                Location = new Point(10, 162),
                Width = 145,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(107, 114, 128),
                AutoSize = false
            };

            int maxCantidad = (int)Math.Max(1, Math.Min((double)p.stock, int.MaxValue));
            var nudCantidad = new NumericUpDown
            {
                Minimum = 1,
                Maximum = maxCantidad,
                Value = 1,
                Width = 52,
                Location = new Point(10, 188),
                Font = new Font("Segoe UI", 9),
                Tag = p.id
            };

            // Botón "Agregar" rediseñado en Azul Corporativo Acción
            var btnAgregar = new Button
            {
                Text = "Agregar",
                Width = 83,
                Height = 28,
                Location = new Point(72, 186),
                BackColor = Color.FromArgb(37, 99, 235), // Azul moderno
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Tag = new Tuple<int, NumericUpDown>(p.id, nudCantidad),
                Enabled = p.stock > 0
            };
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.Click += BtnAgregarProductoCatalogo_Click;

            panel.Controls.Add(pb);
            panel.Controls.Add(lblNombre);
            panel.Controls.Add(lblStock);
            panel.Controls.Add(nudCantidad);
            panel.Controls.Add(btnAgregar);

            return panel;
        }

        private void CargarImagenProducto(Producto p, PictureBox pb)
        {
            try
            {
                var baseDir = Path.Combine(Application.StartupPath, "ImagesProductos");
                string[] extensiones = { ".jpg", ".png", ".jpeg" };

                // 1) Buscar por ID (recomendado)
                string ruta = extensiones
                    .Select(ext => Path.Combine(baseDir, p.id.ToString() + ext))
                    .FirstOrDefault(File.Exists);

                // 2) Respaldo: buscar por código si existe
                if (ruta == null && !string.IsNullOrWhiteSpace(p.codigo))
                {
                    ruta = extensiones
                        .Select(ext => Path.Combine(baseDir, p.codigo + ext))
                        .FirstOrDefault(File.Exists);
                }

                if (ruta != null)
                {
                    using (var tmp = Image.FromFile(ruta))
                    {
                        pb.Image = new Bitmap(tmp);
                    }
                    pb.BackColor = Color.White;
                }
                else
                {
                    // Sin imagen: dejamos el fondo gris
                    pb.Image = null;
                    pb.BackColor = Color.FromArgb(243, 244, 246);
                }
            }
            catch
            {
                // Evitar romper flujo si la imagen falla
            }
        }

        private void BtnAgregarProductoCatalogo_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null && btn.Tag is Tuple<int, NumericUpDown>)
            {
                var data = (Tuple<int, NumericUpDown>)btn.Tag;
                int idProducto = data.Item1;
                var nud = data.Item2;
                int cantidad = (int)nud.Value;

                var producto = ProductoCln.obtenerUno(idProducto);
                if (producto == null)
                {
                    MessageBox.Show("Producto no encontrado.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cantidad <= 0)
                {
                    MessageBox.Show("Cantidad inválida.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cantidad > (int)producto.stock)
                {
                    MessageBox.Show("Cantidad supera el stock disponible.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AgregarDetalleProducto(producto, cantidad);
                nud.Value = 1; // Resetea la cantidad seleccionada a 1 tras agregar con éxito
            }
        }

        private void AgregarDetalleProducto(Producto producto, int cantidad)
        {
            var existente = detalles.FirstOrDefault(d => d.idProducto == producto.id);
            if (existente != null)
            {
                double nuevaCantidad = (double)existente.cantidad + (double)cantidad;
                if (nuevaCantidad > (double)producto.stock)
                {
                    MessageBox.Show("La cantidad acumulada excede el stock disponible.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existente.cantidad = (decimal)(double)nuevaCantidad;
                existente.total = existente.cantidad * existente.precioUnitario;
            }
            else
            {
                detalles.Add(new DetalleVenta
                {
                    idProducto = producto.id,
                    cantidad = cantidad,
                    precioUnitario = producto.precioVenta,
                    total = cantidad * producto.precioVenta
                });
            }

            RefrescarDetalle();
        }
    }
}
