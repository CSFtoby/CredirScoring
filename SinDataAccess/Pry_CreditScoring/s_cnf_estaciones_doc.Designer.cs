namespace Docsis_Application
{
    partial class s_cnf_estaciones_doc
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label_Titulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_cerrar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtEstacion_id = new System.Windows.Forms.TextBox();
            this.txtNombre_estacion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton_activo_no = new System.Windows.Forms.RadioButton();
            this.radioButton_activo_si = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton_todafilial_no = new System.Windows.Forms.RadioButton();
            this.radioButton_todafilial_si = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radioButton_csolic_no = new System.Windows.Forms.RadioButton();
            this.radioButton_csolic_si = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.radioButton_Escomite_no = new System.Windows.Forms.RadioButton();
            this.radioButton_Escomite_si = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox_quitar = new System.Windows.Forms.CheckBox();
            this.pbIcono = new System.Windows.Forms.PictureBox();
            this.lLBuscar_imagen = new System.Windows.Forms.LinkLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gbCriterios = new System.Windows.Forms.GroupBox();
            this.txtMonto_maximo = new System.Windows.Forms.TextBox();
            this.txtMonto_minimo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcono)).BeginInit();
            this.gbCriterios.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.label_Titulo);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 53);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
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
            this.btnClose.Location = new System.Drawing.Point(494, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 1;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label_Titulo
            // 
            this.label_Titulo.AutoSize = true;
            this.label_Titulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.label_Titulo.Location = new System.Drawing.Point(57, 18);
            this.label_Titulo.Name = "label_Titulo";
            this.label_Titulo.Size = new System.Drawing.Size(197, 30);
            this.label_Titulo.TabIndex = 0;
            this.label_Titulo.Text = "Agregar Estaciones";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.icon_estacion_wf03;
            this.pictureBox1.Location = new System.Drawing.Point(6, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 48);
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label5.Location = new System.Drawing.Point(57, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Opciones :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Location = new System.Drawing.Point(109, 334);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 2);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // button_cerrar
            // 
            this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_cerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cerrar.FlatAppearance.BorderSize = 0;
            this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cerrar.ForeColor = System.Drawing.Color.White;
            this.button_cerrar.Location = new System.Drawing.Point(307, 348);
            this.button_cerrar.Name = "button_cerrar";
            this.button_cerrar.Size = new System.Drawing.Size(100, 27);
            this.button_cerrar.TabIndex = 17;
            this.button_cerrar.Text = "Cerrar";
            this.button_cerrar.UseVisualStyleBackColor = false;
            this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(198, 348);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 27);
            this.button1.TabIndex = 16;
            this.button1.Text = "Adicionar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtEstacion_id
            // 
            this.txtEstacion_id.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEstacion_id.Location = new System.Drawing.Point(106, 76);
            this.txtEstacion_id.Name = "txtEstacion_id";
            this.txtEstacion_id.Size = new System.Drawing.Size(54, 20);
            this.txtEstacion_id.TabIndex = 2;
            // 
            // txtNombre_estacion
            // 
            this.txtNombre_estacion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombre_estacion.Location = new System.Drawing.Point(106, 101);
            this.txtNombre_estacion.MaxLength = 100;
            this.txtNombre_estacion.Name = "txtNombre_estacion";
            this.txtNombre_estacion.Size = new System.Drawing.Size(211, 20);
            this.txtNombre_estacion.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Estacion ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label2.Location = new System.Drawing.Point(12, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label3.Location = new System.Drawing.Point(12, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Activo :";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.radioButton_activo_no);
            this.panel2.Controls.Add(this.radioButton_activo_si);
            this.panel2.Location = new System.Drawing.Point(118, 126);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(99, 26);
            this.panel2.TabIndex = 6;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label4.Location = new System.Drawing.Point(12, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Todas las Filiales :";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.radioButton_todafilial_no);
            this.panel3.Controls.Add(this.radioButton_todafilial_si);
            this.panel3.Location = new System.Drawing.Point(118, 153);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(99, 26);
            this.panel3.TabIndex = 8;
            // 
            // radioButton_todafilial_no
            // 
            this.radioButton_todafilial_no.AutoSize = true;
            this.radioButton_todafilial_no.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_todafilial_no.Location = new System.Drawing.Point(51, 5);
            this.radioButton_todafilial_no.Name = "radioButton_todafilial_no";
            this.radioButton_todafilial_no.Size = new System.Drawing.Size(39, 17);
            this.radioButton_todafilial_no.TabIndex = 1;
            this.radioButton_todafilial_no.Text = "No";
            this.radioButton_todafilial_no.UseVisualStyleBackColor = false;
            // 
            // radioButton_todafilial_si
            // 
            this.radioButton_todafilial_si.AutoSize = true;
            this.radioButton_todafilial_si.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_todafilial_si.Checked = true;
            this.radioButton_todafilial_si.Location = new System.Drawing.Point(11, 5);
            this.radioButton_todafilial_si.Name = "radioButton_todafilial_si";
            this.radioButton_todafilial_si.Size = new System.Drawing.Size(34, 17);
            this.radioButton_todafilial_si.TabIndex = 0;
            this.radioButton_todafilial_si.TabStop = true;
            this.radioButton_todafilial_si.Text = "Si";
            this.radioButton_todafilial_si.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label6.Location = new System.Drawing.Point(12, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Crear solicitudes :";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.radioButton_csolic_no);
            this.panel4.Controls.Add(this.radioButton_csolic_si);
            this.panel4.Location = new System.Drawing.Point(118, 181);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(99, 26);
            this.panel4.TabIndex = 10;
            // 
            // radioButton_csolic_no
            // 
            this.radioButton_csolic_no.AutoSize = true;
            this.radioButton_csolic_no.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_csolic_no.Location = new System.Drawing.Point(51, 5);
            this.radioButton_csolic_no.Name = "radioButton_csolic_no";
            this.radioButton_csolic_no.Size = new System.Drawing.Size(39, 17);
            this.radioButton_csolic_no.TabIndex = 1;
            this.radioButton_csolic_no.Text = "No";
            this.radioButton_csolic_no.UseVisualStyleBackColor = false;
            // 
            // radioButton_csolic_si
            // 
            this.radioButton_csolic_si.AutoSize = true;
            this.radioButton_csolic_si.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_csolic_si.Checked = true;
            this.radioButton_csolic_si.Location = new System.Drawing.Point(11, 5);
            this.radioButton_csolic_si.Name = "radioButton_csolic_si";
            this.radioButton_csolic_si.Size = new System.Drawing.Size(34, 17);
            this.radioButton_csolic_si.TabIndex = 0;
            this.radioButton_csolic_si.TabStop = true;
            this.radioButton_csolic_si.Text = "Si";
            this.radioButton_csolic_si.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(516, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5, 404);
            this.panel5.TabIndex = 285;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(-4, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(5, 406);
            this.panel6.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Location = new System.Drawing.Point(-2, 406);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(521, 10);
            this.panel7.TabIndex = 287;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.Controls.Add(this.radioButton_Escomite_no);
            this.panel8.Controls.Add(this.radioButton_Escomite_si);
            this.panel8.Location = new System.Drawing.Point(118, 209);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(99, 26);
            this.panel8.TabIndex = 12;
            // 
            // radioButton_Escomite_no
            // 
            this.radioButton_Escomite_no.AutoSize = true;
            this.radioButton_Escomite_no.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_Escomite_no.Location = new System.Drawing.Point(51, 4);
            this.radioButton_Escomite_no.Name = "radioButton_Escomite_no";
            this.radioButton_Escomite_no.Size = new System.Drawing.Size(39, 17);
            this.radioButton_Escomite_no.TabIndex = 1;
            this.radioButton_Escomite_no.Text = "No";
            this.radioButton_Escomite_no.UseVisualStyleBackColor = false;
            // 
            // radioButton_Escomite_si
            // 
            this.radioButton_Escomite_si.AutoSize = true;
            this.radioButton_Escomite_si.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_Escomite_si.Checked = true;
            this.radioButton_Escomite_si.Location = new System.Drawing.Point(11, 4);
            this.radioButton_Escomite_si.Name = "radioButton_Escomite_si";
            this.radioButton_Escomite_si.Size = new System.Drawing.Size(34, 17);
            this.radioButton_Escomite_si.TabIndex = 0;
            this.radioButton_Escomite_si.TabStop = true;
            this.radioButton_Escomite_si.Text = "Si";
            this.radioButton_Escomite_si.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label7.Location = new System.Drawing.Point(10, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Es comite resolutivo";
            // 
            // checkBox_quitar
            // 
            this.checkBox_quitar.AutoSize = true;
            this.checkBox_quitar.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_quitar.Location = new System.Drawing.Point(366, 208);
            this.checkBox_quitar.Name = "checkBox_quitar";
            this.checkBox_quitar.Size = new System.Drawing.Size(91, 17);
            this.checkBox_quitar.TabIndex = 291;
            this.checkBox_quitar.Text = "Quitar imagen";
            this.checkBox_quitar.UseVisualStyleBackColor = false;
            // 
            // pbIcono
            // 
            this.pbIcono.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbIcono.BackColor = System.Drawing.Color.Transparent;
            this.pbIcono.Image = global::Docsis_Application.Properties.Resources.estacion_actual;
            this.pbIcono.Location = new System.Drawing.Point(358, 76);
            this.pbIcono.Name = "pbIcono";
            this.pbIcono.Size = new System.Drawing.Size(156, 131);
            this.pbIcono.TabIndex = 290;
            this.pbIcono.TabStop = false;
            // 
            // lLBuscar_imagen
            // 
            this.lLBuscar_imagen.AutoSize = true;
            this.lLBuscar_imagen.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.lLBuscar_imagen.Location = new System.Drawing.Point(358, 60);
            this.lLBuscar_imagen.Name = "lLBuscar_imagen";
            this.lLBuscar_imagen.Size = new System.Drawing.Size(99, 13);
            this.lLBuscar_imagen.TabIndex = 292;
            this.lLBuscar_imagen.TabStop = true;
            this.lLBuscar_imagen.Text = "Buscar una Imagen";
            this.lLBuscar_imagen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLBuscar_imagen_LinkClicked);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Location = new System.Drawing.Point(0, 385);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(517, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gbCriterios
            // 
            this.gbCriterios.Controls.Add(this.txtMonto_maximo);
            this.gbCriterios.Controls.Add(this.txtMonto_minimo);
            this.gbCriterios.Controls.Add(this.label8);
            this.gbCriterios.Controls.Add(this.label9);
            this.gbCriterios.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCriterios.Location = new System.Drawing.Point(119, 243);
            this.gbCriterios.Name = "gbCriterios";
            this.gbCriterios.Size = new System.Drawing.Size(229, 78);
            this.gbCriterios.TabIndex = 13;
            this.gbCriterios.TabStop = false;
            this.gbCriterios.Text = "Criterios de Aprobación";
            // 
            // txtMonto_maximo
            // 
            this.txtMonto_maximo.BackColor = System.Drawing.Color.White;
            this.txtMonto_maximo.Location = new System.Drawing.Point(114, 48);
            this.txtMonto_maximo.Name = "txtMonto_maximo";
            this.txtMonto_maximo.Size = new System.Drawing.Size(100, 22);
            this.txtMonto_maximo.TabIndex = 3;
            this.txtMonto_maximo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMonto_minimo
            // 
            this.txtMonto_minimo.BackColor = System.Drawing.Color.White;
            this.txtMonto_minimo.Location = new System.Drawing.Point(114, 22);
            this.txtMonto_minimo.Name = "txtMonto_minimo";
            this.txtMonto_minimo.Size = new System.Drawing.Size(100, 22);
            this.txtMonto_minimo.TabIndex = 1;
            this.txtMonto_minimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Monto minimo :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Monto minimo :";
            // 
            // s_cnf_estaciones_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(517, 407);
            this.Controls.Add(this.gbCriterios);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.checkBox_quitar);
            this.Controls.Add(this.pbIcono);
            this.Controls.Add(this.lLBuscar_imagen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEstacion_id);
            this.Controls.Add(this.txtNombre_estacion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_cerrar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_cnf_estaciones_doc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ":::";
            this.Load += new System.EventHandler(this.s_cnf_estaciones_doc01_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcono)).EndInit();
            this.gbCriterios.ResumeLayout(false);
            this.gbCriterios.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Titulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtEstacion_id;
        private System.Windows.Forms.TextBox txtNombre_estacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton_activo_no;
        private System.Windows.Forms.RadioButton radioButton_activo_si;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton_todafilial_no;
        private System.Windows.Forms.RadioButton radioButton_todafilial_si;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton radioButton_csolic_no;
        private System.Windows.Forms.RadioButton radioButton_csolic_si;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton radioButton_Escomite_no;
        private System.Windows.Forms.RadioButton radioButton_Escomite_si;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox_quitar;
        private System.Windows.Forms.PictureBox pbIcono;
        private System.Windows.Forms.LinkLabel lLBuscar_imagen;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox gbCriterios;
        private System.Windows.Forms.TextBox txtMonto_maximo;
        private System.Windows.Forms.TextBox txtMonto_minimo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}