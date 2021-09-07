namespace Docsis_Application.Excepciones
{
	partial class ListadoAnalistas
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
			this.lbxAnalistas = new System.Windows.Forms.ListBox();
			this.btnMoverSolicitud = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbxAnalistas
			// 
			this.lbxAnalistas.FormattingEnabled = true;
			this.lbxAnalistas.Location = new System.Drawing.Point(28, 51);
			this.lbxAnalistas.Name = "lbxAnalistas";
			this.lbxAnalistas.Size = new System.Drawing.Size(252, 238);
			this.lbxAnalistas.TabIndex = 0;
			// 
			// btnMoverSolicitud
			// 
			this.btnMoverSolicitud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnMoverSolicitud.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnMoverSolicitud.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnMoverSolicitud.FlatAppearance.BorderSize = 0;
			this.btnMoverSolicitud.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMoverSolicitud.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.btnMoverSolicitud.ForeColor = System.Drawing.Color.White;
			this.btnMoverSolicitud.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnMoverSolicitud.Location = new System.Drawing.Point(286, 259);
			this.btnMoverSolicitud.Name = "btnMoverSolicitud";
			this.btnMoverSolicitud.Size = new System.Drawing.Size(91, 30);
			this.btnMoverSolicitud.TabIndex = 301;
			this.btnMoverSolicitud.Text = "Seleccionar";
			this.btnMoverSolicitud.UseVisualStyleBackColor = false;
			this.btnMoverSolicitud.Click += new System.EventHandler(this.btnMoverSolicitud_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label3.Location = new System.Drawing.Point(25, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 18);
			this.label3.TabIndex = 302;
			this.label3.Text = "Miembros:";
			// 
			// ListadoAnalistas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login;
			this.ClientSize = new System.Drawing.Size(389, 335);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnMoverSolicitud);
			this.Controls.Add(this.lbxAnalistas);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ListadoAnalistas";
			this.Text = "Seleccionar miembros";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lbxAnalistas;
		private System.Windows.Forms.Button btnMoverSolicitud;
		private System.Windows.Forms.Label label3;
	}
}