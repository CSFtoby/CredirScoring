namespace Docsis_Application.Excepciones
{
	partial class e_documento_excep
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
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.label_header = new System.Windows.Forms.Label();
			this.btnSalir = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.button_cerrar = new System.Windows.Forms.Button();
			this.btnSeleccionar = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btn_guardar = new System.Windows.Forms.Button();
			this.dgvDocumentosExc = new System.Windows.Forms.DataGridView();
			this.no_archivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nombre_documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.quitar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.nombre_archivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.extension = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosExc)).BeginInit();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panel3.Controls.Add(this.btnClose);
			this.panel3.Controls.Add(this.label_header);
			this.panel3.Controls.Add(this.btnSalir);
			this.panel3.Controls.Add(this.pictureBox2);
			this.panel3.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.panel3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(586, 35);
			this.panel3.TabIndex = 95;
			this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
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
			this.btnClose.Location = new System.Drawing.Point(550, 7);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(15, 16);
			this.btnClose.TabIndex = 5;
			this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label_header
			// 
			this.label_header.AutoSize = true;
			this.label_header.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_header.ForeColor = System.Drawing.Color.Silver;
			this.label_header.Location = new System.Drawing.Point(40, 7);
			this.label_header.Name = "label_header";
			this.label_header.Size = new System.Drawing.Size(164, 21);
			this.label_header.TabIndex = 0;
			this.label_header.Text = "Documentos Adjuntos";
			// 
			// btnSalir
			// 
			this.btnSalir.BackColor = System.Drawing.Color.Transparent;
			this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSalir.FlatAppearance.BorderSize = 0;
			this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(100)))), ((int)(((byte)(74)))));
			this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSalir.ForeColor = System.Drawing.Color.White;
			this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnSalir.Location = new System.Drawing.Point(983, 8);
			this.btnSalir.Name = "btnSalir";
			this.btnSalir.Size = new System.Drawing.Size(29, 29);
			this.btnSalir.TabIndex = 108;
			this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSalir.UseVisualStyleBackColor = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::Docsis_Application.Properties.Resources.icon_adjunto;
			this.pictureBox2.Location = new System.Drawing.Point(3, 2);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(40, 32);
			this.pictureBox2.TabIndex = 110;
			this.pictureBox2.TabStop = false;
			// 
			// button_cerrar
			// 
			this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_cerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.button_cerrar.FlatAppearance.BorderSize = 0;
			this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_cerrar.ForeColor = System.Drawing.Color.White;
			this.button_cerrar.Location = new System.Drawing.Point(265, 296);
			this.button_cerrar.Name = "button_cerrar";
			this.button_cerrar.Size = new System.Drawing.Size(106, 30);
			this.button_cerrar.TabIndex = 97;
			this.button_cerrar.Text = "Cerrar";
			this.button_cerrar.UseVisualStyleBackColor = false;
			this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
			// 
			// btnSeleccionar
			// 
			this.btnSeleccionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnSeleccionar.FlatAppearance.BorderSize = 0;
			this.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSeleccionar.ForeColor = System.Drawing.Color.White;
			this.btnSeleccionar.Location = new System.Drawing.Point(252, 64);
			this.btnSeleccionar.Name = "btnSeleccionar";
			this.btnSeleccionar.Size = new System.Drawing.Size(150, 30);
			this.btnSeleccionar.TabIndex = 96;
			this.btnSeleccionar.Text = "Seleccionar documento";
			this.btnSeleccionar.UseVisualStyleBackColor = false;
			this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(50, 73);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 13);
			this.label1.TabIndex = 99;
			this.label1.Text = "Archivo seleccionado...";
			// 
			// btn_guardar
			// 
			this.btn_guardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btn_guardar.FlatAppearance.BorderSize = 0;
			this.btn_guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn_guardar.ForeColor = System.Drawing.Color.White;
			this.btn_guardar.Location = new System.Drawing.Point(120, 296);
			this.btn_guardar.Name = "btn_guardar";
			this.btn_guardar.Size = new System.Drawing.Size(106, 30);
			this.btn_guardar.TabIndex = 100;
			this.btn_guardar.Text = "Guardar";
			this.btn_guardar.UseVisualStyleBackColor = false;
			this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
			// 
			// dgvDocumentosExc
			// 
			this.dgvDocumentosExc.AllowUserToAddRows = false;
			this.dgvDocumentosExc.AllowUserToDeleteRows = false;
			this.dgvDocumentosExc.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			this.dgvDocumentosExc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvDocumentosExc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvDocumentosExc.BackgroundColor = System.Drawing.Color.White;
			this.dgvDocumentosExc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgvDocumentosExc.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvDocumentosExc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvDocumentosExc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvDocumentosExc.ColumnHeadersHeight = 20;
			this.dgvDocumentosExc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no_archivo,
            this.nombre_documento,
            this.quitar,
            this.nombre_archivo,
            this.extension});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvDocumentosExc.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvDocumentosExc.GridColor = System.Drawing.Color.LightSteelBlue;
			this.dgvDocumentosExc.Location = new System.Drawing.Point(44, 118);
			this.dgvDocumentosExc.MultiSelect = false;
			this.dgvDocumentosExc.Name = "dgvDocumentosExc";
			this.dgvDocumentosExc.ReadOnly = true;
			this.dgvDocumentosExc.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvDocumentosExc.RowHeadersVisible = false;
			this.dgvDocumentosExc.RowHeadersWidth = 10;
			this.dgvDocumentosExc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvDocumentosExc.Size = new System.Drawing.Size(512, 138);
			this.dgvDocumentosExc.TabIndex = 101;
			this.dgvDocumentosExc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDocumentosExc_CellContentClick);
			// 
			// no_archivo
			// 
			this.no_archivo.HeaderText = "No";
			this.no_archivo.Name = "no_archivo";
			this.no_archivo.ReadOnly = true;
			this.no_archivo.Width = 50;
			// 
			// nombre_documento
			// 
			this.nombre_documento.HeaderText = "Nombre Documento";
			this.nombre_documento.Name = "nombre_documento";
			this.nombre_documento.ReadOnly = true;
			this.nombre_documento.Width = 300;
			// 
			// quitar
			// 
			this.quitar.HeaderText = "Formato Excepción";
			this.quitar.Name = "quitar";
			this.quitar.ReadOnly = true;
			this.quitar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.quitar.ToolTipText = "Es el formato de excepción firmado";
			this.quitar.Width = 150;
			// 
			// nombre_archivo
			// 
			this.nombre_archivo.HeaderText = "";
			this.nombre_archivo.Name = "nombre_archivo";
			this.nombre_archivo.ReadOnly = true;
			this.nombre_archivo.Visible = false;
			this.nombre_archivo.Width = 500;
			// 
			// extension
			// 
			this.extension.HeaderText = "";
			this.extension.Name = "extension";
			this.extension.ReadOnly = true;
			this.extension.Visible = false;
			// 
			// e_documento_excep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(586, 359);
			this.Controls.Add(this.dgvDocumentosExc);
			this.Controls.Add(this.btn_guardar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button_cerrar);
			this.Controls.Add(this.btnSeleccionar);
			this.Controls.Add(this.panel3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximumSize = new System.Drawing.Size(586, 359);
			this.MinimumSize = new System.Drawing.Size(586, 359);
			this.Name = "e_documento_excep";
			this.Text = "e_documento_excep";
			this.Load += new System.EventHandler(this.e_documento_excep_Load);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosExc)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button btnClose;
		public System.Windows.Forms.Label label_header;
		private System.Windows.Forms.Button btnSalir;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button button_cerrar;
		private System.Windows.Forms.Button btnSeleccionar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_guardar;
		private System.Windows.Forms.DataGridView dgvDocumentosExc;
		private System.Windows.Forms.DataGridViewTextBoxColumn no_archivo;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre_documento;
		private System.Windows.Forms.DataGridViewCheckBoxColumn quitar;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre_archivo;
		private System.Windows.Forms.DataGridViewTextBoxColumn extension;
	}
}