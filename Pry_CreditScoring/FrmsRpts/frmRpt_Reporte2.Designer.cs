namespace Docsis_Application.FrmsRpts
{
    partial class frmRpt_Reporte2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_Reporte2));
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMaximizar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtNo_solicitud = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbEstadoSolic = new System.Windows.Forms.ComboBox();
            this.labelEstado = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbFiliales = new System.Windows.Forms.ComboBox();
            this.cmbRegionales = new System.Windows.Forms.ComboBox();
            this.labelFilial = new System.Windows.Forms.Label();
            this.labelRegional = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label249 = new System.Windows.Forms.Label();
            this.dpFecha2 = new System.Windows.Forms.DateTimePicker();
            this.dpFecha1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelWait = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.label105 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.gvReporte1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LabelResultado = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbExport = new System.Windows.Forms.RadioButton();
            this.rbCopíar = new System.Windows.Forms.RadioButton();
            this.cmbEstaciones = new System.Windows.Forms.ComboBox();
            this.labelEstacionActual = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelWait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReporte1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.btnMaximizar);
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Location = new System.Drawing.Point(0, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(672, 34);
            this.panelTop.TabIndex = 108;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
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
            this.btnClose.Location = new System.Drawing.Point(650, 4);
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
            this.btnMaximizar.Location = new System.Drawing.Point(633, 4);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(15, 16);
            this.btnMaximizar.TabIndex = 115;
            this.btnMaximizar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMaximizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnMaximizar.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(9, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 15);
            this.label4.TabIndex = 108;
            this.label4.Text = "Reporte de Solicitudes ";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(14, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 230);
            this.groupBox1.TabIndex = 348;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "  Filtros  ";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.labelEstacionActual);
            this.groupBox4.Controls.Add(this.cmbEstaciones);
            this.groupBox4.Controls.Add(this.txtNo_solicitud);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.cmbEstadoSolic);
            this.groupBox4.Controls.Add(this.labelEstado);
            this.groupBox4.Location = new System.Drawing.Point(19, 112);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(602, 112);
            this.groupBox4.TabIndex = 101;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Por estado / No. Solicitud";
            // 
            // txtNo_solicitud
            // 
            this.txtNo_solicitud.Location = new System.Drawing.Point(131, 26);
            this.txtNo_solicitud.Name = "txtNo_solicitud";
            this.txtNo_solicitud.Size = new System.Drawing.Size(65, 20);
            this.txtNo_solicitud.TabIndex = 102;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(12, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 101;
            this.label7.Text = "No. Solicitud";
            // 
            // cmbEstadoSolic
            // 
            this.cmbEstadoSolic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoSolic.FormattingEnabled = true;
            this.cmbEstadoSolic.Location = new System.Drawing.Point(131, 51);
            this.cmbEstadoSolic.Name = "cmbEstadoSolic";
            this.cmbEstadoSolic.Size = new System.Drawing.Size(213, 21);
            this.cmbEstadoSolic.TabIndex = 100;
            this.cmbEstadoSolic.SelectionChangeCommitted += new System.EventHandler(this.cmbEstadoSolic_SelectionChangeCommitted);
            // 
            // labelEstado
            // 
            this.labelEstado.AutoSize = true;
            this.labelEstado.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEstado.ForeColor = System.Drawing.Color.Black;
            this.labelEstado.Location = new System.Drawing.Point(12, 54);
            this.labelEstado.Name = "labelEstado";
            this.labelEstado.Size = new System.Drawing.Size(95, 13);
            this.labelEstado.TabIndex = 99;
            this.labelEstado.Text = "Estado solicitud :";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.cmbFiliales);
            this.groupBox3.Controls.Add(this.cmbRegionales);
            this.groupBox3.Controls.Add(this.labelFilial);
            this.groupBox3.Controls.Add(this.labelRegional);
            this.groupBox3.Location = new System.Drawing.Point(272, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(349, 87);
            this.groupBox3.TabIndex = 100;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Por Regional / Filial Especifica";
            // 
            // cmbFiliales
            // 
            this.cmbFiliales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiliales.FormattingEnabled = true;
            this.cmbFiliales.Location = new System.Drawing.Point(94, 49);
            this.cmbFiliales.Name = "cmbFiliales";
            this.cmbFiliales.Size = new System.Drawing.Size(237, 21);
            this.cmbFiliales.TabIndex = 98;
            this.cmbFiliales.DropDown += new System.EventHandler(this.cmbFiliales_DropDown);
            this.cmbFiliales.SelectionChangeCommitted += new System.EventHandler(this.cmbFiliales_SelectionChangeCommitted);
            // 
            // cmbRegionales
            // 
            this.cmbRegionales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegionales.FormattingEnabled = true;
            this.cmbRegionales.Location = new System.Drawing.Point(94, 24);
            this.cmbRegionales.Name = "cmbRegionales";
            this.cmbRegionales.Size = new System.Drawing.Size(237, 21);
            this.cmbRegionales.TabIndex = 97;
            this.cmbRegionales.SelectionChangeCommitted += new System.EventHandler(this.cmbRegionales_SelectionChangeCommitted);
            // 
            // labelFilial
            // 
            this.labelFilial.AutoSize = true;
            this.labelFilial.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilial.ForeColor = System.Drawing.Color.Black;
            this.labelFilial.Location = new System.Drawing.Point(19, 53);
            this.labelFilial.Name = "labelFilial";
            this.labelFilial.Size = new System.Drawing.Size(37, 13);
            this.labelFilial.TabIndex = 96;
            this.labelFilial.Text = "Filial :";
            // 
            // labelRegional
            // 
            this.labelRegional.AutoSize = true;
            this.labelRegional.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegional.ForeColor = System.Drawing.Color.Black;
            this.labelRegional.Location = new System.Drawing.Point(19, 28);
            this.labelRegional.Name = "labelRegional";
            this.labelRegional.Size = new System.Drawing.Size(59, 13);
            this.labelRegional.TabIndex = 95;
            this.labelRegional.Text = "Regional :";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.label249);
            this.groupBox2.Controls.Add(this.dpFecha2);
            this.groupBox2.Controls.Add(this.dpFecha1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(19, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(247, 87);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fecha presentacion";
            // 
            // label249
            // 
            this.label249.AutoSize = true;
            this.label249.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label249.ForeColor = System.Drawing.Color.Black;
            this.label249.Location = new System.Drawing.Point(38, 32);
            this.label249.Name = "label249";
            this.label249.Size = new System.Drawing.Size(76, 13);
            this.label249.TabIndex = 94;
            this.label249.Text = "Fecha inicial :";
            // 
            // dpFecha2
            // 
            this.dpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpFecha2.Location = new System.Drawing.Point(117, 51);
            this.dpFecha2.Name = "dpFecha2";
            this.dpFecha2.Size = new System.Drawing.Size(100, 20);
            this.dpFecha2.TabIndex = 98;
            // 
            // dpFecha1
            // 
            this.dpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpFecha1.Location = new System.Drawing.Point(117, 28);
            this.dpFecha1.Name = "dpFecha1";
            this.dpFecha1.Size = new System.Drawing.Size(100, 20);
            this.dpFecha1.TabIndex = 95;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(38, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "Fecha final :";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.panelWait);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.gvReporte1);
            this.panel2.Location = new System.Drawing.Point(0, 336);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(672, 60);
            this.panel2.TabIndex = 347;
            // 
            // panelWait
            // 
            this.panelWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelWait.BackColor = System.Drawing.Color.Transparent;
            this.panelWait.Controls.Add(this.label3);
            this.panelWait.Controls.Add(this.pictureBox10);
            this.panelWait.Controls.Add(this.label105);
            this.panelWait.Location = new System.Drawing.Point(6, 9);
            this.panelWait.Name = "panelWait";
            this.panelWait.Size = new System.Drawing.Size(273, 44);
            this.panelWait.TabIndex = 112;
            this.panelWait.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(49, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 20);
            this.label3.TabIndex = 123;
            this.label3.Text = "El proceso puede tardar, espere por favor ";
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox10.Image = global::Docsis_Application.Properties.Resources._3011;
            this.pictureBox10.Location = new System.Drawing.Point(6, 3);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(35, 36);
            this.pictureBox10.TabIndex = 122;
            this.pictureBox10.TabStop = false;
            // 
            // label105
            // 
            this.label105.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label105.Location = new System.Drawing.Point(44, 6);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(145, 20);
            this.label105.TabIndex = 0;
            this.label105.Text = "Generando info.....";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(560, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(96, 35);
            this.btnOk.TabIndex = 111;
            this.btnOk.Text = "Exportar";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gvReporte1
            // 
            this.gvReporte1.AllowUserToAddRows = false;
            this.gvReporte1.AllowUserToDeleteRows = false;
            this.gvReporte1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gvReporte1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvReporte1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvReporte1.BackgroundColor = System.Drawing.Color.White;
            this.gvReporte1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvReporte1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvReporte1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvReporte1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvReporte1.ColumnHeadersHeight = 20;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvReporte1.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvReporte1.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvReporte1.Location = new System.Drawing.Point(303, 3);
            this.gvReporte1.Name = "gvReporte1";
            this.gvReporte1.ReadOnly = true;
            this.gvReporte1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvReporte1.RowHeadersVisible = false;
            this.gvReporte1.RowHeadersWidth = 10;
            this.gvReporte1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvReporte1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvReporte1.Size = new System.Drawing.Size(55, 50);
            this.gvReporte1.TabIndex = 344;
            this.gvReporte1.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelResultado,
            this.labelTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 396);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(673, 22);
            this.statusStrip1.TabIndex = 346;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LabelResultado
            // 
            this.LabelResultado.BackColor = System.Drawing.Color.Transparent;
            this.LabelResultado.ForeColor = System.Drawing.Color.White;
            this.LabelResultado.Name = "LabelResultado";
            this.LabelResultado.Size = new System.Drawing.Size(22, 17);
            this.LabelResultado.Text = ":::::";
            // 
            // labelTime
            // 
            this.labelTime.ForeColor = System.Drawing.Color.White;
            this.labelTime.Image = ((System.Drawing.Image)(resources.GetObject("labelTime.Image")));
            this.labelTime.Margin = new System.Windows.Forms.Padding(600, 3, 0, 2);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(29, 17);
            this.labelTime.Text = "::";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox5.Controls.Add(this.rbExport);
            this.groupBox5.Controls.Add(this.rbCopíar);
            this.groupBox5.Location = new System.Drawing.Point(18, 281);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(637, 40);
            this.groupBox5.TabIndex = 349;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Opciones de Exportar / Archivo";
            // 
            // rbExport
            // 
            this.rbExport.AutoSize = true;
            this.rbExport.Checked = true;
            this.rbExport.Location = new System.Drawing.Point(475, 14);
            this.rbExport.Name = "rbExport";
            this.rbExport.Size = new System.Drawing.Size(108, 17);
            this.rbExport.TabIndex = 100;
            this.rbExport.TabStop = true;
            this.rbExport.Text = "Exportar a Excel..";
            this.rbExport.UseVisualStyleBackColor = true;
            // 
            // rbCopíar
            // 
            this.rbCopíar.AutoSize = true;
            this.rbCopíar.Location = new System.Drawing.Point(296, 14);
            this.rbCopíar.Name = "rbCopíar";
            this.rbCopíar.Size = new System.Drawing.Size(171, 17);
            this.rbCopíar.TabIndex = 99;
            this.rbCopíar.Text = "Copiar al portapapeles o Clipart";
            this.rbCopíar.UseVisualStyleBackColor = true;
            // 
            // cmbEstaciones
            // 
            this.cmbEstaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstaciones.FormattingEnabled = true;
            this.cmbEstaciones.Location = new System.Drawing.Point(131, 80);
            this.cmbEstaciones.Name = "cmbEstaciones";
            this.cmbEstaciones.Size = new System.Drawing.Size(213, 21);
            this.cmbEstaciones.TabIndex = 103;
            this.cmbEstaciones.SelectionChangeCommitted += new System.EventHandler(this.cmbEstaciones_SelectionChangeCommitted);
            // 
            // labelEstacionActual
            // 
            this.labelEstacionActual.AutoSize = true;
            this.labelEstacionActual.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEstacionActual.ForeColor = System.Drawing.Color.Black;
            this.labelEstacionActual.Location = new System.Drawing.Point(12, 84);
            this.labelEstacionActual.Name = "labelEstacionActual";
            this.labelEstacionActual.Size = new System.Drawing.Size(84, 13);
            this.labelEstacionActual.TabIndex = 104;
            this.labelEstacionActual.Text = "Estación actual";
            // 
            // frmRpt_Reporte2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 418);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRpt_Reporte2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRpt_Reporte2";
            this.Load += new System.EventHandler(this.frmRpt_Reporte2_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panelWait.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReporte1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMaximizar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtNo_solicitud;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbEstadoSolic;
        private System.Windows.Forms.Label labelEstado;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbFiliales;
        private System.Windows.Forms.ComboBox cmbRegionales;
        private System.Windows.Forms.Label labelFilial;
        private System.Windows.Forms.Label labelRegional;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label249;
        private System.Windows.Forms.DateTimePicker dpFecha2;
        private System.Windows.Forms.DateTimePicker dpFecha1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelWait;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView gvReporte1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LabelResultado;
        private System.Windows.Forms.ToolStripStatusLabel labelTime;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbExport;
        private System.Windows.Forms.RadioButton rbCopíar;
        private System.Windows.Forms.Label labelEstacionActual;
        private System.Windows.Forms.ComboBox cmbEstaciones;
    }
}