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
    public partial class FrmReporte : Form
    {
        private List<DetalleVenta> detalles = new List<DetalleVenta>();
        private Cliente clienteSeleccionado = null;
        public FrmReporte()
        {
            InitializeComponent();
            this.Load += FrmVentaDetalle_Load;
        }

        private void FrmVentaDetalle_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;
            txtNombreCliente.ReadOnly = true;
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
        }

        private void ConfigurarDgvDetalle()
        {
            dgvDetalleVenta.Columns.Clear();

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
                HeaderText = "Cantidad",
                DataPropertyName = "cantidad",
                Width = 80
            };
            var colPrecio = new DataGridViewTextBoxColumn
            {
                Name = "precioUnitario",
                HeaderText = "Precio Unitario",
                DataPropertyName = "precioUnitario",
                Width = 110,
                DefaultCellStyle = { Format = "0.00" }
            };
            var colTotal = new DataGridViewTextBoxColumn
            {
                Name = "total",
                HeaderText = "Total",
                DataPropertyName = "total",
                Width = 110,
                DefaultCellStyle = { Format = "0.00" }
            };

            dgvDetalleVenta.Columns.AddRange(colNombre, colCantidad, colPrecio, colTotal);
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string nit = txtCedulaCliente.Text.Trim();
            clienteSeleccionado = ClienteCln.listar().FirstOrDefault(c => c.nitId == nit);
            if (clienteSeleccionado != null)
                txtNombreCliente.Text = clienteSeleccionado.razonSocial;
            else
            {
                txtNombreCliente.Clear();
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
            dgvDetalleVenta.DataSource = detalles.Select(d => new
            {
                nombreProducto = d.Producto != null ? d.Producto.nombre : ProductoCln.obtenerUno(d.idProducto).nombre,
                cantidad = d.cantidad,
                precioUnitario = d.precioUnitario,
                total = d.total
            }).ToList();

            txtTotal.Text = detalles.Sum(d => d.total).ToString();
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
            txtCambio.Text = (efectivo - total).ToString("0.00");
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            if (clienteSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            var venta = new Venta
            {
                idCliente = clienteSeleccionado.id,
                idUsuario = Util.usuario.id,
                usuarioRegistro = Util.usuario.usuario1,
                fechaRegistro = DateTime.Now,
                estado = 1
            };

            try
            {
                long idVenta = VentaCln.crearConDetalles(venta, detalles);
                MessageBox.Show("Venta registrada correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                ConstruirCatalogoProductos(); // refresca el stock mostrado
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------
        // Implementación: catálogo visual con tarjetas y agregado directo
        // ------------------------
        private void LimpiarFormulario()
        {
            txtCedulaCliente.Clear();
            txtNombreCliente.Clear();
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
            var panel = new Panel
            {
                Width = 160,
                Height = 230,
                BackColor = Color.White,
                Margin = new Padding(8),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = p.id
            };

            var pb = new PictureBox
            {
                Width = 140,
                Height = 120,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Gainsboro // si no hay imagen queda el recuadro gris
            };
            CargarImagenProducto(p, pb);

            var lblNombre = new Label
            {
                Text = p.nombre,
                Location = new Point(10, 135),
                Width = 140,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = false
            };

            var lblStock = new Label
            {
                Text = "Stock: " + p.stock,
                Location = new Point(10, 155),
                Width = 140,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.DimGray,
                AutoSize = false
            };

            int maxCantidad = (int)Math.Max(1, Math.Min((double)p.stock, int.MaxValue));
            var nudCantidad = new NumericUpDown
            {
                Minimum = 1,
                Maximum = maxCantidad,
                Value = 1,
                Width = 60,
                Location = new Point(10, 180),
                Font = new Font("Segoe UI", 9),
                Tag = p.id
            };

            var btnAgregar = new Button
            {
                Text = "Agregar",
                Width = 70,
                Height = 26,
                Location = new Point(80, 180),
                BackColor = Color.SaddleBrown,
                ForeColor = Color.White,
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
                    pb.BackColor = Color.Gainsboro;
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
                    MessageBox.Show("La suma excede el stock.", "Aviso",
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
