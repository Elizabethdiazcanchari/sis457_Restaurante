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
        public FrmCategoria()
        {
            InitializeComponent();
        }
        private void CargarCategoriasListBox()
        {
            var categorias = CategoriaCln.listar();
            lstCategorias.DataSource = categorias;
            lstCategorias.ValueMember = "id";
            lstCategorias.DisplayMember = "nombre";
        }
        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            CargarCategoriasListBox();
        }

        private void btnAgregarCate_Click(object sender, EventArgs e)
        {
            var categoria = new Categoria();
            categoria.nombre = txtNombreCat.Text.Trim();

            var existe = CategoriaCln.listar().Any(c => c.nombre.Equals(categoria.nombre, StringComparison.OrdinalIgnoreCase));
            if (existe)
            {
                MessageBox.Show("Ya existe una categoría con ese nombre.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtNombreCat.Text))
            {
                erpNombreCategoria.SetError(txtNombreCat, "El campo Nombre no debe estar Vacio");
            }
            else
            {
                categoria.estado = 1;
                CategoriaCln.insertar(categoria);
                txtNombreCat.Clear();
                CargarCategoriasListBox();
                MessageBox.Show("Se agrego la Categoria", "::: Restaurant - Mensaje :::",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminarCate_Click(object sender, EventArgs e)
        {
            if (lstCategorias.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar una categoría antes de eliminarla.",
                                "::: Restaurant - Mensaje :::",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }


            int idCategoria = Convert.ToInt32(lstCategorias.SelectedValue);
            string nombreCat = lstCategorias.Text;
            DialogResult dialog = MessageBox.Show(
                $"¿Está seguro que desea dar de baja la categoría “{nombreCat}”?",
                "::: Restaurant - Confirmación :::",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                CategoriaCln.eliminar(idCategoria);
                CargarCategoriasListBox();
                MessageBox.Show("Categoría dada de baja correctamente.",
                                "::: Restaurant - Mensaje :::",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void btnSalirCate_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtNombreCat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAgregarCate_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }
    }
}
