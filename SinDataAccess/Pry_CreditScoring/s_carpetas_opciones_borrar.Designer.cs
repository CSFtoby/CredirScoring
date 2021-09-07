namespace Docsis_Application
{
    partial class s_carpetas_opciones_borrar
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.labelFilial = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.listBox_carpetas = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_indicativo_superior = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panel_indicativo_superior.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.labelFilial);
            this.panelTop.Controls.Add(this.btnSalir);
            this.panelTop.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panelTop.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelTop.Location = new System.Drawing.Point(0, 1);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(274, 29);
            this.panelTop.TabIndex = 97;
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
            this.btnClose.Location = new System.Drawing.Point(254, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 5;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelFilial
            // 
            this.labelFilial.AutoSize = true;
            this.labelFilial.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelFilial.Location = new System.Drawing.Point(6, 8);
            this.labelFilial.Name = "labelFilial";
            this.labelFilial.Size = new System.Drawing.Size(83, 13);
            this.labelFilial.TabIndex = 0;
            this.labelFilial.Text = "Eliminar Carpeta";
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
            // listBox_carpetas
            // 
            this.listBox_carpetas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_carpetas.FormattingEnabled = true;
            this.listBox_carpetas.Location = new System.Drawing.Point(13, 37);
            this.listBox_carpetas.Name = "listBox_carpetas";
            this.listBox_carpetas.Size = new System.Drawing.Size(238, 199);
            this.listBox_carpetas.TabIndex = 95;
            this.listBox_carpetas.SelectedIndexChanged += new System.EventHandler(this.listBox_carpetas_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(142, 339);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 32);
            this.button1.TabIndex = 96;
            this.button1.Text = "Eliminar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel_indicativo_superior
            // 
            this.panel_indicativo_superior.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_indicativo_superior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.panel_indicativo_superior.Controls.Add(this.label1);
            this.panel_indicativo_superior.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel_indicativo_superior.Location = new System.Drawing.Point(10, 239);
            this.panel_indicativo_superior.Name = "panel_indicativo_superior";
            this.panel_indicativo_superior.Size = new System.Drawing.Size(246, 74);
            this.panel_indicativo_superior.TabIndex = 98;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 57);
            this.label1.TabIndex = 23;
            this.label1.Text = "Nota: las solicitudes NO son eliminadas al borrar la carpeta, siempre permanecen " +
    "en la base de datos. Las carpetas solo son una organizacion del usuario";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // s_carpetas_opciones_borrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(275, 389);
            this.Controls.Add(this.panel_indicativo_superior);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.listBox_carpetas);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_carpetas_opciones_borrar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_carpetas_opciones_borrar";
            this.Load += new System.EventHandler(this.s_carpetas_opciones_borrar_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panel_indicativo_superior.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labelFilial;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ListBox listBox_carpetas;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel_indicativo_superior;
        private System.Windows.Forms.Label label1;

    }
}