namespace Docsis_Application
{
    partial class s_cnf_decisiones_doc
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
			this.button_cerrar = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label_Titulo = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.radioButton_activo_no = new System.Windows.Forms.RadioButton();
			this.radioButton_activo_si = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtDecision_id = new System.Windows.Forms.TextBox();
			this.txtNombre_decision = new System.Windows.Forms.TextBox();
			this.txtEstado_id = new System.Windows.Forms.TextBox();
			this.comboBox_estados_solic = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.rbtSolicitud = new System.Windows.Forms.RadioButton();
			this.rbtExcepcion = new System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_cerrar
			// 
			this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_cerrar.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button_cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_cerrar.Location = new System.Drawing.Point(232, 274);
			this.button_cerrar.Name = "button_cerrar";
			this.button_cerrar.Size = new System.Drawing.Size(75, 23);
			this.button_cerrar.TabIndex = 16;
			this.button_cerrar.Text = "Cerrar";
			this.button_cerrar.UseVisualStyleBackColor = false;
			this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(103, 274);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 23);
			this.button1.TabIndex = 15;
			this.button1.Text = "Adicionar";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.label5.Location = new System.Drawing.Point(29, 254);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(68, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Opciones :";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.BackColor = System.Drawing.Color.Transparent;
			this.groupBox1.Location = new System.Drawing.Point(81, 261);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(250, 2);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label_Titulo);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(-1, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(393, 46);
			this.panel1.TabIndex = 19;
			// 
			// label_Titulo
			// 
			this.label_Titulo.AutoSize = true;
			this.label_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_Titulo.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label_Titulo.Location = new System.Drawing.Point(66, 15);
			this.label_Titulo.Name = "label_Titulo";
			this.label_Titulo.Size = new System.Drawing.Size(147, 20);
			this.label_Titulo.TabIndex = 0;
			this.label_Titulo.Text = "Agregar Decisión";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.icon_decision02;
			this.pictureBox1.Location = new System.Drawing.Point(6, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(57, 41);
			this.pictureBox1.TabIndex = 87;
			this.pictureBox1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Transparent;
			this.panel2.Controls.Add(this.radioButton_activo_no);
			this.panel2.Controls.Add(this.radioButton_activo_si);
			this.panel2.Location = new System.Drawing.Point(118, 175);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(99, 26);
			this.panel2.TabIndex = 25;
			// 
			// radioButton_activo_no
			// 
			this.radioButton_activo_no.AutoSize = true;
			this.radioButton_activo_no.BackColor = System.Drawing.Color.Transparent;
			this.radioButton_activo_no.Location = new System.Drawing.Point(51, 5);
			this.radioButton_activo_no.Name = "radioButton_activo_no";
			this.radioButton_activo_no.Size = new System.Drawing.Size(39, 17);
			this.radioButton_activo_no.TabIndex = 1;
			this.radioButton_activo_no.Text = "No";
			this.radioButton_activo_no.UseVisualStyleBackColor = false;
			// 
			// radioButton_activo_si
			// 
			this.radioButton_activo_si.AutoSize = true;
			this.radioButton_activo_si.BackColor = System.Drawing.Color.Transparent;
			this.radioButton_activo_si.Checked = true;
			this.radioButton_activo_si.Location = new System.Drawing.Point(11, 5);
			this.radioButton_activo_si.Name = "radioButton_activo_si";
			this.radioButton_activo_si.Size = new System.Drawing.Size(34, 17);
			this.radioButton_activo_si.TabIndex = 0;
			this.radioButton_activo_si.TabStop = true;
			this.radioButton_activo_si.Text = "Si";
			this.radioButton_activo_si.UseVisualStyleBackColor = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.ForeColor = System.Drawing.Color.DarkBlue;
			this.label3.Location = new System.Drawing.Point(24, 182);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 13);
			this.label3.TabIndex = 24;
			this.label3.Text = "Activo :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.ForeColor = System.Drawing.Color.DarkBlue;
			this.label2.Location = new System.Drawing.Point(24, 127);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "Nombre";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.ForeColor = System.Drawing.Color.DarkBlue;
			this.label1.Location = new System.Drawing.Point(24, 103);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 13);
			this.label1.TabIndex = 20;
			this.label1.Text = "Decisión ID:";
			// 
			// txtDecision_id
			// 
			this.txtDecision_id.BackColor = System.Drawing.Color.White;
			this.txtDecision_id.Location = new System.Drawing.Point(118, 99);
			this.txtDecision_id.Name = "txtDecision_id";
			this.txtDecision_id.Size = new System.Drawing.Size(54, 20);
			this.txtDecision_id.TabIndex = 21;
			// 
			// txtNombre_decision
			// 
			this.txtNombre_decision.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.txtNombre_decision.Location = new System.Drawing.Point(118, 123);
			this.txtNombre_decision.MaxLength = 50;
			this.txtNombre_decision.Name = "txtNombre_decision";
			this.txtNombre_decision.Size = new System.Drawing.Size(242, 20);
			this.txtNombre_decision.TabIndex = 23;
			// 
			// txtEstado_id
			// 
			this.txtEstado_id.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.txtEstado_id.Enabled = false;
			this.txtEstado_id.Location = new System.Drawing.Point(329, 175);
			this.txtEstado_id.Name = "txtEstado_id";
			this.txtEstado_id.ReadOnly = true;
			this.txtEstado_id.Size = new System.Drawing.Size(31, 20);
			this.txtEstado_id.TabIndex = 76;
			// 
			// comboBox_estados_solic
			// 
			this.comboBox_estados_solic.BackColor = System.Drawing.Color.White;
			this.comboBox_estados_solic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_estados_solic.FormattingEnabled = true;
			this.comboBox_estados_solic.Location = new System.Drawing.Point(118, 148);
			this.comboBox_estados_solic.Name = "comboBox_estados_solic";
			this.comboBox_estados_solic.Size = new System.Drawing.Size(242, 21);
			this.comboBox_estados_solic.TabIndex = 75;
			this.comboBox_estados_solic.SelectionChangeCommitted += new System.EventHandler(this.comboBox_estados_solic_SelectionChangeCommitted);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.ForeColor = System.Drawing.Color.DarkBlue;
			this.label4.Location = new System.Drawing.Point(24, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 13);
			this.label4.TabIndex = 74;
			this.label4.Text = "Estado destino :";
			// 
			// rbtSolicitud
			// 
			this.rbtSolicitud.AutoSize = true;
			this.rbtSolicitud.BackColor = System.Drawing.Color.Transparent;
			this.rbtSolicitud.Location = new System.Drawing.Point(69, 61);
			this.rbtSolicitud.Name = "rbtSolicitud";
			this.rbtSolicitud.Size = new System.Drawing.Size(109, 17);
			this.rbtSolicitud.TabIndex = 77;
			this.rbtSolicitud.TabStop = true;
			this.rbtSolicitud.Text = "Decisión Solicitud";
			this.rbtSolicitud.UseVisualStyleBackColor = false;
			this.rbtSolicitud.CheckedChanged += new System.EventHandler(this.rbtSolicitud_CheckedChanged);
			// 
			// rbtExcepcion
			// 
			this.rbtExcepcion.AutoSize = true;
			this.rbtExcepcion.BackColor = System.Drawing.Color.Transparent;
			this.rbtExcepcion.Location = new System.Drawing.Point(198, 61);
			this.rbtExcepcion.Name = "rbtExcepcion";
			this.rbtExcepcion.Size = new System.Drawing.Size(119, 17);
			this.rbtExcepcion.TabIndex = 78;
			this.rbtExcepcion.TabStop = true;
			this.rbtExcepcion.Text = "Decisión Excepción";
			this.rbtExcepcion.UseVisualStyleBackColor = false;
			this.rbtExcepcion.CheckedChanged += new System.EventHandler(this.rbtExcepcion_CheckedChanged);
			// 
			// s_cnf_decisiones_doc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.fondo_formularios2;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(389, 331);
			this.Controls.Add(this.rbtExcepcion);
			this.Controls.Add(this.rbtSolicitud);
			this.Controls.Add(this.txtEstado_id);
			this.Controls.Add(this.comboBox_estados_solic);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtDecision_id);
			this.Controls.Add(this.txtNombre_decision);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button_cerrar);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "s_cnf_decisiones_doc";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = ":::";
			this.Load += new System.EventHandler(this.s_cnf_decisiones_doc_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Titulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton_activo_no;
        private System.Windows.Forms.RadioButton radioButton_activo_si;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDecision_id;
        private System.Windows.Forms.TextBox txtNombre_decision;
        private System.Windows.Forms.TextBox txtEstado_id;
        private System.Windows.Forms.ComboBox comboBox_estados_solic;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton rbtSolicitud;
		private System.Windows.Forms.RadioButton rbtExcepcion;
	}
}