namespace Docsis_Application
{
    partial class s_add_miembros
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label_header = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label_caracteres = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPrimer_apellido = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCogAfiliado = new System.Windows.Forms.TextBox();
            this.cbxZona = new System.Windows.Forms.ComboBox();
            this.cbxTipoMiembro = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoIdentidad = new System.Windows.Forms.TextBox();
            this.txtNombreMiembro = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtxSegundoApellido = new System.Windows.Forms.TextBox();
            this.panelTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.label_header);
            this.panelTop.Controls.Add(this.btnSalir);
            this.panelTop.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelTop.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelTop.Location = new System.Drawing.Point(0, -1);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(533, 50);
            this.panelTop.TabIndex = 103;
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
            this.btnClose.Location = new System.Drawing.Point(508, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 5;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label_header
            // 
            this.label_header.AutoSize = true;
            this.label_header.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_header.ForeColor = System.Drawing.Color.Silver;
            this.label_header.Location = new System.Drawing.Point(26, 10);
            this.label_header.Name = "label_header";
            this.label_header.Size = new System.Drawing.Size(344, 21);
            this.label_header.TabIndex = 0;
            this.label_header.Text = "Agregar Miembro de Junta Directiva o Delegado";
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Transparent;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(100)))), ((int)(((byte)(74)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalir.Location = new System.Drawing.Point(983, 8);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(29, 29);
            this.btnSalir.TabIndex = 108;
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.UseVisualStyleBackColor = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.label_caracteres});
            this.statusStrip1.Location = new System.Drawing.Point(0, 403);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(533, 22);
            this.statusStrip1.TabIndex = 104;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label_caracteres
            // 
            this.label_caracteres.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.label_caracteres.Name = "label_caracteres";
            this.label_caracteres.Size = new System.Drawing.Size(0, 17);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(396, 363);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(127, 37);
            this.btnAceptar.TabIndex = 105;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPrimer_apellido);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtCogAfiliado);
            this.panel1.Controls.Add(this.cbxZona);
            this.panel1.Controls.Add(this.cbxTipoMiembro);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNoIdentidad);
            this.panel1.Controls.Add(this.txtNombreMiembro);
            this.panel1.Location = new System.Drawing.Point(12, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 250);
            this.panel1.TabIndex = 106;
            // 
            // txtPrimer_apellido
            // 
            this.txtPrimer_apellido.Location = new System.Drawing.Point(219, 125);
            this.txtPrimer_apellido.Name = "txtPrimer_apellido";
            this.txtPrimer_apellido.ReadOnly = true;
            this.txtPrimer_apellido.Size = new System.Drawing.Size(139, 20);
            this.txtPrimer_apellido.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Código del Afiliado:";
            // 
            // txtCogAfiliado
            // 
            this.txtCogAfiliado.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtCogAfiliado.Location = new System.Drawing.Point(18, 43);
            this.txtCogAfiliado.Name = "txtCogAfiliado";
            this.txtCogAfiliado.Size = new System.Drawing.Size(208, 22);
            this.txtCogAfiliado.TabIndex = 8;
            this.txtCogAfiliado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCogAfiliado_KeyPress);
            this.txtCogAfiliado.Leave += new System.EventHandler(this.txtCogAfiliado_Leave);
            // 
            // cbxZona
            // 
            this.cbxZona.FormattingEnabled = true;
            this.cbxZona.Location = new System.Drawing.Point(279, 200);
            this.cbxZona.Name = "cbxZona";
            this.cbxZona.Size = new System.Drawing.Size(213, 21);
            this.cbxZona.TabIndex = 7;
            // 
            // cbxTipoMiembro
            // 
            this.cbxTipoMiembro.FormattingEnabled = true;
            this.cbxTipoMiembro.Location = new System.Drawing.Point(18, 200);
            this.cbxTipoMiembro.Name = "cbxTipoMiembro";
            this.cbxTipoMiembro.Size = new System.Drawing.Size(208, 21);
            this.cbxTipoMiembro.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Selecciones la Zona";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Selecciones el tipo de Miembro";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "No. de Identidad:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre del Miembro:";
            // 
            // txtNoIdentidad
            // 
            this.txtNoIdentidad.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtNoIdentidad.Location = new System.Drawing.Point(279, 43);
            this.txtNoIdentidad.Name = "txtNoIdentidad";
            this.txtNoIdentidad.Size = new System.Drawing.Size(213, 22);
            this.txtNoIdentidad.TabIndex = 1;
            this.txtNoIdentidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoIdentidad_KeyPress);
            this.txtNoIdentidad.Leave += new System.EventHandler(this.txtNoIdentidad_Leave);
            // 
            // txtNombreMiembro
            // 
            this.txtNombreMiembro.Location = new System.Drawing.Point(18, 125);
            this.txtNombreMiembro.Name = "txtNombreMiembro";
            this.txtNombreMiembro.ReadOnly = true;
            this.txtNombreMiembro.Size = new System.Drawing.Size(195, 20);
            this.txtNombreMiembro.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(252, 363);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(127, 37);
            this.btnCancelar.TabIndex = 107;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtxSegundoApellido
            // 
            this.txtxSegundoApellido.Location = new System.Drawing.Point(376, 208);
            this.txtxSegundoApellido.Name = "txtxSegundoApellido";
            this.txtxSegundoApellido.ReadOnly = true;
            this.txtxSegundoApellido.Size = new System.Drawing.Size(128, 20);
            this.txtxSegundoApellido.TabIndex = 11;
            // 
            // s_add_miembros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(533, 425);
            this.Controls.Add(this.txtxSegundoApellido);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "s_add_miembros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_add_miembros";
            this.Load += new System.EventHandler(this.s_add_miembros_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label label_header;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel label_caracteres;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxZona;
        private System.Windows.Forms.ComboBox cbxTipoMiembro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoIdentidad;
        private System.Windows.Forms.TextBox txtNombreMiembro;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCogAfiliado;
        private System.Windows.Forms.TextBox txtPrimer_apellido;
        private System.Windows.Forms.TextBox txtxSegundoApellido;
    }
}