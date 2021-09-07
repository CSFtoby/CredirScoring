namespace Docsis_Application.Excepciones
{
	partial class e_cnf_reglas_excepciones
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnRefrescar = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.dgvReglas = new System.Windows.Forms.DataGridView();
			this.no_condicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.codigo_lineamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cod_tipo_excepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cod_linea_prohibida = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cod_tipo_excep_prohibida = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cod_tipo_condicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tipo_condicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Label_currentrow = new System.Windows.Forms.Label();
			this.btnEliminar = new System.Windows.Forms.Button();
			this.btnModificar = new System.Windows.Forms.Button();
			this.btnAgregar = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvReglas)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panel1.Controls.Add(this.btnRefrescar);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(873, 49);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
			// 
			// btnRefrescar
			// 
			this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefrescar.BackColor = System.Drawing.Color.Transparent;
			this.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnRefrescar.FlatAppearance.BorderSize = 0;
			this.btnRefrescar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
			this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRefrescar.ForeColor = System.Drawing.Color.White;
			this.btnRefrescar.Image = global::Docsis_Application.Properties.Resources.refresh2;
			this.btnRefrescar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnRefrescar.Location = new System.Drawing.Point(692, 12);
			this.btnRefrescar.Name = "btnRefrescar";
			this.btnRefrescar.Size = new System.Drawing.Size(114, 29);
			this.btnRefrescar.TabIndex = 291;
			this.btnRefrescar.Text = "Refrescar info";
			this.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnRefrescar.UseVisualStyleBackColor = false;
			this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
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
			this.btnClose.Location = new System.Drawing.Point(828, 18);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(15, 16);
			this.btnClose.TabIndex = 292;
			this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label1.Location = new System.Drawing.Point(27, 62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(175, 20);
			this.label1.TabIndex = 290;
			this.label1.Text = "Reglas de Excepciones";
			// 
			// dgvReglas
			// 
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			this.dgvReglas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvReglas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvReglas.BackgroundColor = System.Drawing.Color.White;
			this.dgvReglas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgvReglas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvReglas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvReglas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvReglas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no_condicion,
            this.codigo_lineamiento,
            this.cod_tipo_excepcion,
            this.cod_linea_prohibida,
            this.cod_tipo_excep_prohibida,
            this.cod_tipo_condicion,
            this.tipo_condicion,
            this.estado});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvReglas.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvReglas.Location = new System.Drawing.Point(31, 85);
			this.dgvReglas.Name = "dgvReglas";
			this.dgvReglas.Size = new System.Drawing.Size(813, 278);
			this.dgvReglas.TabIndex = 291;
			// 
			// no_condicion
			// 
			this.no_condicion.DataPropertyName = "no_condicion";
			this.no_condicion.HeaderText = "No.";
			this.no_condicion.Name = "no_condicion";
			this.no_condicion.ReadOnly = true;
			this.no_condicion.Width = 50;
			// 
			// codigo_lineamiento
			// 
			this.codigo_lineamiento.DataPropertyName = "codigo_lineamiento";
			this.codigo_lineamiento.HeaderText = "Lineamiento Base";
			this.codigo_lineamiento.Name = "codigo_lineamiento";
			this.codigo_lineamiento.ReadOnly = true;
			this.codigo_lineamiento.Width = 120;
			// 
			// cod_tipo_excepcion
			// 
			this.cod_tipo_excepcion.DataPropertyName = "cod_tipo_excepcion";
			this.cod_tipo_excepcion.HeaderText = "Excepción Base";
			this.cod_tipo_excepcion.Name = "cod_tipo_excepcion";
			this.cod_tipo_excepcion.ReadOnly = true;
			this.cod_tipo_excepcion.Width = 120;
			// 
			// cod_linea_prohibida
			// 
			this.cod_linea_prohibida.DataPropertyName = "cod_linea_prohibida";
			this.cod_linea_prohibida.HeaderText = "Lineamiento Restringido";
			this.cod_linea_prohibida.Name = "cod_linea_prohibida";
			this.cod_linea_prohibida.ReadOnly = true;
			this.cod_linea_prohibida.Width = 120;
			// 
			// cod_tipo_excep_prohibida
			// 
			this.cod_tipo_excep_prohibida.DataPropertyName = "cod_tipo_excep_prohibida";
			this.cod_tipo_excep_prohibida.HeaderText = "Excepción Restringida";
			this.cod_tipo_excep_prohibida.Name = "cod_tipo_excep_prohibida";
			this.cod_tipo_excep_prohibida.ReadOnly = true;
			this.cod_tipo_excep_prohibida.Width = 120;
			// 
			// cod_tipo_condicion
			// 
			this.cod_tipo_condicion.DataPropertyName = "cod_tipo_condicion";
			this.cod_tipo_condicion.HeaderText = "cod_condicion";
			this.cod_tipo_condicion.Name = "cod_tipo_condicion";
			this.cod_tipo_condicion.ReadOnly = true;
			this.cod_tipo_condicion.Visible = false;
			// 
			// tipo_condicion
			// 
			this.tipo_condicion.DataPropertyName = "tipo_condicion";
			this.tipo_condicion.HeaderText = "Condición";
			this.tipo_condicion.Name = "tipo_condicion";
			this.tipo_condicion.ReadOnly = true;
			this.tipo_condicion.Width = 150;
			// 
			// estado
			// 
			this.estado.DataPropertyName = "estado";
			this.estado.HeaderText = "Estado";
			this.estado.Name = "estado";
			this.estado.ReadOnly = true;
			// 
			// Label_currentrow
			// 
			this.Label_currentrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Label_currentrow.AutoSize = true;
			this.Label_currentrow.Location = new System.Drawing.Point(36, 441);
			this.Label_currentrow.Name = "Label_currentrow";
			this.Label_currentrow.Size = new System.Drawing.Size(54, 13);
			this.Label_currentrow.TabIndex = 295;
			this.Label_currentrow.Text = "Elemento:";
			// 
			// btnEliminar
			// 
			this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEliminar.ForeColor = System.Drawing.Color.White;
			this.btnEliminar.Location = new System.Drawing.Point(367, 399);
			this.btnEliminar.Name = "btnEliminar";
			this.btnEliminar.Size = new System.Drawing.Size(131, 29);
			this.btnEliminar.TabIndex = 294;
			this.btnEliminar.Text = "Eliminar Lineamiento";
			this.btnEliminar.UseVisualStyleBackColor = false;
			// 
			// btnModificar
			// 
			this.btnModificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnModificar.ForeColor = System.Drawing.Color.White;
			this.btnModificar.Location = new System.Drawing.Point(203, 399);
			this.btnModificar.Name = "btnModificar";
			this.btnModificar.Size = new System.Drawing.Size(131, 29);
			this.btnModificar.TabIndex = 293;
			this.btnModificar.Text = "Modificar Lineamiento";
			this.btnModificar.UseVisualStyleBackColor = false;
			// 
			// btnAgregar
			// 
			this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAgregar.ForeColor = System.Drawing.Color.White;
			this.btnAgregar.Location = new System.Drawing.Point(38, 399);
			this.btnAgregar.Name = "btnAgregar";
			this.btnAgregar.Size = new System.Drawing.Size(131, 29);
			this.btnAgregar.TabIndex = 292;
			this.btnAgregar.Text = "Agregar Lineamiento";
			this.btnAgregar.UseVisualStyleBackColor = false;
			this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
			// 
			// e_cnf_reglas_excepciones
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
			this.ClientSize = new System.Drawing.Size(873, 464);
			this.Controls.Add(this.Label_currentrow);
			this.Controls.Add(this.btnEliminar);
			this.Controls.Add(this.btnModificar);
			this.Controls.Add(this.btnAgregar);
			this.Controls.Add(this.dgvReglas);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "e_cnf_reglas_excepciones";
			this.Text = "e_cnf_reglas_excepciones";
			this.Load += new System.EventHandler(this.e_cnf_reglas_excepciones_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvReglas)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnRefrescar;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.DataGridView dgvReglas;
		private System.Windows.Forms.Label Label_currentrow;
		private System.Windows.Forms.Button btnEliminar;
		private System.Windows.Forms.Button btnModificar;
		private System.Windows.Forms.Button btnAgregar;
		private System.Windows.Forms.DataGridViewTextBoxColumn no_condicion;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_lineamiento;
		private System.Windows.Forms.DataGridViewTextBoxColumn cod_tipo_excepcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn cod_linea_prohibida;
		private System.Windows.Forms.DataGridViewTextBoxColumn cod_tipo_excep_prohibida;
		private System.Windows.Forms.DataGridViewTextBoxColumn cod_tipo_condicion;
		private System.Windows.Forms.DataGridViewTextBoxColumn tipo_condicion;
		private System.Windows.Forms.DataGridViewTextBoxColumn estado;
	}
}