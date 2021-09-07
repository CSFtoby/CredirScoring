namespace Docsis_Application
{
	partial class s_preanalisis
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
			this.btnObtener_cuotas_buro = new System.Windows.Forms.Button();
			this.gvCuotasBuro = new System.Windows.Forms.DataGridView();
			this.seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.NombreInstitucion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.monto_cuota = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label106 = new System.Windows.Forms.Label();
			this.panelWait_cuotasconsolo = new System.Windows.Forms.Panel();
			this.labelTimer = new System.Windows.Forms.Label();
			this.pictureBox11 = new System.Windows.Forms.PictureBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtIDSolicitante = new System.Windows.Forms.TextBox();
			this.label175 = new System.Windows.Forms.Label();
			this.label101 = new System.Windows.Forms.Label();
			this.label138 = new System.Windows.Forms.Label();
			this.txtTotal_cuotas_consolidar = new System.Windows.Forms.TextBox();
			this.label139 = new System.Windows.Forms.Label();
			this.txtTotal_capital_consolidar = new System.Windows.Forms.TextBox();
			this.cmbPagoMediante = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gvCuotasBuro)).BeginInit();
			this.panelWait_cuotasconsolo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
			this.SuspendLayout();
			// 
			// btnObtener_cuotas_buro
			// 
			this.btnObtener_cuotas_buro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnObtener_cuotas_buro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnObtener_cuotas_buro.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnObtener_cuotas_buro.FlatAppearance.BorderSize = 0;
			this.btnObtener_cuotas_buro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnObtener_cuotas_buro.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.btnObtener_cuotas_buro.ForeColor = System.Drawing.Color.White;
			this.btnObtener_cuotas_buro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnObtener_cuotas_buro.Location = new System.Drawing.Point(591, 91);
			this.btnObtener_cuotas_buro.Name = "btnObtener_cuotas_buro";
			this.btnObtener_cuotas_buro.Size = new System.Drawing.Size(165, 22);
			this.btnObtener_cuotas_buro.TabIndex = 32;
			this.btnObtener_cuotas_buro.Text = "Obtener cuotas del buro";
			this.btnObtener_cuotas_buro.UseVisualStyleBackColor = false;
			this.btnObtener_cuotas_buro.Click += new System.EventHandler(this.btnObtener_cuotas_buro_Click);
			// 
			// gvCuotasBuro
			// 
			this.gvCuotasBuro.AllowUserToAddRows = false;
			this.gvCuotasBuro.AllowUserToDeleteRows = false;
			this.gvCuotasBuro.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			this.gvCuotasBuro.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.gvCuotasBuro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gvCuotasBuro.BackgroundColor = System.Drawing.Color.White;
			this.gvCuotasBuro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gvCuotasBuro.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.gvCuotasBuro.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gvCuotasBuro.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.gvCuotasBuro.ColumnHeadersHeight = 20;
			this.gvCuotasBuro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seleccion,
            this.NombreInstitucion,
            this.monto_cuota,
            this.saldo});
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gvCuotasBuro.DefaultCellStyle = dataGridViewCellStyle6;
			this.gvCuotasBuro.GridColor = System.Drawing.Color.LightSteelBlue;
			this.gvCuotasBuro.Location = new System.Drawing.Point(12, 119);
			this.gvCuotasBuro.Name = "gvCuotasBuro";
			this.gvCuotasBuro.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.gvCuotasBuro.RowHeadersVisible = false;
			this.gvCuotasBuro.RowHeadersWidth = 10;
			this.gvCuotasBuro.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.gvCuotasBuro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gvCuotasBuro.Size = new System.Drawing.Size(744, 203);
			this.gvCuotasBuro.TabIndex = 33;
			this.gvCuotasBuro.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCuotasBuro_CellContentClick);
			this.gvCuotasBuro.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCuotasBuro_CellValueChanged);
			// 
			// seleccion
			// 
			this.seleccion.DataPropertyName = "seleccion";
			this.seleccion.FalseValue = "false";
			this.seleccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.seleccion.HeaderText = "";
			this.seleccion.IndeterminateValue = "0";
			this.seleccion.Name = "seleccion";
			this.seleccion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.seleccion.TrueValue = "true";
			this.seleccion.Width = 20;
			// 
			// NombreInstitucion
			// 
			this.NombreInstitucion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.NombreInstitucion.DataPropertyName = "institucion";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			this.NombreInstitucion.DefaultCellStyle = dataGridViewCellStyle3;
			this.NombreInstitucion.HeaderText = "Institución";
			this.NombreInstitucion.Name = "NombreInstitucion";
			this.NombreInstitucion.ReadOnly = true;
			// 
			// monto_cuota
			// 
			this.monto_cuota.DataPropertyName = "cuota";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle4.Format = "N2";
			dataGridViewCellStyle4.NullValue = "0";
			this.monto_cuota.DefaultCellStyle = dataGridViewCellStyle4;
			this.monto_cuota.HeaderText = "Cuota";
			this.monto_cuota.Name = "monto_cuota";
			this.monto_cuota.ReadOnly = true;
			this.monto_cuota.Width = 120;
			// 
			// saldo
			// 
			this.saldo.DataPropertyName = "saldo";
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle5.Format = "N2";
			this.saldo.DefaultCellStyle = dataGridViewCellStyle5;
			this.saldo.HeaderText = "Saldo";
			this.saldo.Name = "saldo";
			this.saldo.ReadOnly = true;
			this.saldo.Width = 120;
			// 
			// label106
			// 
			this.label106.AutoSize = true;
			this.label106.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label106.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label106.Location = new System.Drawing.Point(41, 6);
			this.label106.Name = "label106";
			this.label106.Size = new System.Drawing.Size(184, 16);
			this.label106.TabIndex = 0;
			this.label106.Text = "Obteniendo cuotas del buro";
			// 
			// panelWait_cuotasconsolo
			// 
			this.panelWait_cuotasconsolo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.panelWait_cuotasconsolo.BackColor = System.Drawing.Color.Transparent;
			this.panelWait_cuotasconsolo.Controls.Add(this.labelTimer);
			this.panelWait_cuotasconsolo.Controls.Add(this.pictureBox11);
			this.panelWait_cuotasconsolo.Controls.Add(this.label106);
			this.panelWait_cuotasconsolo.Location = new System.Drawing.Point(265, 196);
			this.panelWait_cuotasconsolo.Name = "panelWait_cuotasconsolo";
			this.panelWait_cuotasconsolo.Size = new System.Drawing.Size(246, 44);
			this.panelWait_cuotasconsolo.TabIndex = 30;
			this.panelWait_cuotasconsolo.Visible = false;
			// 
			// labelTimer
			// 
			this.labelTimer.AutoSize = true;
			this.labelTimer.Location = new System.Drawing.Point(43, 24);
			this.labelTimer.Name = "labelTimer";
			this.labelTimer.Size = new System.Drawing.Size(64, 13);
			this.labelTimer.TabIndex = 1;
			this.labelTimer.Text = "0 Segundos";
			// 
			// pictureBox11
			// 
			this.pictureBox11.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox11.Image = global::Docsis_Application.Properties.Resources._3011;
			this.pictureBox11.Location = new System.Drawing.Point(3, 4);
			this.pictureBox11.Name = "pictureBox11";
			this.pictureBox11.Size = new System.Drawing.Size(35, 36);
			this.pictureBox11.TabIndex = 3;
			this.pictureBox11.TabStop = false;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.BackColor = System.Drawing.Color.Transparent;
			this.label11.Font = new System.Drawing.Font("Segoe UI Light", 12F);
			this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.label11.Location = new System.Drawing.Point(12, 92);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(272, 21);
			this.label11.TabIndex = 31;
			this.label11.Text = "Detalle de Cuotas encontradas en Buro";
			// 
			// txtIDSolicitante
			// 
			this.txtIDSolicitante.BackColor = System.Drawing.Color.White;
			this.txtIDSolicitante.Location = new System.Drawing.Point(123, 41);
			this.txtIDSolicitante.MaxLength = 25;
			this.txtIDSolicitante.Name = "txtIDSolicitante";
			this.txtIDSolicitante.Size = new System.Drawing.Size(187, 20);
			this.txtIDSolicitante.TabIndex = 37;
			// 
			// label175
			// 
			this.label175.AutoSize = true;
			this.label175.BackColor = System.Drawing.Color.Transparent;
			this.label175.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label175.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.label175.Location = new System.Drawing.Point(14, 46);
			this.label175.Name = "label175";
			this.label175.Size = new System.Drawing.Size(96, 13);
			this.label175.TabIndex = 36;
			this.label175.Text = "No. Identificación :";
			// 
			// label101
			// 
			this.label101.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label101.AutoSize = true;
			this.label101.BackColor = System.Drawing.Color.Transparent;
			this.label101.Font = new System.Drawing.Font("Segoe UI", 14F);
			this.label101.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.label101.Location = new System.Drawing.Point(12, 9);
			this.label101.Name = "label101";
			this.label101.Size = new System.Drawing.Size(126, 25);
			this.label101.TabIndex = 38;
			this.label101.Text = "PRE ANALISIS";
			// 
			// label138
			// 
			this.label138.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label138.AutoSize = true;
			this.label138.BackColor = System.Drawing.Color.Transparent;
			this.label138.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label138.ForeColor = System.Drawing.Color.Black;
			this.label138.Location = new System.Drawing.Point(503, 333);
			this.label138.Name = "label138";
			this.label138.Size = new System.Drawing.Size(92, 13);
			this.label138.TabIndex = 39;
			this.label138.Text = "Cuota consolidar :";
			// 
			// txtTotal_cuotas_consolidar
			// 
			this.txtTotal_cuotas_consolidar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTotal_cuotas_consolidar.BackColor = System.Drawing.Color.White;
			this.txtTotal_cuotas_consolidar.Location = new System.Drawing.Point(606, 328);
			this.txtTotal_cuotas_consolidar.Multiline = true;
			this.txtTotal_cuotas_consolidar.Name = "txtTotal_cuotas_consolidar";
			this.txtTotal_cuotas_consolidar.ReadOnly = true;
			this.txtTotal_cuotas_consolidar.Size = new System.Drawing.Size(136, 23);
			this.txtTotal_cuotas_consolidar.TabIndex = 40;
			this.txtTotal_cuotas_consolidar.Text = "0.00";
			this.txtTotal_cuotas_consolidar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label139
			// 
			this.label139.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label139.AutoSize = true;
			this.label139.BackColor = System.Drawing.Color.Transparent;
			this.label139.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label139.ForeColor = System.Drawing.Color.Black;
			this.label139.Location = new System.Drawing.Point(503, 362);
			this.label139.Name = "label139";
			this.label139.Size = new System.Drawing.Size(103, 13);
			this.label139.TabIndex = 41;
			this.label139.Text = "Monto a consolidar :";
			// 
			// txtTotal_capital_consolidar
			// 
			this.txtTotal_capital_consolidar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTotal_capital_consolidar.BackColor = System.Drawing.Color.White;
			this.txtTotal_capital_consolidar.Location = new System.Drawing.Point(606, 357);
			this.txtTotal_capital_consolidar.Multiline = true;
			this.txtTotal_capital_consolidar.Name = "txtTotal_capital_consolidar";
			this.txtTotal_capital_consolidar.ReadOnly = true;
			this.txtTotal_capital_consolidar.Size = new System.Drawing.Size(136, 23);
			this.txtTotal_capital_consolidar.TabIndex = 42;
			this.txtTotal_capital_consolidar.Text = "0.00";
			this.txtTotal_capital_consolidar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cmbPagoMediante
			// 
			this.cmbPagoMediante.AutoCompleteCustomSource.AddRange(new string[] {
            "Pago por Ventanilla",
            "Deducción por Planilla"});
			this.cmbPagoMediante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPagoMediante.FormattingEnabled = true;
			this.cmbPagoMediante.Items.AddRange(new object[] {
            "Ventanilla",
            "Planilla"});
			this.cmbPagoMediante.Location = new System.Drawing.Point(431, 37);
			this.cmbPagoMediante.Name = "cmbPagoMediante";
			this.cmbPagoMediante.Size = new System.Drawing.Size(151, 21);
			this.cmbPagoMediante.TabIndex = 43;
			this.cmbPagoMediante.SelectionChangeCommitted += new System.EventHandler(this.cmbPagoMediante_SelectionChangeCommitted);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.label1.Location = new System.Drawing.Point(329, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 13);
			this.label1.TabIndex = 44;
			this.label1.Text = "Pago Mediante :";
			// 
			// s_preanalisis
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login3;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(768, 387);
			this.Controls.Add(this.panelWait_cuotasconsolo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbPagoMediante);
			this.Controls.Add(this.label139);
			this.Controls.Add(this.txtTotal_capital_consolidar);
			this.Controls.Add(this.label138);
			this.Controls.Add(this.txtTotal_cuotas_consolidar);
			this.Controls.Add(this.label101);
			this.Controls.Add(this.txtIDSolicitante);
			this.Controls.Add(this.label175);
			this.Controls.Add(this.btnObtener_cuotas_buro);
			this.Controls.Add(this.gvCuotasBuro);
			this.Controls.Add(this.label11);
			this.Name = "s_preanalisis";
			((System.ComponentModel.ISupportInitialize)(this.gvCuotasBuro)).EndInit();
			this.panelWait_cuotasconsolo.ResumeLayout(false);
			this.panelWait_cuotasconsolo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnObtener_cuotas_buro;
		private System.Windows.Forms.DataGridView gvCuotasBuro;
		private System.Windows.Forms.DataGridViewCheckBoxColumn seleccion;
		private System.Windows.Forms.DataGridViewTextBoxColumn NombreInstitucion;
		private System.Windows.Forms.DataGridViewTextBoxColumn monto_cuota;
		private System.Windows.Forms.DataGridViewTextBoxColumn saldo;
		private System.Windows.Forms.Label label106;
		private System.Windows.Forms.Panel panelWait_cuotasconsolo;
		private System.Windows.Forms.Label labelTimer;
		private System.Windows.Forms.PictureBox pictureBox11;
		private System.Windows.Forms.Label label11;
		public System.Windows.Forms.TextBox txtIDSolicitante;
		private System.Windows.Forms.Label label175;
		private System.Windows.Forms.Label label101;
		private System.Windows.Forms.Label label138;
		private System.Windows.Forms.TextBox txtTotal_cuotas_consolidar;
		private System.Windows.Forms.Label label139;
		private System.Windows.Forms.TextBox txtTotal_capital_consolidar;
		private System.Windows.Forms.ComboBox cmbPagoMediante;
		private System.Windows.Forms.Label label1;
	}
}