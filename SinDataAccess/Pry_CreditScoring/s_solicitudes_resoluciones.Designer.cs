namespace Docsis_Application
{
    partial class s_solicitudes_resoluciones
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.labelFilial = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lvfotos = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.picAlto = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.picAncho = new System.Windows.Forms.NumericUpDown();
            this.panelCargando = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.lnkTablaresultado = new System.Windows.Forms.LinkLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAncho)).BeginInit();
            this.panelCargando.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlHeader.Controls.Add(this.labelFilial);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.btnSalir);
            this.pnlHeader.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pnlHeader.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlHeader.Location = new System.Drawing.Point(-1, -1);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(783, 29);
            this.pnlHeader.TabIndex = 93;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            // 
            // labelFilial
            // 
            this.labelFilial.AutoSize = true;
            this.labelFilial.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilial.Location = new System.Drawing.Point(9, 4);
            this.labelFilial.Name = "labelFilial";
            this.labelFilial.Size = new System.Drawing.Size(172, 21);
            this.labelFilial.TabIndex = 109;
            this.labelFilial.Text = "Resoluciones del Comite";
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
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClose.Location = new System.Drawing.Point(764, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 91;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            // lvfotos
            // 
            this.lvfotos.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvfotos.AllowColumnReorder = true;
            this.lvfotos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvfotos.BackColor = System.Drawing.Color.White;
            this.lvfotos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvfotos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvfotos.HoverSelection = true;
            this.lvfotos.Location = new System.Drawing.Point(7, 55);
            this.lvfotos.Name = "lvfotos";
            this.lvfotos.ShowItemToolTips = true;
            this.lvfotos.Size = new System.Drawing.Size(765, 222);
            this.lvfotos.TabIndex = 94;
            this.lvfotos.UseCompatibleStateImageBehavior = false;
            this.lvfotos.View = System.Windows.Forms.View.Details;
            this.lvfotos.SelectedIndexChanged += new System.EventHandler(this.lvfotos_SelectedIndexChanged);
            this.lvfotos.MouseLeave += new System.EventHandler(this.lvfotos_MouseLeave);
            this.lvfotos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvfotos_MouseMove);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.label5.Location = new System.Drawing.Point(665, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 98;
            this.label5.Text = "Alto :";
            this.label5.Visible = false;
            // 
            // picAlto
            // 
            this.picAlto.Location = new System.Drawing.Point(699, 30);
            this.picAlto.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.picAlto.Name = "picAlto";
            this.picAlto.Size = new System.Drawing.Size(44, 20);
            this.picAlto.TabIndex = 97;
            this.picAlto.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.label4.Location = new System.Drawing.Point(565, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 96;
            this.label4.Text = "Ancho :";
            this.label4.Visible = false;
            // 
            // picAncho
            // 
            this.picAncho.Location = new System.Drawing.Point(613, 30);
            this.picAncho.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.picAncho.Name = "picAncho";
            this.picAncho.Size = new System.Drawing.Size(44, 20);
            this.picAncho.TabIndex = 95;
            this.picAncho.Visible = false;
            // 
            // panelCargando
            // 
            this.panelCargando.BackColor = System.Drawing.Color.White;
            this.panelCargando.Controls.Add(this.pictureBox1);
            this.panelCargando.Controls.Add(this.label1);
            this.panelCargando.Location = new System.Drawing.Point(258, 141);
            this.panelCargando.Name = "panelCargando";
            this.panelCargando.Size = new System.Drawing.Size(213, 40);
            this.panelCargando.TabIndex = 99;
            this.panelCargando.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources._3011;
            this.pictureBox1.Location = new System.Drawing.Point(6, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 29);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(54, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cargando fotos.....";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(97, 33);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(78, 17);
            this.radioButton3.TabIndex = 102;
            this.radioButton3.Text = "Vista Icono";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(7, 33);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(84, 17);
            this.radioButton4.TabIndex = 103;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Vista Detalle";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // lnkTablaresultado
            // 
            this.lnkTablaresultado.AutoSize = true;
            this.lnkTablaresultado.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTablaresultado.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.lnkTablaresultado.Location = new System.Drawing.Point(192, 35);
            this.lnkTablaresultado.Name = "lnkTablaresultado";
            this.lnkTablaresultado.Size = new System.Drawing.Size(149, 13);
            this.lnkTablaresultado.TabIndex = 107;
            this.lnkTablaresultado.TabStop = true;
            this.lnkTablaresultado.Text = "Ver Certificación  de Comite";
            this.lnkTablaresultado.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTablaresultado_LinkClicked);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 397);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(781, 22);
            this.statusStrip1.TabIndex = 108;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.StatusLabel1.Text = "::.";
            // 
            // txtObs
            // 
            this.txtObs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtObs.Location = new System.Drawing.Point(7, 279);
            this.txtObs.Multiline = true;
            this.txtObs.Name = "txtObs";
            this.txtObs.Size = new System.Drawing.Size(765, 115);
            this.txtObs.TabIndex = 109;
            // 
            // s_solicitudes_resoluciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 419);
            this.Controls.Add(this.txtObs);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lnkTablaresultado);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.panelCargando);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picAlto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picAncho);
            this.Controls.Add(this.lvfotos);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_solicitudes_resoluciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_solicitudes_resoluciones";
            this.Load += new System.EventHandler(this.s_solicitudes_resoluciones_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAncho)).EndInit();
            this.panelCargando.ResumeLayout(false);
            this.panelCargando.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label labelFilial;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSalir;
        internal System.Windows.Forms.ListView lvfotos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown picAlto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown picAncho;
        private System.Windows.Forms.Panel panelCargando;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.LinkLabel lnkTablaresultado;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.TextBox txtObs;
    }
}