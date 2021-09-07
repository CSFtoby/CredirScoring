namespace Docsis_Application
{
    partial class s_buscar_sol
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvSolicitudes = new System.Windows.Forms.DataGridView();
            this.No_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc_sub_aplicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultado_buro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_agencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oficial_servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.analista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion_fuente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc_moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto_solicitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meses_plazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estacion_actual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no_movimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMaximizar = new System.Windows.Forms.Button();
            this.pboxLoading02 = new System.Windows.Forms.PictureBox();
            this.label_Titulo_lista = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTexto_busqueda = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_cerrar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pboxLoading = new System.Windows.Forms.PictureBox();
            this.button_ejecutar = new System.Windows.Forms.Button();
            this.radioButton_abiertas = new System.Windows.Forms.RadioButton();
            this.radioButton_cerradas = new System.Windows.Forms.RadioButton();
            this.radioButton_todas = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCampos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox_wf = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBox_vistarapida = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvSolicitudes)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // gvSolicitudes
            // 
            this.gvSolicitudes.AllowUserToAddRows = false;
            this.gvSolicitudes.AllowUserToDeleteRows = false;
            this.gvSolicitudes.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.gvSolicitudes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.gvSolicitudes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvSolicitudes.BackgroundColor = System.Drawing.Color.White;
            this.gvSolicitudes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvSolicitudes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSolicitudes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.gvSolicitudes.ColumnHeadersHeight = 20;
            this.gvSolicitudes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No_solicitud,
            this.estado_solicitud,
            this.desc_sub_aplicacion,
            this.fecha,
            this.resultado_buro,
            this.nombre_agencia,
            this.oficial_servicio,
            this.analista,
            this.descripcion_fuente,
            this.codigo_cliente,
            this.nombre_cliente,
            this.desc_moneda,
            this.Monto_solicitado,
            this.meses_plazo,
            this.estacion_actual,
            this.no_movimiento});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvSolicitudes.DefaultCellStyle = dataGridViewCellStyle16;
            this.gvSolicitudes.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvSolicitudes.Location = new System.Drawing.Point(8, 141);
            this.gvSolicitudes.Name = "gvSolicitudes";
            this.gvSolicitudes.ReadOnly = true;
            this.gvSolicitudes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvSolicitudes.RowHeadersVisible = false;
            this.gvSolicitudes.RowHeadersWidth = 10;
            this.gvSolicitudes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvSolicitudes.Size = new System.Drawing.Size(1199, 327);
            this.gvSolicitudes.TabIndex = 12;
            this.gvSolicitudes.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSolicitudes_CellMouseLeave);
            this.gvSolicitudes.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvSolicitudes_CellMouseMove);
            this.gvSolicitudes.DoubleClick += new System.EventHandler(this.gvSolicitudes_DoubleClick);
            // 
            // No_solicitud
            // 
            this.No_solicitud.DataPropertyName = "No_solicitud";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.No_solicitud.DefaultCellStyle = dataGridViewCellStyle11;
            this.No_solicitud.HeaderText = "No. Solicitud";
            this.No_solicitud.Name = "No_solicitud";
            this.No_solicitud.ReadOnly = true;
            this.No_solicitud.Width = 80;
            // 
            // estado_solicitud
            // 
            this.estado_solicitud.DataPropertyName = "estado_solicitud";
            this.estado_solicitud.HeaderText = "Estado Solicitud";
            this.estado_solicitud.Name = "estado_solicitud";
            this.estado_solicitud.ReadOnly = true;
            this.estado_solicitud.Width = 140;
            // 
            // desc_sub_aplicacion
            // 
            this.desc_sub_aplicacion.DataPropertyName = "desc_sub_aplicacion";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.desc_sub_aplicacion.DefaultCellStyle = dataGridViewCellStyle12;
            this.desc_sub_aplicacion.HeaderText = "Producto";
            this.desc_sub_aplicacion.Name = "desc_sub_aplicacion";
            this.desc_sub_aplicacion.ReadOnly = true;
            this.desc_sub_aplicacion.Width = 160;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "fecha_presentacion";
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Width = 140;
            // 
            // resultado_buro
            // 
            this.resultado_buro.DataPropertyName = "resultado_buro";
            this.resultado_buro.HeaderText = "Resultado Buro";
            this.resultado_buro.Name = "resultado_buro";
            this.resultado_buro.ReadOnly = true;
            this.resultado_buro.Width = 140;
            // 
            // nombre_agencia
            // 
            this.nombre_agencia.DataPropertyName = "nombre_agencia";
            this.nombre_agencia.HeaderText = "Filial Remitente";
            this.nombre_agencia.Name = "nombre_agencia";
            this.nombre_agencia.ReadOnly = true;
            this.nombre_agencia.Width = 150;
            // 
            // oficial_servicio
            // 
            this.oficial_servicio.DataPropertyName = "oficial_servicio";
            this.oficial_servicio.HeaderText = "Oficial de Servicio";
            this.oficial_servicio.Name = "oficial_servicio";
            this.oficial_servicio.ReadOnly = true;
            this.oficial_servicio.Width = 150;
            // 
            // analista
            // 
            this.analista.DataPropertyName = "analista";
            this.analista.HeaderText = "Analista";
            this.analista.Name = "analista";
            this.analista.ReadOnly = true;
            // 
            // descripcion_fuente
            // 
            this.descripcion_fuente.DataPropertyName = "descripcion_fuente";
            this.descripcion_fuente.HeaderText = "Fuente Financiamiento";
            this.descripcion_fuente.Name = "descripcion_fuente";
            this.descripcion_fuente.ReadOnly = true;
            this.descripcion_fuente.Width = 180;
            // 
            // codigo_cliente
            // 
            this.codigo_cliente.DataPropertyName = "codigo_cliente";
            this.codigo_cliente.HeaderText = "Codigo Cliente";
            this.codigo_cliente.Name = "codigo_cliente";
            this.codigo_cliente.ReadOnly = true;
            // 
            // nombre_cliente
            // 
            this.nombre_cliente.DataPropertyName = "nombre_cliente";
            this.nombre_cliente.HeaderText = "Nombre del Cliente";
            this.nombre_cliente.Name = "nombre_cliente";
            this.nombre_cliente.ReadOnly = true;
            this.nombre_cliente.Width = 300;
            // 
            // desc_moneda
            // 
            this.desc_moneda.DataPropertyName = "desc_moneda";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.desc_moneda.DefaultCellStyle = dataGridViewCellStyle13;
            this.desc_moneda.HeaderText = "Moneda";
            this.desc_moneda.Name = "desc_moneda";
            this.desc_moneda.ReadOnly = true;
            // 
            // Monto_solicitado
            // 
            this.Monto_solicitado.DataPropertyName = "Monto_solicitado";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            this.Monto_solicitado.DefaultCellStyle = dataGridViewCellStyle14;
            this.Monto_solicitado.HeaderText = "Monto Solicitado";
            this.Monto_solicitado.Name = "Monto_solicitado";
            this.Monto_solicitado.ReadOnly = true;
            // 
            // meses_plazo
            // 
            this.meses_plazo.DataPropertyName = "meses_plazo";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.meses_plazo.DefaultCellStyle = dataGridViewCellStyle15;
            this.meses_plazo.HeaderText = "Plazo Meses";
            this.meses_plazo.Name = "meses_plazo";
            this.meses_plazo.ReadOnly = true;
            // 
            // estacion_actual
            // 
            this.estacion_actual.DataPropertyName = "estacion_actual";
            this.estacion_actual.HeaderText = "Estacion Actual";
            this.estacion_actual.Name = "estacion_actual";
            this.estacion_actual.ReadOnly = true;
            this.estacion_actual.Width = 250;
            // 
            // no_movimiento
            // 
            this.no_movimiento.DataPropertyName = "no_movimiento";
            this.no_movimiento.HeaderText = "No Movimiento actual";
            this.no_movimiento.Name = "no_movimiento";
            this.no_movimiento.ReadOnly = true;
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.btnMaximizar);
            this.panelTop.Controls.Add(this.pboxLoading02);
            this.panelTop.Controls.Add(this.label_Titulo_lista);
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Location = new System.Drawing.Point(-2, -2);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1216, 52);
            this.panelTop.TabIndex = 12;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(100)))), ((int)(((byte)(74)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::Docsis_Application.Properties.Resources.icon_close01;
            this.btnClose.Location = new System.Drawing.Point(1192, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 213;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximizar.FlatAppearance.BorderSize = 0;
            this.btnMaximizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(100)))), ((int)(((byte)(74)))));
            this.btnMaximizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximizar.ForeColor = System.Drawing.Color.White;
            this.btnMaximizar.Image = global::Docsis_Application.Properties.Resources.maximizar_icon;
            this.btnMaximizar.Location = new System.Drawing.Point(1177, 8);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(15, 16);
            this.btnMaximizar.TabIndex = 212;
            this.btnMaximizar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMaximizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnMaximizar.UseVisualStyleBackColor = false;
            this.btnMaximizar.Click += new System.EventHandler(this.btnMaximizar_Click);
            // 
            // pboxLoading02
            // 
            this.pboxLoading02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pboxLoading02.Image = global::Docsis_Application.Properties.Resources._303;
            this.pboxLoading02.Location = new System.Drawing.Point(1116, 6);
            this.pboxLoading02.Name = "pboxLoading02";
            this.pboxLoading02.Size = new System.Drawing.Size(44, 41);
            this.pboxLoading02.TabIndex = 85;
            this.pboxLoading02.TabStop = false;
            this.pboxLoading02.Visible = false;
            // 
            // label_Titulo_lista
            // 
            this.label_Titulo_lista.AutoSize = true;
            this.label_Titulo_lista.BackColor = System.Drawing.Color.Transparent;
            this.label_Titulo_lista.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Titulo_lista.ForeColor = System.Drawing.Color.Silver;
            this.label_Titulo_lista.Location = new System.Drawing.Point(41, 13);
            this.label_Titulo_lista.Name = "label_Titulo_lista";
            this.label_Titulo_lista.Size = new System.Drawing.Size(170, 24);
            this.label_Titulo_lista.TabIndex = 0;
            this.label_Titulo_lista.Text = "Buscar Solicitudes";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.lupa_icon1;
            this.pictureBox1.Location = new System.Drawing.Point(6, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 25);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(9, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Criterios de busqueda";
            // 
            // txtTexto_busqueda
            // 
            this.txtTexto_busqueda.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTexto_busqueda.Location = new System.Drawing.Point(122, 81);
            this.txtTexto_busqueda.Name = "txtTexto_busqueda";
            this.txtTexto_busqueda.Size = new System.Drawing.Size(215, 20);
            this.txtTexto_busqueda.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(17, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Texto de busqueda :";
            // 
            // button_cerrar
            // 
            this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_cerrar.FlatAppearance.BorderSize = 0;
            this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cerrar.ForeColor = System.Drawing.Color.White;
            this.button_cerrar.Location = new System.Drawing.Point(585, 488);
            this.button_cerrar.Name = "button_cerrar";
            this.button_cerrar.Size = new System.Drawing.Size(115, 33);
            this.button_cerrar.TabIndex = 11;
            this.button_cerrar.Text = "&Cerrar";
            this.button_cerrar.UseVisualStyleBackColor = false;
            this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Location = new System.Drawing.Point(122, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(100, 2);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Location = new System.Drawing.Point(0, 537);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1212, 22);
            this.statusStrip1.TabIndex = 86;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pboxLoading
            // 
            this.pboxLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pboxLoading.BackColor = System.Drawing.Color.Transparent;
            this.pboxLoading.Image = global::Docsis_Application.Properties.Resources.loading1;
            this.pboxLoading.Location = new System.Drawing.Point(0, 539);
            this.pboxLoading.Name = "pboxLoading";
            this.pboxLoading.Size = new System.Drawing.Size(18, 19);
            this.pboxLoading.TabIndex = 84;
            this.pboxLoading.TabStop = false;
            this.pboxLoading.Visible = false;
            // 
            // button_ejecutar
            // 
            this.button_ejecutar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ejecutar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_ejecutar.FlatAppearance.BorderSize = 0;
            this.button_ejecutar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ejecutar.ForeColor = System.Drawing.Color.White;
            this.button_ejecutar.Image = global::Docsis_Application.Properties.Resources.lupa_icon1;
            this.button_ejecutar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_ejecutar.Location = new System.Drawing.Point(1072, 100);
            this.button_ejecutar.Name = "button_ejecutar";
            this.button_ejecutar.Size = new System.Drawing.Size(128, 33);
            this.button_ejecutar.TabIndex = 3;
            this.button_ejecutar.Text = "&Buscar";
            this.button_ejecutar.UseVisualStyleBackColor = false;
            this.button_ejecutar.Click += new System.EventHandler(this.button_ejecutar_Click);
            // 
            // radioButton_abiertas
            // 
            this.radioButton_abiertas.AutoSize = true;
            this.radioButton_abiertas.Location = new System.Drawing.Point(185, 114);
            this.radioButton_abiertas.Name = "radioButton_abiertas";
            this.radioButton_abiertas.Size = new System.Drawing.Size(63, 17);
            this.radioButton_abiertas.TabIndex = 5;
            this.radioButton_abiertas.Text = "Abiertas";
            this.radioButton_abiertas.UseVisualStyleBackColor = true;
            // 
            // radioButton_cerradas
            // 
            this.radioButton_cerradas.AutoSize = true;
            this.radioButton_cerradas.Location = new System.Drawing.Point(254, 114);
            this.radioButton_cerradas.Name = "radioButton_cerradas";
            this.radioButton_cerradas.Size = new System.Drawing.Size(67, 17);
            this.radioButton_cerradas.TabIndex = 6;
            this.radioButton_cerradas.Text = "Cerradas";
            this.radioButton_cerradas.UseVisualStyleBackColor = true;
            // 
            // radioButton_todas
            // 
            this.radioButton_todas.AutoSize = true;
            this.radioButton_todas.Checked = true;
            this.radioButton_todas.Location = new System.Drawing.Point(330, 114);
            this.radioButton_todas.Name = "radioButton_todas";
            this.radioButton_todas.Size = new System.Drawing.Size(55, 17);
            this.radioButton_todas.TabIndex = 7;
            this.radioButton_todas.TabStop = true;
            this.radioButton_todas.Text = "Todas";
            this.radioButton_todas.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(17, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Estado de solicitud :";
            // 
            // comboBoxCampos
            // 
            this.comboBoxCampos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCampos.FormattingEnabled = true;
            this.comboBoxCampos.Items.AddRange(new object[] {
            "No Solicitud",
            "Codigo Cliente",
            "Nombre",
            "Filial",
            "Oficial de Servicio",
            "No. Solicitud Formulario"});
            this.comboBoxCampos.Location = new System.Drawing.Point(409, 81);
            this.comboBoxCampos.Name = "comboBoxCampos";
            this.comboBoxCampos.Size = new System.Drawing.Size(145, 21);
            this.comboBoxCampos.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(343, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "buscar en :";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(391, 488);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 33);
            this.button1.TabIndex = 10;
            this.button1.Text = "&Ver tramites";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox_wf
            // 
            this.checkBox_wf.AutoSize = true;
            this.checkBox_wf.Checked = true;
            this.checkBox_wf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_wf.Location = new System.Drawing.Point(409, 114);
            this.checkBox_wf.Name = "checkBox_wf";
            this.checkBox_wf.Size = new System.Drawing.Size(169, 17);
            this.checkBox_wf.TabIndex = 8;
            this.checkBox_wf.Text = "Buscar en todos los workflows";
            this.toolTip1.SetToolTip(this.checkBox_wf, "Con el cheque se busca en todos los workflow, sin el cheque solo busca en el work" +
        "flow que esta seleccionado en el menu");
            this.checkBox_wf.UseVisualStyleBackColor = true;
            this.checkBox_wf.Visible = false;
            // 
            // checkBox_vistarapida
            // 
            this.checkBox_vistarapida.AutoSize = true;
            this.checkBox_vistarapida.Location = new System.Drawing.Point(599, 114);
            this.checkBox_vistarapida.Name = "checkBox_vistarapida";
            this.checkBox_vistarapida.Size = new System.Drawing.Size(116, 17);
            this.checkBox_vistarapida.TabIndex = 9;
            this.checkBox_vistarapida.Text = "Activar vista rapida";
            this.toolTip1.SetToolTip(this.checkBox_vistarapida, "Activa o desactiva vista preliminar cuando se pasa el mouse por la solicitud");
            this.checkBox_vistarapida.UseVisualStyleBackColor = true;
            // 
            // s_buscar_sol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(1212, 559);
            this.Controls.Add(this.checkBox_vistarapida);
            this.Controls.Add(this.checkBox_wf);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxCampos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButton_todas);
            this.Controls.Add(this.radioButton_cerradas);
            this.Controls.Add(this.radioButton_abiertas);
            this.Controls.Add(this.pboxLoading);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button_ejecutar);
            this.Controls.Add(this.gvSolicitudes);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTexto_busqueda);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_cerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_buscar_sol";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ":::";
            this.Load += new System.EventHandler(this.s_buscar_sol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvSolicitudes)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxLoading;
        private System.Windows.Forms.DataGridView gvSolicitudes;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label_Titulo_lista;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTexto_busqueda;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.Button button_ejecutar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.RadioButton radioButton_abiertas;
        private System.Windows.Forms.RadioButton radioButton_cerradas;
        private System.Windows.Forms.RadioButton radioButton_todas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCampos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pboxLoading02;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox_wf;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBox_vistarapida;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMaximizar;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado_solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc_sub_aplicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultado_buro;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_agencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn oficial_servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn analista;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion_fuente;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc_moneda;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto_solicitado;
        private System.Windows.Forms.DataGridViewTextBoxColumn meses_plazo;
        private System.Windows.Forms.DataGridViewTextBoxColumn estacion_actual;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_movimiento;
    }
}