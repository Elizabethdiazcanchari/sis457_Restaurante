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
            this.lblPedidos = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.pnListaProductos = new System.Windows.Forms.Panel();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.pnlWorkspace = new System.Windows.Forms.Panel();
            this.pnlTopProductos = new System.Windows.Forms.Panel();
            this.panelBarra3 = new System.Windows.Forms.Panel();
            this.valP3 = new System.Windows.Forms.Label();
            this.panelBarra2 = new System.Windows.Forms.Panel();
            this.valP2 = new System.Windows.Forms.Label();
            this.panelBarra1 = new System.Windows.Forms.Panel();
            this.valP1 = new System.Windows.Forms.Label();
            this.lblProd1 = new System.Windows.Forms.Label();
            this.lblProd3 = new System.Windows.Forms.Label();
            this.lblProd2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.pnlKpiTiket = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblKpiTicket = new System.Windows.Forms.Label();
            this.pnlKpiOredenes = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblKpiOrdenes = new System.Windows.Forms.Label();
            this.pnlKpiIngresos = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblKpiIngresos = new System.Windows.Forms.Label();
            this.pnListaProductos.SuspendLayout();
            this.pnlWorkspace.SuspendLayout();
            this.pnlTopProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            this.pnlKpiTiket.SuspendLayout();
            this.pnlKpiOredenes.SuspendLayout();
            this.pnlKpiIngresos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPedidos
            // 
            this.lblPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPedidos.AutoSize = true;
            this.lblPedidos.BackColor = System.Drawing.Color.Transparent;
            this.lblPedidos.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedidos.ForeColor = System.Drawing.Color.LemonChiffon;
            this.lblPedidos.Location = new System.Drawing.Point(255, 1);
            this.lblPedidos.Name = "lblPedidos";
            this.lblPedidos.Size = new System.Drawing.Size(464, 38);
            this.lblPedidos.TabIndex = 5;
            this.lblPedidos.Text = "Reporte de Ventas Gerencial";
            this.lblPedidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(188, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 21);
            this.label4.TabIndex = 76;
            this.label4.Text = "HASTA:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(804, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 21);
            this.label3.TabIndex = 75;
            this.label3.Text = "ACCIONES:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 21);
            this.label2.TabIndex = 74;
            this.label2.Text = "DESDE:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpHasta.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(174, 88);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(100, 20);
            this.dtpHasta.TabIndex = 72;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpDesde.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(51, 88);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(100, 20);
            this.dtpDesde.TabIndex = 71;
            // 
            // pnListaProductos
            // 
            this.pnListaProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnListaProductos.Controls.Add(this.lblPedidos);
            this.pnListaProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnListaProductos.Location = new System.Drawing.Point(0, 0);
            this.pnListaProductos.Name = "pnListaProductos";
            this.pnListaProductos.Size = new System.Drawing.Size(1000, 45);
            this.pnListaProductos.TabIndex = 67;
            // 
            // btnProcesar
            // 
            this.btnProcesar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcesar.BackColor = System.Drawing.Color.Transparent;
            this.btnProcesar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnProcesar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnProcesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcesar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnProcesar.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesar.Image")));
            this.btnProcesar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProcesar.Location = new System.Drawing.Point(770, 81);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(0);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(199, 36);
            this.btnProcesar.TabIndex = 65;
            this.btnProcesar.Text = "PROCESAR REPORTE";
            this.btnProcesar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProcesar.UseVisualStyleBackColor = false;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // pnlWorkspace
            // 
            this.pnlWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(125)))), ((int)(((byte)(146)))));
            this.pnlWorkspace.Controls.Add(this.pnlTopProductos);
            this.pnlWorkspace.Controls.Add(this.dgvReporte);
            this.pnlWorkspace.Controls.Add(this.pnlKpiTiket);
            this.pnlWorkspace.Controls.Add(this.pnlKpiOredenes);
            this.pnlWorkspace.Controls.Add(this.pnlKpiIngresos);
            this.pnlWorkspace.Location = new System.Drawing.Point(12, 132);
            this.pnlWorkspace.Name = "pnlWorkspace";
            this.pnlWorkspace.Size = new System.Drawing.Size(976, 456);
            this.pnlWorkspace.TabIndex = 78;
            // 
            // pnlTopProductos
            // 
            this.pnlTopProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTopProductos.BackColor = System.Drawing.Color.White;
            this.pnlTopProductos.Controls.Add(this.valP3);
            this.pnlTopProductos.Controls.Add(this.valP2);
            this.pnlTopProductos.Controls.Add(this.valP1);
            this.pnlTopProductos.Controls.Add(this.panelBarra3);
            this.pnlTopProductos.Controls.Add(this.panelBarra2);
            this.pnlTopProductos.Controls.Add(this.panelBarra1);
            this.pnlTopProductos.Controls.Add(this.lblProd1);
            this.pnlTopProductos.Controls.Add(this.lblProd3);
            this.pnlTopProductos.Controls.Add(this.lblProd2);
            this.pnlTopProductos.Controls.Add(this.label6);
            this.pnlTopProductos.Location = new System.Drawing.Point(608, 166);
            this.pnlTopProductos.Name = "pnlTopProductos";
            this.pnlTopProductos.Size = new System.Drawing.Size(338, 251);
            this.pnlTopProductos.TabIndex = 78;
            // 
            // panelBarra3
            // 
            this.panelBarra3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBarra3.BackColor = System.Drawing.Color.Green;
            this.panelBarra3.Location = new System.Drawing.Point(120, 193);
            this.panelBarra3.Name = "panelBarra3";
            this.panelBarra3.Size = new System.Drawing.Size(174, 24);
            this.panelBarra3.TabIndex = 81;
            // 
            // valP3
            // 
            this.valP3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.valP3.AutoSize = true;
            this.valP3.BackColor = System.Drawing.Color.Transparent;
            this.valP3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valP3.ForeColor = System.Drawing.Color.Black;
            this.valP3.Location = new System.Drawing.Point(300, 193);
            this.valP3.Name = "valP3";
            this.valP3.Size = new System.Drawing.Size(14, 16);
            this.valP3.TabIndex = 84;
            this.valP3.Text = "0";
            // 
            // panelBarra2
            // 
            this.panelBarra2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBarra2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panelBarra2.Location = new System.Drawing.Point(120, 136);
            this.panelBarra2.Name = "panelBarra2";
            this.panelBarra2.Size = new System.Drawing.Size(174, 24);
            this.panelBarra2.TabIndex = 81;
            // 
            // valP2
            // 
            this.valP2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.valP2.AutoSize = true;
            this.valP2.BackColor = System.Drawing.Color.Transparent;
            this.valP2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valP2.ForeColor = System.Drawing.Color.Black;
            this.valP2.Location = new System.Drawing.Point(300, 144);
            this.valP2.Name = "valP2";
            this.valP2.Size = new System.Drawing.Size(14, 16);
            this.valP2.TabIndex = 83;
            this.valP2.Text = "0";
            // 
            // panelBarra1
            // 
            this.panelBarra1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panelBarra1.Location = new System.Drawing.Point(121, 74);
            this.panelBarra1.Name = "panelBarra1";
            this.panelBarra1.Size = new System.Drawing.Size(174, 24);
            this.panelBarra1.TabIndex = 80;
            // 
            // valP1
            // 
            this.valP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.valP1.AutoSize = true;
            this.valP1.BackColor = System.Drawing.Color.Transparent;
            this.valP1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valP1.ForeColor = System.Drawing.Color.Black;
            this.valP1.Location = new System.Drawing.Point(301, 80);
            this.valP1.Name = "valP1";
            this.valP1.Size = new System.Drawing.Size(14, 16);
            this.valP1.TabIndex = 82;
            this.valP1.Text = "0";
            // 
            // lblProd1
            // 
            this.lblProd1.AutoSize = true;
            this.lblProd1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProd1.ForeColor = System.Drawing.Color.Gray;
            this.lblProd1.Location = new System.Drawing.Point(24, 80);
            this.lblProd1.Name = "lblProd1";
            this.lblProd1.Size = new System.Drawing.Size(75, 16);
            this.lblProd1.TabIndex = 79;
            this.lblProd1.Text = "Producto 1";
            // 
            // lblProd3
            // 
            this.lblProd3.AutoSize = true;
            this.lblProd3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProd3.ForeColor = System.Drawing.Color.Gray;
            this.lblProd3.Location = new System.Drawing.Point(24, 201);
            this.lblProd3.Name = "lblProd3";
            this.lblProd3.Size = new System.Drawing.Size(75, 16);
            this.lblProd3.TabIndex = 78;
            this.lblProd3.Text = "Producto 3";
            // 
            // lblProd2
            // 
            this.lblProd2.AutoSize = true;
            this.lblProd2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProd2.ForeColor = System.Drawing.Color.Gray;
            this.lblProd2.Location = new System.Drawing.Point(24, 144);
            this.lblProd2.Name = "lblProd2";
            this.lblProd2.Size = new System.Drawing.Size(75, 16);
            this.lblProd2.TabIndex = 77;
            this.lblProd2.Text = "Producto 2";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(81, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 16);
            this.label6.TabIndex = 76;
            this.label6.Text = "PRODUCTOS MÁS VENDIDOS";
            // 
            // dgvReporte
            // 
            this.dgvReporte.AllowUserToAddRows = false;
            this.dgvReporte.AllowUserToDeleteRows = false;
            this.dgvReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReporte.BackgroundColor = System.Drawing.Color.White;
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Location = new System.Drawing.Point(34, 166);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.ReadOnly = true;
            this.dgvReporte.RowHeadersWidth = 51;
            this.dgvReporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporte.Size = new System.Drawing.Size(551, 251);
            this.dgvReporte.TabIndex = 78;
            // 
            // pnlKpiTiket
            // 
            this.pnlKpiTiket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlKpiTiket.BackColor = System.Drawing.Color.White;
            this.pnlKpiTiket.Controls.Add(this.label7);
            this.pnlKpiTiket.Controls.Add(this.lblKpiTicket);
            this.pnlKpiTiket.Location = new System.Drawing.Point(696, 35);
            this.pnlKpiTiket.Name = "pnlKpiTiket";
            this.pnlKpiTiket.Size = new System.Drawing.Size(250, 80);
            this.pnlKpiTiket.TabIndex = 77;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(37, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 16);
            this.label7.TabIndex = 76;
            this.label7.Text = "TICKET PROMEDIO DIARIO";
            // 
            // lblKpiTicket
            // 
            this.lblKpiTicket.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblKpiTicket.AutoSize = true;
            this.lblKpiTicket.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpiTicket.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblKpiTicket.Location = new System.Drawing.Point(93, 44);
            this.lblKpiTicket.Name = "lblKpiTicket";
            this.lblKpiTicket.Size = new System.Drawing.Size(83, 22);
            this.lblKpiTicket.TabIndex = 75;
            this.lblKpiTicket.Text = "Bs. 0.00";
            // 
            // pnlKpiOredenes
            // 
            this.pnlKpiOredenes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlKpiOredenes.BackColor = System.Drawing.Color.White;
            this.pnlKpiOredenes.Controls.Add(this.label1);
            this.pnlKpiOredenes.Controls.Add(this.lblKpiOrdenes);
            this.pnlKpiOredenes.Location = new System.Drawing.Point(391, 35);
            this.pnlKpiOredenes.Name = "pnlKpiOredenes";
            this.pnlKpiOredenes.Size = new System.Drawing.Size(239, 80);
            this.pnlKpiOredenes.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(31, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 16);
            this.label1.TabIndex = 76;
            this.label1.Text = "ÓRDENES PROCESADAS";
            // 
            // lblKpiOrdenes
            // 
            this.lblKpiOrdenes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblKpiOrdenes.AutoSize = true;
            this.lblKpiOrdenes.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpiOrdenes.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblKpiOrdenes.Location = new System.Drawing.Point(56, 44);
            this.lblKpiOrdenes.Name = "lblKpiOrdenes";
            this.lblKpiOrdenes.Size = new System.Drawing.Size(106, 22);
            this.lblKpiOrdenes.TabIndex = 75;
            this.lblKpiOrdenes.Text = "0 Órdenes";
            // 
            // pnlKpiIngresos
            // 
            this.pnlKpiIngresos.BackColor = System.Drawing.Color.White;
            this.pnlKpiIngresos.Controls.Add(this.label5);
            this.pnlKpiIngresos.Controls.Add(this.lblKpiIngresos);
            this.pnlKpiIngresos.Location = new System.Drawing.Point(33, 35);
            this.pnlKpiIngresos.Name = "pnlKpiIngresos";
            this.pnlKpiIngresos.Size = new System.Drawing.Size(250, 80);
            this.pnlKpiIngresos.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(37, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 16);
            this.label5.TabIndex = 76;
            this.label5.Text = "INGRESOS TOTALES NETO";
            // 
            // lblKpiIngresos
            // 
            this.lblKpiIngresos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblKpiIngresos.AutoSize = true;
            this.lblKpiIngresos.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKpiIngresos.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblKpiIngresos.Location = new System.Drawing.Point(73, 44);
            this.lblKpiIngresos.Name = "lblKpiIngresos";
            this.lblKpiIngresos.Size = new System.Drawing.Size(83, 22);
            this.lblKpiIngresos.TabIndex = 75;
            this.lblKpiIngresos.Text = "Bs. 0.00";
            // 
            // FrmReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.pnListaProductos);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.pnlWorkspace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReporte";
            this.Text = "FrmReporte";
            this.Load += new System.EventHandler(this.FrmReporte_Load);
            this.pnListaProductos.ResumeLayout(false);
            this.pnListaProductos.PerformLayout();
            this.pnlWorkspace.ResumeLayout(false);
            this.pnlTopProductos.ResumeLayout(false);
            this.pnlTopProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.pnlKpiTiket.ResumeLayout(false);
            this.pnlKpiTiket.PerformLayout();
            this.pnlKpiOredenes.ResumeLayout(false);
            this.pnlKpiOredenes.PerformLayout();
            this.pnlKpiIngresos.ResumeLayout(false);
            this.pnlKpiIngresos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPedidos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Panel pnListaProductos;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Panel pnlWorkspace;
        private System.Windows.Forms.Panel pnlKpiIngresos;
        private System.Windows.Forms.Panel pnlKpiOredenes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblKpiOrdenes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblKpiIngresos;
        private System.Windows.Forms.Panel pnlTopProductos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvReporte;
        private System.Windows.Forms.Panel pnlKpiTiket;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblKpiTicket;
        private System.Windows.Forms.Label lblProd1;
        private System.Windows.Forms.Label lblProd3;
        private System.Windows.Forms.Label lblProd2;
        private System.Windows.Forms.Panel panelBarra3;
        private System.Windows.Forms.Label valP3;
        private System.Windows.Forms.Panel panelBarra2;
        private System.Windows.Forms.Label valP2;
        private System.Windows.Forms.Panel panelBarra1;
        private System.Windows.Forms.Label valP1;
    }
}