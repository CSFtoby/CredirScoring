namespace Docsis_Application
{
    partial class s_analisis_cuantitativo02
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvProyeccion = new System.Windows.Forms.DataGridView();
            this.txtGuidID_generado = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotal_intereses = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.no_pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuota_simple = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capital = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo_capital = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvProyeccion)).BeginInit();
            this.SuspendLayout();
            // 
            // gvProyeccion
            // 
            this.gvProyeccion.AllowUserToAddRows = false;
            this.gvProyeccion.AllowUserToDeleteRows = false;
            this.gvProyeccion.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.gvProyeccion.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.gvProyeccion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvProyeccion.BackgroundColor = System.Drawing.Color.White;
            this.gvProyeccion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvProyeccion.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gvProyeccion.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProyeccion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.gvProyeccion.ColumnHeadersHeight = 20;
            this.gvProyeccion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no_pago,
            this.cuota_simple,
            this.capital,
            this.Interes,
            this.saldo_capital});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(200)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvProyeccion.DefaultCellStyle = dataGridViewCellStyle16;
            this.gvProyeccion.GridColor = System.Drawing.Color.LightSteelBlue;
            this.gvProyeccion.Location = new System.Drawing.Point(19, 34);
            this.gvProyeccion.Name = "gvProyeccion";
            this.gvProyeccion.ReadOnly = true;
            this.gvProyeccion.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvProyeccion.RowHeadersVisible = false;
            this.gvProyeccion.RowHeadersWidth = 10;
            this.gvProyeccion.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvProyeccion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProyeccion.Size = new System.Drawing.Size(534, 264);
            this.gvProyeccion.TabIndex = 272;
            // 
            // txtGuidID_generado
            // 
            this.txtGuidID_generado.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtGuidID_generado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGuidID_generado.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuidID_generado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.txtGuidID_generado.Location = new System.Drawing.Point(75, 9);
            this.txtGuidID_generado.MaxLength = 12;
            this.txtGuidID_generado.Name = "txtGuidID_generado";
            this.txtGuidID_generado.ReadOnly = true;
            this.txtGuidID_generado.Size = new System.Drawing.Size(223, 15);
            this.txtGuidID_generado.TabIndex = 273;
            this.txtGuidID_generado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.btnClose.Location = new System.Drawing.Point(549, -2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 274;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label8.Location = new System.Drawing.Point(233, 310);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 275;
            this.label8.Text = "Total Interéses";
            // 
            // txtTotal_intereses
            // 
            this.txtTotal_intereses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotal_intereses.BackColor = System.Drawing.Color.White;
            this.txtTotal_intereses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal_intereses.Enabled = false;
            this.txtTotal_intereses.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal_intereses.Location = new System.Drawing.Point(322, 306);
            this.txtTotal_intereses.Multiline = true;
            this.txtTotal_intereses.Name = "txtTotal_intereses";
            this.txtTotal_intereses.ReadOnly = true;
            this.txtTotal_intereses.Size = new System.Drawing.Size(114, 21);
            this.txtTotal_intereses.TabIndex = 276;
            this.txtTotal_intereses.Text = "0.00";
            this.txtTotal_intereses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label1.Location = new System.Drawing.Point(19, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 277;
            this.label1.Text = "GUID";
            // 
            // no_pago
            // 
            this.no_pago.DataPropertyName = "no_pago";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.no_pago.DefaultCellStyle = dataGridViewCellStyle11;
            this.no_pago.HeaderText = "No Pago";
            this.no_pago.Name = "no_pago";
            this.no_pago.ReadOnly = true;
            this.no_pago.Width = 50;
            // 
            // cuota_simple
            // 
            this.cuota_simple.DataPropertyName = "cuota_simple";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = "0";
            this.cuota_simple.DefaultCellStyle = dataGridViewCellStyle12;
            this.cuota_simple.HeaderText = "Cuota Nivelada";
            this.cuota_simple.Name = "cuota_simple";
            this.cuota_simple.ReadOnly = true;
            this.cuota_simple.Width = 120;
            // 
            // capital
            // 
            this.capital.DataPropertyName = "capital";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            dataGridViewCellStyle13.NullValue = "0";
            this.capital.DefaultCellStyle = dataGridViewCellStyle13;
            this.capital.HeaderText = "Principal";
            this.capital.Name = "capital";
            this.capital.ReadOnly = true;
            this.capital.Width = 120;
            // 
            // Interes
            // 
            this.Interes.DataPropertyName = "interes";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = "0";
            this.Interes.DefaultCellStyle = dataGridViewCellStyle14;
            this.Interes.HeaderText = "Interes";
            this.Interes.Name = "Interes";
            this.Interes.ReadOnly = true;
            this.Interes.Width = 120;
            // 
            // saldo_capital
            // 
            this.saldo_capital.DataPropertyName = "saldo_capital";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N2";
            dataGridViewCellStyle15.NullValue = "0";
            this.saldo_capital.DefaultCellStyle = dataGridViewCellStyle15;
            this.saldo_capital.HeaderText = "Saldo Capital";
            this.saldo_capital.Name = "saldo_capital";
            this.saldo_capital.ReadOnly = true;
            this.saldo_capital.Width = 120;
            // 
            // s_analisis_cuantitativo02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(565, 342);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtTotal_intereses);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtGuidID_generado);
            this.Controls.Add(this.gvProyeccion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_analisis_cuantitativo02";
            this.Text = "s_analisis_cuantitativo02";
            this.Load += new System.EventHandler(this.s_analisis_cuantitativo02_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.s_analisis_cuantitativo02_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.gvProyeccion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvProyeccion;
        public System.Windows.Forms.TextBox txtGuidID_generado;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtTotal_intereses;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn no_pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuota_simple;
        private System.Windows.Forms.DataGridViewTextBoxColumn capital;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interes;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldo_capital;
    }
}