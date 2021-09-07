namespace Docsis_Application
{
    partial class s_cnf_workflow_conf_det01
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
			this.label1 = new System.Windows.Forms.Label();
			this.label_decision = new System.Windows.Forms.Label();
			this.ComboBox_decision = new System.Windows.Forms.ComboBox();
			this.textBox_no_paso = new System.Windows.Forms.TextBox();
			this.textBox_descripcion_flujo = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox_de = new System.Windows.Forms.ComboBox();
			this.comboBox_para = new System.Windows.Forms.ComboBox();
			this.label_de = new System.Windows.Forms.Label();
			this.label_para = new System.Windows.Forms.Label();
			this.textBox_paso_to = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label_flujoid = new System.Windows.Forms.Label();
			this.label_flujo = new System.Windows.Forms.Label();
			this.label_Titulo = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button_cerrar = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel_wf = new System.Windows.Forms.ToolStripStatusLabel();
			this.button1 = new System.Windows.Forms.Button();
			this.radioButton_afirmativa = new System.Windows.Forms.RadioButton();
			this.radioButton_negativa = new System.Windows.Forms.RadioButton();
			this.label7 = new System.Windows.Forms.Label();
			this.rbtSolicitud = new System.Windows.Forms.RadioButton();
			this.rbtExcepcion = new System.Windows.Forms.RadioButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(25, 94);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "No. Paso :";
			// 
			// label_decision
			// 
			this.label_decision.AutoSize = true;
			this.label_decision.BackColor = System.Drawing.Color.Transparent;
			this.label_decision.Location = new System.Drawing.Point(25, 116);
			this.label_decision.Name = "label_decision";
			this.label_decision.Size = new System.Drawing.Size(54, 13);
			this.label_decision.TabIndex = 3;
			this.label_decision.Text = "Decisión :";
			// 
			// ComboBox_decision
			// 
			this.ComboBox_decision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBox_decision.FormattingEnabled = true;
			this.ComboBox_decision.Location = new System.Drawing.Point(101, 117);
			this.ComboBox_decision.Name = "ComboBox_decision";
			this.ComboBox_decision.Size = new System.Drawing.Size(164, 21);
			this.ComboBox_decision.TabIndex = 4;
			this.ComboBox_decision.SelectedIndexChanged += new System.EventHandler(this.ComboBox_decision_SelectedIndexChanged);
			this.ComboBox_decision.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_decision_SelectionChangeCommitted);
			// 
			// textBox_no_paso
			// 
			this.textBox_no_paso.Location = new System.Drawing.Point(101, 90);
			this.textBox_no_paso.Name = "textBox_no_paso";
			this.textBox_no_paso.Size = new System.Drawing.Size(50, 20);
			this.textBox_no_paso.TabIndex = 2;
			// 
			// textBox_descripcion_flujo
			// 
			this.textBox_descripcion_flujo.Location = new System.Drawing.Point(101, 166);
			this.textBox_descripcion_flujo.MaxLength = 120;
			this.textBox_descripcion_flujo.Multiline = true;
			this.textBox_descripcion_flujo.Name = "textBox_descripcion_flujo";
			this.textBox_descripcion_flujo.Size = new System.Drawing.Size(272, 63);
			this.textBox_descripcion_flujo.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Location = new System.Drawing.Point(25, 167);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Descripción :";
			// 
			// comboBox_de
			// 
			this.comboBox_de.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_de.FormattingEnabled = true;
			this.comboBox_de.Location = new System.Drawing.Point(101, 246);
			this.comboBox_de.Name = "comboBox_de";
			this.comboBox_de.Size = new System.Drawing.Size(272, 21);
			this.comboBox_de.TabIndex = 10;
			this.comboBox_de.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_estaciones_de_SelectionChangeCommitted);
			// 
			// comboBox_para
			// 
			this.comboBox_para.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_para.FormattingEnabled = true;
			this.comboBox_para.Location = new System.Drawing.Point(101, 275);
			this.comboBox_para.Name = "comboBox_para";
			this.comboBox_para.Size = new System.Drawing.Size(272, 21);
			this.comboBox_para.TabIndex = 12;
			this.comboBox_para.SelectionChangeCommitted += new System.EventHandler(this.ComboBox_estaciones_para_SelectionChangeCommitted);
			// 
			// label_de
			// 
			this.label_de.AutoSize = true;
			this.label_de.BackColor = System.Drawing.Color.Transparent;
			this.label_de.Location = new System.Drawing.Point(25, 250);
			this.label_de.Name = "label_de";
			this.label_de.Size = new System.Drawing.Size(27, 13);
			this.label_de.TabIndex = 9;
			this.label_de.Text = "De :";
			// 
			// label_para
			// 
			this.label_para.AutoSize = true;
			this.label_para.BackColor = System.Drawing.Color.Transparent;
			this.label_para.Location = new System.Drawing.Point(25, 279);
			this.label_para.Name = "label_para";
			this.label_para.Size = new System.Drawing.Size(35, 13);
			this.label_para.TabIndex = 11;
			this.label_para.Text = "Para :";
			// 
			// textBox_paso_to
			// 
			this.textBox_paso_to.Location = new System.Drawing.Point(101, 304);
			this.textBox_paso_to.Name = "textBox_paso_to";
			this.textBox_paso_to.Size = new System.Drawing.Size(50, 20);
			this.textBox_paso_to.TabIndex = 14;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label_flujoid);
			this.panel1.Controls.Add(this.label_flujo);
			this.panel1.Controls.Add(this.label_Titulo);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(395, 54);
			this.panel1.TabIndex = 0;
			// 
			// label_flujoid
			// 
			this.label_flujoid.AutoSize = true;
			this.label_flujoid.BackColor = System.Drawing.Color.Transparent;
			this.label_flujoid.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.label_flujoid.Location = new System.Drawing.Point(363, 33);
			this.label_flujoid.Name = "label_flujoid";
			this.label_flujoid.Size = new System.Drawing.Size(27, 13);
			this.label_flujoid.TabIndex = 15;
			this.label_flujoid.Text = "xxxx";
			// 
			// label_flujo
			// 
			this.label_flujo.AutoSize = true;
			this.label_flujo.BackColor = System.Drawing.Color.Transparent;
			this.label_flujo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_flujo.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.label_flujo.Location = new System.Drawing.Point(309, 34);
			this.label_flujo.Name = "label_flujo";
			this.label_flujo.Size = new System.Drawing.Size(51, 13);
			this.label_flujo.TabIndex = 14;
			this.label_flujo.Text = "Flujo ID";
			// 
			// label_Titulo
			// 
			this.label_Titulo.AutoSize = true;
			this.label_Titulo.BackColor = System.Drawing.Color.Transparent;
			this.label_Titulo.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_Titulo.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label_Titulo.Location = new System.Drawing.Point(54, 24);
			this.label_Titulo.Name = "label_Titulo";
			this.label_Titulo.Size = new System.Drawing.Size(101, 18);
			this.label_Titulo.TabIndex = 0;
			this.label_Titulo.Text = "Agregar Ruta";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.icon_ruta02;
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(49, 51);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// button_cerrar
			// 
			this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_cerrar.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_cerrar.ForeColor = System.Drawing.Color.Navy;
			this.button_cerrar.Location = new System.Drawing.Point(242, 345);
			this.button_cerrar.Name = "button_cerrar";
			this.button_cerrar.Size = new System.Drawing.Size(106, 23);
			this.button_cerrar.TabIndex = 16;
			this.button_cerrar.Text = "Cerrar";
			this.button_cerrar.UseVisualStyleBackColor = false;
			this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel_wf});
			this.statusStrip1.Location = new System.Drawing.Point(0, 384);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(395, 22);
			this.statusStrip1.TabIndex = 17;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLabel_wf
			// 
			this.StatusLabel_wf.Name = "StatusLabel_wf";
			this.StatusLabel_wf.Size = new System.Drawing.Size(85, 17);
			this.StatusLabel_wf.Text = "StatusLabel_wf";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ForeColor = System.Drawing.Color.Navy;
			this.button1.Location = new System.Drawing.Point(104, 345);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(106, 23);
			this.button1.TabIndex = 15;
			this.button1.Text = "&Guardar";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// radioButton_afirmativa
			// 
			this.radioButton_afirmativa.AutoSize = true;
			this.radioButton_afirmativa.BackColor = System.Drawing.Color.Transparent;
			this.radioButton_afirmativa.Checked = true;
			this.radioButton_afirmativa.Location = new System.Drawing.Point(106, 144);
			this.radioButton_afirmativa.Name = "radioButton_afirmativa";
			this.radioButton_afirmativa.Size = new System.Drawing.Size(71, 17);
			this.radioButton_afirmativa.TabIndex = 5;
			this.radioButton_afirmativa.TabStop = true;
			this.radioButton_afirmativa.Text = "Afirmativa";
			this.radioButton_afirmativa.UseVisualStyleBackColor = false;
			// 
			// radioButton_negativa
			// 
			this.radioButton_negativa.AutoSize = true;
			this.radioButton_negativa.BackColor = System.Drawing.Color.Transparent;
			this.radioButton_negativa.Location = new System.Drawing.Point(191, 144);
			this.radioButton_negativa.Name = "radioButton_negativa";
			this.radioButton_negativa.Size = new System.Drawing.Size(68, 17);
			this.radioButton_negativa.TabIndex = 6;
			this.radioButton_negativa.Text = "Negativa";
			this.radioButton_negativa.UseVisualStyleBackColor = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Location = new System.Drawing.Point(25, 308);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(74, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = "Paso destino :";
			// 
			// rbtSolicitud
			// 
			this.rbtSolicitud.AutoSize = true;
			this.rbtSolicitud.BackColor = System.Drawing.Color.Transparent;
			this.rbtSolicitud.Location = new System.Drawing.Point(3, 3);
			this.rbtSolicitud.Name = "rbtSolicitud";
			this.rbtSolicitud.Size = new System.Drawing.Size(91, 17);
			this.rbtSolicitud.TabIndex = 18;
			this.rbtSolicitud.TabStop = true;
			this.rbtSolicitud.Text = "Ruta Solicitud";
			this.rbtSolicitud.UseVisualStyleBackColor = false;
			this.rbtSolicitud.CheckedChanged += new System.EventHandler(this.rbtSolicitud_CheckedChanged);
			// 
			// rbtExcepcion
			// 
			this.rbtExcepcion.AutoSize = true;
			this.rbtExcepcion.BackColor = System.Drawing.Color.Transparent;
			this.rbtExcepcion.Location = new System.Drawing.Point(138, 3);
			this.rbtExcepcion.Name = "rbtExcepcion";
			this.rbtExcepcion.Size = new System.Drawing.Size(101, 17);
			this.rbtExcepcion.TabIndex = 19;
			this.rbtExcepcion.TabStop = true;
			this.rbtExcepcion.Text = "Ruta Excepción";
			this.rbtExcepcion.UseVisualStyleBackColor = false;
			this.rbtExcepcion.CheckedChanged += new System.EventHandler(this.rbtExcepcion_CheckedChanged);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Transparent;
			this.panel2.Controls.Add(this.rbtExcepcion);
			this.panel2.Controls.Add(this.rbtSolicitud);
			this.panel2.Location = new System.Drawing.Point(76, 60);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(254, 26);
			this.panel2.TabIndex = 20;
			// 
			// s_cnf_workflow_conf_det01
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.fondo_formularios2;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(395, 406);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.radioButton_negativa);
			this.Controls.Add(this.radioButton_afirmativa);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.button_cerrar);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.textBox_paso_to);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label_para);
			this.Controls.Add(this.label_de);
			this.Controls.Add(this.comboBox_para);
			this.Controls.Add(this.comboBox_de);
			this.Controls.Add(this.textBox_descripcion_flujo);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox_no_paso);
			this.Controls.Add(this.ComboBox_decision);
			this.Controls.Add(this.label_decision);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "s_cnf_workflow_conf_det01";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = ":::";
			this.Load += new System.EventHandler(this.s_workflow_conf_det01_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_decision;
        private System.Windows.Forms.ComboBox ComboBox_decision;
        private System.Windows.Forms.TextBox textBox_no_paso;
        private System.Windows.Forms.TextBox textBox_descripcion_flujo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_de;
        private System.Windows.Forms.ComboBox comboBox_para;
        private System.Windows.Forms.Label label_de;
        private System.Windows.Forms.Label label_para;
        private System.Windows.Forms.TextBox textBox_paso_to;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Titulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton_afirmativa;
        private System.Windows.Forms.RadioButton radioButton_negativa;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_wf;
        private System.Windows.Forms.Label label_flujoid;
        private System.Windows.Forms.Label label_flujo;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RadioButton rbtSolicitud;
		private System.Windows.Forms.RadioButton rbtExcepcion;
		private System.Windows.Forms.Panel panel2;
	}
}