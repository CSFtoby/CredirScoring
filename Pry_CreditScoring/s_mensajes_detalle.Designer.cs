namespace Docsis_Application
{
    partial class s_mensajes_detalle
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label7 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.gv_avisos = new System.Windows.Forms.DataGridView();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.no_entrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.usuario_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estacion_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estacion_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tipo_mensaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mensaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.leido = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gv_avisos)).BeginInit();
			this.SuspendLayout();
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.Black;
			this.label7.Location = new System.Drawing.Point(62, 11);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(71, 24);
			this.label7.TabIndex = 95;
			this.label7.Text = "Avisos";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.sobres;
			this.pictureBox1.Location = new System.Drawing.Point(2, 1);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(54, 50);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 94;
			this.pictureBox1.TabStop = false;
			// 
			// gv_avisos
			// 
			this.gv_avisos.AllowUserToAddRows = false;
			this.gv_avisos.AllowUserToDeleteRows = false;
			this.gv_avisos.AllowUserToResizeRows = false;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
			this.gv_avisos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
			this.gv_avisos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gv_avisos.BackgroundColor = System.Drawing.Color.White;
			this.gv_avisos.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gv_avisos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.gv_avisos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.Color.DodgerBlue;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gv_avisos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
			this.gv_avisos.ColumnHeadersHeight = 20;
			this.gv_avisos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no_entrada,
            this.usuario_from,
            this.estacion_from,
            this.estacion_to,
            this.tipo_mensaje,
            this.fecha,
            this.mensaje,
            this.leido});
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gv_avisos.DefaultCellStyle = dataGridViewCellStyle11;
			this.gv_avisos.GridColor = System.Drawing.Color.LightSteelBlue;
			this.gv_avisos.Location = new System.Drawing.Point(1, 55);
			this.gv_avisos.Name = "gv_avisos";
			this.gv_avisos.ReadOnly = true;
			this.gv_avisos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle12.ForeColor = System.Drawing.Color.DodgerBlue;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gv_avisos.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			this.gv_avisos.RowHeadersVisible = false;
			this.gv_avisos.RowHeadersWidth = 10;
			this.gv_avisos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gv_avisos.Size = new System.Drawing.Size(1022, 289);
			this.gv_avisos.TabIndex = 96;
			this.gv_avisos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_avisos_CellClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 346);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1035, 22);
			this.statusStrip1.TabIndex = 97;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// no_entrada
			// 
			this.no_entrada.DataPropertyName = "no_entrada";
			this.no_entrada.HeaderText = "No.";
			this.no_entrada.Name = "no_entrada";
			this.no_entrada.ReadOnly = true;
			this.no_entrada.Width = 70;
			// 
			// usuario_from
			// 
			this.usuario_from.DataPropertyName = "usuario_from";
			this.usuario_from.HeaderText = "De:";
			this.usuario_from.Name = "usuario_from";
			this.usuario_from.ReadOnly = true;
			// 
			// estacion_from
			// 
			this.estacion_from.DataPropertyName = "estacion_from";
			this.estacion_from.HeaderText = "Estación De:";
			this.estacion_from.Name = "estacion_from";
			this.estacion_from.ReadOnly = true;
			this.estacion_from.Width = 150;
			// 
			// estacion_to
			// 
			this.estacion_to.DataPropertyName = "estacion_to";
			this.estacion_to.HeaderText = "Estación Para";
			this.estacion_to.Name = "estacion_to";
			this.estacion_to.ReadOnly = true;
			this.estacion_to.Width = 150;
			// 
			// tipo_mensaje
			// 
			this.tipo_mensaje.DataPropertyName = "tipo_mensaje";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.tipo_mensaje.DefaultCellStyle = dataGridViewCellStyle9;
			this.tipo_mensaje.HeaderText = "Tipo Mensaje";
			this.tipo_mensaje.Name = "tipo_mensaje";
			this.tipo_mensaje.ReadOnly = true;
			// 
			// fecha
			// 
			this.fecha.DataPropertyName = "fecha";
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.fecha.DefaultCellStyle = dataGridViewCellStyle10;
			this.fecha.HeaderText = "Fecha Envío";
			this.fecha.Name = "fecha";
			this.fecha.ReadOnly = true;
			// 
			// mensaje
			// 
			this.mensaje.DataPropertyName = "mensaje";
			this.mensaje.HeaderText = "Mensaje";
			this.mensaje.Name = "mensaje";
			this.mensaje.ReadOnly = true;
			this.mensaje.Width = 300;
			// 
			// leido
			// 
			this.leido.DataPropertyName = "leido";
			this.leido.FalseValue = "N";
			this.leido.HeaderText = "Leído";
			this.leido.IndeterminateValue = "";
			this.leido.Name = "leido";
			this.leido.ReadOnly = true;
			this.leido.TrueValue = "S";
			this.leido.Width = 50;
			// 
			// s_mensajes_detalle
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.fondo_formularios2;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(1035, 368);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.gv_avisos);
			this.DoubleBuffered = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "s_mensajes_detalle";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = ":::";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.s_mensajes_detalle_FormClosing);
			this.Load += new System.EventHandler(this.s_mensajes_detalle_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gv_avisos)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView gv_avisos;
        private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.DataGridViewTextBoxColumn no_entrada;
		private System.Windows.Forms.DataGridViewTextBoxColumn usuario_from;
		private System.Windows.Forms.DataGridViewTextBoxColumn estacion_from;
		private System.Windows.Forms.DataGridViewTextBoxColumn estacion_to;
		private System.Windows.Forms.DataGridViewTextBoxColumn tipo_mensaje;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
		private System.Windows.Forms.DataGridViewTextBoxColumn mensaje;
		private System.Windows.Forms.DataGridViewCheckBoxColumn leido;
	}
}