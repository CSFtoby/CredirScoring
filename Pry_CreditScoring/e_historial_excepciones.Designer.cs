namespace Docsis_Application
{
	partial class e_historial_excepciones
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
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.codigo_excepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_presentacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_resolucion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pago_mediante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto_solicitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.condicion_tu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_zona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_zona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_agencia_origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_agencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc_sub_aplicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion_destino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estacion_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estacion_Actual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANTIGUEDAD_MESES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANTIGUEDAD_DIAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANTIGUEDAD_HORAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANTIGUEDAD_MINUTOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANTIGUEDAD_SEGUNDOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCodigoCliente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button_cerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDetalle.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalle.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDetalle.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetalle.ColumnHeadersHeight = 20;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo_excepcion,
            this.nombre_cliente,
            this.no_solicitud,
            this.codigo_cliente,
            this.fecha_presentacion,
            this.fecha_resolucion,
            this.estado,
            this.pago_mediante,
            this.monto_solicitado,
            this.condicion_tu,
            this.codigo_zona,
            this.nombre_zona,
            this.codigo_agencia_origen,
            this.nombre_agencia,
            this.desc_sub_aplicacion,
            this.descripcion_destino,
            this.estacion_id,
            this.estacion_Actual,
            this.ANTIGUEDAD_MESES,
            this.ANTIGUEDAD_DIAS,
            this.ANTIGUEDAD_HORAS,
            this.ANTIGUEDAD_MINUTOS,
            this.ANTIGUEDAD_SEGUNDOS});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetalle.GridColor = System.Drawing.Color.LightSteelBlue;
            this.dgvDetalle.Location = new System.Drawing.Point(12, 108);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.RowHeadersWidth = 10;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(865, 282);
            this.dgvDetalle.TabIndex = 128;
            // 
            // codigo_excepcion
            // 
            this.codigo_excepcion.DataPropertyName = "codigo_Excepcion";
            this.codigo_excepcion.HeaderText = "Codigo Excepcion";
            this.codigo_excepcion.Name = "codigo_excepcion";
            this.codigo_excepcion.ReadOnly = true;
            // 
            // nombre_cliente
            // 
            this.nombre_cliente.DataPropertyName = "nombre_cliente";
            this.nombre_cliente.HeaderText = "Nombre";
            this.nombre_cliente.Name = "nombre_cliente";
            this.nombre_cliente.ReadOnly = true;
            // 
            // no_solicitud
            // 
            this.no_solicitud.DataPropertyName = "no_solicitud";
            this.no_solicitud.HeaderText = "Solicitud";
            this.no_solicitud.Name = "no_solicitud";
            this.no_solicitud.ReadOnly = true;
            // 
            // codigo_cliente
            // 
            this.codigo_cliente.DataPropertyName = "codigo_cliente";
            this.codigo_cliente.HeaderText = "Cliente";
            this.codigo_cliente.Name = "codigo_cliente";
            this.codigo_cliente.ReadOnly = true;
            this.codigo_cliente.Visible = false;
            // 
            // fecha_presentacion
            // 
            this.fecha_presentacion.DataPropertyName = "fecha_presentacion";
            this.fecha_presentacion.HeaderText = "Fecha Presentación";
            this.fecha_presentacion.Name = "fecha_presentacion";
            this.fecha_presentacion.ReadOnly = true;
            // 
            // fecha_resolucion
            // 
            this.fecha_resolucion.DataPropertyName = "fecha_resolucion";
            this.fecha_resolucion.HeaderText = "Fecha Resolución";
            this.fecha_resolucion.Name = "fecha_resolucion";
            this.fecha_resolucion.ReadOnly = true;
            this.fecha_resolucion.Visible = false;
            // 
            // estado
            // 
            this.estado.DataPropertyName = "estado";
            this.estado.HeaderText = "Estado";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // pago_mediante
            // 
            this.pago_mediante.DataPropertyName = "pago_mediante";
            this.pago_mediante.HeaderText = "Pago Mediante";
            this.pago_mediante.Name = "pago_mediante";
            this.pago_mediante.ReadOnly = true;
            // 
            // monto_solicitado
            // 
            this.monto_solicitado.DataPropertyName = "monto_solicitado";
            this.monto_solicitado.HeaderText = "Monto";
            this.monto_solicitado.Name = "monto_solicitado";
            this.monto_solicitado.ReadOnly = true;
            // 
            // condicion_tu
            // 
            this.condicion_tu.DataPropertyName = "condicion_tu";
            this.condicion_tu.HeaderText = "Condición TU";
            this.condicion_tu.Name = "condicion_tu";
            this.condicion_tu.ReadOnly = true;
            // 
            // codigo_zona
            // 
            this.codigo_zona.DataPropertyName = "codigo_zona";
            this.codigo_zona.HeaderText = "codigo_zona";
            this.codigo_zona.Name = "codigo_zona";
            this.codigo_zona.ReadOnly = true;
            this.codigo_zona.Visible = false;
            // 
            // nombre_zona
            // 
            this.nombre_zona.DataPropertyName = "nombre_zona";
            this.nombre_zona.HeaderText = "Zona";
            this.nombre_zona.Name = "nombre_zona";
            this.nombre_zona.ReadOnly = true;
            // 
            // codigo_agencia_origen
            // 
            this.codigo_agencia_origen.DataPropertyName = "codigo_agencia_origen";
            this.codigo_agencia_origen.HeaderText = "Codigo Agencia";
            this.codigo_agencia_origen.Name = "codigo_agencia_origen";
            this.codigo_agencia_origen.ReadOnly = true;
            // 
            // nombre_agencia
            // 
            this.nombre_agencia.DataPropertyName = "nombre_agencia";
            this.nombre_agencia.HeaderText = "Agencia";
            this.nombre_agencia.Name = "nombre_agencia";
            this.nombre_agencia.ReadOnly = true;
            // 
            // desc_sub_aplicacion
            // 
            this.desc_sub_aplicacion.DataPropertyName = "desc_sub_aplicacion";
            this.desc_sub_aplicacion.HeaderText = "Producto";
            this.desc_sub_aplicacion.Name = "desc_sub_aplicacion";
            this.desc_sub_aplicacion.ReadOnly = true;
            // 
            // descripcion_destino
            // 
            this.descripcion_destino.DataPropertyName = "descripcion_destino";
            this.descripcion_destino.HeaderText = "Destino";
            this.descripcion_destino.Name = "descripcion_destino";
            this.descripcion_destino.ReadOnly = true;
            // 
            // estacion_id
            // 
            this.estacion_id.DataPropertyName = "estacion_id";
            this.estacion_id.HeaderText = "estacion_id";
            this.estacion_id.Name = "estacion_id";
            this.estacion_id.ReadOnly = true;
            this.estacion_id.Visible = false;
            // 
            // estacion_Actual
            // 
            this.estacion_Actual.DataPropertyName = "estacion_Actual";
            this.estacion_Actual.HeaderText = "estacion_Actual";
            this.estacion_Actual.Name = "estacion_Actual";
            this.estacion_Actual.ReadOnly = true;
            this.estacion_Actual.Visible = false;
            // 
            // ANTIGUEDAD_MESES
            // 
            this.ANTIGUEDAD_MESES.DataPropertyName = "ANTIGUEDAD_MESES";
            this.ANTIGUEDAD_MESES.HeaderText = "ANTIGUEDAD_MESES";
            this.ANTIGUEDAD_MESES.Name = "ANTIGUEDAD_MESES";
            this.ANTIGUEDAD_MESES.ReadOnly = true;
            this.ANTIGUEDAD_MESES.Visible = false;
            // 
            // ANTIGUEDAD_DIAS
            // 
            this.ANTIGUEDAD_DIAS.DataPropertyName = "ANTIGUEDAD_DIAS";
            this.ANTIGUEDAD_DIAS.HeaderText = "ANTIGUEDAD_DIAS";
            this.ANTIGUEDAD_DIAS.Name = "ANTIGUEDAD_DIAS";
            this.ANTIGUEDAD_DIAS.ReadOnly = true;
            this.ANTIGUEDAD_DIAS.Visible = false;
            // 
            // ANTIGUEDAD_HORAS
            // 
            this.ANTIGUEDAD_HORAS.DataPropertyName = "ANTIGUEDAD_HORAS";
            this.ANTIGUEDAD_HORAS.HeaderText = "ANTIGUEDAD_HORAS";
            this.ANTIGUEDAD_HORAS.Name = "ANTIGUEDAD_HORAS";
            this.ANTIGUEDAD_HORAS.ReadOnly = true;
            this.ANTIGUEDAD_HORAS.Visible = false;
            // 
            // ANTIGUEDAD_MINUTOS
            // 
            this.ANTIGUEDAD_MINUTOS.DataPropertyName = "ANTIGUEDAD_MINUTOS";
            this.ANTIGUEDAD_MINUTOS.HeaderText = "ANTIGUEDAD_MINUTOS";
            this.ANTIGUEDAD_MINUTOS.Name = "ANTIGUEDAD_MINUTOS";
            this.ANTIGUEDAD_MINUTOS.ReadOnly = true;
            this.ANTIGUEDAD_MINUTOS.Visible = false;
            // 
            // ANTIGUEDAD_SEGUNDOS
            // 
            this.ANTIGUEDAD_SEGUNDOS.DataPropertyName = "ANTIGUEDAD_SEGUNDOS";
            this.ANTIGUEDAD_SEGUNDOS.HeaderText = "ANTIGUEDAD_SEGUNDOS";
            this.ANTIGUEDAD_SEGUNDOS.Name = "ANTIGUEDAD_SEGUNDOS";
            this.ANTIGUEDAD_SEGUNDOS.ReadOnly = true;
            this.ANTIGUEDAD_SEGUNDOS.Visible = false;
            // 
            // txtCodigoCliente
            // 
            this.txtCodigoCliente.Location = new System.Drawing.Point(143, 23);
            this.txtCodigoCliente.Name = "txtCodigoCliente";
            this.txtCodigoCliente.Size = new System.Drawing.Size(100, 20);
            this.txtCodigoCliente.TabIndex = 130;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(19, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 129;
            this.label5.Text = "Código de Cliente:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.txtCodigoCliente);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(49, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(770, 68);
            this.groupBox2.TabIndex = 131;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(277, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(96, 24);
            this.btnBuscar.TabIndex = 131;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(289, 404);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 33);
            this.button1.TabIndex = 132;
            this.button1.Text = "&Ver tramites";
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
            this.button_cerrar.Location = new System.Drawing.Point(498, 404);
            this.button_cerrar.Name = "button_cerrar";
            this.button_cerrar.Size = new System.Drawing.Size(115, 33);
            this.button_cerrar.TabIndex = 133;
            this.button_cerrar.Text = "&Cerrar";
            this.button_cerrar.UseVisualStyleBackColor = false;
            this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
            // 
            // e_historial_excepciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login3;
            this.ClientSize = new System.Drawing.Size(889, 449);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_cerrar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvDetalle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "e_historial_excepciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "e_historial_excepciones";
            this.Load += new System.EventHandler(this.e_historial_excepciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvDetalle;
		private System.Windows.Forms.TextBox txtCodigoCliente;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnBuscar;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_excepcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre_cliente;
		private System.Windows.Forms.DataGridViewTextBoxColumn no_solicitud;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_cliente;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_presentacion;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_resolucion;
		private System.Windows.Forms.DataGridViewTextBoxColumn estado;
		private System.Windows.Forms.DataGridViewTextBoxColumn pago_mediante;
		private System.Windows.Forms.DataGridViewTextBoxColumn monto_solicitado;
		private System.Windows.Forms.DataGridViewTextBoxColumn condicion_tu;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_zona;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre_zona;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_agencia_origen;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre_agencia;
		private System.Windows.Forms.DataGridViewTextBoxColumn desc_sub_aplicacion;
		private System.Windows.Forms.DataGridViewTextBoxColumn descripcion_destino;
		private System.Windows.Forms.DataGridViewTextBoxColumn estacion_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn estacion_Actual;
		private System.Windows.Forms.DataGridViewTextBoxColumn ANTIGUEDAD_MESES;
		private System.Windows.Forms.DataGridViewTextBoxColumn ANTIGUEDAD_DIAS;
		private System.Windows.Forms.DataGridViewTextBoxColumn ANTIGUEDAD_HORAS;
		private System.Windows.Forms.DataGridViewTextBoxColumn ANTIGUEDAD_MINUTOS;
		private System.Windows.Forms.DataGridViewTextBoxColumn ANTIGUEDAD_SEGUNDOS;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button_cerrar;
	}
}