namespace Docsis_Application.Excepciones
{
	partial class r_resoluciones_miembros
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
			this.txtObservAnterior = new System.Windows.Forms.TextBox();
			this.txtObservNueva = new System.Windows.Forms.TextBox();
			this.label87 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancelar = new System.Windows.Forms.Button();
			this.btnAgregar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtObservAnterior
			// 
			this.txtObservAnterior.BackColor = System.Drawing.Color.White;
			this.txtObservAnterior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtObservAnterior.Location = new System.Drawing.Point(34, 42);
			this.txtObservAnterior.Multiline = true;
			this.txtObservAnterior.Name = "txtObservAnterior";
			this.txtObservAnterior.ReadOnly = true;
			this.txtObservAnterior.Size = new System.Drawing.Size(372, 55);
			this.txtObservAnterior.TabIndex = 355;
			// 
			// txtObservNueva
			// 
			this.txtObservNueva.BackColor = System.Drawing.Color.White;
			this.txtObservNueva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtObservNueva.Location = new System.Drawing.Point(34, 135);
			this.txtObservNueva.MaxLength = 4000;
			this.txtObservNueva.Multiline = true;
			this.txtObservNueva.Name = "txtObservNueva";
			this.txtObservNueva.Size = new System.Drawing.Size(372, 55);
			this.txtObservNueva.TabIndex = 356;
			// 
			// label87
			// 
			this.label87.AutoSize = true;
			this.label87.BackColor = System.Drawing.Color.Transparent;
			this.label87.Font = new System.Drawing.Font("Segoe UI Light", 12F);
			this.label87.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.label87.Location = new System.Drawing.Point(38, 18);
			this.label87.Name = "label87";
			this.label87.Size = new System.Drawing.Size(152, 21);
			this.label87.TabIndex = 357;
			this.label87.Text = "Observación Anterior";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Segoe UI Light", 12F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.label1.Location = new System.Drawing.Point(36, 110);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(141, 21);
			this.label1.TabIndex = 358;
			this.label1.Text = "Observación Nueva";
			// 
			// btnCancelar
			// 
			this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancelar.ForeColor = System.Drawing.Color.White;
			this.btnCancelar.Location = new System.Drawing.Point(100, 203);
			this.btnCancelar.Name = "btnCancelar";
			this.btnCancelar.Size = new System.Drawing.Size(100, 29);
			this.btnCancelar.TabIndex = 360;
			this.btnCancelar.Text = "Cancelar";
			this.btnCancelar.UseVisualStyleBackColor = false;
			this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
			// 
			// btnAgregar
			// 
			this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAgregar.ForeColor = System.Drawing.Color.White;
			this.btnAgregar.Location = new System.Drawing.Point(232, 203);
			this.btnAgregar.Name = "btnAgregar";
			this.btnAgregar.Size = new System.Drawing.Size(100, 29);
			this.btnAgregar.TabIndex = 359;
			this.btnAgregar.Text = "Aceptar";
			this.btnAgregar.UseVisualStyleBackColor = false;
			this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
			// 
			// r_resoluciones_miembros
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(442, 244);
			this.Controls.Add(this.btnCancelar);
			this.Controls.Add(this.btnAgregar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label87);
			this.Controls.Add(this.txtObservNueva);
			this.Controls.Add(this.txtObservAnterior);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "r_resoluciones_miembros";
			this.Text = "Nueva Observación";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtObservAnterior;
		private System.Windows.Forms.TextBox txtObservNueva;
		private System.Windows.Forms.Label label87;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancelar;
		private System.Windows.Forms.Button btnAgregar;
	}
}