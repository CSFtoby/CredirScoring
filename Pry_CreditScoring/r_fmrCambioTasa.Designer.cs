namespace Docsis_Application
{
    partial class r_fmrCambioTasa
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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.txttasaNueva = new System.Windows.Forms.TextBox();
            this.txtTasaAnterio = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(12, 79);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(74, 28);
            this.btnCancelar.TabIndex = 372;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(111, 79);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(83, 28);
            this.btnAgregar.TabIndex = 371;
            this.btnAgregar.Text = "Aceptar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label1.Location = new System.Drawing.Point(123, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 21);
            this.label1.TabIndex = 370;
            this.label1.Text = "Tasa Nueva";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.BackColor = System.Drawing.Color.Transparent;
            this.label87.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.label87.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label87.Location = new System.Drawing.Point(12, 13);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(95, 21);
            this.label87.TabIndex = 369;
            this.label87.Text = "Tasa Anterior";
            // 
            // txttasaNueva
            // 
            this.txttasaNueva.BackColor = System.Drawing.Color.White;
            this.txttasaNueva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttasaNueva.Location = new System.Drawing.Point(127, 37);
            this.txttasaNueva.MaxLength = 4000;
            this.txttasaNueva.Multiline = true;
            this.txttasaNueva.Name = "txttasaNueva";
            this.txttasaNueva.Size = new System.Drawing.Size(67, 27);
            this.txttasaNueva.TabIndex = 368;
            // 
            // txtTasaAnterio
            // 
            this.txtTasaAnterio.BackColor = System.Drawing.Color.White;
            this.txtTasaAnterio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTasaAnterio.Location = new System.Drawing.Point(12, 37);
            this.txtTasaAnterio.Multiline = true;
            this.txtTasaAnterio.Name = "txtTasaAnterio";
            this.txtTasaAnterio.ReadOnly = true;
            this.txtTasaAnterio.Size = new System.Drawing.Size(67, 27);
            this.txtTasaAnterio.TabIndex = 367;
            // 
            // r_fmrCambioTasa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 130);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label87);
            this.Controls.Add(this.txttasaNueva);
            this.Controls.Add(this.txtTasaAnterio);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "r_fmrCambioTasa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "r_fmrCambioTasa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TextBox txtTasaAnterio;
        public System.Windows.Forms.TextBox txttasaNueva;
    }
}