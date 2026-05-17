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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCategoria));
            this.lstCategorias = new System.Windows.Forms.ListBox();
            this.lblAgregarCategoria = new System.Windows.Forms.Label();
            this.btnEliminarCate = new System.Windows.Forms.Button();
            this.lblListaCategorias = new System.Windows.Forms.Label();
            this.lblNombreCategoria = new System.Windows.Forms.Label();
            this.txtNombreCat = new System.Windows.Forms.TextBox();
            this.btnAgregarCate = new System.Windows.Forms.Button();
            this.erpNombreCategoria = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSalirCate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.erpNombreCategoria)).BeginInit();
            this.SuspendLayout();
            // 
            // lstCategorias
            // 
            this.lstCategorias.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lstCategorias.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCategorias.ForeColor = System.Drawing.Color.Black;
            this.lstCategorias.FormattingEnabled = true;
            this.lstCategorias.ItemHeight = 16;
            this.lstCategorias.Location = new System.Drawing.Point(12, 49);
            this.lstCategorias.Name = "lstCategorias";
            this.lstCategorias.Size = new System.Drawing.Size(235, 180);
            this.lstCategorias.TabIndex = 27;
            // 
            // lblAgregarCategoria
            // 
            this.lblAgregarCategoria.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgregarCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblAgregarCategoria.Location = new System.Drawing.Point(62, 243);
            this.lblAgregarCategoria.Name = "lblAgregarCategoria";
            this.lblAgregarCategoria.Size = new System.Drawing.Size(143, 18);
            this.lblAgregarCategoria.TabIndex = 26;
            this.lblAgregarCategoria.Text = "AGREGAR CATEGORIA";
            // 
            // btnEliminarCate
            // 
            this.btnEliminarCate.BackColor = System.Drawing.Color.Red;
            this.btnEliminarCate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.btnEliminarCate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminarCate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarCate.ForeColor = System.Drawing.Color.White;
            this.btnEliminarCate.Location = new System.Drawing.Point(12, 357);
            this.btnEliminarCate.Name = "btnEliminarCate";
            this.btnEliminarCate.Size = new System.Drawing.Size(235, 23);
            this.btnEliminarCate.TabIndex = 24;
            this.btnEliminarCate.Text = "ELIMINAR";
            this.btnEliminarCate.UseVisualStyleBackColor = false;
            this.btnEliminarCate.Click += new System.EventHandler(this.btnEliminarCate_Click);
            // 
            // lblListaCategorias
            // 
            this.lblListaCategorias.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListaCategorias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblListaCategorias.Location = new System.Drawing.Point(12, 9);
            this.lblListaCategorias.Name = "lblListaCategorias";
            this.lblListaCategorias.Size = new System.Drawing.Size(235, 26);
            this.lblListaCategorias.TabIndex = 23;
            this.lblListaCategorias.Text = "LISTA DE CATEGORIAS";
            // 
            // lblNombreCategoria
            // 
            this.lblNombreCategoria.Font = new System.Drawing.Font("Century Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCategoria.ForeColor = System.Drawing.Color.White;
            this.lblNombreCategoria.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblNombreCategoria.Location = new System.Drawing.Point(9, 270);
            this.lblNombreCategoria.Name = "lblNombreCategoria";
            this.lblNombreCategoria.Size = new System.Drawing.Size(238, 17);
            this.lblNombreCategoria.TabIndex = 28;
            this.lblNombreCategoria.Text = "NOMBRE DE LA CATEGORÍA:";
            // 
            // txtNombreCat
            // 
            this.txtNombreCat.Location = new System.Drawing.Point(12, 290);
            this.txtNombreCat.Name = "txtNombreCat";
            this.txtNombreCat.Size = new System.Drawing.Size(235, 20);
            this.txtNombreCat.TabIndex = 22;
            this.txtNombreCat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreCat_KeyPress);
            // 
            // btnAgregarCate
            // 
            this.btnAgregarCate.BackColor = System.Drawing.Color.Lime;
            this.btnAgregarCate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnAgregarCate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregarCate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCate.ForeColor = System.Drawing.Color.White;
            this.btnAgregarCate.Location = new System.Drawing.Point(12, 322);
            this.btnAgregarCate.Name = "btnAgregarCate";
            this.btnAgregarCate.Size = new System.Drawing.Size(235, 23);
            this.btnAgregarCate.TabIndex = 21;
            this.btnAgregarCate.Text = "AGREGAR";
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
            this.btnSalirCate.BackColor = System.Drawing.Color.Brown;
            this.btnSalirCate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalirCate.FlatAppearance.BorderSize = 0;
            this.btnSalirCate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.btnSalirCate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalirCate.Image = ((System.Drawing.Image)(resources.GetObject("btnSalirCate.Image")));
            this.btnSalirCate.Location = new System.Drawing.Point(222, 9);
            this.btnSalirCate.Margin = new System.Windows.Forms.Padding(0);
            this.btnSalirCate.Name = "btnSalirCate";
            this.btnSalirCate.Size = new System.Drawing.Size(25, 25);
            this.btnSalirCate.TabIndex = 29;
            this.btnSalirCate.UseVisualStyleBackColor = false;
            this.btnSalirCate.Click += new System.EventHandler(this.btnSalirCate_Click);
            // 
            // FrmCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(257, 397);
            this.Controls.Add(this.btnSalirCate);
            this.Controls.Add(this.lstCategorias);
            this.Controls.Add(this.lblAgregarCategoria);
            this.Controls.Add(this.btnEliminarCate);
            this.Controls.Add(this.lblListaCategorias);
            this.Controls.Add(this.lblNombreCategoria);
            this.Controls.Add(this.txtNombreCat);
            this.Controls.Add(this.btnAgregarCate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCategoria";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCategoria";
            this.Load += new System.EventHandler(this.FrmCategoria_Load);
            ((System.ComponentModel.ISupportInitialize)(this.erpNombreCategoria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstCategorias;
        private System.Windows.Forms.Label lblAgregarCategoria;
        private System.Windows.Forms.Button btnEliminarCate;
        private System.Windows.Forms.Label lblListaCategorias;
        private System.Windows.Forms.Label lblNombreCategoria;
        private System.Windows.Forms.TextBox txtNombreCat;
        private System.Windows.Forms.Button btnAgregarCate;
        private System.Windows.Forms.ErrorProvider erpNombreCategoria;
        private System.Windows.Forms.Button btnSalirCate;
    }
}