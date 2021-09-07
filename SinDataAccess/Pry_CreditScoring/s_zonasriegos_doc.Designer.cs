namespace Docsis_Application
{
    partial class s_zonasriegos_doc
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
            this.label_no_solicitud = new System.Windows.Forms.Label();
            this.txtPunto = new System.Windows.Forms.TextBox();
            this.txtPoligono = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_no_solicitud
            // 
            this.label_no_solicitud.AutoSize = true;
            this.label_no_solicitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_no_solicitud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_no_solicitud.Location = new System.Drawing.Point(35, 19);
            this.label_no_solicitud.Name = "label_no_solicitud";
            this.label_no_solicitud.Size = new System.Drawing.Size(95, 13);
            this.label_no_solicitud.TabIndex = 56;
            this.label_no_solicitud.Text = "Punto Hipoteca";
            // 
            // txtPunto
            // 
            this.txtPunto.BackColor = System.Drawing.Color.White;
            this.txtPunto.Location = new System.Drawing.Point(136, 16);
            this.txtPunto.Name = "txtPunto";
            this.txtPunto.Size = new System.Drawing.Size(173, 20);
            this.txtPunto.TabIndex = 97;
            // 
            // txtPoligono
            // 
            this.txtPoligono.BackColor = System.Drawing.Color.White;
            this.txtPoligono.Location = new System.Drawing.Point(38, 77);
            this.txtPoligono.Multiline = true;
            this.txtPoligono.Name = "txtPoligono";
            this.txtPoligono.Size = new System.Drawing.Size(528, 259);
            this.txtPoligono.TabIndex = 98;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(35, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 99;
            this.label1.Text = "Poligono de Riego";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 100;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // s_zonasriegos_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 373);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPoligono);
            this.Controls.Add(this.txtPunto);
            this.Controls.Add(this.label_no_solicitud);
            this.Name = "s_zonasriegos_doc";
            this.Text = "s_zonasriegos_doc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_no_solicitud;
        private System.Windows.Forms.TextBox txtPunto;
        private System.Windows.Forms.TextBox txtPoligono;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}