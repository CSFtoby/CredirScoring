namespace Docsis_Application
{
    partial class s_login
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(s_login));
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox_botones = new System.Windows.Forms.GroupBox();
			this.txtBase = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button_cancelar = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.txtPass = new System.Windows.Forms.TextBox();
			this.txtUsuario = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panelTop = new System.Windows.Forms.Panel();
			this.button9 = new System.Windows.Forms.Button();
			this.labelFechaPanel = new System.Windows.Forms.Label();
			this.labelDiaPanel = new System.Windows.Forms.Label();
			this.labelRelojPanel = new System.Windows.Forms.Label();
			this.btnSalir = new System.Windows.Forms.Button();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.TimeBar = new System.Windows.Forms.Timer(this.components);
			this.labelAviso = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.labelCultureInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.labelUbicacion = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.groupBox_botones.SuspendLayout();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panel1.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota1;
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.pictureBox2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Location = new System.Drawing.Point(0, 266);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(560, 56);
			this.panel1.TabIndex = 17;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.logo_crediscoring;
			this.pictureBox1.Location = new System.Drawing.Point(448, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(102, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 26;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::Docsis_Application.Properties.Resources.Logo_Blanco_01;
			this.pictureBox2.Location = new System.Drawing.Point(5, -1);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(155, 56);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 8;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(342, 35);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(208, 17);
			this.label3.TabIndex = 6;
			this.label3.Text = "Cooperativa Sagrada Familia, Ltda";
			// 
			// groupBox_botones
			// 
			this.groupBox_botones.BackColor = System.Drawing.Color.Transparent;
			this.groupBox_botones.Controls.Add(this.txtBase);
			this.groupBox_botones.Controls.Add(this.label6);
			this.groupBox_botones.Controls.Add(this.button_cancelar);
			this.groupBox_botones.Controls.Add(this.button1);
			this.groupBox_botones.Controls.Add(this.txtPass);
			this.groupBox_botones.Controls.Add(this.txtUsuario);
			this.groupBox_botones.Controls.Add(this.label2);
			this.groupBox_botones.Controls.Add(this.label1);
			this.groupBox_botones.Location = new System.Drawing.Point(302, 49);
			this.groupBox_botones.Name = "groupBox_botones";
			this.groupBox_botones.Size = new System.Drawing.Size(244, 144);
			this.groupBox_botones.TabIndex = 14;
			this.groupBox_botones.TabStop = false;
			this.groupBox_botones.Text = "Acceso al sistema";
			// 
			// txtBase
			// 
			this.txtBase.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtBase.ForeColor = System.Drawing.Color.DarkGray;
			this.txtBase.Location = new System.Drawing.Point(90, 77);
			this.txtBase.Name = "txtBase";
			this.txtBase.ReadOnly = true;
			this.txtBase.Size = new System.Drawing.Size(121, 13);
			this.txtBase.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.DarkGray;
			this.label6.Location = new System.Drawing.Point(20, 77);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Base :";
			// 
			// button_cancelar
			// 
			this.button_cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.button_cancelar.FlatAppearance.BorderSize = 0;
			this.button_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_cancelar.ForeColor = System.Drawing.Color.White;
			this.button_cancelar.Location = new System.Drawing.Point(131, 106);
			this.button_cancelar.Name = "button_cancelar";
			this.button_cancelar.Size = new System.Drawing.Size(104, 27);
			this.button_cancelar.TabIndex = 5;
			this.button_cancelar.Text = "Cancelar";
			this.button_cancelar.UseVisualStyleBackColor = false;
			this.button_cancelar.Click += new System.EventHandler(this.button_cancelar_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Location = new System.Drawing.Point(23, 106);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(90, 27);
			this.button1.TabIndex = 4;
			this.button1.Text = "Ingresar";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			this.button1.MouseHover += new System.EventHandler(this.button1_MouseHover);
			// 
			// txtPass
			// 
			this.txtPass.Location = new System.Drawing.Point(90, 47);
			this.txtPass.Name = "txtPass";
			this.txtPass.Size = new System.Drawing.Size(121, 20);
			this.txtPass.TabIndex = 3;
			this.txtPass.UseSystemPasswordChar = true;
			this.txtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPass_KeyDown);
			// 
			// txtUsuario
			// 
			this.txtUsuario.Location = new System.Drawing.Point(90, 19);
			this.txtUsuario.Name = "txtUsuario";
			this.txtUsuario.Size = new System.Drawing.Size(121, 20);
			this.txtUsuario.TabIndex = 2;
			this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsuario_KeyDown);
			this.txtUsuario.Leave += new System.EventHandler(this.txtUsuario_Leave);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Contraseña :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(20, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Usuario :.";
			// 
			// panelTop
			// 
			this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.panelTop.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota;
			this.panelTop.Controls.Add(this.button9);
			this.panelTop.Controls.Add(this.labelFechaPanel);
			this.panelTop.Controls.Add(this.labelDiaPanel);
			this.panelTop.Controls.Add(this.labelRelojPanel);
			this.panelTop.Controls.Add(this.btnSalir);
			this.panelTop.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.panelTop.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.panelTop.Location = new System.Drawing.Point(-1, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(561, 35);
			this.panelTop.TabIndex = 21;
			this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
			// 
			// button9
			// 
			this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button9.BackColor = System.Drawing.Color.Transparent;
			this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button9.FlatAppearance.BorderSize = 0;
			this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(96)))), ((int)(((byte)(66)))));
			this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button9.ForeColor = System.Drawing.Color.White;
			this.button9.Image = global::Docsis_Application.Properties.Resources.icon_salir_w;
			this.button9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button9.Location = new System.Drawing.Point(529, 4);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(29, 29);
			this.button9.TabIndex = 5;
			this.button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.button9.UseVisualStyleBackColor = false;
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// labelFechaPanel
			// 
			this.labelFechaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelFechaPanel.AutoSize = true;
			this.labelFechaPanel.BackColor = System.Drawing.Color.Transparent;
			this.labelFechaPanel.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelFechaPanel.ForeColor = System.Drawing.Color.White;
			this.labelFechaPanel.Location = new System.Drawing.Point(470, 19);
			this.labelFechaPanel.Name = "labelFechaPanel";
			this.labelFechaPanel.Size = new System.Drawing.Size(58, 15);
			this.labelFechaPanel.TabIndex = 4;
			this.labelFechaPanel.Text = "Febrero 11";
			// 
			// labelDiaPanel
			// 
			this.labelDiaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDiaPanel.AutoSize = true;
			this.labelDiaPanel.BackColor = System.Drawing.Color.Transparent;
			this.labelDiaPanel.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDiaPanel.ForeColor = System.Drawing.Color.White;
			this.labelDiaPanel.Location = new System.Drawing.Point(470, 3);
			this.labelDiaPanel.Name = "labelDiaPanel";
			this.labelDiaPanel.Size = new System.Drawing.Size(54, 15);
			this.labelDiaPanel.TabIndex = 3;
			this.labelDiaPanel.Text = "Miercoles";
			// 
			// labelRelojPanel
			// 
			this.labelRelojPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRelojPanel.AutoSize = true;
			this.labelRelojPanel.BackColor = System.Drawing.Color.Transparent;
			this.labelRelojPanel.Font = new System.Drawing.Font("Segoe UI Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelRelojPanel.ForeColor = System.Drawing.Color.White;
			this.labelRelojPanel.Location = new System.Drawing.Point(412, 4);
			this.labelRelojPanel.Name = "labelRelojPanel";
			this.labelRelojPanel.Size = new System.Drawing.Size(50, 28);
			this.labelRelojPanel.TabIndex = 2;
			this.labelRelojPanel.Text = "11:39";
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
			// pictureBox3
			// 
			this.pictureBox3.Image = global::Docsis_Application.Properties.Resources.logo_crediscoring1;
			this.pictureBox3.Location = new System.Drawing.Point(2, 32);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(294, 187);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox3.TabIndex = 21;
			this.pictureBox3.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Location = new System.Drawing.Point(560, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(5, 344);
			this.panel2.TabIndex = 283;
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Location = new System.Drawing.Point(-4, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(5, 344);
			this.panel4.TabIndex = 284;
			// 
			// TimeBar
			// 
			this.TimeBar.Enabled = true;
			this.TimeBar.Interval = 1000;
			this.TimeBar.Tick += new System.EventHandler(this.TimeBar_Tick);
			// 
			// labelAviso
			// 
			this.labelAviso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelAviso.BackColor = System.Drawing.Color.Transparent;
			this.labelAviso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelAviso.ForeColor = System.Drawing.Color.Red;
			this.labelAviso.Location = new System.Drawing.Point(2, 235);
			this.labelAviso.Name = "labelAviso";
			this.labelAviso.Size = new System.Drawing.Size(544, 29);
			this.labelAviso.TabIndex = 287;
			this.labelAviso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelCultureInfo,
            this.toolStripStatusLabel2,
            this.labelUbicacion});
			this.statusStrip1.Location = new System.Drawing.Point(0, 322);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(561, 22);
			this.statusStrip1.TabIndex = 289;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(141, 17);
			this.toolStripStatusLabel1.Text = "Configuración Regional   ";
			// 
			// labelCultureInfo
			// 
			this.labelCultureInfo.ForeColor = System.Drawing.Color.White;
			this.labelCultureInfo.Name = "labelCultureInfo";
			this.labelCultureInfo.Size = new System.Drawing.Size(13, 17);
			this.labelCultureInfo.Text = "::";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.White;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
			this.toolStripStatusLabel2.Text = "|";
			// 
			// labelUbicacion
			// 
			this.labelUbicacion.ForeColor = System.Drawing.Color.White;
			this.labelUbicacion.Name = "labelUbicacion";
			this.labelUbicacion.Size = new System.Drawing.Size(13, 17);
			this.labelUbicacion.Text = "::";
			// 
			// s_login
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota;
			this.ClientSize = new System.Drawing.Size(561, 344);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.labelAviso);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox_botones);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "s_login";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ingreso al Sistema";
			this.Load += new System.EventHandler(this.s_login_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.groupBox_botones.ResumeLayout(false);
			this.groupBox_botones.PerformLayout();
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox_botones;
        public System.Windows.Forms.TextBox txtBase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_cancelar;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txtPass;
        public System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label labelFechaPanel;
        private System.Windows.Forms.Label labelDiaPanel;
        private System.Windows.Forms.Label labelRelojPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Timer TimeBar;
        private System.Windows.Forms.Label labelAviso;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel labelCultureInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel labelUbicacion;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

