namespace Docsis_Application
{
    partial class r_comentarios_solicitud
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
            this.txtObservNueva = new System.Windows.Forms.TextBox();
            this.txtObservAnterior = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(66, 194);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(108, 43);
            this.btnCancelar.TabIndex = 366;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(206, 194);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(112, 43);
            this.btnAgregar.TabIndex = 365;
            this.btnAgregar.Text = "Aceptar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label1.Location = new System.Drawing.Point(10, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 21);
            this.label1.TabIndex = 364;
            this.label1.Text = "Observación Nueva";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.BackColor = System.Drawing.Color.Transparent;
            this.label87.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.label87.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label87.Location = new System.Drawing.Point(12, 9);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(152, 21);
            this.label87.TabIndex = 363;
            this.label87.Text = "Observación Anterior";
            // 
            // txtObservNueva
            // 
            this.txtObservNueva.BackColor = System.Drawing.Color.White;
            this.txtObservNueva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObservNueva.Location = new System.Drawing.Point(8, 126);
            this.txtObservNueva.MaxLength = 4000;
            this.txtObservNueva.Multiline = true;
            this.txtObservNueva.Name = "txtObservNueva";
            this.txtObservNueva.Size = new System.Drawing.Size(372, 55);
            this.txtObservNueva.TabIndex = 362;
            // 
            // txtObservAnterior
            // 
            this.txtObservAnterior.BackColor = System.Drawing.Color.White;
            this.txtObservAnterior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObservAnterior.Location = new System.Drawing.Point(8, 33);
            this.txtObservAnterior.Multiline = true;
            this.txtObservAnterior.Name = "txtObservAnterior";
            this.txtObservAnterior.ReadOnly = true;
            this.txtObservAnterior.Size = new System.Drawing.Size(372, 55);
            this.txtObservAnterior.TabIndex = 361;
            // 
            // r_comentarios_solicitud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 249);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label87);
            this.Controls.Add(this.txtObservNueva);
            this.Controls.Add(this.txtObservAnterior);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "r_comentarios_solicitud";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar Comentario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TextBox txtObservNueva;
        private System.Windows.Forms.TextBox txtObservAnterior;
    }
}