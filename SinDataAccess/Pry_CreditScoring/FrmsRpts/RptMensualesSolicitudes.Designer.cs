namespace Docsis_Application.FrmsRpts
{
	partial class RptMensualesSolicitudes
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelTop = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label249 = new System.Windows.Forms.Label();
			this.dpFecha2 = new System.Windows.Forms.DateTimePicker();
			this.dpFecha1 = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.rbnNucleo = new System.Windows.Forms.RadioButton();
			this.rbnComite = new System.Windows.Forms.RadioButton();
			this.rbnAprobadas = new System.Windows.Forms.RadioButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panelWait = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox10 = new System.Windows.Forms.PictureBox();
			this.label105 = new System.Windows.Forms.Label();
			this.btnExportar = new System.Windows.Forms.Button();
			this.gvReporte1 = new System.Windows.Forms.DataGridView();
			this.panelTop.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panelWait.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvReporte1)).BeginInit();
			this.SuspendLayout();
			// 
			// panelTop
			// 
			this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelTop.Controls.Add(this.btnClose);
			this.panelTop.Controls.Add(this.label4);
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(666, 34);
			this.panelTop.TabIndex = 109;
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
			this.btnClose.Location = new System.Drawing.Point(631, 4);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(15, 16);
			this.btnClose.TabIndex = 116;
			this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(9, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(165, 15);
			this.label4.TabIndex = 108;
			this.label4.Text = "Reportes Mesuales Solicitudes";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.groupBox5);
			this.groupBox1.Location = new System.Drawing.Point(12, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(641, 143);
			this.groupBox1.TabIndex = 349;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "  Filtros  ";
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
			// groupBox5
			// 
			this.groupBox5.BackColor = System.Drawing.Color.WhiteSmoke;
			this.groupBox5.Controls.Add(this.rbnNucleo);
			this.groupBox5.Controls.Add(this.rbnComite);
			this.groupBox5.Controls.Add(this.rbnAprobadas);
			this.groupBox5.Location = new System.Drawing.Point(282, 19);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(305, 97);
			this.groupBox5.TabIndex = 350;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Tipo de Reporte";
			// 
			// rbnNucleo
			// 
			this.rbnNucleo.AutoSize = true;
			this.rbnNucleo.Location = new System.Drawing.Point(15, 68);
			this.rbnNucleo.Name = "rbnNucleo";
			this.rbnNucleo.Size = new System.Drawing.Size(138, 17);
			this.rbnNucleo.TabIndex = 101;
			this.rbnNucleo.Text = "Reporte Núcleo Familiar";
			this.rbnNucleo.UseVisualStyleBackColor = true;
			// 
			// rbnComite
			// 
			this.rbnComite.AutoSize = true;
			this.rbnComite.Location = new System.Drawing.Point(15, 45);
			this.rbnComite.Name = "rbnComite";
			this.rbnComite.Size = new System.Drawing.Size(160, 17);
			this.rbnComite.TabIndex = 100;
			this.rbnComite.Text = "Reporte Aprobación Comités";
			this.rbnComite.UseVisualStyleBackColor = true;
			// 
			// rbnAprobadas
			// 
			this.rbnAprobadas.AutoSize = true;
			this.rbnAprobadas.Checked = true;
			this.rbnAprobadas.Location = new System.Drawing.Point(15, 23);
			this.rbnAprobadas.Name = "rbnAprobadas";
			this.rbnAprobadas.Size = new System.Drawing.Size(182, 17);
			this.rbnAprobadas.TabIndex = 99;
			this.rbnAprobadas.TabStop = true;
			this.rbnAprobadas.Text = "Reporte Aprobadas y en Proceso";
			this.rbnAprobadas.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.panel2.Controls.Add(this.panelWait);
			this.panel2.Controls.Add(this.btnExportar);
			this.panel2.Controls.Add(this.gvReporte1);
			this.panel2.Location = new System.Drawing.Point(12, 189);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(642, 62);
			this.panel2.TabIndex = 351;
			// 
			// panelWait
			// 
			this.panelWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.panelWait.BackColor = System.Drawing.Color.Transparent;
			this.panelWait.Controls.Add(this.label3);
			this.panelWait.Controls.Add(this.pictureBox10);
			this.panelWait.Controls.Add(this.label105);
			this.panelWait.Location = new System.Drawing.Point(22, 11);
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
			// btnExportar
			// 
			this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
			this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnExportar.FlatAppearance.BorderSize = 0;
			this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExportar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExportar.ForeColor = System.Drawing.Color.White;
			this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnExportar.Location = new System.Drawing.Point(517, 15);
			this.btnExportar.Name = "btnExportar";
			this.btnExportar.Size = new System.Drawing.Size(96, 35);
			this.btnExportar.TabIndex = 111;
			this.btnExportar.Text = "Exportar";
			this.btnExportar.UseVisualStyleBackColor = false;
			this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
			// 
			// gvReporte1
			// 
			this.gvReporte1.AllowUserToAddRows = false;
			this.gvReporte1.AllowUserToDeleteRows = false;
			this.gvReporte1.AllowUserToResizeRows = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			this.gvReporte1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.gvReporte1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gvReporte1.BackgroundColor = System.Drawing.Color.White;
			this.gvReporte1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gvReporte1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.gvReporte1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gvReporte1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.gvReporte1.ColumnHeadersHeight = 20;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(234)))), ((int)(((byte)(253)))));
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gvReporte1.DefaultCellStyle = dataGridViewCellStyle6;
			this.gvReporte1.GridColor = System.Drawing.Color.LightSteelBlue;
			this.gvReporte1.Location = new System.Drawing.Point(313, 9);
			this.gvReporte1.Name = "gvReporte1";
			this.gvReporte1.ReadOnly = true;
			this.gvReporte1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.gvReporte1.RowHeadersVisible = false;
			this.gvReporte1.RowHeadersWidth = 10;
			this.gvReporte1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.gvReporte1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gvReporte1.Size = new System.Drawing.Size(142, 44);
			this.gvReporte1.TabIndex = 344;
			this.gvReporte1.Visible = false;
			// 
			// RptMensualesSolicitudes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(666, 297);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panelTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "RptMensualesSolicitudes";
			this.Text = "RptMensualesSolicitudes";
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panelWait.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvReporte1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label249;
		private System.Windows.Forms.DateTimePicker dpFecha2;
		private System.Windows.Forms.DateTimePicker dpFecha1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton rbnNucleo;
		private System.Windows.Forms.RadioButton rbnComite;
		private System.Windows.Forms.RadioButton rbnAprobadas;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panelWait;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox10;
		private System.Windows.Forms.Label label105;
		private System.Windows.Forms.Button btnExportar;
		private System.Windows.Forms.DataGridView gvReporte1;
	}
}