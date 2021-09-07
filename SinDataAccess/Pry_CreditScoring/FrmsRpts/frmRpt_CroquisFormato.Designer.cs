namespace Docsis_Application.FrmsRpts
{
    partial class frmRpt_CroquisFormato
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
            this.rbAval2 = new System.Windows.Forms.RadioButton();
            this.rbAval1 = new System.Windows.Forms.RadioButton();
            this.rbCodeudor = new System.Windows.Forms.RadioButton();
            this.rbSolicitante = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMaximizar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.rbAval2);
            this.panelTop.Controls.Add(this.rbAval1);
            this.panelTop.Controls.Add(this.rbCodeudor);
            this.panelTop.Controls.Add(this.rbSolicitante);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.btnMaximizar);
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Location = new System.Drawing.Point(2, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1114, 34);
            this.panelTop.TabIndex = 108;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // rbAval2
            // 
            this.rbAval2.AutoSize = true;
            this.rbAval2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rbAval2.Location = new System.Drawing.Point(593, 8);
            this.rbAval2.Name = "rbAval2";
            this.rbAval2.Size = new System.Drawing.Size(55, 17);
            this.rbAval2.TabIndex = 120;
            this.rbAval2.TabStop = true;
            this.rbAval2.Text = "Aval 2";
            this.rbAval2.UseVisualStyleBackColor = true;
            this.rbAval2.CheckedChanged += new System.EventHandler(this.rbs_CheckedChanged);
            // 
            // rbAval1
            // 
            this.rbAval1.AutoSize = true;
            this.rbAval1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rbAval1.Location = new System.Drawing.Point(523, 8);
            this.rbAval1.Name = "rbAval1";
            this.rbAval1.Size = new System.Drawing.Size(55, 17);
            this.rbAval1.TabIndex = 119;
            this.rbAval1.TabStop = true;
            this.rbAval1.Text = "Aval 1";
            this.rbAval1.UseVisualStyleBackColor = true;
            this.rbAval1.CheckedChanged += new System.EventHandler(this.rbs_CheckedChanged);
            // 
            // rbCodeudor
            // 
            this.rbCodeudor.AutoSize = true;
            this.rbCodeudor.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rbCodeudor.Location = new System.Drawing.Point(439, 8);
            this.rbCodeudor.Name = "rbCodeudor";
            this.rbCodeudor.Size = new System.Drawing.Size(71, 17);
            this.rbCodeudor.TabIndex = 118;
            this.rbCodeudor.TabStop = true;
            this.rbCodeudor.Text = "Codeudor";
            this.rbCodeudor.UseVisualStyleBackColor = true;
            this.rbCodeudor.CheckedChanged += new System.EventHandler(this.rbs_CheckedChanged);
            // 
            // rbSolicitante
            // 
            this.rbSolicitante.AutoSize = true;
            this.rbSolicitante.Checked = true;
            this.rbSolicitante.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rbSolicitante.Location = new System.Drawing.Point(354, 8);
            this.rbSolicitante.Name = "rbSolicitante";
            this.rbSolicitante.Size = new System.Drawing.Size(74, 17);
            this.rbSolicitante.TabIndex = 117;
            this.rbSolicitante.TabStop = true;
            this.rbSolicitante.Text = "Solicitante";
            this.rbSolicitante.UseVisualStyleBackColor = true;
            this.rbSolicitante.CheckedChanged += new System.EventHandler(this.rbs_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(100)))), ((int)(((byte)(74)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::Docsis_Application.Properties.Resources.icon_close01;
            this.btnClose.Location = new System.Drawing.Point(1088, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 116;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximizar.FlatAppearance.BorderSize = 0;
            this.btnMaximizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(100)))), ((int)(((byte)(74)))));
            this.btnMaximizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximizar.ForeColor = System.Drawing.Color.White;
            this.btnMaximizar.Image = global::Docsis_Application.Properties.Resources.maximizar_icon;
            this.btnMaximizar.Location = new System.Drawing.Point(1071, 4);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(15, 16);
            this.btnMaximizar.TabIndex = 115;
            this.btnMaximizar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMaximizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnMaximizar.UseVisualStyleBackColor = false;
            this.btnMaximizar.Click += new System.EventHandler(this.btnMaximizar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 15);
            this.label4.TabIndex = 108;
            this.label4.Text = "Visualización Formato de Croquis";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.Location = new System.Drawing.Point(2, 35);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1114, 595);
            this.reportViewer1.TabIndex = 109;
            // 
            // frmRpt_CroquisFormato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 625);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRpt_CroquisFormato";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRpt_CroquisFormato";
            this.Load += new System.EventHandler(this.frmRpt_CroquisFormato_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMaximizar;
        private System.Windows.Forms.Label label4;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.RadioButton rbAval2;
        private System.Windows.Forms.RadioButton rbAval1;
        private System.Windows.Forms.RadioButton rbCodeudor;
        private System.Windows.Forms.RadioButton rbSolicitante;
    }
}