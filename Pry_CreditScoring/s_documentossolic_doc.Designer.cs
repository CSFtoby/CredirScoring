namespace Docsis_Application
{
    partial class s_documentossolic_doc
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gvDocumentos_solicitud = new System.Windows.Forms.DataGridView();
			this.recibido_b = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.documento_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nombre_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tipo_exigencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.no_archivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Ver = new System.Windows.Forms.DataGridViewImageColumn();
			this.button1 = new System.Windows.Forms.Button();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.button_cerrar = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.label_header = new System.Windows.Forms.Label();
			this.btnSalir = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.gvDocumentos_solicitud)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// gvDocumentos_solicitud
			// 
			this.gvDocumentos_solicitud.AllowUserToAddRows = false;
			this.gvDocumentos_solicitud.AllowUserToDeleteRows = false;
			this.gvDocumentos_solicitud.AllowUserToResizeRows = false;
			dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
			this.gvDocumentos_solicitud.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
			this.gvDocumentos_solicitud.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gvDocumentos_solicitud.BackgroundColor = System.Drawing.Color.White;
			this.gvDocumentos_solicitud.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gvDocumentos_solicitud.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.gvDocumentos_solicitud.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gvDocumentos_solicitud.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
			this.gvDocumentos_solicitud.ColumnHeadersHeight = 20;
			this.gvDocumentos_solicitud.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recibido_b,
            this.documento_id,
            this.nombre_doc,
            this.tipo_exigencia,
            this.no_archivo,
            this.Ver});
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gvDocumentos_solicitud.DefaultCellStyle = dataGridViewCellStyle18;
			this.gvDocumentos_solicitud.GridColor = System.Drawing.Color.LightSteelBlue;
			this.gvDocumentos_solicitud.Location = new System.Drawing.Point(10, 41);
			this.gvDocumentos_solicitud.MultiSelect = false;
			this.gvDocumentos_solicitud.Name = "gvDocumentos_solicitud";
			this.gvDocumentos_solicitud.ReadOnly = true;
			this.gvDocumentos_solicitud.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.gvDocumentos_solicitud.RowHeadersVisible = false;
			this.gvDocumentos_solicitud.RowHeadersWidth = 10;
			this.gvDocumentos_solicitud.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gvDocumentos_solicitud.Size = new System.Drawing.Size(550, 352);
			this.gvDocumentos_solicitud.TabIndex = 74;
			this.gvDocumentos_solicitud.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvDocumentos_solicitud_CellContentClick);
			this.gvDocumentos_solicitud.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvDocumentos_solicitud_CellContentDoubleClick);
			this.gvDocumentos_solicitud.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gvDocumentos_solicitud_DataError);
			// 
			// recibido_b
			// 
			this.recibido_b.DataPropertyName = "recibido_b";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle15.NullValue = false;
			this.recibido_b.DefaultCellStyle = dataGridViewCellStyle15;
			this.recibido_b.FalseValue = "0";
			this.recibido_b.Frozen = true;
			this.recibido_b.HeaderText = "";
			this.recibido_b.Name = "recibido_b";
			this.recibido_b.ReadOnly = true;
			this.recibido_b.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.recibido_b.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.recibido_b.TrueValue = "1";
			this.recibido_b.Width = 20;
			// 
			// documento_id
			// 
			this.documento_id.DataPropertyName = "documento_id";
			this.documento_id.HeaderText = "";
			this.documento_id.Name = "documento_id";
			this.documento_id.ReadOnly = true;
			this.documento_id.Visible = false;
			// 
			// nombre_doc
			// 
			this.nombre_doc.DataPropertyName = "nombre_doc";
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.nombre_doc.DefaultCellStyle = dataGridViewCellStyle16;
			this.nombre_doc.HeaderText = "Descripción Documento";
			this.nombre_doc.Name = "nombre_doc";
			this.nombre_doc.ReadOnly = true;
			this.nombre_doc.Width = 250;
			// 
			// tipo_exigencia
			// 
			this.tipo_exigencia.DataPropertyName = "tipo_exigencia";
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.tipo_exigencia.DefaultCellStyle = dataGridViewCellStyle17;
			this.tipo_exigencia.HeaderText = "Tipo";
			this.tipo_exigencia.Name = "tipo_exigencia";
			this.tipo_exigencia.ReadOnly = true;
			this.tipo_exigencia.Width = 200;
			// 
			// no_archivo
			// 
			this.no_archivo.DataPropertyName = "no_archivo";
			this.no_archivo.HeaderText = "";
			this.no_archivo.Name = "no_archivo";
			this.no_archivo.ReadOnly = true;
			this.no_archivo.Visible = false;
			this.no_archivo.Width = 5;
			// 
			// Ver
			// 
			this.Ver.DataPropertyName = "ver";
			this.Ver.HeaderText = "";
			this.Ver.Image = global::Docsis_Application.Properties.Resources.lupa_15x14;
			this.Ver.Name = "Ver";
			this.Ver.ReadOnly = true;
			this.Ver.ToolTipText = "Visualizar documento";
			this.Ver.Width = 20;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Location = new System.Drawing.Point(195, 419);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(150, 30);
			this.button1.TabIndex = 75;
			this.button1.Text = "Seleccionar documento";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// pictureBox4
			// 
			this.pictureBox4.Location = new System.Drawing.Point(561, 12);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(18, 23);
			this.pictureBox4.TabIndex = 76;
			this.pictureBox4.TabStop = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 466);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(569, 22);
			this.statusStrip1.TabIndex = 77;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// button_cerrar
			// 
			this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_cerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.button_cerrar.FlatAppearance.BorderSize = 0;
			this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_cerrar.ForeColor = System.Drawing.Color.White;
			this.button_cerrar.Location = new System.Drawing.Point(351, 419);
			this.button_cerrar.Name = "button_cerrar";
			this.button_cerrar.Size = new System.Drawing.Size(106, 30);
			this.button_cerrar.TabIndex = 78;
			this.button_cerrar.Text = "Cerrar";
			this.button_cerrar.UseVisualStyleBackColor = false;
			this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panel3.Controls.Add(this.btnClose);
			this.panel3.Controls.Add(this.label_header);
			this.panel3.Controls.Add(this.btnSalir);
			this.panel3.Controls.Add(this.pictureBox2);
			this.panel3.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.panel3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel3.Location = new System.Drawing.Point(0, 1);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(569, 35);
			this.panel3.TabIndex = 94;
			this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
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
			this.btnClose.Location = new System.Drawing.Point(544, 10);
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
			this.label_header.Location = new System.Drawing.Point(40, 7);
			this.label_header.Name = "label_header";
			this.label_header.Size = new System.Drawing.Size(164, 21);
			this.label_header.TabIndex = 0;
			this.label_header.Text = "Documentos Adjuntos";
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
			// pictureBox2
			// 
			this.pictureBox2.Image = global::Docsis_Application.Properties.Resources.icon_adjunto;
			this.pictureBox2.Location = new System.Drawing.Point(3, 2);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(40, 32);
			this.pictureBox2.TabIndex = 110;
			this.pictureBox2.TabStop = false;
			// 
			// panel5
			// 
			this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel5.Location = new System.Drawing.Point(568, 2);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(2, 488);
			this.panel5.TabIndex = 291;
			// 
			// panel6
			// 
			this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel6.Location = new System.Drawing.Point(0, -2);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(1, 488);
			this.panel6.TabIndex = 292;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(-24, 487);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(617, 2);
			this.panel1.TabIndex = 293;
			// 
			// s_documentossolic_doc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
			this.ClientSize = new System.Drawing.Size(569, 488);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel6);
			this.Controls.Add(this.panel5);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.button_cerrar);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.pictureBox4);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.gvDocumentos_solicitud);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "s_documentossolic_doc";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Documentos presentados";
			((System.ComponentModel.ISupportInitialize)(this.gvDocumentos_solicitud)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvDocumentos_solicitud;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn recibido_b;
        private System.Windows.Forms.DataGridViewTextBoxColumn documento_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo_exigencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_archivo;
        private System.Windows.Forms.DataGridViewImageColumn Ver;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label label_header;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel1;
    }
}