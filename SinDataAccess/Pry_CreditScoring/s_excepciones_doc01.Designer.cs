namespace Docsis_Application
{
    partial class s_excepciones_doc01
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
			this.panelTop = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.label_header = new System.Windows.Forms.Label();
			this.btnSalir = new System.Windows.Forms.Button();
			this.txtCliente = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtNoSolicitud = new System.Windows.Forms.TextBox();
			this.txtProducto = new System.Windows.Forms.TextBox();
			this.txtMonto = new System.Windows.Forms.TextBox();
			this.label97 = new System.Windows.Forms.Label();
			this.labelProducto = new System.Windows.Forms.Label();
			this.label249 = new System.Windows.Forms.Label();
			this.gvExcepciones = new System.Windows.Forms.DataGridView();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.labelUser_comite = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnCerrar = new System.Windows.Forms.Button();
			this.btnObtener_cuotas_buro = new System.Windows.Forms.Button();
			this.codigo_excepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estado_excep = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fecha_presentacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estacion_actual = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pago_mediante = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fecha_cierre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ver_detalle = new System.Windows.Forms.DataGridViewImageColumn();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gvExcepciones)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
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
			this.panelTop.Location = new System.Drawing.Point(1, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(813, 35);
			this.panelTop.TabIndex = 103;
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
			this.btnClose.Location = new System.Drawing.Point(788, 10);
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
			this.label_header.Location = new System.Drawing.Point(6, 6);
			this.label_header.Name = "label_header";
			this.label_header.Size = new System.Drawing.Size(201, 21);
			this.label_header.TabIndex = 0;
			this.label_header.Text = "Otorgamiento de Excepcion";
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
			// txtCliente
			// 
			this.txtCliente.BackColor = System.Drawing.Color.White;
			this.txtCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtCliente.Enabled = false;
			this.txtCliente.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCliente.Location = new System.Drawing.Point(93, 64);
			this.txtCliente.Multiline = true;
			this.txtCliente.Name = "txtCliente";
			this.txtCliente.ReadOnly = true;
			this.txtCliente.Size = new System.Drawing.Size(309, 21);
			this.txtCliente.TabIndex = 105;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(8, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 104;
			this.label2.Text = "Solicitante :";
			// 
			// txtNoSolicitud
			// 
			this.txtNoSolicitud.BackColor = System.Drawing.Color.White;
			this.txtNoSolicitud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtNoSolicitud.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNoSolicitud.Location = new System.Drawing.Point(93, 39);
			this.txtNoSolicitud.MaxLength = 12;
			this.txtNoSolicitud.Name = "txtNoSolicitud";
			this.txtNoSolicitud.ReadOnly = true;
			this.txtNoSolicitud.Size = new System.Drawing.Size(90, 22);
			this.txtNoSolicitud.TabIndex = 112;
			// 
			// txtProducto
			// 
			this.txtProducto.BackColor = System.Drawing.Color.White;
			this.txtProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtProducto.Enabled = false;
			this.txtProducto.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtProducto.Location = new System.Drawing.Point(93, 88);
			this.txtProducto.Multiline = true;
			this.txtProducto.Name = "txtProducto";
			this.txtProducto.ReadOnly = true;
			this.txtProducto.Size = new System.Drawing.Size(309, 21);
			this.txtProducto.TabIndex = 107;
			// 
			// txtMonto
			// 
			this.txtMonto.BackColor = System.Drawing.Color.White;
			this.txtMonto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMonto.Enabled = false;
			this.txtMonto.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMonto.Location = new System.Drawing.Point(93, 111);
			this.txtMonto.Multiline = true;
			this.txtMonto.Name = "txtMonto";
			this.txtMonto.ReadOnly = true;
			this.txtMonto.Size = new System.Drawing.Size(122, 21);
			this.txtMonto.TabIndex = 109;
			// 
			// label97
			// 
			this.label97.AutoSize = true;
			this.label97.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label97.ForeColor = System.Drawing.Color.Black;
			this.label97.Location = new System.Drawing.Point(8, 115);
			this.label97.Name = "label97";
			this.label97.Size = new System.Drawing.Size(45, 13);
			this.label97.TabIndex = 108;
			this.label97.Text = "Monto:";
			// 
			// labelProducto
			// 
			this.labelProducto.AutoSize = true;
			this.labelProducto.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelProducto.ForeColor = System.Drawing.Color.Black;
			this.labelProducto.Location = new System.Drawing.Point(8, 92);
			this.labelProducto.Name = "labelProducto";
			this.labelProducto.Size = new System.Drawing.Size(60, 13);
			this.labelProducto.TabIndex = 106;
			this.labelProducto.Text = "Producto :";
			// 
			// label249
			// 
			this.label249.AutoSize = true;
			this.label249.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label249.ForeColor = System.Drawing.Color.Black;
			this.label249.Location = new System.Drawing.Point(8, 44);
			this.label249.Name = "label249";
			this.label249.Size = new System.Drawing.Size(79, 13);
			this.label249.TabIndex = 113;
			this.label249.Text = "No. Solicitud :";
			// 
			// gvExcepciones
			// 
			this.gvExcepciones.AllowUserToAddRows = false;
			this.gvExcepciones.AllowUserToDeleteRows = false;
			this.gvExcepciones.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			this.gvExcepciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.gvExcepciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gvExcepciones.BackgroundColor = System.Drawing.Color.White;
			this.gvExcepciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gvExcepciones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.gvExcepciones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gvExcepciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.gvExcepciones.ColumnHeadersHeight = 20;
			this.gvExcepciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo_excepcion,
            this.estado_excep,
            this.fecha_presentacion,
            this.estacion_actual,
            this.pago_mediante,
            this.fecha_cierre,
            this.ver_detalle});
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gvExcepciones.DefaultCellStyle = dataGridViewCellStyle4;
			this.gvExcepciones.GridColor = System.Drawing.Color.LightSteelBlue;
			this.gvExcepciones.Location = new System.Drawing.Point(25, 169);
			this.gvExcepciones.Name = "gvExcepciones";
			this.gvExcepciones.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.gvExcepciones.RowHeadersVisible = false;
			this.gvExcepciones.RowHeadersWidth = 10;
			this.gvExcepciones.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.gvExcepciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gvExcepciones.Size = new System.Drawing.Size(753, 131);
			this.gvExcepciones.TabIndex = 114;
			this.gvExcepciones.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvExcepciones_CellDoubleClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.Color.Silver;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelUser_comite});
			this.statusStrip1.Location = new System.Drawing.Point(0, 367);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(812, 22);
			this.statusStrip1.TabIndex = 115;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// labelUser_comite
			// 
			this.labelUser_comite.Name = "labelUser_comite";
			this.labelUser_comite.Size = new System.Drawing.Size(13, 17);
			this.labelUser_comite.Text = "::";
			// 
			// btnCerrar
			// 
			this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCerrar.FlatAppearance.BorderSize = 0;
			this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCerrar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.btnCerrar.ForeColor = System.Drawing.Color.White;
			this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCerrar.Location = new System.Drawing.Point(643, 319);
			this.btnCerrar.Name = "btnCerrar";
			this.btnCerrar.Size = new System.Drawing.Size(120, 33);
			this.btnCerrar.TabIndex = 119;
			this.btnCerrar.Text = "Cerrar";
			this.btnCerrar.UseVisualStyleBackColor = false;
			this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
			// 
			// btnObtener_cuotas_buro
			// 
			this.btnObtener_cuotas_buro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnObtener_cuotas_buro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnObtener_cuotas_buro.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnObtener_cuotas_buro.FlatAppearance.BorderSize = 0;
			this.btnObtener_cuotas_buro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnObtener_cuotas_buro.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.btnObtener_cuotas_buro.ForeColor = System.Drawing.Color.White;
			this.btnObtener_cuotas_buro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnObtener_cuotas_buro.Location = new System.Drawing.Point(598, 141);
			this.btnObtener_cuotas_buro.Name = "btnObtener_cuotas_buro";
			this.btnObtener_cuotas_buro.Size = new System.Drawing.Size(165, 22);
			this.btnObtener_cuotas_buro.TabIndex = 117;
			this.btnObtener_cuotas_buro.Text = "Agregar excepción";
			this.btnObtener_cuotas_buro.UseVisualStyleBackColor = false;
			this.btnObtener_cuotas_buro.Visible = false;
			// 
			// codigo_excepcion
			// 
			this.codigo_excepcion.DataPropertyName = "codigo_excepcion";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
			this.codigo_excepcion.DefaultCellStyle = dataGridViewCellStyle3;
			this.codigo_excepcion.HeaderText = "No.";
			this.codigo_excepcion.Name = "codigo_excepcion";
			this.codigo_excepcion.ReadOnly = true;
			this.codigo_excepcion.Width = 50;
			// 
			// estado_excep
			// 
			this.estado_excep.DataPropertyName = "estado_excep";
			this.estado_excep.HeaderText = "Estado";
			this.estado_excep.Name = "estado_excep";
			this.estado_excep.ReadOnly = true;
			this.estado_excep.Width = 150;
			// 
			// fecha_presentacion
			// 
			this.fecha_presentacion.DataPropertyName = "fecha_presentacion";
			this.fecha_presentacion.HeaderText = "Fecha Presentación";
			this.fecha_presentacion.Name = "fecha_presentacion";
			this.fecha_presentacion.ReadOnly = true;
			// 
			// estacion_actual
			// 
			this.estacion_actual.DataPropertyName = "estacion_actual";
			this.estacion_actual.HeaderText = "Estacion Actual";
			this.estacion_actual.Name = "estacion_actual";
			this.estacion_actual.ReadOnly = true;
			this.estacion_actual.Width = 150;
			// 
			// pago_mediante
			// 
			this.pago_mediante.DataPropertyName = "pago_mediante";
			this.pago_mediante.HeaderText = "Pago Mediante";
			this.pago_mediante.Name = "pago_mediante";
			this.pago_mediante.ReadOnly = true;
			// 
			// fecha_cierre
			// 
			this.fecha_cierre.DataPropertyName = "fecha_cierre";
			this.fecha_cierre.HeaderText = "Fecha Cierre";
			this.fecha_cierre.Name = "fecha_cierre";
			this.fecha_cierre.ReadOnly = true;
			// 
			// ver_detalle
			// 
			this.ver_detalle.HeaderText = "Ver Detalle";
			this.ver_detalle.Image = global::Docsis_Application.Properties.Resources.confirmacion_recepcion;
			this.ver_detalle.Name = "ver_detalle";
			this.ver_detalle.ReadOnly = true;
			// 
			// s_excepciones_doc01
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
			this.ClientSize = new System.Drawing.Size(812, 389);
			this.Controls.Add(this.btnCerrar);
			this.Controls.Add(this.btnObtener_cuotas_buro);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.gvExcepciones);
			this.Controls.Add(this.label249);
			this.Controls.Add(this.txtCliente);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtNoSolicitud);
			this.Controls.Add(this.txtProducto);
			this.Controls.Add(this.txtMonto);
			this.Controls.Add(this.label97);
			this.Controls.Add(this.labelProducto);
			this.Controls.Add(this.panelTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "s_excepciones_doc01";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Excepciones ";
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gvExcepciones)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label label_header;
        private System.Windows.Forms.Button btnSalir;
        public System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtNoSolicitud;
        public System.Windows.Forms.TextBox txtProducto;
        public System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label labelProducto;
        private System.Windows.Forms.Label label249;
        private System.Windows.Forms.DataGridView gvExcepciones;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelUser_comite;
        private System.Windows.Forms.Button btnCerrar;
		private System.Windows.Forms.Button btnObtener_cuotas_buro;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_excepcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn estado_excep;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_presentacion;
		private System.Windows.Forms.DataGridViewTextBoxColumn estacion_actual;
		private System.Windows.Forms.DataGridViewTextBoxColumn pago_mediante;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_cierre;
		private System.Windows.Forms.DataGridViewImageColumn ver_detalle;
	}
}