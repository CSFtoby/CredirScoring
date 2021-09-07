namespace Docsis_Application.Excepciones
{
	partial class e_cnf_excepciones
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnRefrescar = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.Label_currentrow = new System.Windows.Forms.Label();
			this.btnEliminar = new System.Windows.Forms.Button();
			this.btnModificar = new System.Windows.Forms.Button();
			this.btnAgregar = new System.Windows.Forms.Button();
			this.checkBox_colorear = new System.Windows.Forms.CheckBox();
			this.dgvExcepciones = new System.Windows.Forms.DataGridView();
			this.cod_tipo_excepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tipo_excepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.codigo_lineamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvExcepciones)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panel1.Controls.Add(this.btnRefrescar);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Location = new System.Drawing.Point(-1, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(801, 48);
			this.panel1.TabIndex = 0;
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
			this.btnRefrescar.Location = new System.Drawing.Point(617, 12);
			this.btnRefrescar.Name = "btnRefrescar";
			this.btnRefrescar.Size = new System.Drawing.Size(114, 29);
			this.btnRefrescar.TabIndex = 290;
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
			this.btnClose.Location = new System.Drawing.Point(761, 18);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(15, 16);
			this.btnClose.TabIndex = 291;
			this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// Label_currentrow
			// 
			this.Label_currentrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Label_currentrow.AutoSize = true;
			this.Label_currentrow.Location = new System.Drawing.Point(11, 450);
			this.Label_currentrow.Name = "Label_currentrow";
			this.Label_currentrow.Size = new System.Drawing.Size(54, 13);
			this.Label_currentrow.TabIndex = 100;
			this.Label_currentrow.Text = "Elemento:";
			// 
			// btnEliminar
			// 
			this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEliminar.ForeColor = System.Drawing.Color.White;
			this.btnEliminar.Location = new System.Drawing.Point(342, 408);
			this.btnEliminar.Name = "btnEliminar";
			this.btnEliminar.Size = new System.Drawing.Size(131, 29);
			this.btnEliminar.TabIndex = 98;
			this.btnEliminar.Text = "Eliminar Lineamiento";
			this.btnEliminar.UseVisualStyleBackColor = false;
			this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
			// 
			// btnModificar
			// 
			this.btnModificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnModificar.ForeColor = System.Drawing.Color.White;
			this.btnModificar.Location = new System.Drawing.Point(178, 408);
			this.btnModificar.Name = "btnModificar";
			this.btnModificar.Size = new System.Drawing.Size(131, 29);
			this.btnModificar.TabIndex = 97;
			this.btnModificar.Text = "Modificar Lineamiento";
			this.btnModificar.UseVisualStyleBackColor = false;
			this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
			// 
			// btnAgregar
			// 
			this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAgregar.ForeColor = System.Drawing.Color.White;
			this.btnAgregar.Location = new System.Drawing.Point(13, 408);
			this.btnAgregar.Name = "btnAgregar";
			this.btnAgregar.Size = new System.Drawing.Size(131, 29);
			this.btnAgregar.TabIndex = 96;
			this.btnAgregar.Text = "Agregar Lineamiento";
			this.btnAgregar.UseVisualStyleBackColor = false;
			this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
			// 
			// checkBox_colorear
			// 
			this.checkBox_colorear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_colorear.AutoSize = true;
			this.checkBox_colorear.BackColor = System.Drawing.Color.Transparent;
			this.checkBox_colorear.Cursor = System.Windows.Forms.Cursors.Hand;
			this.checkBox_colorear.Location = new System.Drawing.Point(561, 383);
			this.checkBox_colorear.Name = "checkBox_colorear";
			this.checkBox_colorear.Size = new System.Drawing.Size(212, 17);
			this.checkBox_colorear.TabIndex = 99;
			this.checkBox_colorear.Text = "Colorear para distinguir cada excepción";
			this.checkBox_colorear.UseVisualStyleBackColor = false;
			// 
			// dgvExcepciones
			// 
			this.dgvExcepciones.AllowUserToAddRows = false;
			this.dgvExcepciones.AllowUserToDeleteRows = false;
			this.dgvExcepciones.AllowUserToResizeRows = false;
			dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
			this.dgvExcepciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
			this.dgvExcepciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvExcepciones.BackgroundColor = System.Drawing.Color.White;
			this.dgvExcepciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgvExcepciones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvExcepciones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvExcepciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
			this.dgvExcepciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cod_tipo_excepcion,
            this.tipo_excepcion,
            this.codigo_lineamiento,
            this.estado,
            this.nombre,
            this.descripcion});
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvExcepciones.DefaultCellStyle = dataGridViewCellStyle18;
			this.dgvExcepciones.Location = new System.Drawing.Point(14, 94);
			this.dgvExcepciones.Name = "dgvExcepciones";
			this.dgvExcepciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvExcepciones.Size = new System.Drawing.Size(774, 283);
			this.dgvExcepciones.TabIndex = 101;
			// 
			// cod_tipo_excepcion
			// 
			this.cod_tipo_excepcion.DataPropertyName = "cod_tipo_excepcion";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.cod_tipo_excepcion.DefaultCellStyle = dataGridViewCellStyle12;
			this.cod_tipo_excepcion.HeaderText = "Codigo Excepción";
			this.cod_tipo_excepcion.Name = "cod_tipo_excepcion";
			this.cod_tipo_excepcion.ReadOnly = true;
			// 
			// tipo_excepcion
			// 
			this.tipo_excepcion.DataPropertyName = "tipo_excepcion";
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.tipo_excepcion.DefaultCellStyle = dataGridViewCellStyle13;
			this.tipo_excepcion.HeaderText = "Excepción";
			this.tipo_excepcion.Name = "tipo_excepcion";
			this.tipo_excepcion.ReadOnly = true;
			this.tipo_excepcion.Width = 400;
			// 
			// codigo_lineamiento
			// 
			this.codigo_lineamiento.DataPropertyName = "codigo_lineamiento";
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.codigo_lineamiento.DefaultCellStyle = dataGridViewCellStyle14;
			this.codigo_lineamiento.HeaderText = "Código Lineamiento";
			this.codigo_lineamiento.Name = "codigo_lineamiento";
			this.codigo_lineamiento.ReadOnly = true;
			this.codigo_lineamiento.Width = 150;
			// 
			// estado
			// 
			this.estado.DataPropertyName = "estado";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.estado.DefaultCellStyle = dataGridViewCellStyle15;
			this.estado.HeaderText = "Estado";
			this.estado.Name = "estado";
			this.estado.ReadOnly = true;
			this.estado.Width = 50;
			// 
			// nombre
			// 
			this.nombre.DataPropertyName = "nombre";
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.nombre.DefaultCellStyle = dataGridViewCellStyle16;
			this.nombre.HeaderText = "nombre";
			this.nombre.Name = "nombre";
			this.nombre.ReadOnly = true;
			this.nombre.Visible = false;
			// 
			// descripcion
			// 
			this.descripcion.DataPropertyName = "descripcion";
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.descripcion.DefaultCellStyle = dataGridViewCellStyle17;
			this.descripcion.HeaderText = "descripcion";
			this.descripcion.Name = "descripcion";
			this.descripcion.ReadOnly = true;
			this.descripcion.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label1.Location = new System.Drawing.Point(30, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 20);
			this.label1.TabIndex = 102;
			this.label1.Text = "Excepciones";
			// 
			// e_cnf_excepciones
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
			this.ClientSize = new System.Drawing.Size(800, 473);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dgvExcepciones);
			this.Controls.Add(this.Label_currentrow);
			this.Controls.Add(this.checkBox_colorear);
			this.Controls.Add(this.btnEliminar);
			this.Controls.Add(this.btnModificar);
			this.Controls.Add(this.btnAgregar);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "e_cnf_excepciones";
			this.Load += new System.EventHandler(this.e_cnf_excepciones_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvExcepciones)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label Label_currentrow;
		private System.Windows.Forms.Button btnEliminar;
		private System.Windows.Forms.Button btnModificar;
		private System.Windows.Forms.Button btnAgregar;
		private System.Windows.Forms.Button btnRefrescar;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.CheckBox checkBox_colorear;
		private System.Windows.Forms.DataGridView dgvExcepciones;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn cod_tipo_excepcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn tipo_excepcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_lineamiento;
		private System.Windows.Forms.DataGridViewTextBoxColumn estado;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
	}
}