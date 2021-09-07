namespace Docsis_Application.Excepciones
{
	partial class e_cnf_lineamientos
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(e_cnf_lineamientos));
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnRefrescar = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.dgvLineamientos = new System.Windows.Forms.DataGridView();
			this.foto = new System.Windows.Forms.DataGridViewImageColumn();
			this.codigo_lineamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnAgregar = new System.Windows.Forms.Button();
			this.btnModificar = new System.Windows.Forms.Button();
			this.btnEliminar = new System.Windows.Forms.Button();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.Label_currentrow = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvLineamientos)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panel1.Controls.Add(this.btnRefrescar);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Location = new System.Drawing.Point(0, -1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(756, 50);
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
			this.btnRefrescar.Location = new System.Drawing.Point(573, 7);
			this.btnRefrescar.Name = "btnRefrescar";
			this.btnRefrescar.Size = new System.Drawing.Size(114, 29);
			this.btnRefrescar.TabIndex = 288;
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
			this.btnClose.Location = new System.Drawing.Point(717, 13);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(15, 16);
			this.btnClose.TabIndex = 289;
			this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label1.Location = new System.Drawing.Point(26, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(198, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Lineamientos Excepciones";
			// 
			// dgvLineamientos
			// 
			this.dgvLineamientos.AllowUserToAddRows = false;
			this.dgvLineamientos.AllowUserToDeleteRows = false;
			this.dgvLineamientos.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			this.dgvLineamientos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvLineamientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvLineamientos.BackgroundColor = System.Drawing.Color.White;
			this.dgvLineamientos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgvLineamientos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvLineamientos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvLineamientos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvLineamientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.foto,
            this.codigo_lineamiento,
            this.nombre,
            this.descripcion,
            this.estado});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvLineamientos.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvLineamientos.Location = new System.Drawing.Point(12, 76);
			this.dgvLineamientos.Name = "dgvLineamientos";
			this.dgvLineamientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvLineamientos.Size = new System.Drawing.Size(730, 313);
			this.dgvLineamientos.TabIndex = 2;
			// 
			// foto
			// 
			this.foto.DataPropertyName = "foto";
			this.foto.HeaderText = "";
			this.foto.Name = "foto";
			this.foto.ReadOnly = true;
			this.foto.Width = 20;
			// 
			// codigo_lineamiento
			// 
			this.codigo_lineamiento.DataPropertyName = "codigo_lineamiento";
			this.codigo_lineamiento.HeaderText = "Código";
			this.codigo_lineamiento.Name = "codigo_lineamiento";
			this.codigo_lineamiento.ReadOnly = true;
			this.codigo_lineamiento.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.codigo_lineamiento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// nombre
			// 
			this.nombre.DataPropertyName = "nombre";
			this.nombre.HeaderText = "Nombre";
			this.nombre.Name = "nombre";
			this.nombre.ReadOnly = true;
			this.nombre.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// descripcion
			// 
			this.descripcion.DataPropertyName = "descripcion";
			this.descripcion.HeaderText = "Descripcion";
			this.descripcion.Name = "descripcion";
			this.descripcion.ReadOnly = true;
			this.descripcion.Width = 350;
			// 
			// estado
			// 
			this.estado.DataPropertyName = "estado";
			this.estado.HeaderText = "Estado";
			this.estado.Name = "estado";
			this.estado.ReadOnly = true;
			// 
			// btnAgregar
			// 
			this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAgregar.ForeColor = System.Drawing.Color.White;
			this.btnAgregar.Location = new System.Drawing.Point(14, 407);
			this.btnAgregar.Name = "btnAgregar";
			this.btnAgregar.Size = new System.Drawing.Size(131, 29);
			this.btnAgregar.TabIndex = 3;
			this.btnAgregar.Text = "Agregar Lineamiento";
			this.btnAgregar.UseVisualStyleBackColor = false;
			this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
			// 
			// btnModificar
			// 
			this.btnModificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnModificar.ForeColor = System.Drawing.Color.White;
			this.btnModificar.Location = new System.Drawing.Point(179, 407);
			this.btnModificar.Name = "btnModificar";
			this.btnModificar.Size = new System.Drawing.Size(131, 29);
			this.btnModificar.TabIndex = 4;
			this.btnModificar.Text = "Modificar Lineamiento";
			this.btnModificar.UseVisualStyleBackColor = false;
			this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
			// 
			// btnEliminar
			// 
			this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEliminar.ForeColor = System.Drawing.Color.White;
			this.btnEliminar.Location = new System.Drawing.Point(343, 407);
			this.btnEliminar.Name = "btnEliminar";
			this.btnEliminar.Size = new System.Drawing.Size(131, 29);
			this.btnEliminar.TabIndex = 5;
			this.btnEliminar.Text = "Eliminar Lineamiento";
			this.btnEliminar.UseVisualStyleBackColor = false;
			this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "continuar.png");
			this.imageList.Images.SetKeyName(1, "devolver.png");
			this.imageList.Images.SetKeyName(2, "devolver02.png");
			// 
			// Label_currentrow
			// 
			this.Label_currentrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Label_currentrow.AutoSize = true;
			this.Label_currentrow.Location = new System.Drawing.Point(12, 449);
			this.Label_currentrow.Name = "Label_currentrow";
			this.Label_currentrow.Size = new System.Drawing.Size(54, 13);
			this.Label_currentrow.TabIndex = 95;
			this.Label_currentrow.Text = "Elemento:";
			this.Label_currentrow.Click += new System.EventHandler(this.label2_Click);
			// 
			// e_cnf_lineamientos
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
			this.ClientSize = new System.Drawing.Size(754, 471);
			this.Controls.Add(this.Label_currentrow);
			this.Controls.Add(this.btnEliminar);
			this.Controls.Add(this.btnModificar);
			this.Controls.Add(this.btnAgregar);
			this.Controls.Add(this.dgvLineamientos);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "e_cnf_lineamientos";
			this.Text = "e_cnf_lineamientos";
			this.Load += new System.EventHandler(this.e_cnf_lineamientos_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvLineamientos)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dgvLineamientos;
		private System.Windows.Forms.Button btnAgregar;
		private System.Windows.Forms.Button btnModificar;
		private System.Windows.Forms.Button btnEliminar;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.DataGridViewImageColumn foto;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_lineamiento;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn estado;
		private System.Windows.Forms.Label Label_currentrow;
		private System.Windows.Forms.Button btnRefrescar;
	}
}