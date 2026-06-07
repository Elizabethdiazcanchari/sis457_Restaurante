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

            txtCiNit.KeyPress += txtCiNit_KeyPress;

            if (flpCatalogoProductos != null)
            {
                ActivarDoubleBuffer(flpCatalogoProductos);
            }

            txtBuscar.KeyPress += txtBuscar_KeyPress;

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

        private void btnAgregarVenta_Click(object sender, EventArgs e)
        {
            var frm = new FrmSoporte();
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
                idUsuario = Util.usuario.id,
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
            txtBuscar.Clear();
            detalles.Clear();
            RefrescarDetalle();
            txtEfectivo.Clear();
            txtCambio.Clear();

            txtCiNit.Focus();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            clienteSeleccionado = null;
            ConstruirCatalogoProductos();
        }

        private void ConstruirCatalogoProductos(string filtro = "")
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

            //Buscar por nombre del producto (si se ingresó un filtro)
            if (productos != null && !string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.ToLower().Trim();
                productos = productos
                    .Where(p => p.nombre != null && p.nombre.ToLower().Contains(filtro))
                    .ToList();
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
            int columnas = 3;
            int margenTotalPanel = 25;
            int margenEntreCards = 16;

            int anchoCard = (flpCatalogoProductos.Width - margenTotalPanel) / columnas - margenEntreCards;

            // Determinar si el producto tiene stock disponible
            bool tieneStock = p.stock > 0;

            var panel = new Panel
            {
                Width = anchoCard,
                Height = 235,
                // Si no tiene stock, se pinta un fondo gris sutil de "deshabilitado"
                BackColor = tieneStock ? Color.White : Color.FromArgb(243, 244, 246),
                Margin = new Padding(8),
                BorderStyle = BorderStyle.None,
                Tag = p.id
            };

            var pb = new PictureBox
            {
                Width = anchoCard - 20,
                Height = 110,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = tieneStock ? Color.FromArgb(243, 244, 246) : Color.FromArgb(229, 231, 235)
            };
            CargarImagenProducto(p, pb);

            var lblNombre = new Label
            {
                Text = p.nombre,
                Location = new Point(10, 128),
                Width = anchoCard - 20,
                Height = 32,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                // Texto más claro si está agotado
                ForeColor = tieneStock ? Color.FromArgb(31, 41, 55) : Color.DarkGray,
                AutoSize = false
            };

            var lblStock = new Label
            {
                Text = tieneStock ? $"Stock: {p.stock:0.00}" : "AGOTADO",
                Location = new Point(10, 162),
                Width = anchoCard - 20,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                // Rojo si está agotado, gris si tiene stock
                ForeColor = tieneStock ? Color.FromArgb(107, 114, 128) : Color.FromArgb(239, 68, 68),
                AutoSize = false
            };

            int maxCantidad = (int)Math.Max(1, System.Convert.ToDouble(p.stock));
            var nudCantidad = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 999,
                Value = 1,
                Width = 52,
                Location = new Point(10, 188),
                Font = new Font("Segoe UI", 9),
                Tag = p.id,
                Enabled = tieneStock // Deshabilitar control de cantidad si no hay stock
            };

            // Validar mediante evento si el usuario escribe un número fuera de rango manualmente
            /*nudCantidad.Validating += (s, ev) =>
            {
                NumericUpDown currentNud = s as NumericUpDown;
                if (currentNud != null)
                {
                    if (currentNud.Value < currentNud.Minimum || currentNud.Value > currentNud.Maximum)
                    {
                        MessageBox.Show($"La cantidad debe estar entre {currentNud.Minimum} y {currentNud.Maximum} (Stock disponible).",
                                        "Cantidad fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        currentNud.Value = currentNud.Minimum; // Reestablece al mínimo seguro
                    }
                }
            };*/

            var btnAgregar = new Button
            {
                Text = tieneStock ? "Agregar" : "Sin Stock",
                Width = anchoCard - 82,
                Height = 28,
                Location = new Point(72, 186),
                // Si no tiene stock se vuelve gris, si tiene stock usa el azul corporativo
                BackColor = tieneStock ? Color.FromArgb(37, 99, 235) : Color.FromArgb(156, 163, 175),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Tag = new Tuple<int, NumericUpDown>(p.id, nudCantidad),
                Enabled = tieneStock // Bloqueado si el stock es cero
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

                // Validar que la carpeta de imágenes exista, si no, crearla
                if (!Directory.Exists(baseDir)) Directory.CreateDirectory(baseDir);

                // 1) Buscar por ID 
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

                // 3) Si no se encontró ninguna, buscar la imagen por defecto
                if (ruta == null)
                {
                    ruta = extensiones
                        .Select(ext => Path.Combine(baseDir, "default" + ext))
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
                    MessageBox.Show("Producto no encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (producto.stock <= 0)
                {
                    MessageBox.Show("Este producto se encuentra agotado y no puede ser añadido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Alerta si la cantidad seleccionada en la tarjeta supera el stock físico real
                if (cantidad > Convert.ToInt32(producto.stock))
                {
                    MessageBox.Show($"La cantidad solicitada ({cantidad}) no está disponible.\n" +
                                    $"El stock actual de '{producto.nombre}' es de {Convert.ToInt32(producto.stock)} unidades.",
                                    "Cantidad No Disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    nud.Value = Convert.ToInt32(producto.stock);

                    return; 
                }

                // 2. Alerta considerando lo que ya metió previamente a la tabla (carrito)
                var existente = detalles.FirstOrDefault(d => d.idProducto == producto.id);
                if (existente != null)
                {
                    double nuevaCantidadAcumulada = (double)existente.cantidad + (double)cantidad;
                    if (nuevaCantidadAcumulada > (double)producto.stock)
                    {
                        int saldoPermitido = Convert.ToInt32(producto.stock) - Convert.ToInt32(existente.cantidad);

                        if (saldoPermitido <= 0)
                        {
                            MessageBox.Show($"No puedes agregar más unidades. Ya tienes todas las existencias disponibles ({Convert.ToInt32(existente.cantidad)}) añadidas al carrito.",
                                            "Stock Agotado en Carrito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            nud.Value = 1;
                        }
                        else
                        {
                            MessageBox.Show($"Cantidad no disponible. Ya tienes {Convert.ToInt32(existente.cantidad)} unidades en el carrito.\n" +
                                            $"Solo te quedan {saldoPermitido} unidades permitidas para agregar.",
                                            "Cantidad No Disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            nud.Value = saldoPermitido;
                        }

                        return; 
                    }
                }

                // Si pasó ambos filtros limpiecito, recién procesa el agregado real
                AgregarDetalleProducto(producto, cantidad);
                nud.Value = 1; // Resetea el control a 1 para el siguiente pedido limpio
            }
        }

        private void AgregarDetalleProducto(Producto producto, int cantidad)
        {
            var existente = detalles.FirstOrDefault(d => d.idProducto == producto.id);
            if (existente != null)
            {
                // Como las validaciones ya se hicieron arriba en el botón "Click", aquí entra directo y seguro
                double nuevaCantidad = (double)existente.cantidad + (double)cantidad;
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

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Elimina el sonido 'beep' molesto de Windows
                string criterio = txtBuscar.Text;
                ConstruirCatalogoProductos(criterio);
            }
        }

        private void txtCiNit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Evita el sonido de alerta por defecto de Windows
                BuscarClientePorNitCi();
            }
        }

        private void BuscarClientePorNitCi()
        {
            string ciNit = txtCiNit.Text.Trim();

            if (string.IsNullOrWhiteSpace(ciNit))
            {
                MessageBox.Show("Por favor, ingrese un CI o NIT para buscar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Aquí lo filtramos usando Linq desde el listado general:
                var cliente = ClienteCln.listar()
                    .FirstOrDefault(c => c.ciNit == ciNit && c.estado != -1);

                if (cliente != null)
                {
                    clienteSeleccionado = cliente;
                    txtRazonSocial.Text = cliente.razonSocial;

                    // Opcional: Enfocar directamente el catálogo o el efectivo si el cliente ya existe
                    txtEfectivo.Focus();
                }
                else
                {
                    // lo detecte correctamente como cliente provisional / nuevo.
                    clienteSeleccionado = null;
                    txtRazonSocial.Clear();

                    MessageBox.Show("Cliente no encontrado. Proceda a registrar la Razón Social de forma manual.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtRazonSocial.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActivarDoubleBuffer(Control control)
        {
            System.Reflection.PropertyInfo property = typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
            );
            property?.SetValue(control, true, null);
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarClientePorNitCi();
        }

        private void btnRecargarCatalogoProducto_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            ConstruirCatalogoProductos();
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim();
            ConstruirCatalogoProductos(criterio);
        }
    }
}
