namespace Docsis_Application
{
    partial class s_cefinfodocentes_doc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button2 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.labelComite = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gvInfoSueldos = new System.Windows.Forms.DataGridView();
            this.cidpariente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deduccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sueldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtIDSolicitante = new System.Windows.Forms.TextBox();
            this.label175 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInfoSueldos)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(123, 482);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 34);
            this.button2.TabIndex = 9;
            this.button2.Text = "Cerrar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.pictureBox6);
            this.panel3.Controls.Add(this.labelComite);
            this.panel3.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panel3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel3.Location = new System.Drawing.Point(-1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(371, 35);
            this.panel3.TabIndex = 28;
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
            this.btnClose.Location = new System.Drawing.Point(354, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 5;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = global::Docsis_Application.Properties.Resources.usuario;
            this.pictureBox6.Location = new System.Drawing.Point(7, 7);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(16, 21);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 26;
            this.pictureBox6.TabStop = false;
            // 
            // labelComite
            // 
            this.labelComite.AutoSize = true;
            this.labelComite.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComite.ForeColor = System.Drawing.Color.Silver;
            this.labelComite.Location = new System.Drawing.Point(38, 7);
            this.labelComite.Name = "labelComite";
            this.labelComite.Size = new System.Drawing.Size(305, 21);
            this.labelComite.TabIndex = 0;
            this.labelComite.Text = "Información de Docentes de CEF - Planillas";
            this.labelComite.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelComite_MouseDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Location = new System.Drawing.Point(0, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(370, 22);
            this.statusStrip1.TabIndex = 98;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gvInfoSueldos
            // 
            this.gvInfoSueldos.AllowUserToAddRows = false;
            this.gvInfoSueldos.AllowUserToDeleteRows = false;
            this.gvInfoSueldos.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.gvInfoSueldos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.gvInfoSueldos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvInfoSueldos.BackgroundColor = System.Drawing.Color.White;
            this.gvInfoSueldos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvInfoSueldos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvInfoSueldos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvInfoSueldos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.gvInfoSueldos.ColumnHeadersHeight = 20;
            this.gvInfoSueldos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cidpariente,
            this.deduccion,
            this.sueldo});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvInfoSueldos.DefaultCellStyle = dataGridViewCellStyle15;
            this.gvInfoSueldos.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvInfoSueldos.Location = new System.Drawing.Point(9, 62);
            this.gvInfoSueldos.Name = "gvInfoSueldos";
            this.gvInfoSueldos.ReadOnly = true;
            this.gvInfoSueldos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvInfoSueldos.RowHeadersVisible = false;
            this.gvInfoSueldos.RowHeadersWidth = 10;
            this.gvInfoSueldos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvInfoSueldos.RowTemplate.Height = 26;
            this.gvInfoSueldos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvInfoSueldos.Size = new System.Drawing.Size(350, 414);
            this.gvInfoSueldos.TabIndex = 99;
            // 
            // cidpariente
            // 
            this.cidpariente.DataPropertyName = "periodo";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cidpariente.DefaultCellStyle = dataGridViewCellStyle13;
            this.cidpariente.HeaderText = "Periodo";
            this.cidpariente.Name = "cidpariente";
            this.cidpariente.ReadOnly = true;
            // 
            // deduccion
            // 
            this.deduccion.DataPropertyName = "deduccion";
            this.deduccion.HeaderText = "Deduccion";
            this.deduccion.Name = "deduccion";
            this.deduccion.ReadOnly = true;
            this.deduccion.Width = 120;
            // 
            // sueldo
            // 
            this.sueldo.DataPropertyName = "sueldo";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.sueldo.DefaultCellStyle = dataGridViewCellStyle14;
            this.sueldo.HeaderText = "Sueldo";
            this.sueldo.Name = "sueldo";
            this.sueldo.ReadOnly = true;
            this.sueldo.Width = 120;
            // 
            // txtIDSolicitante
            // 
            this.txtIDSolicitante.BackColor = System.Drawing.Color.White;
            this.txtIDSolicitante.Location = new System.Drawing.Point(120, 39);
            this.txtIDSolicitante.MaxLength = 20;
            this.txtIDSolicitante.Name = "txtIDSolicitante";
            this.txtIDSolicitante.ReadOnly = true;
            this.txtIDSolicitante.Size = new System.Drawing.Size(187, 20);
            this.txtIDSolicitante.TabIndex = 101;
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label175.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label175.Location = new System.Drawing.Point(11, 43);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(96, 13);
            this.label175.TabIndex = 100;
            this.label175.Text = "No. Identificación :";
            // 
            // s_cefinfodocentes_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(370, 543);
            this.Controls.Add(this.txtIDSolicitante);
            this.Controls.Add(this.label175);
            this.Controls.Add(this.gvInfoSueldos);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_cefinfodocentes_doc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_cefinfodocentes_doc";
            this.Load += new System.EventHandler(this.s_cefinfodocentes_doc_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInfoSueldos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox6;
        public System.Windows.Forms.Label labelComite;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView gvInfoSueldos;
        private System.Windows.Forms.DataGridViewTextBoxColumn cidpariente;
        private System.Windows.Forms.DataGridViewTextBoxColumn deduccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn sueldo;
        public System.Windows.Forms.TextBox txtIDSolicitante;
        private System.Windows.Forms.Label label175;
    }
}