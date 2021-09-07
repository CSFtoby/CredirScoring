namespace Docsis_Application.Excepciones
{
	partial class e_buscar_excepcion
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelTop = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.pboxLoading02 = new System.Windows.Forms.PictureBox();
			this.label_Titulo_lista = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxCampos = new System.Windows.Forms.ComboBox();
			this.txtTexto_busqueda = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.radioButton_todas = new System.Windows.Forms.RadioButton();
			this.radioButton_cerradas = new System.Windows.Forms.RadioButton();
			this.radioButton_abiertas = new System.Windows.Forms.RadioButton();
			this.dgvExcepciones = new System.Windows.Forms.DataGridView();
			this.button_ejecutar = new System.Windows.Forms.Button();
			this.codigo_excepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.no_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.codigo_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nombre_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pago_mediante = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fecha_presentacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.oficial_servicios = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.abierta = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.no_movimiento_actual = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estacion_actual = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.filial = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pboxLoading02)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvExcepciones)).BeginInit();
			this.SuspendLayout();
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
			this.panelTop.Location = new System.Drawing.Point(-1, 0);
			this.panelTop.Margin = new System.Windows.Forms.Padding(0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(947, 51);
			this.panelTop.TabIndex = 13;
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
			this.btnClose.Location = new System.Drawing.Point(923, 8);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(15, 16);
			this.btnClose.TabIndex = 213;
			this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// pboxLoading02
			// 
			this.pboxLoading02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pboxLoading02.Image = global::Docsis_Application.Properties.Resources._303;
			this.pboxLoading02.Location = new System.Drawing.Point(847, 6);
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
			this.label_Titulo_lista.Size = new System.Drawing.Size(183, 24);
			this.label_Titulo_lista.TabIndex = 0;
			this.label_Titulo_lista.Text = "Buscar Excepciones";
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
			this.label7.Location = new System.Drawing.Point(21, 64);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(109, 13);
			this.label7.TabIndex = 14;
			this.label7.Text = "Criterios de busqueda";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label2.Location = new System.Drawing.Point(347, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "buscar en :";
			// 
			// comboBoxCampos
			// 
			this.comboBoxCampos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCampos.FormattingEnabled = true;
			this.comboBoxCampos.Items.AddRange(new object[] {
            "Codigo Excepción",
            "No Solicitud",
            "Codigo Cliente",
            "Nombre",
            "Filial",
            "Oficial de Servicio"});
			this.comboBoxCampos.Location = new System.Drawing.Point(413, 84);
			this.comboBoxCampos.Name = "comboBoxCampos";
			this.comboBoxCampos.Size = new System.Drawing.Size(145, 21);
			this.comboBoxCampos.TabIndex = 17;
			// 
			// txtTexto_busqueda
			// 
			this.txtTexto_busqueda.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.txtTexto_busqueda.Location = new System.Drawing.Point(126, 84);
			this.txtTexto_busqueda.Name = "txtTexto_busqueda";
			this.txtTexto_busqueda.Size = new System.Drawing.Size(215, 20);
			this.txtTexto_busqueda.TabIndex = 15;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label6.Location = new System.Drawing.Point(21, 88);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105, 13);
			this.label6.TabIndex = 18;
			this.label6.Text = "Texto de busqueda :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label1.Location = new System.Drawing.Point(25, 120);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 19;
			this.label1.Text = "Estado Excepción:";
			// 
			// radioButton_todas
			// 
			this.radioButton_todas.AutoSize = true;
			this.radioButton_todas.Checked = true;
			this.radioButton_todas.Location = new System.Drawing.Point(277, 118);
			this.radioButton_todas.Name = "radioButton_todas";
			this.radioButton_todas.Size = new System.Drawing.Size(55, 17);
			this.radioButton_todas.TabIndex = 22;
			this.radioButton_todas.TabStop = true;
			this.radioButton_todas.Text = "Todas";
			this.radioButton_todas.UseVisualStyleBackColor = true;
			// 
			// radioButton_cerradas
			// 
			this.radioButton_cerradas.AutoSize = true;
			this.radioButton_cerradas.Location = new System.Drawing.Point(201, 118);
			this.radioButton_cerradas.Name = "radioButton_cerradas";
			this.radioButton_cerradas.Size = new System.Drawing.Size(67, 17);
			this.radioButton_cerradas.TabIndex = 21;
			this.radioButton_cerradas.Text = "Cerradas";
			this.radioButton_cerradas.UseVisualStyleBackColor = true;
			// 
			// radioButton_abiertas
			// 
			this.radioButton_abiertas.AutoSize = true;
			this.radioButton_abiertas.Location = new System.Drawing.Point(132, 118);
			this.radioButton_abiertas.Name = "radioButton_abiertas";
			this.radioButton_abiertas.Size = new System.Drawing.Size(63, 17);
			this.radioButton_abiertas.TabIndex = 20;
			this.radioButton_abiertas.Text = "Abiertas";
			this.radioButton_abiertas.UseVisualStyleBackColor = true;
			// 
			// dgvExcepciones
			// 
			this.dgvExcepciones.AllowUserToAddRows = false;
			this.dgvExcepciones.AllowUserToDeleteRows = false;
			this.dgvExcepciones.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			this.dgvExcepciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvExcepciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvExcepciones.BackgroundColor = System.Drawing.Color.White;
			this.dgvExcepciones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvExcepciones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvExcepciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvExcepciones.ColumnHeadersHeight = 20;
			this.dgvExcepciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo_excepcion,
            this.no_solicitud,
            this.estado,
            this.codigo_cliente,
            this.nombre_cliente,
            this.pago_mediante,
            this.fecha_presentacion,
            this.oficial_servicios,
            this.abierta,
            this.no_movimiento_actual,
            this.estacion_actual,
            this.filial});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvExcepciones.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvExcepciones.GridColor = System.Drawing.Color.LightSteelBlue;
			this.dgvExcepciones.Location = new System.Drawing.Point(24, 146);
			this.dgvExcepciones.Name = "dgvExcepciones";
			this.dgvExcepciones.ReadOnly = true;
			this.dgvExcepciones.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvExcepciones.RowHeadersVisible = false;
			this.dgvExcepciones.RowHeadersWidth = 10;
			this.dgvExcepciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvExcepciones.Size = new System.Drawing.Size(898, 207);
			this.dgvExcepciones.TabIndex = 24;
			this.dgvExcepciones.DoubleClick += new System.EventHandler(this.dgvExcepciones_DoubleClick);
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
			this.button_ejecutar.Location = new System.Drawing.Point(784, 107);
			this.button_ejecutar.Name = "button_ejecutar";
			this.button_ejecutar.Size = new System.Drawing.Size(128, 33);
			this.button_ejecutar.TabIndex = 25;
			this.button_ejecutar.Text = "&Buscar";
			this.button_ejecutar.UseVisualStyleBackColor = false;
			this.button_ejecutar.Click += new System.EventHandler(this.button_ejecutar_Click);
			// 
			// codigo_excepcion
			// 
			this.codigo_excepcion.DataPropertyName = "codigo_excepcion";
			this.codigo_excepcion.HeaderText = "Código Excepción";
			this.codigo_excepcion.Name = "codigo_excepcion";
			this.codigo_excepcion.ReadOnly = true;
			// 
			// no_solicitud
			// 
			this.no_solicitud.DataPropertyName = "no_solicitud";
			this.no_solicitud.HeaderText = "No. Solicitud";
			this.no_solicitud.Name = "no_solicitud";
			this.no_solicitud.ReadOnly = true;
			// 
			// estado
			// 
			this.estado.DataPropertyName = "estado";
			this.estado.HeaderText = "Estado";
			this.estado.Name = "estado";
			this.estado.ReadOnly = true;
			// 
			// codigo_cliente
			// 
			this.codigo_cliente.DataPropertyName = "codigo_cliente";
			this.codigo_cliente.HeaderText = "Código Cliente";
			this.codigo_cliente.Name = "codigo_cliente";
			this.codigo_cliente.ReadOnly = true;
			// 
			// nombre_cliente
			// 
			this.nombre_cliente.DataPropertyName = "nombre_cliente";
			this.nombre_cliente.HeaderText = "Nombre";
			this.nombre_cliente.Name = "nombre_cliente";
			this.nombre_cliente.ReadOnly = true;
			this.nombre_cliente.Width = 200;
			// 
			// pago_mediante
			// 
			this.pago_mediante.DataPropertyName = "pago_mediante";
			this.pago_mediante.HeaderText = "Pago Mediante";
			this.pago_mediante.Name = "pago_mediante";
			this.pago_mediante.ReadOnly = true;
			// 
			// fecha_presentacion
			// 
			this.fecha_presentacion.DataPropertyName = "fecha_presentacion";
			this.fecha_presentacion.HeaderText = "Fecha Presentación";
			this.fecha_presentacion.Name = "fecha_presentacion";
			this.fecha_presentacion.ReadOnly = true;
			// 
			// oficial_servicios
			// 
			this.oficial_servicios.DataPropertyName = "oficial_servicios";
			this.oficial_servicios.HeaderText = "Oficial Servicios";
			this.oficial_servicios.Name = "oficial_servicios";
			this.oficial_servicios.ReadOnly = true;
			// 
			// abierta
			// 
			this.abierta.DataPropertyName = "abierta";
			this.abierta.HeaderText = "Abierta";
			this.abierta.Name = "abierta";
			this.abierta.ReadOnly = true;
			// 
			// no_movimiento_actual
			// 
			this.no_movimiento_actual.DataPropertyName = "no_movimiento_actual";
			this.no_movimiento_actual.HeaderText = "No. Mov. Actual";
			this.no_movimiento_actual.Name = "no_movimiento_actual";
			this.no_movimiento_actual.ReadOnly = true;
			// 
			// estacion_actual
			// 
			this.estacion_actual.DataPropertyName = "estacion_actual";
			this.estacion_actual.HeaderText = "Estación Actual";
			this.estacion_actual.Name = "estacion_actual";
			this.estacion_actual.ReadOnly = true;
			// 
			// filial
			// 
			this.filial.DataPropertyName = "filial";
			this.filial.HeaderText = "Filial";
			this.filial.Name = "filial";
			this.filial.ReadOnly = true;
			// 
			// e_buscar_excepcion
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(946, 380);
			this.Controls.Add(this.button_ejecutar);
			this.Controls.Add(this.dgvExcepciones);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.radioButton_todas);
			this.Controls.Add(this.radioButton_cerradas);
			this.Controls.Add(this.radioButton_abiertas);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboBoxCampos);
			this.Controls.Add(this.txtTexto_busqueda);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.panelTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "e_buscar_excepcion";
			this.Text = "e_buscar_excepcion";
			this.Load += new System.EventHandler(this.e_buscar_excepcion_Load);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pboxLoading02)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvExcepciones)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.PictureBox pboxLoading02;
		private System.Windows.Forms.Label label_Titulo_lista;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxCampos;
		private System.Windows.Forms.TextBox txtTexto_busqueda;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioButton_todas;
		private System.Windows.Forms.RadioButton radioButton_cerradas;
		private System.Windows.Forms.RadioButton radioButton_abiertas;
		private System.Windows.Forms.DataGridView dgvExcepciones;
		private System.Windows.Forms.Button button_ejecutar;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_excepcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn no_solicitud;
		private System.Windows.Forms.DataGridViewTextBoxColumn estado;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_cliente;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre_cliente;
		private System.Windows.Forms.DataGridViewTextBoxColumn pago_mediante;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_presentacion;
		private System.Windows.Forms.DataGridViewTextBoxColumn oficial_servicios;
		private System.Windows.Forms.DataGridViewTextBoxColumn abierta;
		private System.Windows.Forms.DataGridViewTextBoxColumn no_movimiento_actual;
		private System.Windows.Forms.DataGridViewTextBoxColumn estacion_actual;
		private System.Windows.Forms.DataGridViewTextBoxColumn filial;
	}
}