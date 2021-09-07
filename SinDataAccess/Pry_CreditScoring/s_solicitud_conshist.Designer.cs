namespace Docsis_Application
{
    partial class s_solicitud_conshist
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Titulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtCodigo_cliente = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtPrimer_apellido = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gvSolicHist = new System.Windows.Forms.DataGridView();
            this.img_estado_registro = new System.Windows.Forms.DataGridViewImageColumn();
            this.no_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.abierta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_presentacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_agencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oficial_servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc_sub_aplicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto_solicitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.antiguedad_horas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSolicHist)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 418);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1094, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.LinkColor = System.Drawing.Color.MidnightBlue;
            this.linkLabel1.Location = new System.Drawing.Point(11, 394);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(62, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Ver tramites";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label_Titulo);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1094, 62);
            this.panel1.TabIndex = 111;
            // 
            // label_Titulo
            // 
            this.label_Titulo.AutoSize = true;
            this.label_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Titulo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label_Titulo.Location = new System.Drawing.Point(74, 29);
            this.label_Titulo.Name = "label_Titulo";
            this.label_Titulo.Size = new System.Drawing.Size(308, 22);
            this.label_Titulo.TabIndex = 0;
            this.label_Titulo.Text = "::: Consulta Historica de Solicitudes ::";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.icon_historico;
            this.pictureBox1.Location = new System.Drawing.Point(3, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 58);
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login1;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.txtCodigo_cliente);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtNombre);
            this.panel2.Controls.Add(this.txtPrimer_apellido);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.gvSolicHist);
            this.panel2.Location = new System.Drawing.Point(0, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1094, 326);
            this.panel2.TabIndex = 112;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::Docsis_Application.Properties.Resources.buscar20x20;
            this.pictureBox2.Location = new System.Drawing.Point(227, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 23);
            this.pictureBox2.TabIndex = 118;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // txtCodigo_cliente
            // 
            this.txtCodigo_cliente.Location = new System.Drawing.Point(129, 4);
            this.txtCodigo_cliente.Name = "txtCodigo_cliente";
            this.txtCodigo_cliente.Size = new System.Drawing.Size(94, 20);
            this.txtCodigo_cliente.TabIndex = 111;
            this.txtCodigo_cliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_cliente_KeyDown);
            this.txtCodigo_cliente.Leave += new System.EventHandler(this.txtCodigo_cliente_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(12, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 112;
            this.label6.Text = "Código de Cliente :";
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.White;
            this.txtNombre.Location = new System.Drawing.Point(70, 29);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(242, 20);
            this.txtNombre.TabIndex = 114;
            // 
            // txtPrimer_apellido
            // 
            this.txtPrimer_apellido.BackColor = System.Drawing.Color.White;
            this.txtPrimer_apellido.Location = new System.Drawing.Point(70, 53);
            this.txtPrimer_apellido.Name = "txtPrimer_apellido";
            this.txtPrimer_apellido.ReadOnly = true;
            this.txtPrimer_apellido.Size = new System.Drawing.Size(242, 20);
            this.txtPrimer_apellido.TabIndex = 116;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(13, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "Nombres :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(13, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 115;
            this.label3.Text = "Apellidos :";
            // 
            // gvSolicHist
            // 
            this.gvSolicHist.AllowUserToAddRows = false;
            this.gvSolicHist.AllowUserToDeleteRows = false;
            this.gvSolicHist.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gvSolicHist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSolicHist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvSolicHist.BackgroundColor = System.Drawing.Color.White;
            this.gvSolicHist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvSolicHist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvSolicHist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSolicHist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSolicHist.ColumnHeadersHeight = 20;
            this.gvSolicHist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.img_estado_registro,
            this.no_solicitud,
            this.abierta,
            this.estado,
            this.fecha_presentacion,
            this.nombre_agencia,
            this.oficial_servicio,
            this.desc_sub_aplicacion,
            this.monto_solicitado,
            this.antiguedad_horas});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvSolicHist.DefaultCellStyle = dataGridViewCellStyle8;
            this.gvSolicHist.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvSolicHist.Location = new System.Drawing.Point(8, 82);
            this.gvSolicHist.Name = "gvSolicHist";
            this.gvSolicHist.ReadOnly = true;
            this.gvSolicHist.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvSolicHist.RowHeadersVisible = false;
            this.gvSolicHist.RowHeadersWidth = 10;
            this.gvSolicHist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvSolicHist.Size = new System.Drawing.Size(1079, 237);
            this.gvSolicHist.TabIndex = 117;
            // 
            // img_estado_registro
            // 
            this.img_estado_registro.DataPropertyName = "img_estado_registro";
            this.img_estado_registro.HeaderText = "";
            this.img_estado_registro.Name = "img_estado_registro";
            this.img_estado_registro.ReadOnly = true;
            this.img_estado_registro.Visible = false;
            this.img_estado_registro.Width = 20;
            // 
            // no_solicitud
            // 
            this.no_solicitud.DataPropertyName = "no_solicitud";
            this.no_solicitud.HeaderText = "No. Solicitud";
            this.no_solicitud.Name = "no_solicitud";
            this.no_solicitud.ReadOnly = true;
            this.no_solicitud.ToolTipText = "No de Solicitud";
            this.no_solicitud.Width = 80;
            // 
            // abierta
            // 
            this.abierta.DataPropertyName = "abierta";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = null;
            this.abierta.DefaultCellStyle = dataGridViewCellStyle3;
            this.abierta.HeaderText = "Abierta";
            this.abierta.Name = "abierta";
            this.abierta.ReadOnly = true;
            this.abierta.Width = 80;
            // 
            // estado
            // 
            this.estado.DataPropertyName = "estado";
            dataGridViewCellStyle4.Format = "f";
            dataGridViewCellStyle4.NullValue = null;
            this.estado.DefaultCellStyle = dataGridViewCellStyle4;
            this.estado.HeaderText = "Estado";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            this.estado.Width = 160;
            // 
            // fecha_presentacion
            // 
            this.fecha_presentacion.DataPropertyName = "fecha_presentacion";
            this.fecha_presentacion.HeaderText = "Fecha ";
            this.fecha_presentacion.Name = "fecha_presentacion";
            this.fecha_presentacion.ReadOnly = true;
            this.fecha_presentacion.Width = 160;
            // 
            // nombre_agencia
            // 
            this.nombre_agencia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nombre_agencia.DataPropertyName = "nombre_agencia";
            this.nombre_agencia.HeaderText = "Filial de Tramite";
            this.nombre_agencia.Name = "nombre_agencia";
            this.nombre_agencia.ReadOnly = true;
            this.nombre_agencia.Width = 103;
            // 
            // oficial_servicio
            // 
            this.oficial_servicio.DataPropertyName = "oficial_servicio";
            this.oficial_servicio.HeaderText = "Oficial Servicio";
            this.oficial_servicio.Name = "oficial_servicio";
            this.oficial_servicio.ReadOnly = true;
            this.oficial_servicio.Width = 150;
            // 
            // desc_sub_aplicacion
            // 
            this.desc_sub_aplicacion.DataPropertyName = "desc_sub_aplicacion";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.desc_sub_aplicacion.DefaultCellStyle = dataGridViewCellStyle5;
            this.desc_sub_aplicacion.HeaderText = "Producto";
            this.desc_sub_aplicacion.Name = "desc_sub_aplicacion";
            this.desc_sub_aplicacion.ReadOnly = true;
            this.desc_sub_aplicacion.Width = 150;
            // 
            // monto_solicitado
            // 
            this.monto_solicitado.DataPropertyName = "monto_solicitado";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.monto_solicitado.DefaultCellStyle = dataGridViewCellStyle6;
            this.monto_solicitado.HeaderText = "Monto";
            this.monto_solicitado.Name = "monto_solicitado";
            this.monto_solicitado.ReadOnly = true;
            this.monto_solicitado.Width = 90;
            // 
            // antiguedad_horas
            // 
            this.antiguedad_horas.DataPropertyName = "meses_plazo";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.antiguedad_horas.DefaultCellStyle = dataGridViewCellStyle7;
            this.antiguedad_horas.HeaderText = "Plazo";
            this.antiguedad_horas.Name = "antiguedad_horas";
            this.antiguedad_horas.ReadOnly = true;
            this.antiguedad_horas.ToolTipText = "Meses Plazo";
            // 
            // s_solicitud_conshist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1094, 440);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "s_solicitud_conshist";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ::: Solicitudes Historicas";
            this.Load += new System.EventHandler(this.s_solicitud_conshist_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSolicHist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Titulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtCodigo_cliente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtPrimer_apellido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gvSolicHist;
        private System.Windows.Forms.DataGridViewImageColumn img_estado_registro;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn abierta;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_presentacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_agencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn oficial_servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc_sub_aplicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn monto_solicitado;
        private System.Windows.Forms.DataGridViewTextBoxColumn antiguedad_horas;
    }
}