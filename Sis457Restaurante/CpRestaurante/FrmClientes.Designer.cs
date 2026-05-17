namespace CpRestaurante
{
    partial class FrmClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientes));
            this.erpCiNit = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpRazonSocial = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCerrarAgregar = new System.Windows.Forms.Button();
            this.pnlAgregar = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblAgregarClientes = new System.Windows.Forms.Label();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.lblCiNit = new System.Windows.Forms.Label();
            this.txtCiNit = new System.Windows.Forms.TextBox();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.lblProductos = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.pctBuscar = new System.Windows.Forms.PictureBox();
            this.pnListaProductos = new System.Windows.Forms.Panel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.erpCiNit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpRazonSocial)).BeginInit();
            this.pnlAgregar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBuscar)).BeginInit();
            this.pnListaProductos.SuspendLayout();
            this.SuspendLayout();
            // 
            // erpCiNit
            // 
            this.erpCiNit.ContainerControl = this;
            // 
            // erpRazonSocial
            // 
            this.erpRazonSocial.ContainerControl = this;
            // 
            // btnCerrarAgregar
            // 
            this.btnCerrarAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrarAgregar.BackColor = System.Drawing.Color.Tomato;
            this.btnCerrarAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarAgregar.FlatAppearance.BorderSize = 0;
            this.btnCerrarAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.btnCerrarAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarAgregar.Image")));
            this.btnCerrarAgregar.Location = new System.Drawing.Point(370, 9);
            this.btnCerrarAgregar.Margin = new System.Windows.Forms.Padding(0);
            this.btnCerrarAgregar.Name = "btnCerrarAgregar";
            this.btnCerrarAgregar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrarAgregar.TabIndex = 24;
            this.btnCerrarAgregar.UseVisualStyleBackColor = false;
            this.btnCerrarAgregar.Click += new System.EventHandler(this.btnCerrarAgregar_Click);
            // 
            // pnlAgregar
            // 
            this.pnlAgregar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlAgregar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAgregar.Controls.Add(this.btnCerrarAgregar);
            this.pnlAgregar.Controls.Add(this.btnCancelar);
            this.pnlAgregar.Controls.Add(this.lblAgregarClientes);
            this.pnlAgregar.Controls.Add(this.lblRazonSocial);
            this.pnlAgregar.Controls.Add(this.lblCiNit);
            this.pnlAgregar.Controls.Add(this.txtCiNit);
            this.pnlAgregar.Controls.Add(this.txtRazonSocial);
            this.pnlAgregar.Controls.Add(this.btnGuardar);
            this.pnlAgregar.Location = new System.Drawing.Point(329, 131);
            this.pnlAgregar.Name = "pnlAgregar";
            this.pnlAgregar.Size = new System.Drawing.Size(405, 347);
            this.pnlAgregar.TabIndex = 39;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Tomato;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(226, 270);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 30);
            this.btnCancelar.TabIndex = 34;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblAgregarClientes
            // 
            this.lblAgregarClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblAgregarClientes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAgregarClientes.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgregarClientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblAgregarClientes.Location = new System.Drawing.Point(0, 0);
            this.lblAgregarClientes.Name = "lblAgregarClientes";
            this.lblAgregarClientes.Size = new System.Drawing.Size(403, 76);
            this.lblAgregarClientes.TabIndex = 30;
            this.lblAgregarClientes.Text = "AGREGAR CLIENTES";
            this.lblAgregarClientes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.BackColor = System.Drawing.Color.Transparent;
            this.lblRazonSocial.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazonSocial.ForeColor = System.Drawing.Color.Gray;
            this.lblRazonSocial.Location = new System.Drawing.Point(47, 161);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(107, 18);
            this.lblRazonSocial.TabIndex = 25;
            this.lblRazonSocial.Text = "Razon Social:";
            // 
            // lblCiNit
            // 
            this.lblCiNit.AutoSize = true;
            this.lblCiNit.BackColor = System.Drawing.Color.Transparent;
            this.lblCiNit.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCiNit.ForeColor = System.Drawing.Color.Gray;
            this.lblCiNit.Location = new System.Drawing.Point(78, 104);
            this.lblCiNit.Name = "lblCiNit";
            this.lblCiNit.Size = new System.Drawing.Size(56, 18);
            this.lblCiNit.TabIndex = 23;
            this.lblCiNit.Text = "CI/NIT:";
            // 
            // txtCiNit
            // 
            this.txtCiNit.BackColor = System.Drawing.SystemColors.Window;
            this.txtCiNit.Location = new System.Drawing.Point(171, 105);
            this.txtCiNit.MaxLength = 30;
            this.txtCiNit.Name = "txtCiNit";
            this.txtCiNit.Size = new System.Drawing.Size(221, 20);
            this.txtCiNit.TabIndex = 22;
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Location = new System.Drawing.Point(171, 159);
            this.txtRazonSocial.MaxLength = 100;
            this.txtRazonSocial.Multiline = true;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(221, 62);
            this.txtRazonSocial.TabIndex = 21;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.YellowGreen;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(81, 270);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(0);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 30);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(8, 94);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersWidth = 51;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(1044, 482);
            this.dgvClientes.TabIndex = 32;
            // 
            // lblProductos
            // 
            this.lblProductos.AutoSize = true;
            this.lblProductos.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblProductos.Location = new System.Drawing.Point(3, 2);
            this.lblProductos.Name = "lblProductos";
            this.lblProductos.Size = new System.Drawing.Size(267, 38);
            this.lblProductos.TabIndex = 5;
            this.lblProductos.Text = "Lista de Clientes";
            // 
            // txtBuscar
            // 
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscar.ForeColor = System.Drawing.Color.DimGray;
            this.txtBuscar.Location = new System.Drawing.Point(360, 60);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(268, 13);
            this.txtBuscar.TabIndex = 34;
            // 
            // pctBuscar
            // 
            this.pctBuscar.Image = ((System.Drawing.Image)(resources.GetObject("pctBuscar.Image")));
            this.pctBuscar.InitialImage = null;
            this.pctBuscar.Location = new System.Drawing.Point(355, 51);
            this.pctBuscar.Name = "pctBuscar";
            this.pctBuscar.Size = new System.Drawing.Size(301, 30);
            this.pctBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctBuscar.TabIndex = 36;
            this.pctBuscar.TabStop = false;
            // 
            // pnListaProductos
            // 
            this.pnListaProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnListaProductos.Controls.Add(this.lblProductos);
            this.pnListaProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnListaProductos.ForeColor = System.Drawing.Color.Transparent;
            this.pnListaProductos.Location = new System.Drawing.Point(0, 0);
            this.pnListaProductos.Name = "pnListaProductos";
            this.pnListaProductos.Size = new System.Drawing.Size(1050, 45);
            this.pnListaProductos.TabIndex = 35;
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(115, 51);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnEditar.Size = new System.Drawing.Size(97, 30);
            this.btnEditar.TabIndex = 38;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Tomato;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(222, 51);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(97, 30);
            this.btnEliminar.TabIndex = 37;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Peru;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(8, 51);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(97, 30);
            this.btnAgregar.TabIndex = 33;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // FrmClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 600);
            this.Controls.Add(this.pnlAgregar);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.pctBuscar);
            this.Controls.Add(this.pnListaProductos);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAgregar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmClientes";
            this.Text = "FrmClientes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmClientes_FormClosing);
            this.Load += new System.EventHandler(this.FrmClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.erpCiNit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpRazonSocial)).EndInit();
            this.pnlAgregar.ResumeLayout(false);
            this.pnlAgregar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBuscar)).EndInit();
            this.pnListaProductos.ResumeLayout(false);
            this.pnListaProductos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlAgregar;
        private System.Windows.Forms.Button btnCerrarAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblAgregarClientes;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.Label lblCiNit;
        private System.Windows.Forms.TextBox txtCiNit;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.PictureBox pctBuscar;
        private System.Windows.Forms.Panel pnListaProductos;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ErrorProvider erpCiNit;
        private System.Windows.Forms.ErrorProvider erpRazonSocial;
    }
}