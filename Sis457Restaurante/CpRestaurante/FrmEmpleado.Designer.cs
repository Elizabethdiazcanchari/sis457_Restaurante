namespace CpRestaurante
{
    partial class FrmEmpleado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmpleado));
            this.pctBuscar = new System.Windows.Forms.PictureBox();
            this.lblEmpleados = new System.Windows.Forms.Label();
            this.btnCerrarAgregar = new System.Windows.Forms.Button();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.pnlAgregar = new System.Windows.Forms.Panel();
            this.lblAgregarEmpleados = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cbxCargo = new System.Windows.Forms.ComboBox();
            this.lblCargo = new System.Windows.Forms.Label();
            this.txtCelular = new System.Windows.Forms.TextBox();
            this.lblCelular = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtSegundoApellido = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblSegundoApellido = new System.Windows.Forms.Label();
            this.lblPrimerApellido = new System.Windows.Forms.Label();
            this.lblNombres = new System.Windows.Forms.Label();
            this.lblCedulaIdentidad = new System.Windows.Forms.Label();
            this.txtCedulaIdentidad = new System.Windows.Forms.TextBox();
            this.txtPrimerApellido = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.erpDireccion = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnEditar = new System.Windows.Forms.Button();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.pnListaEmpleados = new System.Windows.Forms.Panel();
            this.erpCedulaIdentidad = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnEliminar = new System.Windows.Forms.Button();
            this.erpApellidos = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpCelular = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpCargo = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpNombres = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnAgregar = new System.Windows.Forms.Button();
            this.erpFechaNacimiento = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pctBuscar)).BeginInit();
            this.pnlAgregar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpDireccion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.pnListaEmpleados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpCedulaIdentidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpApellidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCelular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCargo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpNombres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpFechaNacimiento)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBuscar
            // 
            this.pctBuscar.Image = ((System.Drawing.Image)(resources.GetObject("pctBuscar.Image")));
            this.pctBuscar.InitialImage = null;
            this.pctBuscar.Location = new System.Drawing.Point(65, 9);
            this.pctBuscar.Name = "pctBuscar";
            this.pctBuscar.Size = new System.Drawing.Size(301, 30);
            this.pctBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctBuscar.TabIndex = 39;
            this.pctBuscar.TabStop = false;
            // 
            // lblEmpleados
            // 
            this.lblEmpleados.AutoSize = true;
            this.lblEmpleados.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleados.ForeColor = System.Drawing.Color.White;
            this.lblEmpleados.Location = new System.Drawing.Point(3, 0);
            this.lblEmpleados.Name = "lblEmpleados";
            this.lblEmpleados.Size = new System.Drawing.Size(315, 38);
            this.lblEmpleados.TabIndex = 5;
            this.lblEmpleados.Text = "Lista de Empleados";
            // 
            // btnCerrarAgregar
            // 
            this.btnCerrarAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrarAgregar.BackColor = System.Drawing.Color.DarkRed;
            this.btnCerrarAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarAgregar.FlatAppearance.BorderSize = 0;
            this.btnCerrarAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.btnCerrarAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarAgregar.Image")));
            this.btnCerrarAgregar.Location = new System.Drawing.Point(392, 9);
            this.btnCerrarAgregar.Margin = new System.Windows.Forms.Padding(0);
            this.btnCerrarAgregar.Name = "btnCerrarAgregar";
            this.btnCerrarAgregar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrarAgregar.TabIndex = 24;
            this.btnCerrarAgregar.UseVisualStyleBackColor = false;
            this.btnCerrarAgregar.Click += new System.EventHandler(this.btnCerrarAgregar_Click);
            // 
            // txtNombres
            // 
            this.txtNombres.Location = new System.Drawing.Point(190, 127);
            this.txtNombres.MaxLength = 30;
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(208, 20);
            this.txtNombres.TabIndex = 21;
            // 
            // pnlAgregar
            // 
            this.pnlAgregar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlAgregar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAgregar.Controls.Add(this.lblAgregarEmpleados);
            this.pnlAgregar.Controls.Add(this.dtpFechaNacimiento);
            this.pnlAgregar.Controls.Add(this.lblFechaNacimiento);
            this.pnlAgregar.Controls.Add(this.txtUsuario);
            this.pnlAgregar.Controls.Add(this.lblUsuario);
            this.pnlAgregar.Controls.Add(this.txtBuscar);
            this.pnlAgregar.Controls.Add(this.cbxCargo);
            this.pnlAgregar.Controls.Add(this.lblCargo);
            this.pnlAgregar.Controls.Add(this.txtCelular);
            this.pnlAgregar.Controls.Add(this.lblCelular);
            this.pnlAgregar.Controls.Add(this.txtDireccion);
            this.pnlAgregar.Controls.Add(this.txtSegundoApellido);
            this.pnlAgregar.Controls.Add(this.btnCerrarAgregar);
            this.pnlAgregar.Controls.Add(this.btnCancelar);
            this.pnlAgregar.Controls.Add(this.lblDireccion);
            this.pnlAgregar.Controls.Add(this.lblSegundoApellido);
            this.pnlAgregar.Controls.Add(this.lblPrimerApellido);
            this.pnlAgregar.Controls.Add(this.lblNombres);
            this.pnlAgregar.Controls.Add(this.lblCedulaIdentidad);
            this.pnlAgregar.Controls.Add(this.txtCedulaIdentidad);
            this.pnlAgregar.Controls.Add(this.txtNombres);
            this.pnlAgregar.Controls.Add(this.txtPrimerApellido);
            this.pnlAgregar.Controls.Add(this.btnGuardar);
            this.pnlAgregar.Controls.Add(this.pctBuscar);
            this.pnlAgregar.Location = new System.Drawing.Point(319, 51);
            this.pnlAgregar.Name = "pnlAgregar";
            this.pnlAgregar.Size = new System.Drawing.Size(427, 523);
            this.pnlAgregar.TabIndex = 37;
            // 
            // lblAgregarEmpleados
            // 
            this.lblAgregarEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblAgregarEmpleados.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAgregarEmpleados.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgregarEmpleados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(115)))), ((int)(((byte)(22)))));
            this.lblAgregarEmpleados.Location = new System.Drawing.Point(0, 0);
            this.lblAgregarEmpleados.Name = "lblAgregarEmpleados";
            this.lblAgregarEmpleados.Size = new System.Drawing.Size(425, 76);
            this.lblAgregarEmpleados.TabIndex = 30;
            this.lblAgregarEmpleados.Text = "AGREGAR EMPLEADOS";
            this.lblAgregarEmpleados.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(190, 234);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(208, 20);
            this.dtpFechaNacimiento.TabIndex = 44;
            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.AutoSize = true;
            this.lblFechaNacimiento.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblFechaNacimiento.Location = new System.Drawing.Point(12, 236);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(172, 18);
            this.lblFechaNacimiento.TabIndex = 43;
            this.lblFechaNacimiento.Text = "Fecha de Nacimiento:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(190, 344);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(208, 20);
            this.txtUsuario.TabIndex = 42;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lblUsuario.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblUsuario.Location = new System.Drawing.Point(117, 344);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(66, 18);
            this.lblUsuario.TabIndex = 41;
            this.lblUsuario.Text = "Usuario:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscar.ForeColor = System.Drawing.Color.DimGray;
            this.txtBuscar.Location = new System.Drawing.Point(78, 19);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(268, 13);
            this.txtBuscar.TabIndex = 38;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // cbxCargo
            // 
            this.cbxCargo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCargo.FormattingEnabled = true;
            this.cbxCargo.Items.AddRange(new object[] {
            "Administrador",
            "Cajero",
            "Cocinero",
            "Mesero",
            "Practicante"});
            this.cbxCargo.Location = new System.Drawing.Point(190, 303);
            this.cbxCargo.Name = "cbxCargo";
            this.cbxCargo.Size = new System.Drawing.Size(208, 21);
            this.cbxCargo.TabIndex = 40;
            // 
            // lblCargo
            // 
            this.lblCargo.AutoSize = true;
            this.lblCargo.BackColor = System.Drawing.Color.Transparent;
            this.lblCargo.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCargo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCargo.Location = new System.Drawing.Point(124, 303);
            this.lblCargo.Name = "lblCargo";
            this.lblCargo.Size = new System.Drawing.Size(59, 18);
            this.lblCargo.TabIndex = 39;
            this.lblCargo.Text = "Cargo:";
            // 
            // txtCelular
            // 
            this.txtCelular.Location = new System.Drawing.Point(190, 267);
            this.txtCelular.MaxLength = 8;
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.Size = new System.Drawing.Size(208, 20);
            this.txtCelular.TabIndex = 38;
            // 
            // lblCelular
            // 
            this.lblCelular.AutoSize = true;
            this.lblCelular.BackColor = System.Drawing.Color.Transparent;
            this.lblCelular.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCelular.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCelular.Location = new System.Drawing.Point(119, 265);
            this.lblCelular.Name = "lblCelular";
            this.lblCelular.Size = new System.Drawing.Size(66, 18);
            this.lblCelular.TabIndex = 37;
            this.lblCelular.Text = "Celular:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(190, 376);
            this.txtDireccion.MaxLength = 250;
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(208, 72);
            this.txtDireccion.TabIndex = 36;
            // 
            // txtSegundoApellido
            // 
            this.txtSegundoApellido.Location = new System.Drawing.Point(190, 201);
            this.txtSegundoApellido.MaxLength = 30;
            this.txtSegundoApellido.Name = "txtSegundoApellido";
            this.txtSegundoApellido.Size = new System.Drawing.Size(208, 20);
            this.txtSegundoApellido.TabIndex = 35;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Maroon;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(236, 473);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 30);
            this.btnCancelar.TabIndex = 34;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.BackColor = System.Drawing.Color.Transparent;
            this.lblDireccion.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblDireccion.Location = new System.Drawing.Point(100, 381);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(85, 18);
            this.lblDireccion.TabIndex = 28;
            this.lblDireccion.Text = "Direccion:";
            // 
            // lblSegundoApellido
            // 
            this.lblSegundoApellido.AutoSize = true;
            this.lblSegundoApellido.BackColor = System.Drawing.Color.Transparent;
            this.lblSegundoApellido.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSegundoApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSegundoApellido.Location = new System.Drawing.Point(42, 199);
            this.lblSegundoApellido.Name = "lblSegundoApellido";
            this.lblSegundoApellido.Size = new System.Drawing.Size(145, 18);
            this.lblSegundoApellido.TabIndex = 27;
            this.lblSegundoApellido.Text = "Segundo Apellido:";
            // 
            // lblPrimerApellido
            // 
            this.lblPrimerApellido.AutoSize = true;
            this.lblPrimerApellido.BackColor = System.Drawing.Color.Transparent;
            this.lblPrimerApellido.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimerApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblPrimerApellido.Location = new System.Drawing.Point(62, 165);
            this.lblPrimerApellido.Name = "lblPrimerApellido";
            this.lblPrimerApellido.Size = new System.Drawing.Size(125, 18);
            this.lblPrimerApellido.TabIndex = 26;
            this.lblPrimerApellido.Text = "Primer Apellido:";
            // 
            // lblNombres
            // 
            this.lblNombres.AutoSize = true;
            this.lblNombres.BackColor = System.Drawing.Color.Transparent;
            this.lblNombres.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblNombres.Location = new System.Drawing.Point(106, 127);
            this.lblNombres.Name = "lblNombres";
            this.lblNombres.Size = new System.Drawing.Size(78, 18);
            this.lblNombres.TabIndex = 25;
            this.lblNombres.Text = "Nombres:";
            // 
            // lblCedulaIdentidad
            // 
            this.lblCedulaIdentidad.AutoSize = true;
            this.lblCedulaIdentidad.BackColor = System.Drawing.Color.Transparent;
            this.lblCedulaIdentidad.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCedulaIdentidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCedulaIdentidad.Location = new System.Drawing.Point(17, 89);
            this.lblCedulaIdentidad.Name = "lblCedulaIdentidad";
            this.lblCedulaIdentidad.Size = new System.Drawing.Size(166, 18);
            this.lblCedulaIdentidad.TabIndex = 23;
            this.lblCedulaIdentidad.Text = "Cedula de Identidad:";
            // 
            // txtCedulaIdentidad
            // 
            this.txtCedulaIdentidad.BackColor = System.Drawing.SystemColors.Window;
            this.txtCedulaIdentidad.Location = new System.Drawing.Point(190, 89);
            this.txtCedulaIdentidad.MaxLength = 12;
            this.txtCedulaIdentidad.Name = "txtCedulaIdentidad";
            this.txtCedulaIdentidad.Size = new System.Drawing.Size(208, 20);
            this.txtCedulaIdentidad.TabIndex = 22;
            // 
            // txtPrimerApellido
            // 
            this.txtPrimerApellido.Location = new System.Drawing.Point(190, 165);
            this.txtPrimerApellido.MaxLength = 30;
            this.txtPrimerApellido.Name = "txtPrimerApellido";
            this.txtPrimerApellido.Size = new System.Drawing.Size(208, 20);
            this.txtPrimerApellido.TabIndex = 20;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(90, 473);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(0);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 30);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // erpDireccion
            // 
            this.erpDireccion.ContainerControl = this;
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Yellow;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(113, 61);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnEditar.Size = new System.Drawing.Size(97, 30);
            this.btnEditar.TabIndex = 35;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.AllowUserToAddRows = false;
            this.dgvEmpleados.AllowUserToDeleteRows = false;
            this.dgvEmpleados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmpleados.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleados.Location = new System.Drawing.Point(8, 114);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.ReadOnly = true;
            this.dgvEmpleados.RowHeadersWidth = 51;
            this.dgvEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpleados.Size = new System.Drawing.Size(1028, 455);
            this.dgvEmpleados.TabIndex = 36;
            // 
            // pnListaEmpleados
            // 
            this.pnListaEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnListaEmpleados.Controls.Add(this.lblEmpleados);
            this.pnListaEmpleados.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnListaEmpleados.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnListaEmpleados.Location = new System.Drawing.Point(0, 0);
            this.pnListaEmpleados.Name = "pnListaEmpleados";
            this.pnListaEmpleados.Size = new System.Drawing.Size(1034, 49);
            this.pnListaEmpleados.TabIndex = 32;
            // 
            // erpCedulaIdentidad
            // 
            this.erpCedulaIdentidad.ContainerControl = this;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(216, 61);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(97, 30);
            this.btnEliminar.TabIndex = 34;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // erpApellidos
            // 
            this.erpApellidos.ContainerControl = this;
            // 
            // erpCelular
            // 
            this.erpCelular.ContainerControl = this;
            // 
            // erpCargo
            // 
            this.erpCargo.ContainerControl = this;
            // 
            // erpNombres
            // 
            this.erpNombres.ContainerControl = this;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(10, 61);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(97, 30);
            this.btnAgregar.TabIndex = 33;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // erpFechaNacimiento
            // 
            this.erpFechaNacimiento.ContainerControl = this;
            // 
            // FrmEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 561);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.pnListaEmpleados);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.pnlAgregar);
            this.Controls.Add(this.dgvEmpleados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmEmpleado";
            this.Text = "FrmEmpleado";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmEmpleado_FormClosing);
            this.Load += new System.EventHandler(this.FrmEmpleado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctBuscar)).EndInit();
            this.pnlAgregar.ResumeLayout(false);
            this.pnlAgregar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpDireccion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.pnListaEmpleados.ResumeLayout(false);
            this.pnListaEmpleados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpCedulaIdentidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpApellidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCelular)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCargo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpNombres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpFechaNacimiento)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBuscar;
        private System.Windows.Forms.Label lblEmpleados;
        private System.Windows.Forms.Button btnCerrarAgregar;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.Panel pnlAgregar;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cbxCargo;
        private System.Windows.Forms.Label lblCargo;
        private System.Windows.Forms.TextBox txtCelular;
        private System.Windows.Forms.Label lblCelular;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtSegundoApellido;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblAgregarEmpleados;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblSegundoApellido;
        private System.Windows.Forms.Label lblPrimerApellido;
        private System.Windows.Forms.Label lblNombres;
        private System.Windows.Forms.Label lblCedulaIdentidad;
        private System.Windows.Forms.TextBox txtCedulaIdentidad;
        private System.Windows.Forms.TextBox txtPrimerApellido;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ErrorProvider erpDireccion;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.Panel pnListaEmpleados;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ErrorProvider erpCedulaIdentidad;
        private System.Windows.Forms.ErrorProvider erpApellidos;
        private System.Windows.Forms.ErrorProvider erpCelular;
        private System.Windows.Forms.ErrorProvider erpCargo;
        private System.Windows.Forms.ErrorProvider erpNombres;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblFechaNacimiento;
        private System.Windows.Forms.ErrorProvider erpFechaNacimiento;
    }
}