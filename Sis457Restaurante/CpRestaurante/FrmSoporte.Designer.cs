namespace CpRestaurante
{
    partial class FrmSoporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSoporte));
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnListaProductos = new System.Windows.Forms.Panel();
            this.lblPedidos = new System.Windows.Forms.Label();
            this.pnlContacto = new System.Windows.Forms.Panel();
            this.btnWhatsapp = new System.Windows.Forms.Button();
            this.lnkCorreo = new System.Windows.Forms.LinkLabel();
            this.lnkTelefono = new System.Windows.Forms.LinkLabel();
            this.lblTituloContacto = new System.Windows.Forms.Label();
            this.tbcSoporte = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnEnviarReporte = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.cbPrioridad = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbModulo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lstPreguntas = new System.Windows.Forms.ListBox();
            this.txtRespuestaFAQ = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pnListaProductos.SuspendLayout();
            this.pnlContacto.SuspendLayout();
            this.tbcSoporte.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblTotal.Location = new System.Drawing.Point(39, 135);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(119, 20);
            this.lblTotal.TabIndex = 29;
            this.lblTotal.Text = "📍 Sucre, Bolivia";
            // 
            // pnListaProductos
            // 
            this.pnListaProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnListaProductos.Controls.Add(this.lblPedidos);
            this.pnListaProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnListaProductos.Location = new System.Drawing.Point(0, 0);
            this.pnListaProductos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnListaProductos.Name = "pnListaProductos";
            this.pnListaProductos.Size = new System.Drawing.Size(1000, 52);
            this.pnListaProductos.TabIndex = 55;
            // 
            // lblPedidos
            // 
            this.lblPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPedidos.AutoSize = true;
            this.lblPedidos.BackColor = System.Drawing.Color.Transparent;
            this.lblPedidos.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedidos.ForeColor = System.Drawing.Color.LemonChiffon;
            this.lblPedidos.Location = new System.Drawing.Point(363, 9);
            this.lblPedidos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPedidos.Name = "lblPedidos";
            this.lblPedidos.Size = new System.Drawing.Size(441, 38);
            this.lblPedidos.TabIndex = 5;
            this.lblPedidos.Text = "Gestión de Soporte Técnico";
            // 
            // pnlContacto
            // 
            this.pnlContacto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(37)))));
            this.pnlContacto.Controls.Add(this.btnWhatsapp);
            this.pnlContacto.Controls.Add(this.lnkCorreo);
            this.pnlContacto.Controls.Add(this.lnkTelefono);
            this.pnlContacto.Controls.Add(this.lblTotal);
            this.pnlContacto.Controls.Add(this.lblTituloContacto);
            this.pnlContacto.Location = new System.Drawing.Point(26, 84);
            this.pnlContacto.Name = "pnlContacto";
            this.pnlContacto.Size = new System.Drawing.Size(352, 266);
            this.pnlContacto.TabIndex = 56;
            // 
            // btnWhatsapp
            // 
            this.btnWhatsapp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnWhatsapp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWhatsapp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWhatsapp.ForeColor = System.Drawing.Color.White;
            this.btnWhatsapp.Location = new System.Drawing.Point(134, 204);
            this.btnWhatsapp.Name = "btnWhatsapp";
            this.btnWhatsapp.Size = new System.Drawing.Size(174, 30);
            this.btnWhatsapp.TabIndex = 30;
            this.btnWhatsapp.Text = "SOPORTE POR WHATSAPP";
            this.btnWhatsapp.UseVisualStyleBackColor = false;
            this.btnWhatsapp.Click += new System.EventHandler(this.btnWhatsapp_Click);
            // 
            // lnkCorreo
            // 
            this.lnkCorreo.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkCorreo.AutoSize = true;
            this.lnkCorreo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCorreo.ForeColor = System.Drawing.Color.Gray;
            this.lnkCorreo.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lnkCorreo.Location = new System.Drawing.Point(39, 100);
            this.lnkCorreo.Name = "lnkCorreo";
            this.lnkCorreo.Size = new System.Drawing.Size(281, 20);
            this.lnkCorreo.TabIndex = 2;
            this.lnkCorreo.TabStop = true;
            this.lnkCorreo.Text = "✉ jhoselinfigueroacolque@gmail.com";
            this.lnkCorreo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCorreo_LinkClicked);
            // 
            // lnkTelefono
            // 
            this.lnkTelefono.ActiveLinkColor = System.Drawing.Color.MediumBlue;
            this.lnkTelefono.AutoSize = true;
            this.lnkTelefono.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTelefono.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lnkTelefono.Location = new System.Drawing.Point(38, 69);
            this.lnkTelefono.Name = "lnkTelefono";
            this.lnkTelefono.Size = new System.Drawing.Size(206, 20);
            this.lnkTelefono.TabIndex = 1;
            this.lnkTelefono.TabStop = true;
            this.lnkTelefono.Text = "📞 Central: +591 71806340";
            this.lnkTelefono.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTelefono_LinkClicked);
            // 
            // lblTituloContacto
            // 
            this.lblTituloContacto.AutoSize = true;
            this.lblTituloContacto.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloContacto.ForeColor = System.Drawing.Color.White;
            this.lblTituloContacto.Location = new System.Drawing.Point(38, 24);
            this.lblTituloContacto.Name = "lblTituloContacto";
            this.lblTituloContacto.Size = new System.Drawing.Size(154, 20);
            this.lblTituloContacto.TabIndex = 0;
            this.lblTituloContacto.Text = "CONTACTO DIRECTO";
            // 
            // tbcSoporte
            // 
            this.tbcSoporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcSoporte.Controls.Add(this.tabPage1);
            this.tbcSoporte.Controls.Add(this.tabPage2);
            this.tbcSoporte.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tbcSoporte.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcSoporte.ItemSize = new System.Drawing.Size(288, 30);
            this.tbcSoporte.Location = new System.Drawing.Point(396, 84);
            this.tbcSoporte.Name = "tbcSoporte";
            this.tbcSoporte.SelectedIndex = 0;
            this.tbcSoporte.Size = new System.Drawing.Size(580, 306);
            this.tbcSoporte.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbcSoporte.TabIndex = 57;
            this.tbcSoporte.SelectedIndexChanged += new System.EventHandler(this.tbcSoporte_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage1.Controls.Add(this.btnEnviarReporte);
            this.tabPage1.Controls.Add(this.txtDescripcion);
            this.tabPage1.Controls.Add(this.cbPrioridad);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cbModulo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.ForeColor = System.Drawing.Color.Transparent;
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(572, 268);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "REPORTAR INCIDENCIA";
            // 
            // btnEnviarReporte
            // 
            this.btnEnviarReporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(36)))), ((int)(((byte)(56)))));
            this.btnEnviarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarReporte.ForeColor = System.Drawing.Color.White;
            this.btnEnviarReporte.Location = new System.Drawing.Point(333, 228);
            this.btnEnviarReporte.Name = "btnEnviarReporte";
            this.btnEnviarReporte.Size = new System.Drawing.Size(215, 31);
            this.btnEnviarReporte.TabIndex = 36;
            this.btnEnviarReporte.Text = "ENVIAR INFORME TÉCNICO";
            this.btnEnviarReporte.UseVisualStyleBackColor = false;
            this.btnEnviarReporte.Click += new System.EventHandler(this.btnEnviarReporte_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(18, 98);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(530, 121);
            this.txtDescripcion.TabIndex = 35;
            // 
            // cbPrioridad
            // 
            this.cbPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrioridad.FormattingEnabled = true;
            this.cbPrioridad.Items.AddRange(new object[] {
            "Alta (Bloqueante), ",
            "Media, ",
            "Baja"});
            this.cbPrioridad.Location = new System.Drawing.Point(303, 63);
            this.cbPrioridad.Name = "cbPrioridad";
            this.cbPrioridad.Size = new System.Drawing.Size(245, 23);
            this.cbPrioridad.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(363, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "PRIORIDAD";
            // 
            // cbModulo
            // 
            this.cbModulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModulo.FormattingEnabled = true;
            this.cbModulo.Items.AddRange(new object[] {
            "Venta (POS), ",
            "Productos,",
            "Empleados,",
            "Clientes, ",
            "Reportes,",
            "Inicio de sesion"});
            this.cbModulo.Location = new System.Drawing.Point(18, 63);
            this.cbModulo.Name = "cbModulo";
            this.cbModulo.Size = new System.Drawing.Size(279, 23);
            this.cbModulo.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(70, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 31;
            this.label1.Text = "MÓDULO AFECTADO";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage2.Controls.Add(this.lstPreguntas);
            this.tabPage2.Controls.Add(this.txtRespuestaFAQ);
            this.tabPage2.ForeColor = System.Drawing.Color.Transparent;
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(572, 268);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "PREGUNTAS FRECUENTES";
            // 
            // lstPreguntas
            // 
            this.lstPreguntas.FormattingEnabled = true;
            this.lstPreguntas.ItemHeight = 15;
            this.lstPreguntas.Items.AddRange(new object[] {
            "1. ¿Qué hacer si la ticketera térmica no imprime?",
            "2. ¿Cómo corregir un pedido mal registrado?",
            "3. El sistema no calcula el cambio o vuelto."});
            this.lstPreguntas.Location = new System.Drawing.Point(21, 24);
            this.lstPreguntas.Name = "lstPreguntas";
            this.lstPreguntas.Size = new System.Drawing.Size(530, 49);
            this.lstPreguntas.TabIndex = 40;
            this.lstPreguntas.SelectedIndexChanged += new System.EventHandler(this.LstPreguntas_SelectedIndexChanged);
            // 
            // txtRespuestaFAQ
            // 
            this.txtRespuestaFAQ.BackColor = System.Drawing.Color.White;
            this.txtRespuestaFAQ.Location = new System.Drawing.Point(21, 100);
            this.txtRespuestaFAQ.Multiline = true;
            this.txtRespuestaFAQ.Name = "txtRespuestaFAQ";
            this.txtRespuestaFAQ.ReadOnly = true;
            this.txtRespuestaFAQ.Size = new System.Drawing.Size(530, 158);
            this.txtRespuestaFAQ.TabIndex = 39;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(26, 356);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(241, 223);
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(803, 414);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(173, 169);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 60;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(611, 414);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(176, 169);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 61;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(418, 414);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(176, 169);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 62;
            this.pictureBox4.TabStop = false;
            // 
            // FrmSoporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbcSoporte);
            this.Controls.Add(this.pnlContacto);
            this.Controls.Add(this.pnListaProductos);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmSoporte";
            this.Text = "FrmSeleccionarProducto";
            this.pnListaProductos.ResumeLayout(false);
            this.pnListaProductos.PerformLayout();
            this.pnlContacto.ResumeLayout(false);
            this.pnlContacto.PerformLayout();
            this.tbcSoporte.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnListaProductos;
        private System.Windows.Forms.Label lblPedidos;
        private System.Windows.Forms.Panel pnlContacto;
        private System.Windows.Forms.LinkLabel lnkTelefono;
        private System.Windows.Forms.Label lblTituloContacto;
        private System.Windows.Forms.Button btnWhatsapp;
        private System.Windows.Forms.LinkLabel lnkCorreo;
        private System.Windows.Forms.TabControl tbcSoporte;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbPrioridad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbModulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEnviarReporte;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.ListBox lstPreguntas;
        private System.Windows.Forms.TextBox txtRespuestaFAQ;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}