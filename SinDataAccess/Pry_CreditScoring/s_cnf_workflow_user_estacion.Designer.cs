namespace Docsis_Application
{
    partial class s_cnf_workflow_user_estacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(s_cnf_workflow_user_estacion));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.list_usuarios = new System.Windows.Forms.ListView();
            this.imagesLarge = new System.Windows.Forms.ImageList(this.components);
            this.imagesSmall = new System.Windows.Forms.ImageList(this.components);
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.comboBoxEstaciones = new System.Windows.Forms.ComboBox();
            this.label_Titulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_quitar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbMiembros_detalle = new System.Windows.Forms.RadioButton();
            this.rbMiembros_titulos = new System.Windows.Forms.RadioButton();
            this.rbMiembros_iconos = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gv_usuarios = new System.Windows.Forms.DataGridView();
            this.img_estado_registro = new System.Windows.Forms.DataGridViewImageColumn();
            this.codigo_usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_agencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTexto_busqueda_todos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pbBuscar_miembros = new System.Windows.Forms.PictureBox();
            this.txtTexto_busqueda_miembros = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_usuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBuscar_miembros)).BeginInit();
            this.SuspendLayout();
            // 
            // list_usuarios
            // 
            this.list_usuarios.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.list_usuarios.AllowColumnReorder = true;
            this.list_usuarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_usuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.list_usuarios.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list_usuarios.HotTracking = true;
            this.list_usuarios.HoverSelection = true;
            this.list_usuarios.Location = new System.Drawing.Point(5, 104);
            this.list_usuarios.MultiSelect = false;
            this.list_usuarios.Name = "list_usuarios";
            this.list_usuarios.ShowItemToolTips = true;
            this.list_usuarios.Size = new System.Drawing.Size(462, 382);
            this.list_usuarios.TabIndex = 9;
            this.list_usuarios.UseCompatibleStateImageBehavior = false;
            this.list_usuarios.View = System.Windows.Forms.View.Tile;
            // 
            // imagesLarge
            // 
            this.imagesLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesLarge.ImageStream")));
            this.imagesLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesLarge.Images.SetKeyName(0, "empleado.png");
            this.imagesLarge.Images.SetKeyName(1, "empleada.png");
            this.imagesLarge.Images.SetKeyName(2, "empleado_nodefinido.png");
            // 
            // imagesSmall
            // 
            this.imagesSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imagesSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.imagesSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.panel9);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.comboBoxEstaciones);
            this.panelTop.Controls.Add(this.label_Titulo);
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1214, 62);
            this.panelTop.TabIndex = 20;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Location = new System.Drawing.Point(0, -9);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1214, 10);
            this.panel9.TabIndex = 295;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(96)))), ((int)(((byte)(66)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::Docsis_Application.Properties.Resources.icon_close01;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.Location = new System.Drawing.Point(1189, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 89;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // comboBoxEstaciones
            // 
            this.comboBoxEstaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEstaciones.FormattingEnabled = true;
            this.comboBoxEstaciones.Location = new System.Drawing.Point(90, 38);
            this.comboBoxEstaciones.Name = "comboBoxEstaciones";
            this.comboBoxEstaciones.Size = new System.Drawing.Size(272, 21);
            this.comboBoxEstaciones.TabIndex = 88;
            this.comboBoxEstaciones.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_estaciones_de_SelectionChangeCommitted);
            // 
            // label_Titulo
            // 
            this.label_Titulo.AutoSize = true;
            this.label_Titulo.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.label_Titulo.Location = new System.Drawing.Point(86, 13);
            this.label_Titulo.Name = "label_Titulo";
            this.label_Titulo.Size = new System.Drawing.Size(250, 22);
            this.label_Titulo.TabIndex = 0;
            this.label_Titulo.Text = "Miembros de la Estación :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.icon_miembros2;
            this.pictureBox1.Location = new System.Drawing.Point(5, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(83, 53);
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // button_quitar
            // 
            this.button_quitar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_quitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_quitar.Image = global::Docsis_Application.Properties.Resources.icon_revoke;
            this.button_quitar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_quitar.Location = new System.Drawing.Point(481, 167);
            this.button_quitar.Name = "button_quitar";
            this.button_quitar.Size = new System.Drawing.Size(123, 55);
            this.button_quitar.TabIndex = 21;
            this.button_quitar.Text = "            Separar";
            this.button_quitar.UseVisualStyleBackColor = false;
            this.button_quitar.Click += new System.EventHandler(this.button_quitar_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::Docsis_Application.Properties.Resources.icon_grant;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(481, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 54);
            this.button1.TabIndex = 22;
            this.button1.Text = "           Agregar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(1, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 15);
            this.label1.TabIndex = 89;
            this.label1.Text = "Miembros actuales :";
            // 
            // rbMiembros_detalle
            // 
            this.rbMiembros_detalle.AutoSize = true;
            this.rbMiembros_detalle.BackColor = System.Drawing.Color.Transparent;
            this.rbMiembros_detalle.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMiembros_detalle.Location = new System.Drawing.Point(235, 79);
            this.rbMiembros_detalle.Name = "rbMiembros_detalle";
            this.rbMiembros_detalle.Size = new System.Drawing.Size(57, 18);
            this.rbMiembros_detalle.TabIndex = 92;
            this.rbMiembros_detalle.Text = "Detalle";
            this.rbMiembros_detalle.UseVisualStyleBackColor = false;
            this.rbMiembros_detalle.CheckedChanged += new System.EventHandler(this.rbComentarios_detalle_CheckedChanged);
            // 
            // rbMiembros_titulos
            // 
            this.rbMiembros_titulos.AutoSize = true;
            this.rbMiembros_titulos.BackColor = System.Drawing.Color.Transparent;
            this.rbMiembros_titulos.Checked = true;
            this.rbMiembros_titulos.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMiembros_titulos.Location = new System.Drawing.Point(179, 79);
            this.rbMiembros_titulos.Name = "rbMiembros_titulos";
            this.rbMiembros_titulos.Size = new System.Drawing.Size(56, 18);
            this.rbMiembros_titulos.TabIndex = 91;
            this.rbMiembros_titulos.TabStop = true;
            this.rbMiembros_titulos.Text = "Titulos";
            this.rbMiembros_titulos.UseVisualStyleBackColor = false;
            this.rbMiembros_titulos.CheckedChanged += new System.EventHandler(this.rbComentarios_titulos_CheckedChanged);
            // 
            // rbMiembros_iconos
            // 
            this.rbMiembros_iconos.AutoSize = true;
            this.rbMiembros_iconos.BackColor = System.Drawing.Color.Transparent;
            this.rbMiembros_iconos.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMiembros_iconos.Location = new System.Drawing.Point(122, 79);
            this.rbMiembros_iconos.Name = "rbMiembros_iconos";
            this.rbMiembros_iconos.Size = new System.Drawing.Size(57, 18);
            this.rbMiembros_iconos.TabIndex = 90;
            this.rbMiembros_iconos.Text = "Iconos";
            this.rbMiembros_iconos.UseVisualStyleBackColor = false;
            this.rbMiembros_iconos.CheckedChanged += new System.EventHandler(this.rbComentarios_iconos_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Location = new System.Drawing.Point(0, 495);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1214, 22);
            this.statusStrip1.TabIndex = 93;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gv_usuarios
            // 
            this.gv_usuarios.AllowUserToAddRows = false;
            this.gv_usuarios.AllowUserToDeleteRows = false;
            this.gv_usuarios.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gv_usuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gv_usuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_usuarios.BackgroundColor = System.Drawing.Color.White;
            this.gv_usuarios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gv_usuarios.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gv_usuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv_usuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gv_usuarios.ColumnHeadersHeight = 20;
            this.gv_usuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.img_estado_registro,
            this.codigo_usuario,
            this.codigo_cliente,
            this.nombres,
            this.Estacion,
            this.nombre_agencia});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gv_usuarios.DefaultCellStyle = dataGridViewCellStyle5;
            this.gv_usuarios.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gv_usuarios.Location = new System.Drawing.Point(611, 104);
            this.gv_usuarios.MultiSelect = false;
            this.gv_usuarios.Name = "gv_usuarios";
            this.gv_usuarios.ReadOnly = true;
            this.gv_usuarios.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gv_usuarios.RowHeadersVisible = false;
            this.gv_usuarios.RowHeadersWidth = 10;
            this.gv_usuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_usuarios.Size = new System.Drawing.Size(590, 383);
            this.gv_usuarios.TabIndex = 94;
            this.gv_usuarios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_usuarios_CellContentClick);
            // 
            // img_estado_registro
            // 
            this.img_estado_registro.DataPropertyName = "img_estado_registro";
            this.img_estado_registro.HeaderText = "";
            this.img_estado_registro.Name = "img_estado_registro";
            this.img_estado_registro.ReadOnly = true;
            this.img_estado_registro.Visible = false;
            this.img_estado_registro.Width = 15;
            // 
            // codigo_usuario
            // 
            this.codigo_usuario.DataPropertyName = "codigo_usuario";
            this.codigo_usuario.HeaderText = "Usuario";
            this.codigo_usuario.Name = "codigo_usuario";
            this.codigo_usuario.ReadOnly = true;
            // 
            // codigo_cliente
            // 
            this.codigo_cliente.DataPropertyName = "codigo_cliente";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = null;
            this.codigo_cliente.DefaultCellStyle = dataGridViewCellStyle3;
            this.codigo_cliente.HeaderText = "Codigo Cliente";
            this.codigo_cliente.Name = "codigo_cliente";
            this.codigo_cliente.ReadOnly = true;
            this.codigo_cliente.Width = 80;
            // 
            // nombres
            // 
            this.nombres.DataPropertyName = "nombres";
            this.nombres.HeaderText = "Nombre Empleado";
            this.nombres.Name = "nombres";
            this.nombres.ReadOnly = true;
            this.nombres.Width = 220;
            // 
            // Estacion
            // 
            this.Estacion.DataPropertyName = "estacion_actual";
            this.Estacion.HeaderText = "Estacion Actual";
            this.Estacion.Name = "Estacion";
            this.Estacion.ReadOnly = true;
            this.Estacion.Width = 200;
            // 
            // nombre_agencia
            // 
            this.nombre_agencia.DataPropertyName = "nombre_agencia";
            dataGridViewCellStyle4.Format = "f";
            dataGridViewCellStyle4.NullValue = null;
            this.nombre_agencia.DefaultCellStyle = dataGridViewCellStyle4;
            this.nombre_agencia.HeaderText = "Filial Actual";
            this.nombre_agencia.Name = "nombre_agencia";
            this.nombre_agencia.ReadOnly = true;
            this.nombre_agencia.Width = 250;
            // 
            // txtTexto_busqueda_todos
            // 
            this.txtTexto_busqueda_todos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTexto_busqueda_todos.Location = new System.Drawing.Point(803, 79);
            this.txtTexto_busqueda_todos.Name = "txtTexto_busqueda_todos";
            this.txtTexto_busqueda_todos.Size = new System.Drawing.Size(363, 20);
            this.txtTexto_busqueda_todos.TabIndex = 95;
            this.txtTexto_busqueda_todos.TextChanged += new System.EventHandler(this.txtTexto_busqueda_todos_TextChanged);
            this.txtTexto_busqueda_todos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTexto_busqueda_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(608, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 15);
            this.label2.TabIndex = 96;
            this.label2.Text = "Buscar en todos los empleados :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::Docsis_Application.Properties.Resources.buscar20x20;
            this.pictureBox2.Location = new System.Drawing.Point(1167, 76);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 23);
            this.pictureBox2.TabIndex = 97;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pbBuscar_miembros
            // 
            this.pbBuscar_miembros.BackColor = System.Drawing.Color.Transparent;
            this.pbBuscar_miembros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBuscar_miembros.Image = global::Docsis_Application.Properties.Resources.buscar20x20;
            this.pbBuscar_miembros.Location = new System.Drawing.Point(455, 75);
            this.pbBuscar_miembros.Name = "pbBuscar_miembros";
            this.pbBuscar_miembros.Size = new System.Drawing.Size(24, 23);
            this.pbBuscar_miembros.TabIndex = 99;
            this.pbBuscar_miembros.TabStop = false;
            this.pbBuscar_miembros.Click += new System.EventHandler(this.pbBuscar_miembros_Click);
            // 
            // txtTexto_busqueda_miembros
            // 
            this.txtTexto_busqueda_miembros.Location = new System.Drawing.Point(291, 78);
            this.txtTexto_busqueda_miembros.Name = "txtTexto_busqueda_miembros";
            this.txtTexto_busqueda_miembros.Size = new System.Drawing.Size(158, 20);
            this.txtTexto_busqueda_miembros.TabIndex = 98;
            this.txtTexto_busqueda_miembros.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTexto_busqueda_miembros_KeyPress);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(1213, -2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5, 519);
            this.panel5.TabIndex = 286;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(-4, -1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(5, 518);
            this.panel6.TabIndex = 287;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Location = new System.Drawing.Point(0, 516);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1214, 10);
            this.panel7.TabIndex = 288;
            // 
            // s_cnf_workflow_user_estacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(1214, 517);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pbBuscar_miembros);
            this.Controls.Add(this.txtTexto_busqueda_miembros);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTexto_busqueda_todos);
            this.Controls.Add(this.gv_usuarios);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.rbMiembros_detalle);
            this.Controls.Add(this.rbMiembros_titulos);
            this.Controls.Add(this.rbMiembros_iconos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_quitar);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.list_usuarios);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_cnf_workflow_user_estacion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::: Configuración de miembros de las estaciones";
            this.Load += new System.EventHandler(this.s_cnf_workflow_user_estacion_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_usuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBuscar_miembros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListView list_usuarios;
        internal System.Windows.Forms.ImageList imagesLarge;
        internal System.Windows.Forms.ImageList imagesSmall;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label_Titulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_quitar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxEstaciones;
        private System.Windows.Forms.RadioButton rbMiembros_detalle;
        private System.Windows.Forms.RadioButton rbMiembros_titulos;
        private System.Windows.Forms.RadioButton rbMiembros_iconos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView gv_usuarios;
        private System.Windows.Forms.TextBox txtTexto_busqueda_todos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridViewImageColumn img_estado_registro;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombres;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_agencia;
        private System.Windows.Forms.PictureBox pbBuscar_miembros;
        private System.Windows.Forms.TextBox txtTexto_busqueda_miembros;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel9;
    }
}