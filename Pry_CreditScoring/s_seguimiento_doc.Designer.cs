namespace Docsis_Application
{
    partial class s_seguimiento_doc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(s_seguimiento_doc));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvRuta = new System.Windows.Forms.DataGridView();
            this.list_anotaciones = new System.Windows.Forms.ListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.imagesLarge = new System.Windows.Forms.ImageList(this.components);
            this.imagesSmall = new System.Windows.Forms.ImageList(this.components);
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNo_solicitud = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtFondos = new System.Windows.Forms.TextBox();
            this.button_cons_clientes = new System.Windows.Forms.Button();
            this.txtPlazo_meses = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_ejecutar_consulta = new System.Windows.Forms.Button();
            this.img_estado_registro = new System.Windows.Forms.DataGridViewImageColumn();
            this.img_tiene_adjunto = new System.Windows.Forms.DataGridViewImageColumn();
            this.no_movimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_envio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvRuta)).BeginInit();
            this.SuspendLayout();
            // 
            // gvRuta
            // 
            this.gvRuta.AllowUserToAddRows = false;
            this.gvRuta.AllowUserToDeleteRows = false;
            this.gvRuta.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.gvRuta.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.gvRuta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvRuta.BackgroundColor = System.Drawing.Color.White;
            this.gvRuta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvRuta.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvRuta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRuta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.gvRuta.ColumnHeadersHeight = 20;
            this.gvRuta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.img_estado_registro,
            this.img_tiene_adjunto,
            this.no_movimiento,
            this.no_solicitud,
            this.fecha_envio,
            this.nombre_from,
            this.nombre_to});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvRuta.DefaultCellStyle = dataGridViewCellStyle15;
            this.gvRuta.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvRuta.Location = new System.Drawing.Point(6, 104);
            this.gvRuta.Name = "gvRuta";
            this.gvRuta.ReadOnly = true;
            this.gvRuta.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvRuta.RowHeadersVisible = false;
            this.gvRuta.RowHeadersWidth = 10;
            this.gvRuta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvRuta.Size = new System.Drawing.Size(757, 430);
            this.gvRuta.TabIndex = 24;
            this.gvRuta.SelectionChanged += new System.EventHandler(this.gvRuta_SelectionChanged);
            // 
            // list_anotaciones
            // 
            this.list_anotaciones.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.list_anotaciones.AllowColumnReorder = true;
            this.list_anotaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.list_anotaciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_anotaciones.GridLines = true;
            this.list_anotaciones.HoverSelection = true;
            this.list_anotaciones.Location = new System.Drawing.Point(769, 125);
            this.list_anotaciones.Name = "list_anotaciones";
            this.list_anotaciones.Size = new System.Drawing.Size(277, 405);
            this.list_anotaciones.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.list_anotaciones.TabIndex = 26;
            this.list_anotaciones.UseCompatibleStateImageBehavior = false;
            this.list_anotaciones.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.list_anotaciones_MouseDoubleClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(154)))), ((int)(((byte)(200)))));
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1061, 22);
            this.statusStrip1.TabIndex = 62;
            this.statusStrip1.Text = "statusStrip1";
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
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(826, 103);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(57, 18);
            this.radioButton2.TabIndex = 63;
            this.radioButton2.Text = "Detalle";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(762, 103);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(57, 18);
            this.radioButton1.TabIndex = 64;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Iconos";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "No. Solicitud";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "Nombre :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(13, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 67;
            this.label3.Text = "Fondos :";
            // 
            // txtNo_solicitud
            // 
            this.txtNo_solicitud.Location = new System.Drawing.Point(86, 23);
            this.txtNo_solicitud.Name = "txtNo_solicitud";
            this.txtNo_solicitud.Size = new System.Drawing.Size(76, 20);
            this.txtNo_solicitud.TabIndex = 68;
            this.txtNo_solicitud.MouseLeave += new System.EventHandler(this.txtNo_solicitud_MouseLeave);
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNombre.Location = new System.Drawing.Point(86, 48);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(402, 20);
            this.txtNombre.TabIndex = 69;
            // 
            // txtFondos
            // 
            this.txtFondos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFondos.Location = new System.Drawing.Point(86, 72);
            this.txtFondos.Name = "txtFondos";
            this.txtFondos.ReadOnly = true;
            this.txtFondos.Size = new System.Drawing.Size(158, 20);
            this.txtFondos.TabIndex = 70;
            // 
            // button_cons_clientes
            // 
            this.button_cons_clientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cons_clientes.Image = global::Docsis_Application.Properties.Resources.lupa_15x14;
            this.button_cons_clientes.Location = new System.Drawing.Point(168, 22);
            this.button_cons_clientes.Name = "button_cons_clientes";
            this.button_cons_clientes.Size = new System.Drawing.Size(33, 23);
            this.button_cons_clientes.TabIndex = 71;
            this.toolTip1.SetToolTip(this.button_cons_clientes, "Buscar solicitudes");
            this.button_cons_clientes.UseVisualStyleBackColor = true;
            // 
            // txtPlazo_meses
            // 
            this.txtPlazo_meses.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPlazo_meses.Location = new System.Drawing.Point(293, 72);
            this.txtPlazo_meses.Name = "txtPlazo_meses";
            this.txtPlazo_meses.ReadOnly = true;
            this.txtPlazo_meses.Size = new System.Drawing.Size(45, 20);
            this.txtPlazo_meses.TabIndex = 73;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(251, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 72;
            this.label4.Text = "Plazo :";
            // 
            // txtMonto
            // 
            this.txtMonto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMonto.Location = new System.Drawing.Point(398, 72);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.ReadOnly = true;
            this.txtMonto.Size = new System.Drawing.Size(90, 20);
            this.txtMonto.TabIndex = 75;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(355, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Monto :";
            // 
            // button_ejecutar_consulta
            // 
            this.button_ejecutar_consulta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ejecutar_consulta.Image = global::Docsis_Application.Properties.Resources.lupa_15x14;
            this.button_ejecutar_consulta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ejecutar_consulta.Location = new System.Drawing.Point(555, 60);
            this.button_ejecutar_consulta.Name = "button_ejecutar_consulta";
            this.button_ejecutar_consulta.Size = new System.Drawing.Size(142, 32);
            this.button_ejecutar_consulta.TabIndex = 76;
            this.button_ejecutar_consulta.Text = "Buscar Mov.";
            this.button_ejecutar_consulta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_ejecutar_consulta.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolTip1.SetToolTip(this.button_ejecutar_consulta, "Mostrar los movimientos de la solicitud ingresada");
            this.button_ejecutar_consulta.UseVisualStyleBackColor = true;
            this.button_ejecutar_consulta.Click += new System.EventHandler(this.button_ejecutar_consulta_Click);
            // 
            // img_estado_registro
            // 
            this.img_estado_registro.DataPropertyName = "img_estado_registro";
            this.img_estado_registro.HeaderText = "";
            this.img_estado_registro.Name = "img_estado_registro";
            this.img_estado_registro.ReadOnly = true;
            this.img_estado_registro.Visible = false;
            this.img_estado_registro.Width = 20;
            // 
            // img_tiene_adjunto
            // 
            this.img_tiene_adjunto.DataPropertyName = "img_tiene_adjunto";
            this.img_tiene_adjunto.HeaderText = "";
            this.img_tiene_adjunto.Name = "img_tiene_adjunto";
            this.img_tiene_adjunto.ReadOnly = true;
            this.img_tiene_adjunto.Visible = false;
            this.img_tiene_adjunto.Width = 20;
            // 
            // no_movimiento
            // 
            this.no_movimiento.DataPropertyName = "no_movimiento";
            this.no_movimiento.HeaderText = "No. Mov.";
            this.no_movimiento.Name = "no_movimiento";
            this.no_movimiento.ReadOnly = true;
            this.no_movimiento.ToolTipText = "No. de movimiento de la solicitud";
            // 
            // no_solicitud
            // 
            this.no_solicitud.DataPropertyName = "enviado_por";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.NullValue = null;
            this.no_solicitud.DefaultCellStyle = dataGridViewCellStyle13;
            this.no_solicitud.HeaderText = "Enviado Por";
            this.no_solicitud.Name = "no_solicitud";
            this.no_solicitud.ReadOnly = true;
            // 
            // fecha_envio
            // 
            this.fecha_envio.DataPropertyName = "fecha_envio";
            this.fecha_envio.HeaderText = "Fecha Envio";
            this.fecha_envio.Name = "fecha_envio";
            this.fecha_envio.ReadOnly = true;
            this.fecha_envio.Width = 150;
            // 
            // nombre_from
            // 
            this.nombre_from.DataPropertyName = "nombre_from";
            dataGridViewCellStyle14.Format = "f";
            dataGridViewCellStyle14.NullValue = null;
            this.nombre_from.DefaultCellStyle = dataGridViewCellStyle14;
            this.nombre_from.HeaderText = "De : (Origen)";
            this.nombre_from.Name = "nombre_from";
            this.nombre_from.ReadOnly = true;
            this.nombre_from.Width = 200;
            // 
            // nombre_to
            // 
            this.nombre_to.DataPropertyName = "nombre_to";
            this.nombre_to.HeaderText = "Para : (Destino)";
            this.nombre_to.Name = "nombre_to";
            this.nombre_to.ReadOnly = true;
            this.nombre_to.Width = 200;
            // 
            // s_seguimiento_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1061, 562);
            this.Controls.Add(this.button_ejecutar_consulta);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPlazo_meses);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_cons_clientes);
            this.Controls.Add(this.txtFondos);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtNo_solicitud);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.list_anotaciones);
            this.Controls.Add(this.gvRuta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "s_seguimiento_doc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secuencia de Movimientos de la Solicitud";
            this.Load += new System.EventHandler(this.s_seguimiento_doc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvRuta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvRuta;
        internal System.Windows.Forms.ListView list_anotaciones;
        private System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.ImageList imagesLarge;
        internal System.Windows.Forms.ImageList imagesSmall;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNo_solicitud;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtFondos;
        private System.Windows.Forms.Button button_cons_clientes;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtPlazo_meses;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_ejecutar_consulta;
        private System.Windows.Forms.DataGridViewImageColumn img_estado_registro;
        private System.Windows.Forms.DataGridViewImageColumn img_tiene_adjunto;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_movimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_envio;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_to;
    }
}