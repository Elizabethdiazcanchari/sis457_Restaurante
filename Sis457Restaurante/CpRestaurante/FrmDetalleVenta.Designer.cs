namespace CpRestaurante
{
    partial class FrmDetalleVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetalleVenta));
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblTransaccion = new System.Windows.Forms.Label();
            this.btnSalirCate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkGreen;
            this.label6.Location = new System.Drawing.Point(407, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 32);
            this.label6.TabIndex = 27;
            this.label6.Text = "PRODUCTOS";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkGreen;
            this.label5.Location = new System.Drawing.Point(57, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 32);
            this.label5.TabIndex = 26;
            this.label5.Text = "Total:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGreen;
            this.label4.Location = new System.Drawing.Point(51, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 32);
            this.label4.TabIndex = 25;
            this.label4.Text = "Fecha:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkGreen;
            this.label3.Location = new System.Drawing.Point(41, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 32);
            this.label3.TabIndex = 24;
            this.label3.Text = "Usuario:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkGreen;
            this.label2.Location = new System.Drawing.Point(41, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 32);
            this.label2.TabIndex = 23;
            this.label2.Text = "Cliente:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(17, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 32);
            this.label1.TabIndex = 22;
            this.label1.Text = "N° de Transacción:";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Location = new System.Drawing.Point(297, 62);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(324, 278);
            this.dgvDetalle.TabIndex = 21;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTotal.Location = new System.Drawing.Point(106, 286);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(184, 32);
            this.lblTotal.TabIndex = 20;
            // 
            // lblFecha
            // 
            this.lblFecha.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblFecha.Location = new System.Drawing.Point(107, 222);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(184, 32);
            this.lblFecha.TabIndex = 19;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblUsuario.Location = new System.Drawing.Point(107, 168);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(184, 32);
            this.lblUsuario.TabIndex = 18;
            // 
            // lblCliente
            // 
            this.lblCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblCliente.Location = new System.Drawing.Point(106, 108);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(184, 32);
            this.lblCliente.TabIndex = 17;
            // 
            // lblTransaccion
            // 
            this.lblTransaccion.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransaccion.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTransaccion.Location = new System.Drawing.Point(106, 62);
            this.lblTransaccion.Name = "lblTransaccion";
            this.lblTransaccion.Size = new System.Drawing.Size(183, 24);
            this.lblTransaccion.TabIndex = 16;
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
            this.btnSalirCate.Location = new System.Drawing.Point(605, 9);
            this.btnSalirCate.Margin = new System.Windows.Forms.Padding(0);
            this.btnSalirCate.Name = "btnSalirCate";
            this.btnSalirCate.Size = new System.Drawing.Size(25, 25);
            this.btnSalirCate.TabIndex = 30;
            this.btnSalirCate.UseVisualStyleBackColor = false;
            this.btnSalirCate.Click += new System.EventHandler(this.btnSalirCate_Click);
            // 
            // FrmDetalleVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(639, 366);
            this.Controls.Add(this.btnSalirCate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.lblTransaccion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDetalleVenta";
            this.Text = "FrmDetalleVenta";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblTransaccion;
        private System.Windows.Forms.Button btnSalirCate;
    }
}