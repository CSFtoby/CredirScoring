namespace Docsis_Application
{
    partial class s_cnf_decisiones
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			this.button_cerrar = new System.Windows.Forms.Button();
			this.button_eliminar = new System.Windows.Forms.Button();
			this.button_modificar = new System.Windows.Forms.Button();
			this.button_adicionar = new System.Windows.Forms.Button();
			this.gvDecisiones = new System.Windows.Forms.DataGridView();
			this.icono = new System.Windows.Forms.DataGridViewImageColumn();
			this.decision_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estado_solicitud_id_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.desc_estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pbRefrescar = new System.Windows.Forms.PictureBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.Label_currentrow = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabConf = new System.Windows.Forms.TabControl();
			this.tpSolicitud = new System.Windows.Forms.TabPage();
			this.tpExcepcion = new System.Windows.Forms.TabPage();
			this.dgvDecExcep = new System.Windows.Forms.DataGridView();
			this.icono_exc = new System.Windows.Forms.DataGridViewImageColumn();
			this.decision_id_exc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.descripcion_exc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.activo_exc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estado_excep_id_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.desc_estado_exc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gvDecisiones)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbRefrescar)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.tabConf.SuspendLayout();
			this.tpSolicitud.SuspendLayout();
			this.tpExcepcion.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvDecExcep)).BeginInit();
			this.SuspendLayout();
			// 
			// button_cerrar
			// 
			this.button_cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_cerrar.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button_cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_cerrar.ForeColor = System.Drawing.Color.Navy;
			this.button_cerrar.Location = new System.Drawing.Point(308, 319);
			this.button_cerrar.Name = "button_cerrar";
			this.button_cerrar.Size = new System.Drawing.Size(75, 23);
			this.button_cerrar.TabIndex = 42;
			this.button_cerrar.Text = "Cerrar";
			this.button_cerrar.UseVisualStyleBackColor = false;
			this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
			// 
			// button_eliminar
			// 
			this.button_eliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_eliminar.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button_eliminar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button_eliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_eliminar.ForeColor = System.Drawing.Color.Navy;
			this.button_eliminar.Location = new System.Drawing.Point(168, 319);
			this.button_eliminar.Name = "button_eliminar";
			this.button_eliminar.Size = new System.Drawing.Size(75, 23);
			this.button_eliminar.TabIndex = 40;
			this.button_eliminar.Text = "Eliminar";
			this.button_eliminar.UseVisualStyleBackColor = false;
			this.button_eliminar.Click += new System.EventHandler(this.button_eliminar_Click);
			// 
			// button_modificar
			// 
			this.button_modificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_modificar.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button_modificar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button_modificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_modificar.ForeColor = System.Drawing.Color.Navy;
			this.button_modificar.Location = new System.Drawing.Point(87, 319);
			this.button_modificar.Name = "button_modificar";
			this.button_modificar.Size = new System.Drawing.Size(75, 23);
			this.button_modificar.TabIndex = 39;
			this.button_modificar.Text = "Modificar";
			this.button_modificar.UseVisualStyleBackColor = false;
			this.button_modificar.Click += new System.EventHandler(this.button_modificar_Click);
			// 
			// button_adicionar
			// 
			this.button_adicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button_adicionar.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button_adicionar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button_adicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_adicionar.ForeColor = System.Drawing.Color.Navy;
			this.button_adicionar.Location = new System.Drawing.Point(6, 319);
			this.button_adicionar.Name = "button_adicionar";
			this.button_adicionar.Size = new System.Drawing.Size(75, 23);
			this.button_adicionar.TabIndex = 38;
			this.button_adicionar.Text = "Adicionar";
			this.button_adicionar.UseVisualStyleBackColor = false;
			this.button_adicionar.Click += new System.EventHandler(this.button_adicionar_Click);
			// 
			// gvDecisiones
			// 
			this.gvDecisiones.AllowUserToAddRows = false;
			this.gvDecisiones.AllowUserToDeleteRows = false;
			this.gvDecisiones.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			this.gvDecisiones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.gvDecisiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gvDecisiones.BackgroundColor = System.Drawing.Color.White;
			this.gvDecisiones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gvDecisiones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.gvDecisiones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gvDecisiones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.gvDecisiones.ColumnHeadersHeight = 20;
			this.gvDecisiones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icono,
            this.decision_id,
            this.descripcion,
            this.activo,
            this.estado_solicitud_id_to,
            this.desc_estado});
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gvDecisiones.DefaultCellStyle = dataGridViewCellStyle8;
			this.gvDecisiones.GridColor = System.Drawing.Color.LightSteelBlue;
			this.gvDecisiones.Location = new System.Drawing.Point(0, 0);
			this.gvDecisiones.Name = "gvDecisiones";
			this.gvDecisiones.ReadOnly = true;
			this.gvDecisiones.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.gvDecisiones.RowHeadersVisible = false;
			this.gvDecisiones.RowHeadersWidth = 10;
			this.gvDecisiones.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.gvDecisiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gvDecisiones.Size = new System.Drawing.Size(751, 446);
			this.gvDecisiones.TabIndex = 37;
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
			// decision_id
			// 
			this.decision_id.DataPropertyName = "decision_id";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.decision_id.DefaultCellStyle = dataGridViewCellStyle3;
			this.decision_id.HeaderText = "Decision ID";
			this.decision_id.Name = "decision_id";
			this.decision_id.ReadOnly = true;
			this.decision_id.Width = 80;
			// 
			// descripcion
			// 
			this.descripcion.DataPropertyName = "descripcion";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.descripcion.DefaultCellStyle = dataGridViewCellStyle4;
			this.descripcion.HeaderText = "Nombre Decision";
			this.descripcion.Name = "descripcion";
			this.descripcion.ReadOnly = true;
			this.descripcion.Width = 280;
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
			// estado_solicitud_id_to
			// 
			this.estado_solicitud_id_to.DataPropertyName = "estado_solicitud_id_to";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.estado_solicitud_id_to.DefaultCellStyle = dataGridViewCellStyle6;
			this.estado_solicitud_id_to.HeaderText = "ID Estado";
			this.estado_solicitud_id_to.Name = "estado_solicitud_id_to";
			this.estado_solicitud_id_to.ReadOnly = true;
			// 
			// desc_estado
			// 
			this.desc_estado.DataPropertyName = "desc_estado";
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.desc_estado.DefaultCellStyle = dataGridViewCellStyle7;
			this.desc_estado.HeaderText = "Descripcion de Estado Destino";
			this.desc_estado.Name = "desc_estado";
			this.desc_estado.ReadOnly = true;
			this.desc_estado.Width = 200;
			// 
			// pbRefrescar
			// 
			this.pbRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pbRefrescar.BackColor = System.Drawing.Color.Transparent;
			this.pbRefrescar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbRefrescar.Image = global::Docsis_Application.Properties.Resources.refresh;
			this.pbRefrescar.Location = new System.Drawing.Point(722, 319);
			this.pbRefrescar.Name = "pbRefrescar";
			this.pbRefrescar.Size = new System.Drawing.Size(23, 23);
			this.pbRefrescar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbRefrescar.TabIndex = 41;
			this.pbRefrescar.TabStop = false;
			this.pbRefrescar.Click += new System.EventHandler(this.pbRefrescar_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Label_currentrow});
			this.statusStrip1.Location = new System.Drawing.Point(0, 362);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(751, 22);
			this.statusStrip1.TabIndex = 43;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// Label_currentrow
			// 
			this.Label_currentrow.Name = "Label_currentrow";
			this.Label_currentrow.Size = new System.Drawing.Size(91, 17);
			this.Label_currentrow.Text = "Registro actual :";
			// 
			// tabConf
			// 
			this.tabConf.Controls.Add(this.tpSolicitud);
			this.tabConf.Controls.Add(this.tpExcepcion);
			this.tabConf.Location = new System.Drawing.Point(0, 0);
			this.tabConf.Name = "tabConf";
			this.tabConf.SelectedIndex = 0;
			this.tabConf.Size = new System.Drawing.Size(751, 313);
			this.tabConf.TabIndex = 44;
			// 
			// tpSolicitud
			// 
			this.tpSolicitud.Controls.Add(this.gvDecisiones);
			this.tpSolicitud.Location = new System.Drawing.Point(4, 22);
			this.tpSolicitud.Name = "tpSolicitud";
			this.tpSolicitud.Padding = new System.Windows.Forms.Padding(3);
			this.tpSolicitud.Size = new System.Drawing.Size(743, 287);
			this.tpSolicitud.TabIndex = 0;
			this.tpSolicitud.Text = "Deciones Solicitud";
			this.tpSolicitud.UseVisualStyleBackColor = true;
			// 
			// tpExcepcion
			// 
			this.tpExcepcion.Controls.Add(this.dgvDecExcep);
			this.tpExcepcion.Location = new System.Drawing.Point(4, 22);
			this.tpExcepcion.Name = "tpExcepcion";
			this.tpExcepcion.Padding = new System.Windows.Forms.Padding(3);
			this.tpExcepcion.Size = new System.Drawing.Size(743, 287);
			this.tpExcepcion.TabIndex = 1;
			this.tpExcepcion.Text = "Decisiones Excepción";
			this.tpExcepcion.UseVisualStyleBackColor = true;
			// 
			// dgvDecExcep
			// 
			this.dgvDecExcep.AllowUserToAddRows = false;
			this.dgvDecExcep.AllowUserToDeleteRows = false;
			this.dgvDecExcep.AllowUserToResizeRows = false;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
			this.dgvDecExcep.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
			this.dgvDecExcep.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvDecExcep.BackgroundColor = System.Drawing.Color.White;
			this.dgvDecExcep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgvDecExcep.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvDecExcep.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvDecExcep.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.dgvDecExcep.ColumnHeadersHeight = 20;
			this.dgvDecExcep.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icono_exc,
            this.decision_id_exc,
            this.descripcion_exc,
            this.activo_exc,
            this.estado_excep_id_to,
            this.desc_estado_exc});
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvDecExcep.DefaultCellStyle = dataGridViewCellStyle16;
			this.dgvDecExcep.GridColor = System.Drawing.Color.LightSteelBlue;
			this.dgvDecExcep.Location = new System.Drawing.Point(0, 0);
			this.dgvDecExcep.Name = "dgvDecExcep";
			this.dgvDecExcep.ReadOnly = true;
			this.dgvDecExcep.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgvDecExcep.RowHeadersVisible = false;
			this.dgvDecExcep.RowHeadersWidth = 10;
			this.dgvDecExcep.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dgvDecExcep.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvDecExcep.Size = new System.Drawing.Size(751, 446);
			this.dgvDecExcep.TabIndex = 38;
			// 
			// icono_exc
			// 
			this.icono_exc.DataPropertyName = "foto";
			this.icono_exc.HeaderText = "";
			this.icono_exc.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
			this.icono_exc.Name = "icono_exc";
			this.icono_exc.ReadOnly = true;
			this.icono_exc.Visible = false;
			this.icono_exc.Width = 20;
			// 
			// decision_id_exc
			// 
			this.decision_id_exc.DataPropertyName = "decision_id";
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.decision_id_exc.DefaultCellStyle = dataGridViewCellStyle11;
			this.decision_id_exc.HeaderText = "Decision ID";
			this.decision_id_exc.Name = "decision_id_exc";
			this.decision_id_exc.ReadOnly = true;
			this.decision_id_exc.Width = 80;
			// 
			// descripcion_exc
			// 
			this.descripcion_exc.DataPropertyName = "descripcion";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.descripcion_exc.DefaultCellStyle = dataGridViewCellStyle12;
			this.descripcion_exc.HeaderText = "Nombre Decision";
			this.descripcion_exc.Name = "descripcion_exc";
			this.descripcion_exc.ReadOnly = true;
			this.descripcion_exc.Width = 280;
			// 
			// activo_exc
			// 
			this.activo_exc.DataPropertyName = "activo";
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.activo_exc.DefaultCellStyle = dataGridViewCellStyle13;
			this.activo_exc.HeaderText = "Activo";
			this.activo_exc.Name = "activo_exc";
			this.activo_exc.ReadOnly = true;
			this.activo_exc.Width = 80;
			// 
			// estado_excep_id_to
			// 
			this.estado_excep_id_to.DataPropertyName = "estado_solicitud_id_to";
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.estado_excep_id_to.DefaultCellStyle = dataGridViewCellStyle14;
			this.estado_excep_id_to.HeaderText = "ID Estado";
			this.estado_excep_id_to.Name = "estado_excep_id_to";
			this.estado_excep_id_to.ReadOnly = true;
			// 
			// desc_estado_exc
			// 
			this.desc_estado_exc.DataPropertyName = "desc_estado";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.desc_estado_exc.DefaultCellStyle = dataGridViewCellStyle15;
			this.desc_estado_exc.HeaderText = "Descripcion de Estado Destino";
			this.desc_estado_exc.Name = "desc_estado_exc";
			this.desc_estado_exc.ReadOnly = true;
			this.desc_estado_exc.Width = 200;
			// 
			// s_cnf_decisiones
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.fondo_formularios2;
			this.ClientSize = new System.Drawing.Size(751, 384);
			this.Controls.Add(this.tabConf);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.button_cerrar);
			this.Controls.Add(this.pbRefrescar);
			this.Controls.Add(this.button_eliminar);
			this.Controls.Add(this.button_modificar);
			this.Controls.Add(this.button_adicionar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "s_cnf_decisiones";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " ::: Decisiones ";
			this.Load += new System.EventHandler(this.s_decisiones_doc_Load);
			((System.ComponentModel.ISupportInitialize)(this.gvDecisiones)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbRefrescar)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabConf.ResumeLayout(false);
			this.tpSolicitud.ResumeLayout(false);
			this.tpExcepcion.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvDecExcep)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.PictureBox pbRefrescar;
        private System.Windows.Forms.Button button_eliminar;
        private System.Windows.Forms.Button button_modificar;
        private System.Windows.Forms.Button button_adicionar;
        private System.Windows.Forms.DataGridView gvDecisiones;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Label_currentrow;
        private System.Windows.Forms.DataGridViewImageColumn icono;
        private System.Windows.Forms.DataGridViewTextBoxColumn decision_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn activo;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado_solicitud_id_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc_estado;
		private System.Windows.Forms.TabControl tabConf;
		private System.Windows.Forms.TabPage tpSolicitud;
		private System.Windows.Forms.TabPage tpExcepcion;
		private System.Windows.Forms.DataGridView dgvDecExcep;
		private System.Windows.Forms.DataGridViewImageColumn icono_exc;
		private System.Windows.Forms.DataGridViewTextBoxColumn decision_id_exc;
		private System.Windows.Forms.DataGridViewTextBoxColumn descripcion_exc;
		private System.Windows.Forms.DataGridViewTextBoxColumn activo_exc;
		private System.Windows.Forms.DataGridViewTextBoxColumn estado_excep_id_to;
		private System.Windows.Forms.DataGridViewTextBoxColumn desc_estado_exc;
	}
}