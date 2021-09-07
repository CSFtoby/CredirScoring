namespace Docsis_Application
{
    partial class s_clasificar_sol
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(s_clasificar_sol));
            this.imageSeguimiento = new System.Windows.Forms.ImageList(this.components);
            this.listView_banderin = new System.Windows.Forms.ListView();
            this.button_desclasificar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel_indicativo_superior = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.labelFilial = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel_indicativo_superior.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageSeguimiento
            // 
            this.imageSeguimiento.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageSeguimiento.ImageStream")));
            this.imageSeguimiento.TransparentColor = System.Drawing.Color.Transparent;
            this.imageSeguimiento.Images.SetKeyName(0, "vacio_grid.png");
            this.imageSeguimiento.Images.SetKeyName(1, "banderin_rojo.png");
            this.imageSeguimiento.Images.SetKeyName(2, "banderin_azul.png");
            this.imageSeguimiento.Images.SetKeyName(3, "banderin_verde.png");
            this.imageSeguimiento.Images.SetKeyName(4, "banderin_amarillo.png");
            this.imageSeguimiento.Images.SetKeyName(5, "banderin_fusia.png");
            this.imageSeguimiento.Images.SetKeyName(6, "banderin_gris.png");
            this.imageSeguimiento.Images.SetKeyName(7, "banderin_naranja.png");
            this.imageSeguimiento.Images.SetKeyName(8, "banderin_cafe.png");
            this.imageSeguimiento.Images.SetKeyName(9, "banderin_celeste.png");
            this.imageSeguimiento.Images.SetKeyName(10, "banderin_negro.png");
            // 
            // listView_banderin
            // 
            this.listView_banderin.Location = new System.Drawing.Point(6, 34);
            this.listView_banderin.Name = "listView_banderin";
            this.listView_banderin.Size = new System.Drawing.Size(223, 197);
            this.listView_banderin.SmallImageList = this.imageSeguimiento;
            this.listView_banderin.TabIndex = 0;
            this.listView_banderin.UseCompatibleStateImageBehavior = false;
            this.listView_banderin.View = System.Windows.Forms.View.SmallIcon;
            this.listView_banderin.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_banderin_MouseDoubleClick);
            // 
            // button_desclasificar
            // 
            this.button_desclasificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(138)))), ((int)(((byte)(193)))));
            this.button_desclasificar.FlatAppearance.BorderSize = 0;
            this.button_desclasificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_desclasificar.ForeColor = System.Drawing.Color.White;
            this.button_desclasificar.Location = new System.Drawing.Point(118, 287);
            this.button_desclasificar.Name = "button_desclasificar";
            this.button_desclasificar.Size = new System.Drawing.Size(108, 29);
            this.button_desclasificar.TabIndex = 1;
            this.button_desclasificar.Text = "Desclasificar";
            this.button_desclasificar.UseVisualStyleBackColor = false;
            this.button_desclasificar.Click += new System.EventHandler(this.button_desclasificar_Click);
            // 
            // panel_indicativo_superior
            // 
            this.panel_indicativo_superior.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_indicativo_superior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.panel_indicativo_superior.Controls.Add(this.label1);
            this.panel_indicativo_superior.Controls.Add(this.panel3);
            this.panel_indicativo_superior.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel_indicativo_superior.Location = new System.Drawing.Point(3, 243);
            this.panel_indicativo_superior.Name = "panel_indicativo_superior";
            this.panel_indicativo_superior.Size = new System.Drawing.Size(233, 19);
            this.panel_indicativo_superior.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Doble click para seleccionar banderin";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel3.BackgroundImage = global::Docsis_Application.Properties.Resources.background_login3;
            this.panel3.Location = new System.Drawing.Point(0, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(233, 17);
            this.panel3.TabIndex = 22;
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.labelFilial);
            this.panelTop.Controls.Add(this.btnSalir);
            this.panelTop.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panelTop.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelTop.Location = new System.Drawing.Point(1, -1);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(246, 29);
            this.panelTop.TabIndex = 92;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
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
            this.btnClose.Location = new System.Drawing.Point(221, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(15, 16);
            this.btnClose.TabIndex = 5;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelFilial
            // 
            this.labelFilial.AutoSize = true;
            this.labelFilial.Location = new System.Drawing.Point(4, 9);
            this.labelFilial.Name = "labelFilial";
            this.labelFilial.Size = new System.Drawing.Size(115, 13);
            this.labelFilial.TabIndex = 0;
            this.labelFilial.Text = "Clasificar con Banderin";
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
            // s_clasificar_sol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Docsis_Application.Properties.Resources.Sagrada_logo_rota3;
            this.ClientSize = new System.Drawing.Size(243, 332);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panel_indicativo_superior);
            this.Controls.Add(this.button_desclasificar);
            this.Controls.Add(this.listView_banderin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "s_clasificar_sol";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "  Clasificar";
            this.Load += new System.EventHandler(this.s_clasificar_sol_Load);
            this.panel_indicativo_superior.ResumeLayout(false);
            this.panel_indicativo_superior.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageSeguimiento;
        private System.Windows.Forms.ListView listView_banderin;
        private System.Windows.Forms.Button button_desclasificar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel_indicativo_superior;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labelFilial;
        private System.Windows.Forms.Button btnSalir;


    }
}