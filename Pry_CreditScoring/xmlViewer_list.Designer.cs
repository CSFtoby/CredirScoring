namespace Docsis_Application
{
    partial class xmlViewer_list
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xmlViewer_list));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.labelComite = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.gvListaXMLs = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelNo_solicitud = new System.Windows.Forms.ToolStripStatusLabel();
            this.llCerrar = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.no_entrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario_comite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_ing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xml_enviado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xml_respuesta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaXMLs)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.labelComite);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panel3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel3.Location = new System.Drawing.Point(-1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(646, 35);
            this.panel3.TabIndex = 96;
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
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(620, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 5;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelComite
            // 
            this.labelComite.AutoSize = true;
            this.labelComite.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComite.ForeColor = System.Drawing.Color.Silver;
            this.labelComite.Location = new System.Drawing.Point(36, 8);
            this.labelComite.Name = "labelComite";
            this.labelComite.Size = new System.Drawing.Size(117, 21);
            this.labelComite.TabIndex = 0;
            this.labelComite.Text = "XMLViewer List";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(5, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 110;
            this.pictureBox2.TabStop = false;
            // 
            // gvListaXMLs
            // 
            this.gvListaXMLs.AllowUserToAddRows = false;
            this.gvListaXMLs.AllowUserToDeleteRows = false;
            this.gvListaXMLs.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gvListaXMLs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvListaXMLs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvListaXMLs.BackgroundColor = System.Drawing.Color.White;
            this.gvListaXMLs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvListaXMLs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvListaXMLs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvListaXMLs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvListaXMLs.ColumnHeadersHeight = 20;
            this.gvListaXMLs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no_entrada,
            this.usuario_comite,
            this.fecha_ing,
            this.xml_enviado,
            this.xml_respuesta});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvListaXMLs.DefaultCellStyle = dataGridViewCellStyle6;
            this.gvListaXMLs.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvListaXMLs.Location = new System.Drawing.Point(3, 36);
            this.gvListaXMLs.Name = "gvListaXMLs";
            this.gvListaXMLs.ReadOnly = true;
            this.gvListaXMLs.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvListaXMLs.RowHeadersVisible = false;
            this.gvListaXMLs.RowHeadersWidth = 10;
            this.gvListaXMLs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvListaXMLs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvListaXMLs.Size = new System.Drawing.Size(638, 194);
            this.gvListaXMLs.TabIndex = 344;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelNo_solicitud});
            this.statusStrip1.Location = new System.Drawing.Point(0, 272);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(643, 22);
            this.statusStrip1.TabIndex = 351;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.SteelBlue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(83, 17);
            this.toolStripStatusLabel1.Text = "No. solicitud   ";
            // 
            // labelNo_solicitud
            // 
            this.labelNo_solicitud.Name = "labelNo_solicitud";
            this.labelNo_solicitud.Size = new System.Drawing.Size(16, 17);
            this.labelNo_solicitud.Text = ":::";
            // 
            // llCerrar
            // 
            this.llCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llCerrar.AutoSize = true;
            this.llCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llCerrar.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.llCerrar.Location = new System.Drawing.Point(591, 246);
            this.llCerrar.Name = "llCerrar";
            this.llCerrar.Size = new System.Drawing.Size(41, 15);
            this.llCerrar.TabIndex = 352;
            this.llCerrar.TabStop = true;
            this.llCerrar.Text = "Cerrar";
            this.llCerrar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCerrar_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.linkLabel1.Location = new System.Drawing.Point(374, 246);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(95, 15);
            this.linkLabel1.TabIndex = 353;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Ver xml Enviado";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.linkLabel2.Location = new System.Drawing.Point(480, 246);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(100, 15);
            this.linkLabel2.TabIndex = 354;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Ver xml Recibido";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // no_entrada
            // 
            this.no_entrada.DataPropertyName = "no_entrada";
            this.no_entrada.HeaderText = "No. ";
            this.no_entrada.Name = "no_entrada";
            this.no_entrada.ReadOnly = true;
            this.no_entrada.Width = 50;
            // 
            // usuario_comite
            // 
            this.usuario_comite.DataPropertyName = "usuario_comite";
            this.usuario_comite.HeaderText = "Usuario";
            this.usuario_comite.Name = "usuario_comite";
            this.usuario_comite.ReadOnly = true;
            // 
            // fecha_ing
            // 
            this.fecha_ing.DataPropertyName = "fecha_ing";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fecha_ing.DefaultCellStyle = dataGridViewCellStyle3;
            this.fecha_ing.HeaderText = "Fecha ";
            this.fecha_ing.Name = "fecha_ing";
            this.fecha_ing.ReadOnly = true;
            this.fecha_ing.Width = 130;
            // 
            // xml_enviado
            // 
            this.xml_enviado.DataPropertyName = "xml_enviado";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.xml_enviado.DefaultCellStyle = dataGridViewCellStyle4;
            this.xml_enviado.HeaderText = "XML Enviado";
            this.xml_enviado.Name = "xml_enviado";
            this.xml_enviado.ReadOnly = true;
            this.xml_enviado.Width = 180;
            // 
            // xml_respuesta
            // 
            this.xml_respuesta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.xml_respuesta.DataPropertyName = "xml_respuesta";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.xml_respuesta.DefaultCellStyle = dataGridViewCellStyle5;
            this.xml_respuesta.HeaderText = "XML Recibido";
            this.xml_respuesta.Name = "xml_respuesta";
            this.xml_respuesta.ReadOnly = true;
            // 
            // xmlViewer_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 294);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.llCerrar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gvListaXMLs);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "xmlViewer_list";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "xmlViewer_list";
            this.Load += new System.EventHandler(this.xmlViewer_list_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaXMLs)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label labelComite;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView gvListaXMLs;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.LinkLabel llCerrar;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        public System.Windows.Forms.ToolStripStatusLabel labelNo_solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_entrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario_comite;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_ing;
        private System.Windows.Forms.DataGridViewTextBoxColumn xml_enviado;
        private System.Windows.Forms.DataGridViewTextBoxColumn xml_respuesta;
    }
}