namespace Docsis_Application.Excepciones
{
	partial class e_cnf_agregar_lineamiento
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label_Titulo = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbxEstado = new System.Windows.Forms.CheckBox();
			this.txtCodigo = new System.Windows.Forms.TextBox();
			this.txtNombre = new System.Windows.Forms.TextBox();
			this.txtDescripcion = new System.Windows.Forms.TextBox();
			this.btnAgregar = new System.Windows.Forms.Button();
			this.btnCancelar = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label_Titulo);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(1, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(387, 46);
			this.panel1.TabIndex = 20;
			// 
			// label_Titulo
			// 
			this.label_Titulo.AutoSize = true;
			this.label_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_Titulo.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label_Titulo.Location = new System.Drawing.Point(66, 15);
			this.label_Titulo.Name = "label_Titulo";
			this.label_Titulo.Size = new System.Drawing.Size(49, 20);
			this.label_Titulo.TabIndex = 0;
			this.label_Titulo.Text = "titulo";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.icon_decision02;
			this.pictureBox1.Location = new System.Drawing.Point(6, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(57, 41);
			this.pictureBox1.TabIndex = 87;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(42, 82);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 21;
			this.label1.Text = "Código";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(42, 118);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "Nombre";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Location = new System.Drawing.Point(42, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 13);
			this.label3.TabIndex = 23;
			this.label3.Text = "Descripción";
			// 
			// cbxEstado
			// 
			this.cbxEstado.AutoSize = true;
			this.cbxEstado.Location = new System.Drawing.Point(305, 231);
			this.cbxEstado.Name = "cbxEstado";
			this.cbxEstado.Size = new System.Drawing.Size(56, 17);
			this.cbxEstado.TabIndex = 24;
			this.cbxEstado.Text = "Activo";
			this.cbxEstado.UseVisualStyleBackColor = true;
			// 
			// txtCodigo
			// 
			this.txtCodigo.Location = new System.Drawing.Point(126, 79);
			this.txtCodigo.Name = "txtCodigo";
			this.txtCodigo.Size = new System.Drawing.Size(117, 20);
			this.txtCodigo.TabIndex = 25;
			// 
			// txtNombre
			// 
			this.txtNombre.Location = new System.Drawing.Point(126, 118);
			this.txtNombre.Name = "txtNombre";
			this.txtNombre.Size = new System.Drawing.Size(235, 20);
			this.txtNombre.TabIndex = 26;
			// 
			// txtDescripcion
			// 
			this.txtDescripcion.Location = new System.Drawing.Point(126, 152);
			this.txtDescripcion.Multiline = true;
			this.txtDescripcion.Name = "txtDescripcion";
			this.txtDescripcion.Size = new System.Drawing.Size(235, 60);
			this.txtDescripcion.TabIndex = 27;
			this.txtDescripcion.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
			// 
			// btnAgregar
			// 
			this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAgregar.ForeColor = System.Drawing.Color.White;
			this.btnAgregar.Location = new System.Drawing.Point(71, 269);
			this.btnAgregar.Name = "btnAgregar";
			this.btnAgregar.Size = new System.Drawing.Size(100, 29);
			this.btnAgregar.TabIndex = 28;
			this.btnAgregar.Text = "Guardar";
			this.btnAgregar.UseVisualStyleBackColor = false;
			this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
			// 
			// btnCancelar
			// 
			this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancelar.ForeColor = System.Drawing.Color.White;
			this.btnCancelar.Location = new System.Drawing.Point(213, 269);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.Size = new System.Drawing.Size(100, 29);
			this.btnCancelar.TabIndex = 29;
			this.btnCancelar.Text = "Cancelar";
			this.btnCancelar.UseVisualStyleBackColor = false;
			this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
			// 
			// e_cnf_agregar_lineamiento
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.fondo_formularios2;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(389, 310);
			this.Controls.Add(this.btnCancelar);
			this.Controls.Add(this.btnAgregar);
			this.Controls.Add(this.txtDescripcion);
			this.Controls.Add(this.txtNombre);
			this.Controls.Add(this.txtCodigo);
			this.Controls.Add(this.cbxEstado);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "e_cnf_agregar_lineamiento";
			this.Load += new System.EventHandler(this.e_cnf_agregar_lineamiento_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label_Titulo;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox cbxEstado;
		private System.Windows.Forms.TextBox txtCodigo;
		private System.Windows.Forms.TextBox txtNombre;
		private System.Windows.Forms.TextBox txtDescripcion;
		private System.Windows.Forms.Button btnAgregar;
		private System.Windows.Forms.Button btnCancelar;
	}
}