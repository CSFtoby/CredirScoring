namespace Docsis_Application
{
    partial class s_cnf_documentos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button_cerrar = new System.Windows.Forms.Button();
            this.button_eliminar = new System.Windows.Forms.Button();
            this.button_modificar = new System.Windows.Forms.Button();
            this.button_adicionar = new System.Windows.Forms.Button();
            this.gvDocumentos = new System.Windows.Forms.DataGridView();
            this.pbRefrescar = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Label_currentrow = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Titulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.icono = new System.Windows.Forms.DataGridViewImageColumn();
            this.documento_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sigla_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Persona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefrescar)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_cerrar
            // 
            this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cerrar.FlatAppearance.BorderSize = 0;
            this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cerrar.ForeColor = System.Drawing.Color.White;
            this.button_cerrar.Location = new System.Drawing.Point(808, 339);
            this.button_cerrar.Name = "button_cerrar";
            this.button_cerrar.Size = new System.Drawing.Size(75, 23);
            this.button_cerrar.TabIndex = 48;
            this.button_cerrar.Text = "Cerrar";
            this.button_cerrar.UseVisualStyleBackColor = false;
            this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
            // 
            // button_eliminar
            // 
            this.button_eliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_eliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_eliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_eliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_eliminar.ForeColor = System.Drawing.Color.White;
            this.button_eliminar.Location = new System.Drawing.Point(194, 339);
            this.button_eliminar.Name = "button_eliminar";
            this.button_eliminar.Size = new System.Drawing.Size(95, 23);
            this.button_eliminar.TabIndex = 46;
            this.button_eliminar.Text = "Eliminar";
            this.button_eliminar.UseVisualStyleBackColor = false;
            this.button_eliminar.Click += new System.EventHandler(this.button_eliminar_Click);
            // 
            // button_modificar
            // 
            this.button_modificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_modificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_modificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_modificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_modificar.ForeColor = System.Drawing.Color.White;
            this.button_modificar.Location = new System.Drawing.Point(100, 339);
            this.button_modificar.Name = "button_modificar";
            this.button_modificar.Size = new System.Drawing.Size(90, 23);
            this.button_modificar.TabIndex = 45;
            this.button_modificar.Text = "Modificar";
            this.button_modificar.UseVisualStyleBackColor = false;
            this.button_modificar.Click += new System.EventHandler(this.button_modificar_Click);
            // 
            // button_adicionar
            // 
            this.button_adicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_adicionar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_adicionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_adicionar.FlatAppearance.BorderSize = 0;
            this.button_adicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_adicionar.ForeColor = System.Drawing.Color.White;
            this.button_adicionar.Location = new System.Drawing.Point(7, 339);
            this.button_adicionar.Name = "button_adicionar";
            this.button_adicionar.Size = new System.Drawing.Size(87, 23);
            this.button_adicionar.TabIndex = 44;
            this.button_adicionar.Text = "Adicionar";
            this.button_adicionar.UseVisualStyleBackColor = false;
            this.button_adicionar.Click += new System.EventHandler(this.button_adicionar_Click);
            // 
            // gvDocumentos
            // 
            this.gvDocumentos.AllowUserToAddRows = false;
            this.gvDocumentos.AllowUserToDeleteRows = false;
            this.gvDocumentos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gvDocumentos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvDocumentos.BackgroundColor = System.Drawing.Color.White;
            this.gvDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvDocumentos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvDocumentos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDocumentos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvDocumentos.ColumnHeadersHeight = 20;
            this.gvDocumentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icono,
            this.documento_id,
            this.descripcion,
            this.sigla_doc,
            this.Persona,
            this.activo});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvDocumentos.DefaultCellStyle = dataGridViewCellStyle6;
            this.gvDocumentos.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvDocumentos.Location = new System.Drawing.Point(1, 64);
            this.gvDocumentos.Name = "gvDocumentos";
            this.gvDocumentos.ReadOnly = true;
            this.gvDocumentos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvDocumentos.RowHeadersVisible = false;
            this.gvDocumentos.RowHeadersWidth = 10;
            this.gvDocumentos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvDocumentos.Size = new System.Drawing.Size(894, 263);
            this.gvDocumentos.TabIndex = 43;
            // 
            // pbRefrescar
            // 
            this.pbRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbRefrescar.BackColor = System.Drawing.Color.Transparent;
            this.pbRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRefrescar.Image = global::Docsis_Application.Properties.Resources.refresh;
            this.pbRefrescar.Location = new System.Drawing.Point(564, 339);
            this.pbRefrescar.Name = "pbRefrescar";
            this.pbRefrescar.Size = new System.Drawing.Size(23, 23);
            this.pbRefrescar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRefrescar.TabIndex = 47;
            this.pbRefrescar.TabStop = false;
            this.pbRefrescar.Click += new System.EventHandler(this.pbRefrescar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Label_currentrow});
            this.statusStrip1.Location = new System.Drawing.Point(0, 374);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(895, 22);
            this.statusStrip1.TabIndex = 49;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Label_currentrow
            // 
            this.Label_currentrow.Name = "Label_currentrow";
            this.Label_currentrow.Size = new System.Drawing.Size(91, 17);
            this.Label_currentrow.Text = "Registro actual :";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label_Titulo);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 60);
            this.panel1.TabIndex = 50;
            // 
            // label_Titulo
            // 
            this.label_Titulo.AutoSize = true;
            this.label_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Titulo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label_Titulo.Location = new System.Drawing.Point(67, 25);
            this.label_Titulo.Name = "label_Titulo";
            this.label_Titulo.Size = new System.Drawing.Size(110, 20);
            this.label_Titulo.TabIndex = 0;
            this.label_Titulo.Text = "Documentos";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.documentos1;
            this.pictureBox1.Location = new System.Drawing.Point(6, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // icono
            // 
            this.icono.DataPropertyName = "foto";
            this.icono.HeaderText = "";
            this.icono.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.icono.Name = "icono";
            this.icono.ReadOnly = true;
            this.icono.Visible = false;
            this.icono.Width = 20;
            // 
            // documento_id
            // 
            this.documento_id.DataPropertyName = "documento_id";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.documento_id.DefaultCellStyle = dataGridViewCellStyle3;
            this.documento_id.HeaderText = "Documento ID";
            this.documento_id.Name = "documento_id";
            this.documento_id.ReadOnly = true;
            this.documento_id.Width = 80;
            // 
            // descripcion
            // 
            this.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descripcion.DataPropertyName = "descripcion";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.descripcion.DefaultCellStyle = dataGridViewCellStyle4;
            this.descripcion.HeaderText = "Nombre Documento";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            // 
            // sigla_doc
            // 
            this.sigla_doc.DataPropertyName = "sigla_doc";
            this.sigla_doc.HeaderText = "Sigla";
            this.sigla_doc.Name = "sigla_doc";
            this.sigla_doc.ReadOnly = true;
            // 
            // Persona
            // 
            this.Persona.DataPropertyName = "persona_solicitud";
            this.Persona.HeaderText = "Persona Solicitud";
            this.Persona.Name = "Persona";
            this.Persona.ReadOnly = true;
            // 
            // activo
            // 
            this.activo.DataPropertyName = "activo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.activo.DefaultCellStyle = dataGridViewCellStyle5;
            this.activo.HeaderText = "Activo";
            this.activo.Name = "activo";
            this.activo.ReadOnly = true;
            this.activo.Width = 80;
            // 
            // s_cnf_documentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(895, 396);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_cerrar);
            this.Controls.Add(this.pbRefrescar);
            this.Controls.Add(this.button_eliminar);
            this.Controls.Add(this.button_modificar);
            this.Controls.Add(this.button_adicionar);
            this.Controls.Add(this.gvDocumentos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "s_cnf_documentos";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::: Documentos";
            this.Load += new System.EventHandler(this.s_cnf_documentos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefrescar)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.PictureBox pbRefrescar;
        private System.Windows.Forms.Button button_eliminar;
        private System.Windows.Forms.Button button_modificar;
        private System.Windows.Forms.Button button_adicionar;
        private System.Windows.Forms.DataGridView gvDocumentos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Label_currentrow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Titulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewImageColumn icono;
        private System.Windows.Forms.DataGridViewTextBoxColumn documento_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn sigla_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Persona;
        private System.Windows.Forms.DataGridViewTextBoxColumn activo;
    }
}