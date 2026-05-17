namespace CpRestaurante
{
    partial class FrmReporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporte));
            this.pnListaProductos = new System.Windows.Forms.Panel();
            this.lblPedidos = new System.Windows.Forms.Label();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.pctBuscar = new System.Windows.Forms.PictureBox();
            this.pnListaProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBuscar)).BeginInit();
            this.SuspendLayout();
            // 
            // pnListaProductos
            // 
            this.pnListaProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnListaProductos.Controls.Add(this.lblPedidos);
            this.pnListaProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnListaProductos.Location = new System.Drawing.Point(0, 0);
            this.pnListaProductos.Name = "pnListaProductos";
            this.pnListaProductos.Size = new System.Drawing.Size(1034, 45);
            this.pnListaProductos.TabIndex = 54;
            // 
            // lblPedidos
            // 
            this.lblPedidos.AutoSize = true;
            this.lblPedidos.BackColor = System.Drawing.Color.Transparent;
            this.lblPedidos.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedidos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblPedidos.Location = new System.Drawing.Point(3, 0);
            this.lblPedidos.Name = "lblPedidos";
            this.lblPedidos.Size = new System.Drawing.Size(328, 38);
            this.lblPedidos.TabIndex = 5;
            this.lblPedidos.Text = "Gestion de Reportes";
            // 
            // btnVerDetalle
            // 
            this.btnVerDetalle.BackColor = System.Drawing.Color.Yellow;
            this.btnVerDetalle.FlatAppearance.BorderSize = 0;
            this.btnVerDetalle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnVerDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerDetalle.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerDetalle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnVerDetalle.Image = ((System.Drawing.Image)(resources.GetObject("btnVerDetalle.Image")));
            this.btnVerDetalle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerDetalle.Location = new System.Drawing.Point(855, 50);
            this.btnVerDetalle.Margin = new System.Windows.Forms.Padding(0);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(155, 30);
            this.btnVerDetalle.TabIndex = 40;
            this.btnVerDetalle.Text = "VER DETALLE";
            this.btnVerDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVerDetalle.UseVisualStyleBackColor = false;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            // 
            // dgvReporte
            // 
            this.dgvReporte.AllowUserToAddRows = false;
            this.dgvReporte.AllowUserToDeleteRows = false;
            this.dgvReporte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReporte.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Location = new System.Drawing.Point(5, 88);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.ReadOnly = true;
            this.dgvReporte.RowHeadersWidth = 51;
            this.dgvReporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporte.Size = new System.Drawing.Size(1024, 470);
            this.dgvReporte.TabIndex = 53;
            this.dgvReporte.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReporte_CellDoubleClick);
            // 
            // txtBuscar
            // 
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscar.ForeColor = System.Drawing.Color.DimGray;
            this.txtBuscar.Location = new System.Drawing.Point(140, 59);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(268, 13);
            this.txtBuscar.TabIndex = 55;
            // 
            // pctBuscar
            // 
            this.pctBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pctBuscar.Image = ((System.Drawing.Image)(resources.GetObject("pctBuscar.Image")));
            this.pctBuscar.InitialImage = null;
            this.pctBuscar.Location = new System.Drawing.Point(135, 50);
            this.pctBuscar.Name = "pctBuscar";
            this.pctBuscar.Size = new System.Drawing.Size(301, 30);
            this.pctBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctBuscar.TabIndex = 56;
            this.pctBuscar.TabStop = false;
            // 
            // FrmReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 561);
            this.Controls.Add(this.pnListaProductos);
            this.Controls.Add(this.btnVerDetalle);
            this.Controls.Add(this.dgvReporte);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.pctBuscar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReporte";
            this.Text = "FrmReportes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReporte_FormClosing);
            this.Load += new System.EventHandler(this.FrmReporte_Load);
            this.pnListaProductos.ResumeLayout(false);
            this.pnListaProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBuscar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnListaProductos;
        private System.Windows.Forms.Label lblPedidos;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.DataGridView dgvReporte;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.PictureBox pctBuscar;
    }
}