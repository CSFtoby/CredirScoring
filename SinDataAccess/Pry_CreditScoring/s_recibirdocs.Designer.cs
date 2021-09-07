namespace Docsis_Application
{
    partial class s_recibirdocs
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_recibir = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel_no_movimiento = new System.Windows.Forms.ToolStripStatusLabel();
            this.button_cerrar = new System.Windows.Forms.Button();
            this.textBox_recibido_por = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_fecha_recibido = new System.Windows.Forms.TextBox();
            this.textBox_mensajes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Docsis_Application.Properties.Resources.documentos1;
            this.pictureBox1.Location = new System.Drawing.Point(2, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 136);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(126, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(199, 24);
            this.label7.TabIndex = 93;
            this.label7.Text = "Recibir Documentos";
            // 
            // button_recibir
            // 
            this.button_recibir.Location = new System.Drawing.Point(182, 167);
            this.button_recibir.Name = "button_recibir";
            this.button_recibir.Size = new System.Drawing.Size(116, 34);
            this.button_recibir.TabIndex = 94;
            this.button_recibir.Text = "&Recibir";
            this.button_recibir.UseVisualStyleBackColor = true;
            this.button_recibir.Click += new System.EventHandler(this.button_recibir_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel_no_movimiento});
            this.statusStrip1.Location = new System.Drawing.Point(0, 204);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(485, 22);
            this.statusStrip1.TabIndex = 95;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel_no_movimiento
            // 
            this.StatusLabel_no_movimiento.Name = "StatusLabel_no_movimiento";
            this.StatusLabel_no_movimiento.Size = new System.Drawing.Size(141, 17);
            this.StatusLabel_no_movimiento.Text = "StatusLabel_no_movimiento";
            // 
            // button_cerrar
            // 
            this.button_cerrar.Location = new System.Drawing.Point(301, 167);
            this.button_cerrar.Name = "button_cerrar";
            this.button_cerrar.Size = new System.Drawing.Size(116, 34);
            this.button_cerrar.TabIndex = 96;
            this.button_cerrar.Text = "&Cerrar";
            this.button_cerrar.UseVisualStyleBackColor = true;
            this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
            // 
            // textBox_recibido_por
            // 
            this.textBox_recibido_por.BackColor = System.Drawing.Color.White;
            this.textBox_recibido_por.Location = new System.Drawing.Point(273, 44);
            this.textBox_recibido_por.Name = "textBox_recibido_por";
            this.textBox_recibido_por.ReadOnly = true;
            this.textBox_recibido_por.Size = new System.Drawing.Size(100, 20);
            this.textBox_recibido_por.TabIndex = 97;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(179, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 99;
            this.label9.Text = "Recibido Por :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(179, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 98;
            this.label2.Text = "Fecha / Hora :";
            // 
            // textBox_fecha_recibido
            // 
            this.textBox_fecha_recibido.BackColor = System.Drawing.Color.White;
            this.textBox_fecha_recibido.Location = new System.Drawing.Point(273, 68);
            this.textBox_fecha_recibido.Name = "textBox_fecha_recibido";
            this.textBox_fecha_recibido.ReadOnly = true;
            this.textBox_fecha_recibido.Size = new System.Drawing.Size(185, 20);
            this.textBox_fecha_recibido.TabIndex = 100;
            // 
            // textBox_mensajes
            // 
            this.textBox_mensajes.BackColor = System.Drawing.Color.White;
            this.textBox_mensajes.Location = new System.Drawing.Point(273, 94);
            this.textBox_mensajes.Multiline = true;
            this.textBox_mensajes.Name = "textBox_mensajes";
            this.textBox_mensajes.ReadOnly = true;
            this.textBox_mensajes.Size = new System.Drawing.Size(185, 67);
            this.textBox_mensajes.TabIndex = 102;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(179, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 101;
            this.label1.Text = "Mensajes :";
            // 
            // s_recibirdocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.fondo_formularios;
            this.ClientSize = new System.Drawing.Size(485, 226);
            this.Controls.Add(this.textBox_mensajes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_fecha_recibido);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_recibido_por);
            this.Controls.Add(this.button_cerrar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_recibir);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "s_recibirdocs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_recibir;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.TextBox textBox_recibido_por;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_fecha_recibido;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_no_movimiento;
        private System.Windows.Forms.TextBox textBox_mensajes;
        private System.Windows.Forms.Label label1;
    }
}