namespace Docsis_Application
{
    partial class s_Conciliacion_doc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.labelComite = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label_caracteres = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnCargar = new System.Windows.Forms.Button();
            this.gvTabla = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lLAbrir_archivo_cs = new System.Windows.Forms.LinkLabel();
            this.txtArchivo = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lLimprimir = new System.Windows.Forms.LinkLabel();
            this.rhtxtlog = new System.Windows.Forms.RichTextBox();
            this.label132 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnIniciarConci = new System.Windows.Forms.Button();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gvResultado = new System.Windows.Forms.DataGridView();
            this.cmbTiposResul = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.no_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario_comite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no_identificacion_tu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rol_tu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no_identificacion_cs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rol_cs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTabla)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResultado)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.labelComite);
            this.panelTop.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panelTop.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelTop.Location = new System.Drawing.Point(-1, 1);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(895, 43);
            this.panelTop.TabIndex = 347;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
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
            this.btnClose.Location = new System.Drawing.Point(876, 4);
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
            this.labelComite.Location = new System.Drawing.Point(6, 7);
            this.labelComite.Name = "labelComite";
            this.labelComite.Size = new System.Drawing.Size(276, 21);
            this.labelComite.TabIndex = 0;
            this.labelComite.Text = "Conciliación de solicitudes TransUnion";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.label_caracteres});
            this.statusStrip1.Location = new System.Drawing.Point(0, 469);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(893, 22);
            this.statusStrip1.TabIndex = 352;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label_caracteres
            // 
            this.label_caracteres.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.label_caracteres.Name = "label_caracteres";
            this.label_caracteres.Size = new System.Drawing.Size(27, 17);
            this.label_caracteres.Text = ":::::::::::";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(4, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(886, 420);
            this.tabControl1.TabIndex = 353;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dateTimePicker2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Controls.Add(this.btnNextPage);
            this.tabPage1.Controls.Add(this.btnCargar);
            this.tabPage1.Controls.Add(this.gvTabla);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lLAbrir_archivo_cs);
            this.tabPage1.Controls.Add(this.txtArchivo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(878, 394);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "      Paso 1   ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(257, 38);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 361;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 360;
            this.label2.Text = "Rango de fechas :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(15, 38);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 359;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnNextPage.FlatAppearance.BorderSize = 0;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextPage.ForeColor = System.Drawing.Color.White;
            this.btnNextPage.Location = new System.Drawing.Point(863, 149);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(15, 59);
            this.btnNextPage.TabIndex = 357;
            this.btnNextPage.Text = ">";
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Visible = false;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnCargar
            // 
            this.btnCargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCargar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnCargar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargar.FlatAppearance.BorderSize = 0;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnCargar.ForeColor = System.Drawing.Color.White;
            this.btnCargar.Image = global::Docsis_Application.Properties.Resources.icon_database;
            this.btnCargar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCargar.Location = new System.Drawing.Point(669, 346);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(186, 31);
            this.btnCargar.TabIndex = 356;
            this.btnCargar.Text = "Cargar a base de datos";
            this.btnCargar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Visible = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // gvTabla
            // 
            this.gvTabla.AllowUserToAddRows = false;
            this.gvTabla.AllowUserToDeleteRows = false;
            this.gvTabla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gvTabla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvTabla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvTabla.BackgroundColor = System.Drawing.Color.White;
            this.gvTabla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvTabla.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvTabla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTabla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvTabla.ColumnHeadersHeight = 20;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTabla.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvTabla.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvTabla.Location = new System.Drawing.Point(15, 98);
            this.gvTabla.Name = "gvTabla";
            this.gvTabla.ReadOnly = true;
            this.gvTabla.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvTabla.RowHeadersVisible = false;
            this.gvTabla.RowHeadersWidth = 10;
            this.gvTabla.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvTabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvTabla.Size = new System.Drawing.Size(836, 236);
            this.gvTabla.TabIndex = 355;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 354;
            this.label1.Text = "Nombre del archivo :";
            // 
            // lLAbrir_archivo_cs
            // 
            this.lLAbrir_archivo_cs.AutoSize = true;
            this.lLAbrir_archivo_cs.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.lLAbrir_archivo_cs.Location = new System.Drawing.Point(616, 77);
            this.lLAbrir_archivo_cs.Name = "lLAbrir_archivo_cs";
            this.lLAbrir_archivo_cs.Size = new System.Drawing.Size(100, 13);
            this.lLAbrir_archivo_cs.TabIndex = 353;
            this.lLAbrir_archivo_cs.TabStop = true;
            this.lLAbrir_archivo_cs.Text = "Cargar archivo CSV";
            this.lLAbrir_archivo_cs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLAbrir_archivo_cs_LinkClicked);
            // 
            // txtArchivo
            // 
            this.txtArchivo.Location = new System.Drawing.Point(126, 73);
            this.txtArchivo.Name = "txtArchivo";
            this.txtArchivo.Size = new System.Drawing.Size(463, 20);
            this.txtArchivo.TabIndex = 352;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.linkLabel1);
            this.tabPage2.Controls.Add(this.lLimprimir);
            this.tabPage2.Controls.Add(this.rhtxtlog);
            this.tabPage2.Controls.Add(this.label132);
            this.tabPage2.Controls.Add(this.txtLog);
            this.tabPage2.Controls.Add(this.btnIniciarConci);
            this.tabPage2.Controls.Add(this.btnPrevPage);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(878, 394);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "      Paso 2      ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.linkLabel1.Location = new System.Drawing.Point(789, 327);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(62, 13);
            this.linkLabel1.TabIndex = 367;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Ver detalles";
            // 
            // lLimprimir
            // 
            this.lLimprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lLimprimir.AutoSize = true;
            this.lLimprimir.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.lLimprimir.Location = new System.Drawing.Point(730, 327);
            this.lLimprimir.Name = "lLimprimir";
            this.lLimprimir.Size = new System.Drawing.Size(42, 13);
            this.lLimprimir.TabIndex = 366;
            this.lLimprimir.TabStop = true;
            this.lLimprimir.Text = "Imprimir";
            // 
            // rhtxtlog
            // 
            this.rhtxtlog.Location = new System.Drawing.Point(671, 11);
            this.rhtxtlog.Name = "rhtxtlog";
            this.rhtxtlog.Size = new System.Drawing.Size(13, 14);
            this.rhtxtlog.TabIndex = 365;
            this.rhtxtlog.Text = "";
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.BackColor = System.Drawing.Color.Transparent;
            this.label132.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.label132.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label132.Location = new System.Drawing.Point(33, 15);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(286, 25);
            this.label132.TabIndex = 364;
            this.label132.Text = "Resultado del proceso conciliación";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(38, 48);
            this.txtLog.MaxLength = 12;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(816, 264);
            this.txtLog.TabIndex = 363;
            // 
            // btnIniciarConci
            // 
            this.btnIniciarConci.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIniciarConci.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnIniciarConci.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIniciarConci.FlatAppearance.BorderSize = 0;
            this.btnIniciarConci.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciarConci.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnIniciarConci.ForeColor = System.Drawing.Color.White;
            this.btnIniciarConci.Image = global::Docsis_Application.Properties.Resources.process_icon;
            this.btnIniciarConci.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIniciarConci.Location = new System.Drawing.Point(307, 337);
            this.btnIniciarConci.Name = "btnIniciarConci";
            this.btnIniciarConci.Size = new System.Drawing.Size(239, 42);
            this.btnIniciarConci.TabIndex = 359;
            this.btnIniciarConci.Text = "  Iniciar proceso de conciliación";
            this.btnIniciarConci.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIniciarConci.UseVisualStyleBackColor = false;
            this.btnIniciarConci.Click += new System.EventHandler(this.btnIniciarConci_Click);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnPrevPage.FlatAppearance.BorderSize = 0;
            this.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevPage.ForeColor = System.Drawing.Color.White;
            this.btnPrevPage.Location = new System.Drawing.Point(0, 149);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(15, 59);
            this.btnPrevPage.TabIndex = 358;
            this.btnPrevPage.Text = "<";
            this.btnPrevPage.UseVisualStyleBackColor = false;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.cmbTiposResul);
            this.tabPage3.Controls.Add(this.gvResultado);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(878, 394);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Reportes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gvResultado
            // 
            this.gvResultado.AllowUserToAddRows = false;
            this.gvResultado.AllowUserToDeleteRows = false;
            this.gvResultado.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.gvResultado.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvResultado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvResultado.BackgroundColor = System.Drawing.Color.White;
            this.gvResultado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvResultado.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvResultado.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvResultado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvResultado.ColumnHeadersHeight = 20;
            this.gvResultado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no_solicitud,
            this.usuario_comite,
            this.no_identificacion_tu,
            this.rol_tu,
            this.no_identificacion_cs,
            this.rol_cs,
            this.observaciones});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvResultado.DefaultCellStyle = dataGridViewCellStyle10;
            this.gvResultado.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvResultado.Location = new System.Drawing.Point(13, 44);
            this.gvResultado.Name = "gvResultado";
            this.gvResultado.ReadOnly = true;
            this.gvResultado.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvResultado.RowHeadersVisible = false;
            this.gvResultado.RowHeadersWidth = 10;
            this.gvResultado.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvResultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvResultado.Size = new System.Drawing.Size(856, 303);
            this.gvResultado.TabIndex = 344;
            // 
            // cmbTiposResul
            // 
            this.cmbTiposResul.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTiposResul.FormattingEnabled = true;
            this.cmbTiposResul.Items.AddRange(new object[] {
            "Todas Ok (1)",
            "Solicitante no encontrado en CreditScoring (-97)",
            "Garantes o Codeudor no encontrado en CreditScoring (-98)",
            "ID no son iguales. (-99)",
            "Todas las Application con problemas"});
            this.cmbTiposResul.Location = new System.Drawing.Point(133, 12);
            this.cmbTiposResul.Name = "cmbTiposResul";
            this.cmbTiposResul.Size = new System.Drawing.Size(229, 21);
            this.cmbTiposResul.TabIndex = 345;
            this.cmbTiposResul.SelectedIndexChanged += new System.EventHandler(this.cmbTiposResul_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 346;
            this.label3.Text = "Tipo de Resultado :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(734, 353);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 35);
            this.button1.TabIndex = 347;
            this.button1.Text = "Exportar a Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // no_solicitud
            // 
            this.no_solicitud.DataPropertyName = "no_solicitud";
            this.no_solicitud.HeaderText = "No. Solicitud";
            this.no_solicitud.Name = "no_solicitud";
            this.no_solicitud.ReadOnly = true;
            // 
            // usuario_comite
            // 
            this.usuario_comite.DataPropertyName = "application_id";
            this.usuario_comite.HeaderText = "ApplicationID";
            this.usuario_comite.Name = "usuario_comite";
            this.usuario_comite.ReadOnly = true;
            // 
            // no_identificacion_tu
            // 
            this.no_identificacion_tu.DataPropertyName = "no_identificacion_tu";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.no_identificacion_tu.DefaultCellStyle = dataGridViewCellStyle6;
            this.no_identificacion_tu.HeaderText = "No. ID TU";
            this.no_identificacion_tu.Name = "no_identificacion_tu";
            this.no_identificacion_tu.ReadOnly = true;
            this.no_identificacion_tu.Width = 90;
            // 
            // rol_tu
            // 
            this.rol_tu.DataPropertyName = "rol_tu";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rol_tu.DefaultCellStyle = dataGridViewCellStyle7;
            this.rol_tu.HeaderText = "Rol tu";
            this.rol_tu.Name = "rol_tu";
            this.rol_tu.ReadOnly = true;
            this.rol_tu.Width = 80;
            // 
            // no_identificacion_cs
            // 
            this.no_identificacion_cs.DataPropertyName = "no_identificacion_cs";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.no_identificacion_cs.DefaultCellStyle = dataGridViewCellStyle8;
            this.no_identificacion_cs.HeaderText = "No. ID CS";
            this.no_identificacion_cs.Name = "no_identificacion_cs";
            this.no_identificacion_cs.ReadOnly = true;
            this.no_identificacion_cs.Width = 80;
            // 
            // rol_cs
            // 
            this.rol_cs.DataPropertyName = "rol_cs";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.rol_cs.DefaultCellStyle = dataGridViewCellStyle9;
            this.rol_cs.HeaderText = "Rol CS";
            this.rol_cs.Name = "rol_cs";
            this.rol_cs.ReadOnly = true;
            this.rol_cs.Width = 80;
            // 
            // observaciones
            // 
            this.observaciones.DataPropertyName = "observacion";
            this.observaciones.HeaderText = "Observaciones";
            this.observaciones.Name = "observaciones";
            this.observaciones.ReadOnly = true;
            this.observaciones.Width = 450;
            // 
            // s_Conciliacion_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 491);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_Conciliacion_doc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conciliar";
            this.Load += new System.EventHandler(this.s_Conciliacion_doc_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTabla)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResultado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label labelComite;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel label_caracteres;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.DataGridView gvTabla;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lLAbrir_archivo_cs;
        private System.Windows.Forms.TextBox txtArchivo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnIniciarConci;
        private System.Windows.Forms.Button btnPrevPage;
        public System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.RichTextBox rhtxtlog;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel lLimprimir;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTiposResul;
        private System.Windows.Forms.DataGridView gvResultado;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario_comite;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_identificacion_tu;
        private System.Windows.Forms.DataGridViewTextBoxColumn rol_tu;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_identificacion_cs;
        private System.Windows.Forms.DataGridViewTextBoxColumn rol_cs;
        private System.Windows.Forms.DataGridViewTextBoxColumn observaciones;
    }
}