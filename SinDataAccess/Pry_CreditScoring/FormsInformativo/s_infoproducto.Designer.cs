namespace Docsis_Application.FormsInformativo
{
    partial class s_infoproducto
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
            this.label241 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.labelProducto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpPlazos = new System.Windows.Forms.GroupBox();
            this.txtNum_plazomaximo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNum_plazominimo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNum_plazoomision = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpTasas = new System.Windows.Forms.GroupBox();
            this.txtPor_tasamaxima = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPor_tasaminima = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPor_tasaomision = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodigo_sub_app = new System.Windows.Forms.TextBox();
            this.txtDesc_sub_app = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lLCerrar = new System.Windows.Forms.LinkLabel();
            this.grpPlazos.SuspendLayout();
            this.grpTasas.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label241
            // 
            this.label241.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label241.ForeColor = System.Drawing.Color.White;
            this.label241.Location = new System.Drawing.Point(6, 3);
            this.label241.Name = "label241";
            this.label241.Size = new System.Drawing.Size(179, 19);
            this.label241.TabIndex = 1;
            this.label241.Text = "Parametrización del Producto";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Gray;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(100)))), ((int)(((byte)(74)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::Docsis_Application.Properties.Resources.icon_close01;
            this.btnClose.Location = new System.Drawing.Point(333, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 212;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelProducto
            // 
            this.labelProducto.AutoSize = true;
            this.labelProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProducto.ForeColor = System.Drawing.Color.Black;
            this.labelProducto.Location = new System.Drawing.Point(25, 33);
            this.labelProducto.Name = "labelProducto";
            this.labelProducto.Size = new System.Drawing.Size(56, 13);
            this.labelProducto.TabIndex = 213;
            this.labelProducto.Text = "Producto :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(25, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 214;
            this.label1.Text = "Nombre :";
            // 
            // grpPlazos
            // 
            this.grpPlazos.BackColor = System.Drawing.Color.White;
            this.grpPlazos.Controls.Add(this.txtNum_plazomaximo);
            this.grpPlazos.Controls.Add(this.label7);
            this.grpPlazos.Controls.Add(this.txtNum_plazominimo);
            this.grpPlazos.Controls.Add(this.label3);
            this.grpPlazos.Controls.Add(this.txtNum_plazoomision);
            this.grpPlazos.Controls.Add(this.label2);
            this.grpPlazos.Location = new System.Drawing.Point(12, 82);
            this.grpPlazos.Name = "grpPlazos";
            this.grpPlazos.Size = new System.Drawing.Size(153, 91);
            this.grpPlazos.TabIndex = 215;
            this.grpPlazos.TabStop = false;
            this.grpPlazos.Text = "Plazos (Meses)";
            // 
            // txtNum_plazomaximo
            // 
            this.txtNum_plazomaximo.BackColor = System.Drawing.Color.White;
            this.txtNum_plazomaximo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_plazomaximo.Enabled = false;
            this.txtNum_plazomaximo.Location = new System.Drawing.Point(101, 65);
            this.txtNum_plazomaximo.Multiline = true;
            this.txtNum_plazomaximo.Name = "txtNum_plazomaximo";
            this.txtNum_plazomaximo.ReadOnly = true;
            this.txtNum_plazomaximo.Size = new System.Drawing.Size(39, 21);
            this.txtNum_plazomaximo.TabIndex = 222;
            this.txtNum_plazomaximo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(27, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 221;
            this.label7.Text = "Máximo :";
            // 
            // txtNum_plazominimo
            // 
            this.txtNum_plazominimo.BackColor = System.Drawing.Color.White;
            this.txtNum_plazominimo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_plazominimo.Enabled = false;
            this.txtNum_plazominimo.Location = new System.Drawing.Point(101, 41);
            this.txtNum_plazominimo.Multiline = true;
            this.txtNum_plazominimo.Name = "txtNum_plazominimo";
            this.txtNum_plazominimo.ReadOnly = true;
            this.txtNum_plazominimo.Size = new System.Drawing.Size(39, 21);
            this.txtNum_plazominimo.TabIndex = 220;
            this.txtNum_plazominimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(27, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 219;
            this.label3.Text = "Mínimo :";
            // 
            // txtNum_plazoomision
            // 
            this.txtNum_plazoomision.BackColor = System.Drawing.Color.White;
            this.txtNum_plazoomision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_plazoomision.Enabled = false;
            this.txtNum_plazoomision.Location = new System.Drawing.Point(101, 17);
            this.txtNum_plazoomision.Multiline = true;
            this.txtNum_plazoomision.Name = "txtNum_plazoomision";
            this.txtNum_plazoomision.ReadOnly = true;
            this.txtNum_plazoomision.Size = new System.Drawing.Size(39, 21);
            this.txtNum_plazoomision.TabIndex = 218;
            this.txtNum_plazoomision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(27, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 214;
            this.label2.Text = "Por defecto :";
            // 
            // grpTasas
            // 
            this.grpTasas.BackColor = System.Drawing.Color.White;
            this.grpTasas.Controls.Add(this.txtPor_tasamaxima);
            this.grpTasas.Controls.Add(this.label6);
            this.grpTasas.Controls.Add(this.txtPor_tasaminima);
            this.grpTasas.Controls.Add(this.label4);
            this.grpTasas.Controls.Add(this.txtPor_tasaomision);
            this.grpTasas.Controls.Add(this.label5);
            this.grpTasas.Location = new System.Drawing.Point(171, 82);
            this.grpTasas.Name = "grpTasas";
            this.grpTasas.Size = new System.Drawing.Size(154, 91);
            this.grpTasas.TabIndex = 216;
            this.grpTasas.TabStop = false;
            this.grpTasas.Text = "Tasas (%)";
            // 
            // txtPor_tasamaxima
            // 
            this.txtPor_tasamaxima.BackColor = System.Drawing.Color.White;
            this.txtPor_tasamaxima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPor_tasamaxima.Enabled = false;
            this.txtPor_tasamaxima.Location = new System.Drawing.Point(100, 63);
            this.txtPor_tasamaxima.Multiline = true;
            this.txtPor_tasamaxima.Name = "txtPor_tasamaxima";
            this.txtPor_tasamaxima.ReadOnly = true;
            this.txtPor_tasamaxima.Size = new System.Drawing.Size(36, 21);
            this.txtPor_tasamaxima.TabIndex = 226;
            this.txtPor_tasamaxima.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(22, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 225;
            this.label6.Text = "Máximo :";
            // 
            // txtPor_tasaminima
            // 
            this.txtPor_tasaminima.BackColor = System.Drawing.Color.White;
            this.txtPor_tasaminima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPor_tasaminima.Enabled = false;
            this.txtPor_tasaminima.Location = new System.Drawing.Point(100, 40);
            this.txtPor_tasaminima.Multiline = true;
            this.txtPor_tasaminima.Name = "txtPor_tasaminima";
            this.txtPor_tasaminima.ReadOnly = true;
            this.txtPor_tasaminima.Size = new System.Drawing.Size(36, 21);
            this.txtPor_tasaminima.TabIndex = 224;
            this.txtPor_tasaminima.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(22, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 223;
            this.label4.Text = "Mínimo :";
            // 
            // txtPor_tasaomision
            // 
            this.txtPor_tasaomision.BackColor = System.Drawing.Color.White;
            this.txtPor_tasaomision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPor_tasaomision.Enabled = false;
            this.txtPor_tasaomision.Location = new System.Drawing.Point(100, 17);
            this.txtPor_tasaomision.Multiline = true;
            this.txtPor_tasaomision.Name = "txtPor_tasaomision";
            this.txtPor_tasaomision.ReadOnly = true;
            this.txtPor_tasaomision.Size = new System.Drawing.Size(36, 21);
            this.txtPor_tasaomision.TabIndex = 222;
            this.txtPor_tasaomision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(22, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 221;
            this.label5.Text = "Por defecto :";
            // 
            // txtCodigo_sub_app
            // 
            this.txtCodigo_sub_app.BackColor = System.Drawing.Color.White;
            this.txtCodigo_sub_app.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo_sub_app.Enabled = false;
            this.txtCodigo_sub_app.Location = new System.Drawing.Point(83, 29);
            this.txtCodigo_sub_app.Multiline = true;
            this.txtCodigo_sub_app.Name = "txtCodigo_sub_app";
            this.txtCodigo_sub_app.ReadOnly = true;
            this.txtCodigo_sub_app.Size = new System.Drawing.Size(39, 21);
            this.txtCodigo_sub_app.TabIndex = 217;
            // 
            // txtDesc_sub_app
            // 
            this.txtDesc_sub_app.BackColor = System.Drawing.Color.White;
            this.txtDesc_sub_app.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc_sub_app.Enabled = false;
            this.txtDesc_sub_app.Location = new System.Drawing.Point(83, 51);
            this.txtDesc_sub_app.Multiline = true;
            this.txtDesc_sub_app.Name = "txtDesc_sub_app";
            this.txtDesc_sub_app.ReadOnly = true;
            this.txtDesc_sub_app.Size = new System.Drawing.Size(242, 21);
            this.txtDesc_sub_app.TabIndex = 218;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label241);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 23);
            this.panel1.TabIndex = 219;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // lLCerrar
            // 
            this.lLCerrar.AutoSize = true;
            this.lLCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLCerrar.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.lLCerrar.Location = new System.Drawing.Point(290, 176);
            this.lLCerrar.Name = "lLCerrar";
            this.lLCerrar.Size = new System.Drawing.Size(35, 13);
            this.lLCerrar.TabIndex = 220;
            this.lLCerrar.TabStop = true;
            this.lLCerrar.Text = "Cerrar";
            this.lLCerrar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLCerrar_LinkClicked);
            // 
            // s_infoproducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(349, 206);
            this.Controls.Add(this.lLCerrar);
            this.Controls.Add(this.txtDesc_sub_app);
            this.Controls.Add(this.txtCodigo_sub_app);
            this.Controls.Add(this.grpTasas);
            this.Controls.Add(this.grpPlazos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelProducto);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_infoproducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_infoproducto";
            this.Load += new System.EventHandler(this.s_infoproducto_Load);
            this.grpPlazos.ResumeLayout(false);
            this.grpPlazos.PerformLayout();
            this.grpTasas.ResumeLayout(false);
            this.grpTasas.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label241;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labelProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpPlazos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpTasas;
        private System.Windows.Forms.TextBox txtCodigo_sub_app;
        private System.Windows.Forms.TextBox txtDesc_sub_app;
        private System.Windows.Forms.TextBox txtNum_plazominimo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNum_plazoomision;
        private System.Windows.Forms.TextBox txtPor_tasamaxima;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPor_tasaminima;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPor_tasaomision;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNum_plazomaximo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel lLCerrar;
    }
}