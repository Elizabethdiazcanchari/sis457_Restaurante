namespace CpRestaurante
{
    partial class FrmCategoria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbxCategorias = new System.Windows.Forms.ListBox();
            this.btnEliminarCate = new System.Windows.Forms.Button();
            this.lblListaCategorias = new System.Windows.Forms.Label();
            this.btnAgregarCate = new System.Windows.Forms.Button();
            this.erpNombreCategoria = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSalirCate = new System.Windows.Forms.Button();
            this.pnListaProductos = new System.Windows.Forms.Panel();
            this.lblVentas = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCambio = new System.Windows.Forms.TextBox();
            this.lblAgregar = new System.Windows.Forms.Label();
            this.txtNombreCat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.erpNombreCategoria)).BeginInit();
            this.pnListaProductos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxCategorias
            // 
            this.lbxCategorias.BackColor = System.Drawing.Color.White;
            this.lbxCategorias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbxCategorias.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxCategorias.ForeColor = System.Drawing.Color.Black;
            this.lbxCategorias.FormattingEnabled = true;
            this.lbxCategorias.ItemHeight = 17;
            this.lbxCategorias.Location = new System.Drawing.Point(12, 72);
            this.lbxCategorias.Name = "lbxCategorias";
            this.lbxCategorias.Size = new System.Drawing.Size(328, 172);
            this.lbxCategorias.TabIndex = 27;
            // 
            // btnEliminarCate
            // 
            this.btnEliminarCate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnEliminarCate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.btnEliminarCate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminarCate.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarCate.ForeColor = System.Drawing.Color.White;
            this.btnEliminarCate.Location = new System.Drawing.Point(12, 393);
            this.btnEliminarCate.Name = "btnEliminarCate";
            this.btnEliminarCate.Size = new System.Drawing.Size(156, 30);
            this.btnEliminarCate.TabIndex = 24;
            this.btnEliminarCate.Text = "Eliminar Selección";
            this.btnEliminarCate.UseVisualStyleBackColor = false;
            this.btnEliminarCate.Click += new System.EventHandler(this.btnEliminarCate_Click);
            // 
            // lblListaCategorias
            // 
            this.lblListaCategorias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblListaCategorias.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListaCategorias.ForeColor = System.Drawing.Color.White;
            this.lblListaCategorias.Location = new System.Drawing.Point(12, 9);
            this.lblListaCategorias.Name = "lblListaCategorias";
            this.lblListaCategorias.Size = new System.Drawing.Size(302, 26);
            this.lblListaCategorias.TabIndex = 23;
            this.lblListaCategorias.Text = "LISTA DE CATEGORÍAS";
            this.lblListaCategorias.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAgregarCate
            // 
            this.btnAgregarCate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAgregarCate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnAgregarCate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregarCate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCate.ForeColor = System.Drawing.Color.White;
            this.btnAgregarCate.Location = new System.Drawing.Point(12, 344);
            this.btnAgregarCate.Name = "btnAgregarCate";
            this.btnAgregarCate.Size = new System.Drawing.Size(328, 28);
            this.btnAgregarCate.TabIndex = 21;
            this.btnAgregarCate.Text = "Agregar Categoría";
            this.btnAgregarCate.UseVisualStyleBackColor = false;
            this.btnAgregarCate.Click += new System.EventHandler(this.btnAgregarCate_Click);
            // 
            // erpNombreCategoria
            // 
            this.erpNombreCategoria.ContainerControl = this;
            // 
            // btnSalirCate
            // 
            this.btnSalirCate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalirCate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnSalirCate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalirCate.FlatAppearance.BorderSize = 0;
            this.btnSalirCate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.btnSalirCate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalirCate.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalirCate.ForeColor = System.Drawing.Color.White;
            this.btnSalirCate.Location = new System.Drawing.Point(317, 8);
            this.btnSalirCate.Margin = new System.Windows.Forms.Padding(0);
            this.btnSalirCate.Name = "btnSalirCate";
            this.btnSalirCate.Size = new System.Drawing.Size(25, 25);
            this.btnSalirCate.TabIndex = 29;
            this.btnSalirCate.Text = "X";
            this.btnSalirCate.UseVisualStyleBackColor = false;
            this.btnSalirCate.Click += new System.EventHandler(this.btnSalirCate_Click);
            // 
            // pnListaProductos
            // 
            this.pnListaProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnListaProductos.Controls.Add(this.lblVentas);
            this.pnListaProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnListaProductos.Location = new System.Drawing.Point(0, 0);
            this.pnListaProductos.Name = "pnListaProductos";
            this.pnListaProductos.Size = new System.Drawing.Size(352, 42);
            this.pnListaProductos.TabIndex = 53;
            // 
            // lblVentas
            // 
            this.lblVentas.AutoSize = true;
            this.lblVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblVentas.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVentas.ForeColor = System.Drawing.Color.LemonChiffon;
            this.lblVentas.Location = new System.Drawing.Point(375, -1);
            this.lblVentas.Name = "lblVentas";
            this.lblVentas.Size = new System.Drawing.Size(304, 38);
            this.lblVentas.TabIndex = 5;
            this.lblVentas.Text = "Registro de Ventas";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblAgregar);
            this.groupBox1.Controls.Add(this.txtNombreCat);
            this.groupBox1.Controls.Add(this.txtCambio);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 246);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 92);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            // 
            // txtCambio
            // 
            this.txtCambio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCambio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCambio.Enabled = false;
            this.txtCambio.Location = new System.Drawing.Point(216, 97);
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.ReadOnly = true;
            this.txtCambio.Size = new System.Drawing.Size(101, 16);
            this.txtCambio.TabIndex = 64;
            this.txtCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAgregar
            // 
            this.lblAgregar.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgregar.ForeColor = System.Drawing.Color.Black;
            this.lblAgregar.Location = new System.Drawing.Point(6, 16);
            this.lblAgregar.Name = "lblAgregar";
            this.lblAgregar.Size = new System.Drawing.Size(316, 18);
            this.lblAgregar.TabIndex = 66;
            this.lblAgregar.Text = "AGREGAR NUEVA CATEGORÍA";
            // 
            // txtNombreCat
            // 
            this.txtNombreCat.Location = new System.Drawing.Point(6, 63);
            this.txtNombreCat.Name = "txtNombreCat";
            this.txtNombreCat.Size = new System.Drawing.Size(316, 23);
            this.txtNombreCat.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 18);
            this.label1.TabIndex = 67;
            this.label1.Text = "Nombre de la Categoría:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.White;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.Black;
            this.btnCerrar.Location = new System.Drawing.Point(186, 393);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(154, 30);
            this.btnCerrar.TabIndex = 59;
            this.btnCerrar.Text = "Cerrar Ventana";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(328, 18);
            this.label2.TabIndex = 68;
            this.label2.Text = "CATEGORÍAS ACTIVAS";
            // 
            // FrmCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(352, 432);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbxCategorias);
            this.Controls.Add(this.btnSalirCate);
            this.Controls.Add(this.btnEliminarCate);
            this.Controls.Add(this.lblListaCategorias);
            this.Controls.Add(this.btnAgregarCate);
            this.Controls.Add(this.pnListaProductos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCategoria";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCategoria";
            this.Load += new System.EventHandler(this.FrmCategoria_Load);
            ((System.ComponentModel.ISupportInitialize)(this.erpNombreCategoria)).EndInit();
            this.pnListaProductos.ResumeLayout(false);
            this.pnListaProductos.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxCategorias;
        private System.Windows.Forms.Button btnEliminarCate;
        private System.Windows.Forms.Label lblListaCategorias;
        private System.Windows.Forms.Button btnAgregarCate;
        private System.Windows.Forms.ErrorProvider erpNombreCategoria;
        private System.Windows.Forms.Button btnSalirCate;
        private System.Windows.Forms.Panel pnListaProductos;
        private System.Windows.Forms.Label lblVentas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAgregar;
        private System.Windows.Forms.TextBox txtNombreCat;
        private System.Windows.Forms.TextBox txtCambio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label1;
    }
}