namespace Docsis_Application
{
    partial class s_excepciones_doc02
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label1 = new System.Windows.Forms.Label();
			this.panelTop = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.label_header = new System.Windows.Forms.Label();
			this.btnSalir = new System.Windows.Forms.Button();
			this.btnCerrar = new System.Windows.Forms.Button();
			this.dgvDetalle = new System.Windows.Forms.DataGridView();
			this.contador = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cod_lineamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lineamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cod_excepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tipo_excepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.justificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label1.Location = new System.Drawing.Point(12, 73);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(179, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Excepciones Solicitadas";
			// 
			// panelTop
			// 
			this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panelTop.Controls.Add(this.btnClose);
			this.panelTop.Controls.Add(this.label_header);
			this.panelTop.Controls.Add(this.btnSalir);
			this.panelTop.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.panelTop.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.panelTop.Location = new System.Drawing.Point(1, 1);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(704, 25);
			this.panelTop.TabIndex = 278;
			this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
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
			this.btnClose.Location = new System.Drawing.Point(684, 4);
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
			this.label_header.Location = new System.Drawing.Point(1, 1);
			this.label_header.Name = "label_header";
			this.label_header.Size = new System.Drawing.Size(167, 21);
			this.label_header.TabIndex = 0;
			this.label_header.Text = "Detalle de la excepción";
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
			// btnCerrar
			// 
			this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCerrar.FlatAppearance.BorderSize = 0;
			this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCerrar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.btnCerrar.ForeColor = System.Drawing.Color.White;
			this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCerrar.Location = new System.Drawing.Point(560, 285);
			this.btnCerrar.Name = "btnCerrar";
			this.btnCerrar.Size = new System.Drawing.Size(120, 33);
			this.btnCerrar.TabIndex = 280;
			this.btnCerrar.Text = "Cerrar";
			this.btnCerrar.UseVisualStyleBackColor = false;
			this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
			// 
			// dgvDetalle
			// 
			this.dgvDetalle.AllowUserToAddRows = false;
			this.dgvDetalle.AllowUserToDeleteRows = false;
			this.dgvDetalle.AllowUserToResizeRows = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			this.dgvDetalle.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvDetalle.BackgroundColor = System.Drawing.Color.White;
			this.dgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgvDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvDetalle.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.dgvDetalle.ColumnHeadersHeight = 20;
			this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.contador,
            this.cod_lineamiento,
            this.lineamiento,
            this.cod_excepcion,
            this.tipo_excepcion,
            this.justificacion});
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle6;
			this.dgvDetalle.GridColor = System.Drawing.Color.LightSteelBlue;
			this.dgvDetalle.Location = new System.Drawing.Point(15, 116);
			this.dgvDetalle.Name = "dgvDetalle";
			this.dgvDetalle.ReadOnly = true;
			this.dgvDetalle.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvDetalle.RowHeadersVisible = false;
			this.dgvDetalle.RowHeadersWidth = 10;
			this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvDetalle.Size = new System.Drawing.Size(665, 121);
			this.dgvDetalle.TabIndex = 281;
			// 
			// contador
			// 
			this.contador.HeaderText = "No.";
			this.contador.Name = "contador";
			this.contador.ReadOnly = true;
			this.contador.Width = 50;
			// 
			// cod_lineamiento
			// 
			this.cod_lineamiento.DataPropertyName = "cod_lineamiento";
			this.cod_lineamiento.HeaderText = "Código Lineamiento";
			this.cod_lineamiento.Name = "cod_lineamiento";
			this.cod_lineamiento.ReadOnly = true;
			this.cod_lineamiento.Width = 150;
			// 
			// lineamiento
			// 
			this.lineamiento.DataPropertyName = "lineamiento";
			this.lineamiento.HeaderText = "Lineamiento";
			this.lineamiento.Name = "lineamiento";
			this.lineamiento.ReadOnly = true;
			this.lineamiento.Width = 200;
			// 
			// cod_excepcion
			// 
			this.cod_excepcion.DataPropertyName = "cod_tipo_excepcion";
			this.cod_excepcion.HeaderText = "Codigo Excepción";
			this.cod_excepcion.Name = "cod_excepcion";
			this.cod_excepcion.ReadOnly = true;
			// 
			// tipo_excepcion
			// 
			this.tipo_excepcion.DataPropertyName = "tipo_excepcion";
			this.tipo_excepcion.HeaderText = "Excepción";
			this.tipo_excepcion.Name = "tipo_excepcion";
			this.tipo_excepcion.ReadOnly = true;
			this.tipo_excepcion.Width = 200;
			// 
			// justificacion
			// 
			this.justificacion.DataPropertyName = "observaciones";
			this.justificacion.HeaderText = "Justificación";
			this.justificacion.Name = "justificacion";
			this.justificacion.ReadOnly = true;
			this.justificacion.Width = 250;
			// 
			// s_excepciones_doc02
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
			this.ClientSize = new System.Drawing.Size(704, 344);
			this.Controls.Add(this.dgvDetalle);
			this.Controls.Add(this.btnCerrar);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "s_excepciones_doc02";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "s_excepciones_doc02";
			this.Load += new System.EventHandler(this.s_excepciones_doc02_Load);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label label_header;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCerrar;
		private System.Windows.Forms.DataGridView dgvDetalle;
		private System.Windows.Forms.DataGridViewTextBoxColumn contador;
		private System.Windows.Forms.DataGridViewTextBoxColumn cod_lineamiento;
		private System.Windows.Forms.DataGridViewTextBoxColumn lineamiento;
		private System.Windows.Forms.DataGridViewTextBoxColumn cod_excepcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn tipo_excepcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn justificacion;
	}
}