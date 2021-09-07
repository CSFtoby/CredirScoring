namespace Docsis_Application
{
    partial class s_consclientes
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(s_consclientes));
            this.gvClientes = new System.Windows.Forms.DataGridView();
            this.icono = new System.Windows.Forms.DataGridViewImageColumn();
            this.codigo_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.primer_apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.segundo_apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sexo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESC_TIPO_CLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_nacimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo_persona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lugar_de_trabajo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Activo_b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pbsinfoto = new System.Windows.Forms.PictureBox();
            this.button_ejecutar_consulta = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCampos = new System.Windows.Forms.ComboBox();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.txtCodigo_cliente = new System.Windows.Forms.TextBox();
            this.Button_ok = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelCons = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvClientes)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbsinfoto)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvClientes
            // 
            this.gvClientes.AllowUserToAddRows = false;
            this.gvClientes.AllowUserToDeleteRows = false;
            this.gvClientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.Black;
            this.gvClientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle25;
            this.gvClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvClientes.BackgroundColor = System.Drawing.Color.White;
            this.gvClientes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvClientes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvClientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.gvClientes.ColumnHeadersHeight = 20;
            this.gvClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icono,
            this.codigo_cliente,
            this.nombres,
            this.primer_apellido,
            this.segundo_apellido,
            this.sexo,
            this.DESC_TIPO_CLIENTE,
            this.fecha_nacimiento,
            this.Edad,
            this.tipo_persona,
            this.lugar_de_trabajo,
            this.Activo_b});
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvClientes.DefaultCellStyle = dataGridViewCellStyle32;
            this.gvClientes.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvClientes.Location = new System.Drawing.Point(4, 73);
            this.gvClientes.Name = "gvClientes";
            this.gvClientes.ReadOnly = true;
            this.gvClientes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvClientes.RowHeadersVisible = false;
            this.gvClientes.RowHeadersWidth = 10;
            this.gvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvClientes.Size = new System.Drawing.Size(1072, 331);
            this.gvClientes.TabIndex = 21;
            this.gvClientes.DoubleClick += new System.EventHandler(this.gvClientes_DoubleClick);
            // 
            // icono
            // 
            this.icono.DataPropertyName = "foto";
            this.icono.HeaderText = "";
            this.icono.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.icono.Name = "icono";
            this.icono.ReadOnly = true;
            this.icono.Width = 40;
            // 
            // codigo_cliente
            // 
            this.codigo_cliente.DataPropertyName = "codigo_cliente";
            this.codigo_cliente.HeaderText = "Codigo Cliente";
            this.codigo_cliente.Name = "codigo_cliente";
            this.codigo_cliente.ReadOnly = true;
            this.codigo_cliente.Width = 130;
            // 
            // nombres
            // 
            this.nombres.DataPropertyName = "nombres";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nombres.DefaultCellStyle = dataGridViewCellStyle27;
            this.nombres.HeaderText = "Nombre del Cliente";
            this.nombres.Name = "nombres";
            this.nombres.ReadOnly = true;
            this.nombres.Width = 200;
            // 
            // primer_apellido
            // 
            this.primer_apellido.DataPropertyName = "primer_apellido";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.primer_apellido.DefaultCellStyle = dataGridViewCellStyle28;
            this.primer_apellido.HeaderText = "Primer Apellido";
            this.primer_apellido.Name = "primer_apellido";
            this.primer_apellido.ReadOnly = true;
            this.primer_apellido.Width = 160;
            // 
            // segundo_apellido
            // 
            this.segundo_apellido.DataPropertyName = "segundo_apellido";
            this.segundo_apellido.HeaderText = "Segundo Apellido";
            this.segundo_apellido.Name = "segundo_apellido";
            this.segundo_apellido.ReadOnly = true;
            this.segundo_apellido.Width = 160;
            // 
            // sexo
            // 
            this.sexo.DataPropertyName = "sexo";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.sexo.DefaultCellStyle = dataGridViewCellStyle29;
            this.sexo.HeaderText = "Sexo";
            this.sexo.Name = "sexo";
            this.sexo.ReadOnly = true;
            // 
            // DESC_TIPO_CLIENTE
            // 
            this.DESC_TIPO_CLIENTE.DataPropertyName = "desc_tipo_cliente";
            this.DESC_TIPO_CLIENTE.HeaderText = "Tipo Cliente";
            this.DESC_TIPO_CLIENTE.Name = "DESC_TIPO_CLIENTE";
            this.DESC_TIPO_CLIENTE.ReadOnly = true;
            this.DESC_TIPO_CLIENTE.Width = 130;
            // 
            // fecha_nacimiento
            // 
            this.fecha_nacimiento.DataPropertyName = "fecha_nacimiento";
            this.fecha_nacimiento.HeaderText = "Fec. Nacimiento";
            this.fecha_nacimiento.Name = "fecha_nacimiento";
            this.fecha_nacimiento.ReadOnly = true;
            // 
            // Edad
            // 
            this.Edad.DataPropertyName = "edad";
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Edad.DefaultCellStyle = dataGridViewCellStyle30;
            this.Edad.HeaderText = "Edad";
            this.Edad.Name = "Edad";
            this.Edad.ReadOnly = true;
            // 
            // tipo_persona
            // 
            this.tipo_persona.DataPropertyName = "tipo_persona";
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tipo_persona.DefaultCellStyle = dataGridViewCellStyle31;
            this.tipo_persona.HeaderText = "Tipo Persona";
            this.tipo_persona.Name = "tipo_persona";
            this.tipo_persona.ReadOnly = true;
            // 
            // lugar_de_trabajo
            // 
            this.lugar_de_trabajo.DataPropertyName = "lugar_de_trabajo";
            this.lugar_de_trabajo.HeaderText = "Lugar de Trabajo..";
            this.lugar_de_trabajo.Name = "lugar_de_trabajo";
            this.lugar_de_trabajo.ReadOnly = true;
            this.lugar_de_trabajo.Width = 200;
            // 
            // Activo_b
            // 
            this.Activo_b.DataPropertyName = "activo_b";
            this.Activo_b.HeaderText = "Estado";
            this.Activo_b.Name = "Activo_b";
            this.Activo_b.ReadOnly = true;
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.button_ejecutar_consulta);
            this.panelTop.Controls.Add(this.cmbCampos);
            this.panelTop.Controls.Add(this.labelCons);
            this.panelTop.Controls.Add(this.txtTexto);
            this.panelTop.Controls.Add(this.pbsinfoto);
            this.panelTop.Location = new System.Drawing.Point(1, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1078, 63);
            this.panelTop.TabIndex = 98;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // pbsinfoto
            // 
            this.pbsinfoto.Location = new System.Drawing.Point(851, 33);
            this.pbsinfoto.Name = "pbsinfoto";
            this.pbsinfoto.Size = new System.Drawing.Size(24, 28);
            this.pbsinfoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbsinfoto.TabIndex = 101;
            this.pbsinfoto.TabStop = false;
            this.pbsinfoto.Visible = false;
            // 
            // button_ejecutar_consulta
            // 
            this.button_ejecutar_consulta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ejecutar_consulta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_ejecutar_consulta.FlatAppearance.BorderSize = 0;
            this.button_ejecutar_consulta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ejecutar_consulta.ForeColor = System.Drawing.Color.White;
            this.button_ejecutar_consulta.Image = global::Docsis_Application.Properties.Resources.buscar20x20;
            this.button_ejecutar_consulta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ejecutar_consulta.Location = new System.Drawing.Point(921, 21);
            this.button_ejecutar_consulta.Name = "button_ejecutar_consulta";
            this.button_ejecutar_consulta.Size = new System.Drawing.Size(106, 32);
            this.button_ejecutar_consulta.TabIndex = 6;
            this.button_ejecutar_consulta.Text = "Buscar";
            this.button_ejecutar_consulta.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button_ejecutar_consulta.UseVisualStyleBackColor = false;
            this.button_ejecutar_consulta.Click += new System.EventHandler(this.button_ejecutar_consulta_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(503, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Columna :";
            // 
            // cmbCampos
            // 
            this.cmbCampos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCampos.FormattingEnabled = true;
            this.cmbCampos.Items.AddRange(new object[] {
            "Codigo Cliente",
            "Nombres",
            "Nombres + 1er Apellidos",
            "Nombres + Ambos Apellidos"});
            this.cmbCampos.Location = new System.Drawing.Point(576, 36);
            this.cmbCampos.Name = "cmbCampos";
            this.cmbCampos.Size = new System.Drawing.Size(193, 21);
            this.cmbCampos.TabIndex = 4;
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(127, 37);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(370, 20);
            this.txtTexto.TabIndex = 1;
            this.txtTexto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTexto_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Criterios de busqueda :";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 459);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1081, 22);
            this.statusStrip1.TabIndex = 99;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 16);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.Visible = false;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "icono_masculino.png");
            this.imageList.Images.SetKeyName(1, "icono_femenino.png");
            // 
            // txtCodigo_cliente
            // 
            this.txtCodigo_cliente.Location = new System.Drawing.Point(12, 427);
            this.txtCodigo_cliente.Name = "txtCodigo_cliente";
            this.txtCodigo_cliente.Size = new System.Drawing.Size(77, 20);
            this.txtCodigo_cliente.TabIndex = 102;
            this.txtCodigo_cliente.Visible = false;
            // 
            // Button_ok
            // 
            this.Button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.Button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_ok.FlatAppearance.BorderSize = 0;
            this.Button_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_ok.ForeColor = System.Drawing.Color.White;
            this.Button_ok.Location = new System.Drawing.Point(335, 421);
            this.Button_ok.Name = "Button_ok";
            this.Button_ok.Size = new System.Drawing.Size(138, 28);
            this.Button_ok.TabIndex = 103;
            this.Button_ok.Text = "Seleccionar";
            this.Button_ok.UseVisualStyleBackColor = false;
            this.Button_ok.Click += new System.EventHandler(this.button_ok_Click_1);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(524, 421);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 28);
            this.button1.TabIndex = 104;
            this.button1.Text = "Cerrar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelCons
            // 
            this.labelCons.AutoSize = true;
            this.labelCons.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCons.ForeColor = System.Drawing.Color.Silver;
            this.labelCons.Location = new System.Drawing.Point(50, 8);
            this.labelCons.Name = "labelCons";
            this.labelCons.Size = new System.Drawing.Size(163, 21);
            this.labelCons.TabIndex = 102;
            this.labelCons.Text = "Busqueda de Afiliados";
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
            this.btnClose.Location = new System.Drawing.Point(1056, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 103;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // s_consclientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(1081, 481);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Button_ok);
            this.Controls.Add(this.txtCodigo_cliente);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.gvClientes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_consclientes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " :::";
            this.Load += new System.EventHandler(this.s_consclientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvClientes)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbsinfoto)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvClientes;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pbsinfoto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCampos;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private System.Windows.Forms.ImageList imageList;
        public System.Windows.Forms.TextBox txtCodigo_cliente;
        private System.Windows.Forms.Button Button_ok;
        private System.Windows.Forms.Button button_ejecutar_consulta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewImageColumn icono;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombres;
        private System.Windows.Forms.DataGridViewTextBoxColumn primer_apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn segundo_apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn sexo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESC_TIPO_CLIENTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_nacimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Edad;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo_persona;
        private System.Windows.Forms.DataGridViewTextBoxColumn lugar_de_trabajo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activo_b;
        public System.Windows.Forms.Label labelCons;
        private System.Windows.Forms.Button btnClose;
    }
}