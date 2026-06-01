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
    public partial class FrmProducto : Form
    {
        // Variables de estado globales del formulario
        private bool modoEdicion = false;
        private int _idProductoEditando = 0;
        private System.Threading.Timer searchTimer;
        private const int SearchDelay = 500;
        private string _rutaImagenSeleccionada = null;
        private bool _quitarImagen = false;
        private string _rutaImagenActual = null;
        public FrmProducto()
        {
            InitializeComponent();
            pnlAgregar.Visible = false;
            txtBuscar.TextChanged += txtBuscar_TextChanged_1;
        }
        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            searchTimer?.Dispose();

            searchTimer = new System.Threading.Timer(_ =>
            {
                if (!this.IsDisposed && this.IsHandleCreated)
                {
                    BeginInvoke((MethodInvoker)delegate
                    {
                        listar();
                    });
                }
            }, null, SearchDelay, System.Threading.Timeout.Infinite);
        }
        private void FrmProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            searchTimer?.Dispose();
        }
        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.ForeColor = Color.Black;
        }
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }
        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.ForeColor = Color.DimGray;
            }
        }
        private void listar()
        {
            var lista = ProductoCln.listarPa(txtBuscar.Text.Trim());
            dgvProductos.DataSource = lista;
            dgvProductos.Columns["id"].Visible = false;
            dgvProductos.Columns["idCategoria"].Visible = false;
            dgvProductos.Columns["estado"].Visible = false;
            dgvProductos.Columns["codigo"].HeaderText = "Código";
            dgvProductos.Columns["nombre"].HeaderText = "Nombre";
            dgvProductos.Columns["descripcion"].HeaderText = "Descripción";
            dgvProductos.Columns["categoria"].HeaderText = "Nombre de Categoría";
            dgvProductos.Columns["stock"].HeaderText = "Stock";
            dgvProductos.Columns["stock"].DefaultCellStyle.Format = "N0";
            dgvProductos.Columns["precioVenta"].HeaderText = "Precio de Venta";
            dgvProductos.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvProductos.Columns["fechaRegistro"].HeaderText = "Fecha Registro";

            if (lista.Count > 0) dgvProductos.CurrentCell = dgvProductos.Rows[0].Cells["codigo"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }
        private void cargarCategorias()
        {
            var categorias = CategoriaCln.listar();
            cbxCategoria.DataSource = categorias;
            cbxCategoria.ValueMember = "id";
            cbxCategoria.DisplayMember = "nombre";
        }
        private void limpiar()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            nudStock.Value = 0;
            nudPrecioVenta.Value = 0;
            cbxCategoria.SelectedIndex = -1;

            _rutaImagenSeleccionada = null;
            _quitarImagen = false;
            _rutaImagenActual = null;
            LimpiarImagen();

            if (lblImagenInfo != null) lblImagenInfo.Text = "(Sin imagen)";

            // Se reestablecen los estados de edición de forma segura
            modoEdicion = false;
            _idProductoEditando = 0;
        }

        private void mostrarPanelAgregar()
        {
            pnlAgregar.Visible = true;
            pnlAgregar.BringToFront();

            dgvProductos.Enabled = false;
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnAgregarCategoria.Enabled = false;
            txtBuscar.Enabled = false;
        }
        private void ocultarPanelAgregar()
        {
            pnlAgregar.Visible = false;

            dgvProductos.Enabled = true;
            btnAgregar.Enabled = true;
            btnEditar.Enabled = dgvProductos.Rows.Count > 0;
            btnEliminar.Enabled = dgvProductos.Rows.Count > 0;
            btnAgregarCategoria.Enabled = true;
            txtBuscar.Enabled = true;

            txtBuscar.Focus();
        }
        private bool validar(int idProductoActual = 0)
        {
            bool esValido = true;
            erpCodigo.SetError(txtCodigo, "");
            erpNombre.SetError(txtNombre, "");
            erpDescripcion.SetError(txtDescripcion, "");
            erpCategoria.SetError(cbxCategoria, "");
            erpStock.SetError(nudStock, "");
            erpPrecioVenta.SetError(nudPrecioVenta, "");

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                erpCodigo.SetError(txtCodigo, "El campo Código es obligatorio");
                esValido = false;
            }

            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                string codigoInsertado = txtCodigo.Text.Trim();
                if (ProductoCln.existeCodigo(codigoInsertado, idProductoActual))
                {
                    erpCodigo.SetError(txtCodigo, "Este código de producto ya se encuentra registrado");
                    esValido = false;
                }
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                erpNombre.SetError(txtNombre, "El campo Nombre es obligatorio");
                esValido = false;
            }

            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                string nombreInsertado = txtNombre.Text.Trim();
                if (ProductoCln.existeNombre(nombreInsertado, idProductoActual))
                {
                    erpNombre.SetError(txtNombre, "Este nombre de producto ya se encuentra registrado");
                    esValido = false;
                }
            }

            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                erpDescripcion.SetError(txtDescripcion, "El campo Descripción es obligatorio");
                esValido = false;
            }

            if (cbxCategoria.SelectedIndex == -1)
            {
                erpCategoria.SetError(cbxCategoria, "El campo Categoría es obligatorio");
                esValido = false;
            }

            if (nudStock.Value < 0)
            {
                erpStock.SetError(nudStock, "El campo Stock no puede ser menor a 0");
                esValido = false;
            }

            if (nudPrecioVenta.Value <= 0)
            {
                erpPrecioVenta.SetError(nudPrecioVenta, "El campo Precio de Venta debe ser mayor a 0");
                esValido = false;
            }
            return esValido;
        }
        private void FrmProductos_Load(object sender, EventArgs e)
        {
            nudStock.DecimalPlaces = 0;
            nudStock.Increment = 1;

            cargarCategorias();
            listar();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modoEdicion = false;
            _idProductoEditando = 0;
            cargarCategorias();
            limpiar();
            mostrarPanelAgregar();
            txtCodigo.Focus();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentCell == null) return;

            cargarCategorias();

            int index = dgvProductos.CurrentCell.RowIndex;
            // Se resguarda el ID real en la variable global fija antes de que cambie el grid
            _idProductoEditando = Convert.ToInt32(dgvProductos.Rows[index].Cells["id"].Value);

            var producto = ProductoCln.obtenerUno(_idProductoEditando);

            txtCodigo.Text = producto.codigo;
            txtNombre.Text = producto.nombre;
            txtDescripcion.Text = producto.descripcion;
            cbxCategoria.SelectedValue = producto.idCategoria;
            nudStock.Value = producto.stock;
            nudPrecioVenta.Value = producto.precioVenta;

            modoEdicion = true;

            mostrarPanelAgregar();
            txtCodigo.Focus();

            _quitarImagen = false;
            _rutaImagenSeleccionada = null;
            CargarImagenProductoEnPreview(producto);
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Enviamos el ID recuperado en la edición, o 0 si es un nuevo producto
            int idActual = modoEdicion ? _idProductoEditando : 0;

            if (validar(idActual))
            {
                var producto = new Producto();
                producto.codigo = txtCodigo.Text.Trim();
                producto.nombre = txtNombre.Text.Trim();
                producto.descripcion = txtDescripcion.Text.Trim();
                producto.idCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
                producto.stock = Convert.ToInt32(nudStock.Value);
                producto.precioVenta = nudPrecioVenta.Value;
                producto.usuarioRegistro = Util.usuario.usuario1;

                string baseDir = Path.Combine(Application.StartupPath, "ImagesProductos");
                Directory.CreateDirectory(baseDir);

                if (!modoEdicion)
                {
                    producto.fechaRegistro = DateTime.Now;
                    producto.estado = 1;
                    int nuevoId = ProductoCln.crear(producto);

                    if (!string.IsNullOrWhiteSpace(_rutaImagenSeleccionada))
                    {
                        ReemplazarImagenProducto(nuevoId, producto.codigo, baseDir, _rutaImagenSeleccionada);
                    }
                }
                else
                {
                    var productoExistente = ProductoCln.obtenerUno(_idProductoEditando);
                    productoExistente.codigo = producto.codigo;
                    productoExistente.nombre = producto.nombre;
                    productoExistente.descripcion = producto.descripcion;
                    productoExistente.idCategoria = producto.idCategoria;
                    productoExistente.stock = producto.stock;
                    productoExistente.precioVenta = producto.precioVenta;
                    productoExistente.usuarioRegistro = Util.usuario.usuario1;

                    ProductoCln.actualizar(productoExistente);

                    if (_quitarImagen)
                    {
                        EliminarImagenesProducto(_idProductoEditando, productoExistente.codigo, baseDir);
                    }
                    else if (!string.IsNullOrWhiteSpace(_rutaImagenSeleccionada))
                    {
                        ReemplazarImagenProducto(_idProductoEditando, productoExistente.codigo, baseDir, _rutaImagenSeleccionada);
                    }
                }

                ocultarPanelAgregar();
                limpiar();
                listar();
                MessageBox.Show("Producto guardado correctamente", "::: Restaurant :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ocultarPanelAgregar();
            limpiar();
        }
        private void btnCerrarAgregar_Click(object sender, EventArgs e)
        {
            ocultarPanelAgregar();
            limpiar();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentCell == null) return;

            int index = dgvProductos.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvProductos.Rows[index].Cells["id"].Value);
            string nombre = dgvProductos.Rows[index].Cells["nombre"].Value.ToString();

            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar el Producto {nombre}?",
                "::: Restaurant - Mensaje :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                ProductoCln.eliminar(id, Util.usuario.usuario1);
                listar();
                MessageBox.Show("El Producto se ha eliminado correctamente", "::: Restaurant - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            new FrmCategoria().ShowDialog();
        }
        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            if (ofdImagen == null) return;

            ofdImagen.Filter = "Imágenes|*.jpg;*.jpeg;*.png";
            ofdImagen.Title = "Seleccionar imagen del producto";
            if (ofdImagen.ShowDialog() == DialogResult.OK)
            {
                _rutaImagenSeleccionada = ofdImagen.FileName;
                _quitarImagen = false;
                MostrarImagenPreview(_rutaImagenSeleccionada);
                if (lblImagenInfo != null)
                    lblImagenInfo.Text = Path.GetFileName(_rutaImagenSeleccionada);
            }
        }
        private void btnQuitarImagen_Click(object sender, EventArgs e)
        {
            _rutaImagenSeleccionada = null;
            _quitarImagen = true;
            _rutaImagenActual = null;
            LimpiarImagen();
            if (lblImagenInfo != null) lblImagenInfo.Text = "(Sin imagen)";
        }
        private void LimpiarImagen()
        {
            var img = pbImagenProducto.Image;
            pbImagenProducto.Image = null;
            if (img != null) img.Dispose();
            pbImagenProducto.BackColor = Color.Gainsboro;
        }
        private void MostrarImagenPreview(string ruta)
        {
            try
            {
                if (pbImagenProducto == null || string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta)) return;

                using (var tmp = Image.FromFile(ruta))
                {
                    pbImagenProducto.Image = new Bitmap(tmp);
                    pbImagenProducto.BackColor = Color.White;
                }
            }
            catch { }
        }
        private void CargarImagenProductoEnPreview(Producto p)
        {
            LimpiarImagen();

            try
            {
                string baseDir = Path.Combine(Application.StartupPath, "ImagesProductos");
                string[] extensiones = { ".jpg", ".png", ".jpeg" };

                string ruta = extensiones
                    .Select(ext => Path.Combine(baseDir, p.id.ToString() + ext))
                    .FirstOrDefault(File.Exists);

                if (ruta == null && !string.IsNullOrWhiteSpace(p.codigo))
                {
                    ruta = extensiones
                        .Select(ext => Path.Combine(baseDir, p.codigo + ext))
                        .FirstOrDefault(File.Exists);
                }

                _rutaImagenActual = ruta;
                if (ruta != null)
                {
                    MostrarImagenPreview(ruta);
                    if (lblImagenInfo != null)
                        lblImagenInfo.Text = Path.GetFileName(ruta);
                }
                else
                {
                    if (lblImagenInfo != null) lblImagenInfo.Text = "(Sin imagen)";
                }
            }
            catch { }
        }
        private void ReemplazarImagenProducto(int idProducto, string codigo, string baseDir, string rutaNueva)
        {
            if (string.IsNullOrWhiteSpace(rutaNueva) || !File.Exists(rutaNueva)) return;

            EliminarImagenesProducto(idProducto, codigo, baseDir);

            string ext = Path.GetExtension(rutaNueva).ToLowerInvariant();
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png") return;

            string destinoId = Path.Combine(baseDir, idProducto + ext);
            if (!string.Equals(Path.GetFullPath(rutaNueva), Path.GetFullPath(destinoId), StringComparison.OrdinalIgnoreCase))
            {
                File.Copy(rutaNueva, destinoId, true);
            }
        }
        private void EliminarImagenesProducto(int idProducto, string codigo, string baseDir)
        {
            try
            {
                string[] exts = { ".jpg", ".jpeg", ".png" };

                foreach (var ext in exts)
                {
                    var path = Path.Combine(baseDir, idProducto + ext);
                    if (File.Exists(path))
                    {
                        try { File.Delete(path); } catch { }
                    }
                }

                if (!string.IsNullOrWhiteSpace(codigo))
                {
                    foreach (var ext in exts)
                    {
                        var path = Path.Combine(baseDir, codigo + ext);
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); } catch { }
                        }
                    }
                }
            }
            catch { }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            ocultarPanelAgregar();
            limpiar();
        }
    }
}