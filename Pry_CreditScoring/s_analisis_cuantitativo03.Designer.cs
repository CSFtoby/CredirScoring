namespace Docsis_Application
{
    partial class s_analisis_cuantitativo03
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
            this.label21 = new System.Windows.Forms.Label();
            this.txtMonto_cobro = new System.Windows.Forms.TextBox();
            this.txtCod_cobro = new System.Windows.Forms.TextBox();
            this.txtDesc_cobro = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(385, 40);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(37, 13);
            this.label21.TabIndex = 6;
            this.label21.Text = "Monto";
            // 
            // txtMonto_cobro
            // 
            this.txtMonto_cobro.BackColor = System.Drawing.Color.White;
            this.txtMonto_cobro.Location = new System.Drawing.Point(428, 37);
            this.txtMonto_cobro.MaxLength = 12;
            this.txtMonto_cobro.Name = "txtMonto_cobro";
            this.txtMonto_cobro.Size = new System.Drawing.Size(86, 20);
            this.txtMonto_cobro.TabIndex = 0;
            this.txtMonto_cobro.Text = "100.00";
            this.txtMonto_cobro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMonto_cobro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonto_cobro_KeyPress);
            this.txtMonto_cobro.MouseLeave += new System.EventHandler(this.txtMonto_cobro_MouseLeave);
            // 
            // txtCod_cobro
            // 
            this.txtCod_cobro.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCod_cobro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCod_cobro.Location = new System.Drawing.Point(76, 11);
            this.txtCod_cobro.MaxLength = 5;
            this.txtCod_cobro.Name = "txtCod_cobro";
            this.txtCod_cobro.ReadOnly = true;
            this.txtCod_cobro.Size = new System.Drawing.Size(39, 20);
            this.txtCod_cobro.TabIndex = 4;
            this.txtCod_cobro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDesc_cobro
            // 
            this.txtDesc_cobro.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDesc_cobro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc_cobro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc_cobro.Location = new System.Drawing.Point(115, 11);
            this.txtDesc_cobro.Name = "txtDesc_cobro";
            this.txtDesc_cobro.ReadOnly = true;
            this.txtDesc_cobro.Size = new System.Drawing.Size(400, 20);
            this.txtDesc_cobro.TabIndex = 5;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.label30.Location = new System.Drawing.Point(5, 15);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(68, 13);
            this.label30.TabIndex = 3;
            this.label30.Text = "Deduccion :";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAceptar.BackColor = System.Drawing.Color.Silver;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Arial", 9F);
            this.btnAceptar.ForeColor = System.Drawing.Color.Black;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(297, 96);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(108, 31);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.Color.Silver;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 9F);
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(411, 96);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(108, 31);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // s_analisis_cuantitativo03
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 139);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtCod_cobro);
            this.Controls.Add(this.txtDesc_cobro);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtMonto_cobro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "s_analisis_cuantitativo03";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modificar Deducción";
            this.Load += new System.EventHandler(this.s_analisis_cuantitativo03_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.TextBox txtMonto_cobro;
        public System.Windows.Forms.TextBox txtCod_cobro;
        public System.Windows.Forms.TextBox txtDesc_cobro;
    }
}