namespace Docsis_Application.Excepciones
{
	partial class e_resoluciones_excp
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgvDetalle = new System.Windows.Forms.DataGridView();
			this.usuario_comite = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pendiente_respuesta_b = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
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
            this.usuario_comite,
            this.pendiente_respuesta_b,
            this.estado,
            this.observaciones});
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle4;
			this.dgvDetalle.GridColor = System.Drawing.Color.LightSteelBlue;
			this.dgvDetalle.Location = new System.Drawing.Point(12, 57);
			this.dgvDetalle.Name = "dgvDetalle";
			this.dgvDetalle.ReadOnly = true;
			this.dgvDetalle.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvDetalle.RowHeadersVisible = false;
			this.dgvDetalle.RowHeadersWidth = 10;
			this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvDetalle.Size = new System.Drawing.Size(463, 149);
			this.dgvDetalle.TabIndex = 127;
			// 
			// usuario_comite
			// 
			this.usuario_comite.DataPropertyName = "usuario_comite";
			this.usuario_comite.HeaderText = "Miembro Comité";
			this.usuario_comite.Name = "usuario_comite";
			this.usuario_comite.ReadOnly = true;
			this.usuario_comite.Width = 150;
			// 
			// pendiente_respuesta_b
			// 
			this.pendiente_respuesta_b.DataPropertyName = "pendiente_respuesta_b";
			this.pendiente_respuesta_b.HeaderText = "Pendiente Respuesta";
			this.pendiente_respuesta_b.Name = "pendiente_respuesta_b";
			this.pendiente_respuesta_b.ReadOnly = true;
			// 
			// estado
			// 
			this.estado.DataPropertyName = "estado";
			this.estado.HeaderText = "Estado";
			this.estado.Name = "estado";
			this.estado.ReadOnly = true;
			// 
			// observaciones
			// 
			this.observaciones.DataPropertyName = "observaciones";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.observaciones.DefaultCellStyle = dataGridViewCellStyle3;
			this.observaciones.HeaderText = "Observaciones";
			this.observaciones.Name = "observaciones";
			this.observaciones.ReadOnly = true;
			this.observaciones.Width = 500;
			// 
			// e_resoluciones_excp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login;
			this.ClientSize = new System.Drawing.Size(503, 262);
			this.Controls.Add(this.dgvDetalle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "e_resoluciones_excp";
			this.Text = "Resoluciones";
			this.Load += new System.EventHandler(this.e_resoluciones_excp_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvDetalle;
		private System.Windows.Forms.DataGridViewTextBoxColumn usuario_comite;
		private System.Windows.Forms.DataGridViewTextBoxColumn pendiente_respuesta_b;
		private System.Windows.Forms.DataGridViewTextBoxColumn estado;
		private System.Windows.Forms.DataGridViewTextBoxColumn observaciones;
	}
}