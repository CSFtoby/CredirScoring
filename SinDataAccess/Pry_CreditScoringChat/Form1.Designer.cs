namespace Pry_CreditScoringChat
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lvfotos = new System.Windows.Forms.ListView();
            this.imagesSmall = new System.Windows.Forms.ImageList(this.components);
            this.imagesLarge = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvfotos
            // 
            this.lvfotos.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvfotos.AllowColumnReorder = true;
            this.lvfotos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvfotos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvfotos.HoverSelection = true;
            this.lvfotos.Location = new System.Drawing.Point(12, 12);
            this.lvfotos.Name = "lvfotos";
            this.lvfotos.ShowItemToolTips = true;
            this.lvfotos.Size = new System.Drawing.Size(297, 447);
            this.lvfotos.TabIndex = 12;
            this.lvfotos.UseCompatibleStateImageBehavior = false;
            this.lvfotos.View = System.Windows.Forms.View.Details;
            // 
            // imagesSmall
            // 
            this.imagesSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imagesSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.imagesSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imagesLarge
            // 
            this.imagesLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesLarge.ImageStream")));
            this.imagesLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesLarge.Images.SetKeyName(0, "empleado.png");
            this.imagesLarge.Images.SetKeyName(1, "empleada.png");
            this.imagesLarge.Images.SetKeyName(2, "empleado_nodefinido.png");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(234, 465);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 490);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvfotos);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView lvfotos;
        internal System.Windows.Forms.ImageList imagesSmall;
        internal System.Windows.Forms.ImageList imagesLarge;
        private System.Windows.Forms.Button button1;
    }
}

