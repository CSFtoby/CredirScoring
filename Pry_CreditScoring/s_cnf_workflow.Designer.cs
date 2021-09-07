namespace Docsis_Application
{
    partial class s_cnf_workflow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvWorkflows = new System.Windows.Forms.DataGridView();
            this.button_adicionar = new System.Windows.Forms.Button();
            this.button_modificar = new System.Windows.Forms.Button();
            this.button_eliminar = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Label_currentrow = new System.Windows.Forms.ToolStripStatusLabel();
            this.button_cerrar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.icono = new System.Windows.Forms.DataGridViewImageColumn();
            this.workflow_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_workflows = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_sub_aplicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc_sub_aplicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otros_fondos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkflows)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvWorkflows
            // 
            this.gvWorkflows.AllowUserToAddRows = false;
            this.gvWorkflows.AllowUserToDeleteRows = false;
            this.gvWorkflows.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gvWorkflows.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvWorkflows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvWorkflows.BackgroundColor = System.Drawing.Color.White;
            this.gvWorkflows.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvWorkflows.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvWorkflows.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvWorkflows.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvWorkflows.ColumnHeadersHeight = 20;
            this.gvWorkflows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icono,
            this.workflow_id,
            this.nombre_workflows,
            this.codigo_sub_aplicacion,
            this.desc_sub_aplicacion,
            this.otros_fondos,
            this.activo});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvWorkflows.DefaultCellStyle = dataGridViewCellStyle7;
            this.gvWorkflows.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvWorkflows.Location = new System.Drawing.Point(1, 1);
            this.gvWorkflows.Name = "gvWorkflows";
            this.gvWorkflows.ReadOnly = true;
            this.gvWorkflows.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvWorkflows.RowHeadersVisible = false;
            this.gvWorkflows.RowHeadersWidth = 10;
            this.gvWorkflows.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvWorkflows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvWorkflows.Size = new System.Drawing.Size(724, 288);
            this.gvWorkflows.TabIndex = 24;
            this.gvWorkflows.SelectionChanged += new System.EventHandler(this.gvWorkflows_SelectionChanged);
            // 
            // button_adicionar
            // 
            this.button_adicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_adicionar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button_adicionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_adicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_adicionar.ForeColor = System.Drawing.Color.Navy;
            this.button_adicionar.Location = new System.Drawing.Point(6, 309);
            this.button_adicionar.Name = "button_adicionar";
            this.button_adicionar.Size = new System.Drawing.Size(75, 23);
            this.button_adicionar.TabIndex = 25;
            this.button_adicionar.Text = "Adicionar";
            this.button_adicionar.UseVisualStyleBackColor = false;
            this.button_adicionar.Click += new System.EventHandler(this.button_adicionar_Click);
            // 
            // button_modificar
            // 
            this.button_modificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_modificar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button_modificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_modificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_modificar.ForeColor = System.Drawing.Color.Navy;
            this.button_modificar.Location = new System.Drawing.Point(87, 309);
            this.button_modificar.Name = "button_modificar";
            this.button_modificar.Size = new System.Drawing.Size(75, 23);
            this.button_modificar.TabIndex = 26;
            this.button_modificar.Text = "Modificar";
            this.button_modificar.UseVisualStyleBackColor = false;
            this.button_modificar.Click += new System.EventHandler(this.button_modificar_Click);
            // 
            // button_eliminar
            // 
            this.button_eliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_eliminar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button_eliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_eliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_eliminar.ForeColor = System.Drawing.Color.Navy;
            this.button_eliminar.Location = new System.Drawing.Point(168, 309);
            this.button_eliminar.Name = "button_eliminar";
            this.button_eliminar.Size = new System.Drawing.Size(75, 23);
            this.button_eliminar.TabIndex = 27;
            this.button_eliminar.Text = "Eliminar";
            this.button_eliminar.UseVisualStyleBackColor = false;
            this.button_eliminar.Click += new System.EventHandler(this.button_eliminar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Label_currentrow});
            this.statusStrip1.Location = new System.Drawing.Point(0, 345);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(725, 22);
            this.statusStrip1.TabIndex = 28;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Label_currentrow
            // 
            this.Label_currentrow.Name = "Label_currentrow";
            this.Label_currentrow.Size = new System.Drawing.Size(86, 17);
            this.Label_currentrow.Text = "Registro actual :";
            // 
            // button_cerrar
            // 
            this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_cerrar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button_cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cerrar.ForeColor = System.Drawing.Color.Navy;
            this.button_cerrar.Location = new System.Drawing.Point(308, 309);
            this.button_cerrar.Name = "button_cerrar";
            this.button_cerrar.Size = new System.Drawing.Size(75, 23);
            this.button_cerrar.TabIndex = 30;
            this.button_cerrar.Text = "Cerrar";
            this.button_cerrar.UseVisualStyleBackColor = false;
            this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.refresh;
            this.pictureBox1.Location = new System.Drawing.Point(698, 309);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
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
            // workflow_id
            // 
            this.workflow_id.DataPropertyName = "workflow_id";
            this.workflow_id.HeaderText = "WorkFlow ID";
            this.workflow_id.Name = "workflow_id";
            this.workflow_id.ReadOnly = true;
            this.workflow_id.Width = 80;
            // 
            // nombre_workflows
            // 
            this.nombre_workflows.DataPropertyName = "nombre_workflows";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nombre_workflows.DefaultCellStyle = dataGridViewCellStyle3;
            this.nombre_workflows.HeaderText = "Nombre WorkFlow";
            this.nombre_workflows.Name = "nombre_workflows";
            this.nombre_workflows.ReadOnly = true;
            this.nombre_workflows.Width = 260;
            // 
            // codigo_sub_aplicacion
            // 
            this.codigo_sub_aplicacion.DataPropertyName = "codigo_sub_aplicacion";
            this.codigo_sub_aplicacion.HeaderText = "";
            this.codigo_sub_aplicacion.Name = "codigo_sub_aplicacion";
            this.codigo_sub_aplicacion.ReadOnly = true;
            this.codigo_sub_aplicacion.Visible = false;
            // 
            // desc_sub_aplicacion
            // 
            this.desc_sub_aplicacion.DataPropertyName = "desc_sub_aplicacion";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.desc_sub_aplicacion.DefaultCellStyle = dataGridViewCellStyle4;
            this.desc_sub_aplicacion.HeaderText = "Producto";
            this.desc_sub_aplicacion.Name = "desc_sub_aplicacion";
            this.desc_sub_aplicacion.ReadOnly = true;
            this.desc_sub_aplicacion.Width = 220;
            // 
            // otros_fondos
            // 
            this.otros_fondos.DataPropertyName = "otros_fondos";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.otros_fondos.DefaultCellStyle = dataGridViewCellStyle5;
            this.otros_fondos.HeaderText = "Otros Fondos";
            this.otros_fondos.Name = "otros_fondos";
            this.otros_fondos.ReadOnly = true;
            this.otros_fondos.Width = 80;
            // 
            // activo
            // 
            this.activo.DataPropertyName = "activo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.activo.DefaultCellStyle = dataGridViewCellStyle6;
            this.activo.HeaderText = "Activo";
            this.activo.Name = "activo";
            this.activo.ReadOnly = true;
            this.activo.Width = 80;
            // 
            // s_cnf_workflow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.fondo_formularios2;
            this.ClientSize = new System.Drawing.Size(725, 367);
            this.Controls.Add(this.button_cerrar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_eliminar);
            this.Controls.Add(this.button_modificar);
            this.Controls.Add(this.button_adicionar);
            this.Controls.Add(this.gvWorkflows);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "s_cnf_workflow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ::: Workflows";
            this.Load += new System.EventHandler(this.s_workflow_doc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkflows)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvWorkflows;
        private System.Windows.Forms.Button button_adicionar;
        private System.Windows.Forms.Button button_modificar;
        private System.Windows.Forms.Button button_eliminar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Label_currentrow;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.DataGridViewImageColumn icono;
        private System.Windows.Forms.DataGridViewTextBoxColumn workflow_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_workflows;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_sub_aplicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc_sub_aplicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn otros_fondos;
        private System.Windows.Forms.DataGridViewTextBoxColumn activo;
    }
}