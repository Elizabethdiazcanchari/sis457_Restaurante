using CadRestaurante;
using ClnRestaurante;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpRestaurante
{
    public partial class FrmCategoria : Form
    {
        // Variables de control de estado para alternar entre Agregar y Editar
        private bool esNuevo = true;
        private int idCategoriaSeleccionada = -1;

        public FrmCategoria()
        {
            InitializeComponent();
            this.Load += FrmCategoria_Load;
        }

        private void CargarCategoriasListBox()
        {
            var categorias = CategoriaCln.listar();
            lbxCategorias.DataSource = null;
            lbxCategorias.DataSource = categorias;
            lbxCategorias.ValueMember = "id";
            lbxCategorias.DisplayMember = "nombre";
        }

        private void RestablecerFormulario()
        {
            esNuevo = true;
            idCategoriaSeleccionada = -1;
            txtNombreCat.Clear();

            // Regresar el botón a su estado original de "Agregar"
            btnAgregarCate.Text = "Agregar Categoría";
            btnAgregarCate.BackColor = Color.FromArgb(37, 99, 235); // Azul acción
            erpNombreCategoria.Clear();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(243, 244, 246); // Gris claro limpio

            // Estilo para el ListBox
            lbxCategorias.BackColor = Color.White;
            lbxCategorias.Font = new Font("Segoe UI", 10);
            lbxCategorias.BorderStyle = BorderStyle.FixedSingle;

            // EL EVENTO CLAVE: Detecta cuando el usuario hace clic en un elemento de la lista
            lbxCategorias.SelectedIndexChanged += lbxCategorias_SelectedIndexChanged;

            // Ajustar estilos de los botones a la paleta moderna
            btnAgregarCate.FlatStyle = FlatStyle.Flat;
            btnAgregarCate.FlatAppearance.BorderSize = 0;
            btnAgregarCate.BackColor = Color.FromArgb(37, 99, 235); // Azul acción
            btnAgregarCate.ForeColor = Color.White;
            btnAgregarCate.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

            btnEliminarCate.FlatStyle = FlatStyle.Flat;
            btnEliminarCate.FlatAppearance.BorderSize = 0;
            btnEliminarCate.BackColor = Color.FromArgb(239, 68, 68); // Rojo peligro suave
            btnEliminarCate.ForeColor = Color.White;
            btnEliminarCate.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

            // Cargar datos iniciales
            CargarCategoriasListBox();
            RestablecerFormulario();
        }

        private void lbxCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificamos que realmente haya una selección válida
            if (lbxCategorias.SelectedIndex >= 0 && lbxCategorias.SelectedValue != null)
            {
                var categoria = (Categoria)lbxCategorias.SelectedItem;

                txtNombreCat.Text = categoria.nombre;
                idCategoriaSeleccionada = categoria.id;
                esNuevo = false; // Cambiamos el estado a Modo Edición

                // Cambiar el diseño del botón para que el usuario sepa que va a editar
                btnAgregarCate.Text = "Guardar Cambios";
                btnAgregarCate.BackColor = Color.FromArgb(16, 185, 129); // Verde éxito
            }
        }

        private void btnAgregarCate_Click(object sender, EventArgs e)
        {
            string nombreCat = txtNombreCat.Text.Trim();

            // 1. Validación de vacíos
            if (string.IsNullOrEmpty(nombreCat))
            {
                erpNombreCategoria.SetError(txtNombreCat, "El campo Nombre no debe estar Vacio");
                return;
            }

            // 2. Validación de duplicados (ajustada para ignorar la categoría actual si se está editando)
            var existe = CategoriaCln.listar().Any(c => c.nombre.Equals(nombreCat, StringComparison.OrdinalIgnoreCase)
                                                    && (esNuevo || c.id != idCategoriaSeleccionada));
            if (existe)
            {
                MessageBox.Show("Ya existe una categoría con ese nombre.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Decidir si se Guarda como Nuevo o se Edita
            if (esNuevo)
            {
                var nuevaCategoria = new Categoria
                {
                    nombre = nombreCat,
                    estado = 1
                };
                CategoriaCln.insertar(nuevaCategoria);
                MessageBox.Show("Se agrego la Categoria", "::: Restaurant - Mensaje :::",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var categoriaAEditar = (Categoria)lbxCategorias.SelectedItem;
                categoriaAEditar.nombre = nombreCat;

                // Llama al método de actualización de tu capa lógica
                CategoriaCln.actualizar(categoriaAEditar);
                MessageBox.Show("Se actualizaron los cambios correctamente.", "::: Restaurant - Mensaje :::",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // 4. Limpieza y refresco desactivando temporalmente el evento para evitar errores en bucle
            lbxCategorias.SelectedIndexChanged -= lbxCategorias_SelectedIndexChanged;

            CargarCategoriasListBox();
            RestablecerFormulario();

            lbxCategorias.SelectedIndexChanged += lbxCategorias_SelectedIndexChanged;
        }

        private void btnEliminarCate_Click(object sender, EventArgs e)
        {
            if (lbxCategorias.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar una categoría antes de eliminarla.",
                                "::: Restaurant - Mensaje :::",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            int idCategoria = Convert.ToInt32(lbxCategorias.SelectedValue);
            string nombreCat = lbxCategorias.Text;
            DialogResult dialog = MessageBox.Show(
                $"¿Está seguro que desea dar de baja la categoría “{nombreCat}”?",
                "::: Restaurant - Confirmación :::",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                CategoriaCln.eliminar(idCategoria);

                lbxCategorias.SelectedIndexChanged -= lbxCategorias_SelectedIndexChanged;
                CargarCategoriasListBox();
                RestablecerFormulario();
                lbxCategorias.SelectedIndexChanged += lbxCategorias_SelectedIndexChanged;

                MessageBox.Show("Categoría dada de baja correctamente.",
                                "::: Restaurant - Mensaje :::",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void txtNombreCat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAgregarCate_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void btnSalirCate_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}