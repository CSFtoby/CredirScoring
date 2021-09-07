namespace Docsis_Application.Excepciones
{
	partial class e_excepciones_semaforo
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
			this.dgvExcepciones = new System.Windows.Forms.DataGridView();
			this.labelSemaforo = new System.Windows.Forms.Label();
			this.codigo_excepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.no_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fecha_presentacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fecha_envio = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.antiguedad_meses = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.antiguedad_dias = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.antiguedad_horas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.antiguedad_minutos = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.antiguedad_segundos = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgvExcepciones)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvExcepciones
			// 
			this.dgvExcepciones.AllowUserToAddRows = false;
			this.dgvExcepciones.AllowUserToDeleteRows = false;
			this.dgvExcepciones.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			this.dgvExcepciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvExcepciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvExcepciones.BackgroundColor = System.Drawing.Color.White;
			this.dgvExcepciones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvExcepciones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvExcepciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvExcepciones.ColumnHeadersHeight = 20;
			this.dgvExcepciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo_excepcion,
            this.no_solicitud,
            this.cliente,
            this.fecha_presentacion,
            this.fecha_envio,
            this.antiguedad_meses,
            this.antiguedad_dias,
            this.antiguedad_horas,
            this.antiguedad_minutos,
            this.antiguedad_segundos});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvExcepciones.DefaultCellStyle = dataGridViewCellStyle3;
			this.dgvExcepciones.GridColor = System.Drawing.Color.LightSteelBlue;
			this.dgvExcepciones.Location = new System.Drawing.Point(32, 82);
			this.dgvExcepciones.Name = "dgvExcepciones";
			this.dgvExcepciones.ReadOnly = true;
			this.dgvExcepciones.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvExcepciones.RowHeadersVisible = false;
			this.dgvExcepciones.RowHeadersWidth = 10;
			this.dgvExcepciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvExcepciones.Size = new System.Drawing.Size(792, 196);
			this.dgvExcepciones.TabIndex = 25;
			// 
			// labelSemaforo
			// 
			this.labelSemaforo.AutoSize = true;
			this.labelSemaforo.BackColor = System.Drawing.Color.Transparent;
			this.labelSemaforo.Font = new System.Drawing.Font("Lucida Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelSemaforo.ForeColor = System.Drawing.SystemColors.Highlight;
			this.labelSemaforo.Location = new System.Drawing.Point(28, 33);
			this.labelSemaforo.Name = "labelSemaforo";
			this.labelSemaforo.Size = new System.Drawing.Size(228, 23);
			this.labelSemaforo.TabIndex = 26;
			this.labelSemaforo.Text = "Excepciones en Verde";
			// 
			// codigo_excepcion
			// 
			this.codigo_excepcion.DataPropertyName = "codigo_excepcion";
			this.codigo_excepcion.HeaderText = "Código Excepción";
			this.codigo_excepcion.Name = "codigo_excepcion";
			this.codigo_excepcion.ReadOnly = true;
			// 
			// no_solicitud
			// 
			this.no_solicitud.DataPropertyName = "no_solicitud";
			this.no_solicitud.HeaderText = "No. Solicitud";
			this.no_solicitud.Name = "no_solicitud";
			this.no_solicitud.ReadOnly = true;
			// 
			// cliente
			// 
			this.cliente.DataPropertyName = "cliente";
			this.cliente.HeaderText = "Nombre Cliente";
			this.cliente.Name = "cliente";
			this.cliente.ReadOnly = true;
			this.cliente.Width = 300;
			// 
			// fecha_presentacion
			// 
			this.fecha_presentacion.DataPropertyName = "fecha_presentacion";
			this.fecha_presentacion.HeaderText = "Fecha Presentacion";
			this.fecha_presentacion.Name = "fecha_presentacion";
			this.fecha_presentacion.ReadOnly = true;
			// 
			// fecha_envio
			// 
			this.fecha_envio.DataPropertyName = "fecha_envio";
			this.fecha_envio.HeaderText = "Fecha Envío Estación";
			this.fecha_envio.Name = "fecha_envio";
			this.fecha_envio.ReadOnly = true;
			// 
			// antiguedad_meses
			// 
			this.antiguedad_meses.DataPropertyName = "antiguedad_meses";
			this.antiguedad_meses.HeaderText = "Ant. Meses";
			this.antiguedad_meses.Name = "antiguedad_meses";
			this.antiguedad_meses.ReadOnly = true;
			// 
			// antiguedad_dias
			// 
			this.antiguedad_dias.DataPropertyName = "antiguedad_dias";
			this.antiguedad_dias.HeaderText = "Ant. Días";
			this.antiguedad_dias.Name = "antiguedad_dias";
			this.antiguedad_dias.ReadOnly = true;
			// 
			// antiguedad_horas
			// 
			this.antiguedad_horas.DataPropertyName = "antiguedad_horas";
			this.antiguedad_horas.HeaderText = "Ant. Horas";
			this.antiguedad_horas.Name = "antiguedad_horas";
			this.antiguedad_horas.ReadOnly = true;
			// 
			// antiguedad_minutos
			// 
			this.antiguedad_minutos.DataPropertyName = "antiguedad_minutos";
			this.antiguedad_minutos.HeaderText = "Ant. Minutos";
			this.antiguedad_minutos.Name = "antiguedad_minutos";
			this.antiguedad_minutos.ReadOnly = true;
			// 
			// antiguedad_segundos
			// 
			this.antiguedad_segundos.DataPropertyName = "antiguedad_segundos";
			this.antiguedad_segundos.HeaderText = "Ant. Segundos";
			this.antiguedad_segundos.Name = "antiguedad_segundos";
			this.antiguedad_segundos.ReadOnly = true;
			// 
			// e_excepciones_semaforo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(876, 304);
			this.Controls.Add(this.labelSemaforo);
			this.Controls.Add(this.dgvExcepciones);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "e_excepciones_semaforo";
			((System.ComponentModel.ISupportInitialize)(this.dgvExcepciones)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvExcepciones;
		private System.Windows.Forms.Label labelSemaforo;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_excepcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn no_solicitud;
		private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_presentacion;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_envio;
		private System.Windows.Forms.DataGridViewTextBoxColumn antiguedad_meses;
		private System.Windows.Forms.DataGridViewTextBoxColumn antiguedad_dias;
		private System.Windows.Forms.DataGridViewTextBoxColumn antiguedad_horas;
		private System.Windows.Forms.DataGridViewTextBoxColumn antiguedad_minutos;
		private System.Windows.Forms.DataGridViewTextBoxColumn antiguedad_segundos;
	}
}