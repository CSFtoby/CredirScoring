namespace Docsis_Application
{
    partial class s_notas_aceptar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(s_notas_aceptar));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label_Titulo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.imagesSmall = new System.Windows.Forms.ImageList(this.components);
            this.imagesLarge = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.gv_Anotaciones = new System.Windows.Forms.DataGridView();
            this.img_estado_registro = new System.Windows.Forms.DataGridViewImageColumn();
            this.fecha_ing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no_solicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.analista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aceptada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aceptada_por = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no_anotacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anotacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Anotaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Titulo
            // 
            this.label_Titulo.AutoSize = true;
            this.label_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Titulo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label_Titulo.Location = new System.Drawing.Point(52, 14);
            this.label_Titulo.Name = "label_Titulo";
            this.label_Titulo.Size = new System.Drawing.Size(295, 20);
            this.label_Titulo.TabIndex = 0;
            this.label_Titulo.Text = "Aceptar Anotaciones con Condición";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label_Titulo);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 46);
            this.panel1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 537);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(895, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // imagesSmall
            // 
            this.imagesSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesSmall.ImageStream")));
            this.imagesSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesSmall.Images.SetKeyName(0, "archivo_tipo_word.png");
            this.imagesSmall.Images.SetKeyName(1, "archivo_tipo_excel.png");
            this.imagesSmall.Images.SetKeyName(2, "archivo_tipo_pdf.png");
            this.imagesSmall.Images.SetKeyName(3, "adjunto.png");
            this.imagesSmall.Images.SetKeyName(4, "anotaciones.png");
            // 
            // imagesLarge
            // 
            this.imagesLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesLarge.ImageStream")));
            this.imagesLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesLarge.Images.SetKeyName(0, "archivo_tipo_word.png");
            this.imagesLarge.Images.SetKeyName(1, "archivo_tipo_excel.png");
            this.imagesLarge.Images.SetKeyName(2, "archivo_tipo_pdf.png");
            this.imagesLarge.Images.SetKeyName(3, "adjunto.png");
            this.imagesLarge.Images.SetKeyName(4, "anotaciones.png");
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.txtTexto);
            this.panel2.Controls.Add(this.gv_Anotaciones);
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(895, 490);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(3, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Texto de la anotación :";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(229)))), ((int)(((byte)(252)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(456, 440);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 34);
            this.button2.TabIndex = 28;
            this.button2.Text = "&Cerrar";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(229)))), ((int)(((byte)(252)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::Docsis_Application.Properties.Resources.icon_check;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(295, 440);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 34);
            this.button1.TabIndex = 27;
            this.button1.Text = "   Aceptar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTexto
            // 
            this.txtTexto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTexto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(165)))));
            this.txtTexto.Location = new System.Drawing.Point(6, 260);
            this.txtTexto.MaxLength = 500;
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTexto.Size = new System.Drawing.Size(882, 174);
            this.txtTexto.TabIndex = 26;
            // 
            // gv_Anotaciones
            // 
            this.gv_Anotaciones.AllowUserToAddRows = false;
            this.gv_Anotaciones.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.gv_Anotaciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.gv_Anotaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_Anotaciones.BackgroundColor = System.Drawing.Color.White;
            this.gv_Anotaciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gv_Anotaciones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gv_Anotaciones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv_Anotaciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.gv_Anotaciones.ColumnHeadersHeight = 20;
            this.gv_Anotaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.img_estado_registro,
            this.fecha_ing,
            this.no_solicitud,
            this.nombre_cliente,
            this.estacion,
            this.analista,
            this.aceptada,
            this.aceptada_por,
            this.no_anotacion,
            this.anotacion});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gv_Anotaciones.DefaultCellStyle = dataGridViewCellStyle18;
            this.gv_Anotaciones.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gv_Anotaciones.Location = new System.Drawing.Point(6, 3);
            this.gv_Anotaciones.MultiSelect = false;
            this.gv_Anotaciones.Name = "gv_Anotaciones";
            this.gv_Anotaciones.ReadOnly = true;
            this.gv_Anotaciones.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gv_Anotaciones.RowHeadersVisible = false;
            this.gv_Anotaciones.RowHeadersWidth = 10;
            this.gv_Anotaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_Anotaciones.Size = new System.Drawing.Size(882, 232);
            this.gv_Anotaciones.TabIndex = 24;
            this.gv_Anotaciones.SelectionChanged += new System.EventHandler(this.gv_Anotaciones_SelectionChanged);
            // 
            // img_estado_registro
            // 
            this.img_estado_registro.DataPropertyName = "img_estado_registro";
            this.img_estado_registro.HeaderText = "";
            this.img_estado_registro.Image = global::Docsis_Application.Properties.Resources.anotaciones_condi;
            this.img_estado_registro.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.img_estado_registro.Name = "img_estado_registro";
            this.img_estado_registro.ReadOnly = true;
            this.img_estado_registro.Width = 18;
            // 
            // fecha_ing
            // 
            this.fecha_ing.DataPropertyName = "fecha_ing";
            dataGridViewCellStyle15.Format = "f";
            dataGridViewCellStyle15.NullValue = null;
            this.fecha_ing.DefaultCellStyle = dataGridViewCellStyle15;
            this.fecha_ing.HeaderText = "Fecha";
            this.fecha_ing.Name = "fecha_ing";
            this.fecha_ing.ReadOnly = true;
            this.fecha_ing.Width = 120;
            // 
            // no_solicitud
            // 
            this.no_solicitud.DataPropertyName = "no_solicitud";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.NullValue = null;
            this.no_solicitud.DefaultCellStyle = dataGridViewCellStyle16;
            this.no_solicitud.HeaderText = "No. Solicitud";
            this.no_solicitud.Name = "no_solicitud";
            this.no_solicitud.ReadOnly = true;
            this.no_solicitud.Width = 80;
            // 
            // nombre_cliente
            // 
            this.nombre_cliente.DataPropertyName = "nombre_cliente";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nombre_cliente.DefaultCellStyle = dataGridViewCellStyle17;
            this.nombre_cliente.HeaderText = "Nombre del Cliente";
            this.nombre_cliente.Name = "nombre_cliente";
            this.nombre_cliente.ReadOnly = true;
            this.nombre_cliente.Width = 250;
            // 
            // estacion
            // 
            this.estacion.DataPropertyName = "estacion";
            this.estacion.HeaderText = "Estacion ";
            this.estacion.Name = "estacion";
            this.estacion.ReadOnly = true;
            this.estacion.Width = 250;
            // 
            // analista
            // 
            this.analista.DataPropertyName = "analista";
            this.analista.HeaderText = "Analista";
            this.analista.Name = "analista";
            this.analista.ReadOnly = true;
            // 
            // aceptada
            // 
            this.aceptada.DataPropertyName = "aceptada";
            this.aceptada.HeaderText = "Aceptada";
            this.aceptada.Name = "aceptada";
            this.aceptada.ReadOnly = true;
            this.aceptada.Width = 150;
            // 
            // aceptada_por
            // 
            this.aceptada_por.DataPropertyName = "aceptada_por";
            this.aceptada_por.HeaderText = "Aceptada Por";
            this.aceptada_por.Name = "aceptada_por";
            this.aceptada_por.ReadOnly = true;
            // 
            // no_anotacion
            // 
            this.no_anotacion.DataPropertyName = "no_anotacion";
            this.no_anotacion.HeaderText = "No. Nota";
            this.no_anotacion.Name = "no_anotacion";
            this.no_anotacion.ReadOnly = true;
            // 
            // anotacion
            // 
            this.anotacion.DataPropertyName = "anotacion";
            this.anotacion.HeaderText = "";
            this.anotacion.Name = "anotacion";
            this.anotacion.ReadOnly = true;
            this.anotacion.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.anotaciones_condi;
            this.pictureBox1.Location = new System.Drawing.Point(6, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 37);
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // s_notas_aceptar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 559);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "s_notas_aceptar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ":::";
            this.Load += new System.EventHandler(this.s_notas_aceptar_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Anotaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Titulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gv_Anotaciones;
        internal System.Windows.Forms.ImageList imagesSmall;
        internal System.Windows.Forms.ImageList imagesLarge;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewImageColumn img_estado_registro;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_ing;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_solicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn estacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn analista;
        private System.Windows.Forms.DataGridViewTextBoxColumn aceptada;
        private System.Windows.Forms.DataGridViewTextBoxColumn aceptada_por;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_anotacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn anotacion;
    }
}