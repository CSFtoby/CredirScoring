namespace Docsis_Application
{
    partial class s_solic_abiertas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(s_solic_abiertas));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imagesSmall = new System.Windows.Forms.ImageList(this.components);
            this.imagesLarge = new System.Windows.Forms.ImageList(this.components);
            this.gvSolicitudes = new System.Windows.Forms.DataGridView();
            this.No_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no_solicitud_formulario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc_sub_aplicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.label_Titulo_lista = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pboxLoading02 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.button1 = new System.Windows.Forms.Button();
            this.button_cerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvSolicitudes)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imagesSmall
            // 
            this.imagesSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesSmall.ImageStream")));
            this.imagesSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesSmall.Images.SetKeyName(0, "archivo_tipo_word.png");
            this.imagesSmall.Images.SetKeyName(1, "archivo_tipo_excel.png");
            this.imagesSmall.Images.SetKeyName(2, "archivo_tipo_pdf.png");
            this.imagesSmall.Images.SetKeyName(3, "adjunto.png");
            this.imagesSmall.Images.SetKeyName(4, "anotaciones.png");
            this.imagesSmall.Images.SetKeyName(5, "anotaciones_condi.png");
            // 
            // imagesLarge
            // 
            this.imagesLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesLarge.ImageStream")));
            this.imagesLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesLarge.Images.SetKeyName(0, "archivo_tipo_word.png");
            this.imagesLarge.Images.SetKeyName(1, "archivo_tipo_excel.png");
            this.imagesLarge.Images.SetKeyName(2, "archivo_tipo_pdf.png");
            this.imagesLarge.Images.SetKeyName(3, "adjunto.png");
            this.imagesLarge.Images.SetKeyName(4, "anotaciones.png");
            this.imagesLarge.Images.SetKeyName(5, "anotaciones_condi.png");
            // 
            // gvSolicitudes
            // 
            this.gvSolicitudes.AllowUserToAddRows = false;
            this.gvSolicitudes.AllowUserToDeleteRows = false;
            this.gvSolicitudes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gvSolicitudes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSolicitudes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvSolicitudes.BackgroundColor = System.Drawing.Color.White;
            this.gvSolicitudes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvSolicitudes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSolicitudes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSolicitudes.ColumnHeadersHeight = 20;
            this.gvSolicitudes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No_solicitud,
            this.no_solicitud_formulario,
            this.estado_solicitud,
            this.desc_sub_aplicacion,
            this.fecha,
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
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvSolicitudes.DefaultCellStyle = dataGridViewCellStyle8;
            this.gvSolicitudes.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvSolicitudes.Location = new System.Drawing.Point(6, 53);
            this.gvSolicitudes.Name = "gvSolicitudes";
            this.gvSolicitudes.ReadOnly = true;
            this.gvSolicitudes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvSolicitudes.RowHeadersVisible = false;
            this.gvSolicitudes.RowHeadersWidth = 10;
            this.gvSolicitudes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvSolicitudes.Size = new System.Drawing.Size(1051, 304);
            this.gvSolicitudes.TabIndex = 82;
            this.gvSolicitudes.DoubleClick += new System.EventHandler(this.gvSolicitudes_DoubleClick);
            // 
            // No_solicitud
            // 
            this.No_solicitud.DataPropertyName = "No_solicitud";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.No_solicitud.DefaultCellStyle = dataGridViewCellStyle3;
            this.No_solicitud.HeaderText = "No. Solicitud";
            this.No_solicitud.Name = "No_solicitud";
            this.No_solicitud.ReadOnly = true;
            this.No_solicitud.Width = 80;
            // 
            // no_solicitud_formulario
            // 
            this.no_solicitud_formulario.DataPropertyName = "no_solicitud_formulario";
            this.no_solicitud_formulario.HeaderText = "No.Formulario";
            this.no_solicitud_formulario.Name = "no_solicitud_formulario";
            this.no_solicitud_formulario.ReadOnly = true;
            this.no_solicitud_formulario.Width = 90;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.desc_sub_aplicacion.DefaultCellStyle = dataGridViewCellStyle4;
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.desc_moneda.DefaultCellStyle = dataGridViewCellStyle5;
            this.desc_moneda.HeaderText = "Moneda";
            this.desc_moneda.Name = "desc_moneda";
            this.desc_moneda.ReadOnly = true;
            // 
            // Monto_solicitado
            // 
            this.Monto_solicitado.DataPropertyName = "Monto_solicitado";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.Monto_solicitado.DefaultCellStyle = dataGridViewCellStyle6;
            this.Monto_solicitado.HeaderText = "Monto Solicitado";
            this.Monto_solicitado.Name = "Monto_solicitado";
            this.Monto_solicitado.ReadOnly = true;
            // 
            // meses_plazo
            // 
            this.meses_plazo.DataPropertyName = "meses_plazo";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.meses_plazo.DefaultCellStyle = dataGridViewCellStyle7;
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
            // label_Titulo_lista
            // 
            this.label_Titulo_lista.AutoSize = true;
            this.label_Titulo_lista.BackColor = System.Drawing.Color.Transparent;
            this.label_Titulo_lista.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Titulo_lista.ForeColor = System.Drawing.Color.Silver;
            this.label_Titulo_lista.Location = new System.Drawing.Point(50, 12);
            this.label_Titulo_lista.Name = "label_Titulo_lista";
            this.label_Titulo_lista.Size = new System.Drawing.Size(184, 24);
            this.label_Titulo_lista.TabIndex = 0;
            this.label_Titulo_lista.Text = "Solicitudes Abiertas";
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.pboxLoading02);
            this.panelTop.Controls.Add(this.label_Titulo_lista);
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Location = new System.Drawing.Point(0, -2);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1061, 48);
            this.panelTop.TabIndex = 83;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
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
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(1041, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 86;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pboxLoading02
            // 
            this.pboxLoading02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pboxLoading02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pboxLoading02.Image = global::Docsis_Application.Properties.Resources._303;
            this.pboxLoading02.Location = new System.Drawing.Point(951, 5);
            this.pboxLoading02.Name = "pboxLoading02";
            this.pboxLoading02.Size = new System.Drawing.Size(51, 39);
            this.pboxLoading02.TabIndex = 85;
            this.pboxLoading02.TabStop = false;
            this.pboxLoading02.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.icon_analisis;
            this.pictureBox1.Location = new System.Drawing.Point(2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 45);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Location = new System.Drawing.Point(0, 408);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1061, 22);
            this.statusStrip1.TabIndex = 88;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(350, 369);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 33);
            this.button1.TabIndex = 90;
            this.button1.Text = "Ver tramites";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_cerrar
            // 
            this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_cerrar.FlatAppearance.BorderSize = 0;
            this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cerrar.ForeColor = System.Drawing.Color.White;
            this.button_cerrar.Location = new System.Drawing.Point(544, 369);
            this.button_cerrar.Name = "button_cerrar";
            this.button_cerrar.Size = new System.Drawing.Size(115, 33);
            this.button_cerrar.TabIndex = 89;
            this.button_cerrar.Text = "&Cerrar";
            this.button_cerrar.UseVisualStyleBackColor = false;
            this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
            // 
            // s_solic_abiertas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(1061, 430);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_cerrar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.gvSolicitudes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "s_solic_abiertas";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ":::";
            this.Load += new System.EventHandler(this.s_solic_abiertas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvSolicitudes)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxLoading02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ImageList imagesSmall;
        internal System.Windows.Forms.ImageList imagesLarge;
        private System.Windows.Forms.DataGridView gvSolicitudes;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_solicitud_formulario;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado_solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc_sub_aplicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
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
        private System.Windows.Forms.Label label_Titulo_lista;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pboxLoading02;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.Button btnClose;
    }
}