namespace Docsis_Application.Excepciones
{
	partial class e_add_notas
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
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.label_header = new System.Windows.Forms.Label();
			this.btnSalir = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.txtTexto = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.btnAgregar = new System.Windows.Forms.Button();
			this.radioButton_anota_normal = new System.Windows.Forms.RadioButton();
			this.radioButton_anota_condicion = new System.Windows.Forms.RadioButton();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
			this.panel3.Size = new System.Drawing.Size(529, 35);
			this.panel3.TabIndex = 94;
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
			this.btnClose.Location = new System.Drawing.Point(504, 10);
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
			this.label_header.Location = new System.Drawing.Point(36, 7);
			this.label_header.Name = "label_header";
			this.label_header.Size = new System.Drawing.Size(95, 21);
			this.label_header.TabIndex = 0;
			this.label_header.Text = "Anotaciones";
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
			this.pictureBox2.Image = global::Docsis_Application.Properties.Resources.icon_anotacion;
			this.pictureBox2.Location = new System.Drawing.Point(5, 5);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(25, 24);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 110;
			this.pictureBox2.TabStop = false;
			// 
			// txtTexto
			// 
			this.txtTexto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTexto.BackColor = System.Drawing.Color.White;
			this.txtTexto.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtTexto.Location = new System.Drawing.Point(12, 41);
			this.txtTexto.MaxLength = 4000;
			this.txtTexto.Multiline = true;
			this.txtTexto.Name = "txtTexto";
			this.txtTexto.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtTexto.Size = new System.Drawing.Size(502, 344);
			this.txtTexto.TabIndex = 95;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.button2.FlatAppearance.BorderSize = 0;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.ForeColor = System.Drawing.Color.White;
			this.button2.Location = new System.Drawing.Point(387, 431);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(114, 37);
			this.button2.TabIndex = 97;
			this.button2.Text = "&Cerrar";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// btnAgregar
			// 
			this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnAgregar.FlatAppearance.BorderSize = 0;
			this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAgregar.ForeColor = System.Drawing.Color.White;
			this.btnAgregar.Location = new System.Drawing.Point(254, 431);
			this.btnAgregar.Name = "btnAgregar";
			this.btnAgregar.Size = new System.Drawing.Size(127, 37);
			this.btnAgregar.TabIndex = 96;
			this.btnAgregar.Text = "&Adicionar";
			this.btnAgregar.UseVisualStyleBackColor = false;
			this.btnAgregar.Click += new System.EventHandler(this.button1_Click);
			// 
			// radioButton_anota_normal
			// 
			this.radioButton_anota_normal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.radioButton_anota_normal.AutoSize = true;
			this.radioButton_anota_normal.Checked = true;
			this.radioButton_anota_normal.Location = new System.Drawing.Point(387, 391);
			this.radioButton_anota_normal.Name = "radioButton_anota_normal";
			this.radioButton_anota_normal.Size = new System.Drawing.Size(99, 17);
			this.radioButton_anota_normal.TabIndex = 99;
			this.radioButton_anota_normal.TabStop = true;
			this.radioButton_anota_normal.Text = "Solo anotación ";
			this.radioButton_anota_normal.UseVisualStyleBackColor = true;
			// 
			// radioButton_anota_condicion
			// 
			this.radioButton_anota_condicion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.radioButton_anota_condicion.AutoSize = true;
			this.radioButton_anota_condicion.Location = new System.Drawing.Point(224, 391);
			this.radioButton_anota_condicion.Name = "radioButton_anota_condicion";
			this.radioButton_anota_condicion.Size = new System.Drawing.Size(144, 17);
			this.radioButton_anota_condicion.TabIndex = 98;
			this.radioButton_anota_condicion.Text = "Anotación con Condición";
			this.radioButton_anota_condicion.UseVisualStyleBackColor = true;
			// 
			// e_add_notas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(531, 480);
			this.Controls.Add(this.radioButton_anota_normal);
			this.Controls.Add(this.radioButton_anota_condicion);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btnAgregar);
			this.Controls.Add(this.txtTexto);
			this.Controls.Add(this.panel3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "e_add_notas";
			this.Text = "e_add_notas";
			this.Load += new System.EventHandler(this.e_add_notas_Load);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button btnClose;
		public System.Windows.Forms.Label label_header;
		private System.Windows.Forms.Button btnSalir;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.TextBox txtTexto;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button btnAgregar;
		private System.Windows.Forms.RadioButton radioButton_anota_normal;
		private System.Windows.Forms.RadioButton radioButton_anota_condicion;
	}
}