namespace Docsis_Application
{
    partial class s_conciliacion_doc02
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
            this.gvTabla = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lLAbrir_archivo_cs = new System.Windows.Forms.LinkLabel();
            this.txtArchivo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rbArchivo1 = new System.Windows.Forms.RadioButton();
            this.rbArchivo2 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.gvTabla)).BeginInit();
            this.SuspendLayout();
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
            this.gvTabla.Location = new System.Drawing.Point(12, 41);
            this.gvTabla.Name = "gvTabla";
            this.gvTabla.ReadOnly = true;
            this.gvTabla.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gvTabla.RowHeadersVisible = false;
            this.gvTabla.RowHeadersWidth = 10;
            this.gvTabla.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvTabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvTabla.Size = new System.Drawing.Size(745, 211);
            this.gvTabla.TabIndex = 359;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 358;
            this.label1.Text = "Nombre del archivo :";
            // 
            // lLAbrir_archivo_cs
            // 
            this.lLAbrir_archivo_cs.AutoSize = true;
            this.lLAbrir_archivo_cs.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(136)))), ((int)(((byte)(230)))));
            this.lLAbrir_archivo_cs.Location = new System.Drawing.Point(632, 16);
            this.lLAbrir_archivo_cs.Name = "lLAbrir_archivo_cs";
            this.lLAbrir_archivo_cs.Size = new System.Drawing.Size(100, 13);
            this.lLAbrir_archivo_cs.TabIndex = 357;
            this.lLAbrir_archivo_cs.TabStop = true;
            this.lLAbrir_archivo_cs.Text = "Cargar archivo CSV";
            this.lLAbrir_archivo_cs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLAbrir_archivo_cs_LinkClicked);
            // 
            // txtArchivo
            // 
            this.txtArchivo.Location = new System.Drawing.Point(128, 12);
            this.txtArchivo.Name = "txtArchivo";
            this.txtArchivo.Size = new System.Drawing.Size(463, 20);
            this.txtArchivo.TabIndex = 356;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(642, 258);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 29);
            this.button1.TabIndex = 360;
            this.button1.Text = "Subir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbArchivo1
            // 
            this.rbArchivo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbArchivo1.AutoSize = true;
            this.rbArchivo1.Location = new System.Drawing.Point(37, 268);
            this.rbArchivo1.Name = "rbArchivo1";
            this.rbArchivo1.Size = new System.Drawing.Size(70, 17);
            this.rbArchivo1.TabIndex = 361;
            this.rbArchivo1.TabStop = true;
            this.rbArchivo1.Text = "Archivo 1";
            this.rbArchivo1.UseVisualStyleBackColor = true;
            // 
            // rbArchivo2
            // 
            this.rbArchivo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbArchivo2.AutoSize = true;
            this.rbArchivo2.Location = new System.Drawing.Point(149, 268);
            this.rbArchivo2.Name = "rbArchivo2";
            this.rbArchivo2.Size = new System.Drawing.Size(70, 17);
            this.rbArchivo2.TabIndex = 362;
            this.rbArchivo2.TabStop = true;
            this.rbArchivo2.Text = "Archivo 2";
            this.rbArchivo2.UseVisualStyleBackColor = true;
            // 
            // s_conciliacion_doc02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 299);
            this.Controls.Add(this.rbArchivo2);
            this.Controls.Add(this.rbArchivo1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gvTabla);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lLAbrir_archivo_cs);
            this.Controls.Add(this.txtArchivo);
            this.Name = "s_conciliacion_doc02";
            this.Text = "s_conciliacion_doc02";
            ((System.ComponentModel.ISupportInitialize)(this.gvTabla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvTabla;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lLAbrir_archivo_cs;
        private System.Windows.Forms.TextBox txtArchivo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbArchivo1;
        private System.Windows.Forms.RadioButton rbArchivo2;
    }
}