namespace Docsis_Application
{
    partial class s_buscarusuarios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button9 = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTexto_busqueda_todos = new System.Windows.Forms.TextBox();
            this.gv_usuarios = new System.Windows.Forms.DataGridView();
            this.img_estado_registro = new System.Windows.Forms.DataGridViewImageColumn();
            this.codigo_usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_agencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_usuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.label15);
            this.panelTop.Controls.Add(this.pictureBox3);
            this.panelTop.Controls.Add(this.button9);
            this.panelTop.Controls.Add(this.btnSalir);
            this.panelTop.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panelTop.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelTop.Location = new System.Drawing.Point(0, -2);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(868, 35);
            this.panelTop.TabIndex = 92;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(35, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(132, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = " Busqueda de Usuarios ::::";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Docsis_Application.Properties.Resources.icon_usuario;
            this.pictureBox3.Location = new System.Drawing.Point(4, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(25, 24);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 116;
            this.pictureBox3.TabStop = false;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(96)))), ((int)(((byte)(66)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = global::Docsis_Application.Properties.Resources.icon_close01;
            this.button9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button9.Location = new System.Drawing.Point(843, 10);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(15, 16);
            this.button9.TabIndex = 5;
            this.button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
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
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.buscar20x20;
            this.pictureBox1.Location = new System.Drawing.Point(828, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 23);
            this.pictureBox1.TabIndex = 353;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(7, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 15);
            this.label1.TabIndex = 352;
            this.label1.Text = "Buscar en todos los empleados :";
            // 
            // txtTexto_busqueda_todos
            // 
            this.txtTexto_busqueda_todos.Location = new System.Drawing.Point(204, 37);
            this.txtTexto_busqueda_todos.Name = "txtTexto_busqueda_todos";
            this.txtTexto_busqueda_todos.Size = new System.Drawing.Size(618, 20);
            this.txtTexto_busqueda_todos.TabIndex = 351;
            this.txtTexto_busqueda_todos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTexto_busqueda_todos_KeyDown);
            // 
            // gv_usuarios
            // 
            this.gv_usuarios.AllowUserToAddRows = false;
            this.gv_usuarios.AllowUserToDeleteRows = false;
            this.gv_usuarios.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.gv_usuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gv_usuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_usuarios.BackgroundColor = System.Drawing.Color.White;
            this.gv_usuarios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gv_usuarios.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gv_usuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv_usuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gv_usuarios.ColumnHeadersHeight = 20;
            this.gv_usuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.img_estado_registro,
            this.codigo_usuario,
            this.codigo_cliente,
            this.nombres,
            this.Estacion,
            this.nombre_agencia});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gv_usuarios.DefaultCellStyle = dataGridViewCellStyle5;
            this.gv_usuarios.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gv_usuarios.Location = new System.Drawing.Point(5, 62);
            this.gv_usuarios.MultiSelect = false;
            this.gv_usuarios.Name = "gv_usuarios";
            this.gv_usuarios.ReadOnly = true;
            this.gv_usuarios.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gv_usuarios.RowHeadersVisible = false;
            this.gv_usuarios.RowHeadersWidth = 10;
            this.gv_usuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_usuarios.Size = new System.Drawing.Size(853, 269);
            this.gv_usuarios.TabIndex = 350;
            this.gv_usuarios.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gv_usuarios_MouseDoubleClick);
            // 
            // img_estado_registro
            // 
            this.img_estado_registro.DataPropertyName = "img_estado_registro";
            this.img_estado_registro.HeaderText = "";
            this.img_estado_registro.Name = "img_estado_registro";
            this.img_estado_registro.ReadOnly = true;
            this.img_estado_registro.Visible = false;
            this.img_estado_registro.Width = 15;
            // 
            // codigo_usuario
            // 
            this.codigo_usuario.DataPropertyName = "codigo_usuario";
            this.codigo_usuario.HeaderText = "Usuario";
            this.codigo_usuario.Name = "codigo_usuario";
            this.codigo_usuario.ReadOnly = true;
            // 
            // codigo_cliente
            // 
            this.codigo_cliente.DataPropertyName = "codigo_cliente";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = null;
            this.codigo_cliente.DefaultCellStyle = dataGridViewCellStyle3;
            this.codigo_cliente.HeaderText = "Codigo Cliente";
            this.codigo_cliente.Name = "codigo_cliente";
            this.codigo_cliente.ReadOnly = true;
            this.codigo_cliente.Width = 80;
            // 
            // nombres
            // 
            this.nombres.DataPropertyName = "nombres";
            this.nombres.HeaderText = "Nombre Empleado";
            this.nombres.Name = "nombres";
            this.nombres.ReadOnly = true;
            this.nombres.Width = 220;
            // 
            // Estacion
            // 
            this.Estacion.DataPropertyName = "estacion_actual";
            this.Estacion.HeaderText = "Estacion Actual";
            this.Estacion.Name = "Estacion";
            this.Estacion.ReadOnly = true;
            this.Estacion.Width = 200;
            // 
            // nombre_agencia
            // 
            this.nombre_agencia.DataPropertyName = "nombre_agencia";
            dataGridViewCellStyle4.Format = "f";
            dataGridViewCellStyle4.NullValue = null;
            this.nombre_agencia.DefaultCellStyle = dataGridViewCellStyle4;
            this.nombre_agencia.HeaderText = "Filial Actual";
            this.nombre_agencia.Name = "nombre_agencia";
            this.nombre_agencia.ReadOnly = true;
            this.nombre_agencia.Width = 250;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Location = new System.Drawing.Point(0, 370);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(866, 22);
            this.statusStrip1.TabIndex = 354;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnSeleccionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeleccionar.FlatAppearance.BorderSize = 0;
            this.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnSeleccionar.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeleccionar.Location = new System.Drawing.Point(736, 335);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(118, 30);
            this.btnSeleccionar.TabIndex = 359;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = false;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // s_buscarusuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(866, 392);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTexto_busqueda_todos);
            this.Controls.Add(this.gv_usuarios);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_buscarusuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_buscarusuarios";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_usuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTexto_busqueda_todos;
        private System.Windows.Forms.DataGridView gv_usuarios;
        private System.Windows.Forms.DataGridViewImageColumn img_estado_registro;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombres;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_agencia;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnSeleccionar;
    }
}