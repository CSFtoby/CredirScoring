using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Docsis_Application
{
    public partial class s_zonasriegos_doc : Form
    {
        public s_zonasriegos_doc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string[] cordenadas = txtPoligono.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);

            List<Coordinate> poligono = new List<Coordinate>(cordenadas.Length);
            for (int i = 0; i < cordenadas.Length; i++)
            {
                string s = cordenadas[i];
                string[] latlog = s.Split(',');

                
                Coordinate cord = new Coordinate();
                cord.X = float.Parse(latlog[0]);
                cord.Y = float.Parse(latlog[1]);
                poligono.Add(cord);
                
            }

            string[] punto = txtPunto.Text.Split(',');

            Coordinate coord = new Coordinate();
            coord.X = float.Parse(punto[0]);
            coord.Y = float.Parse(punto[1]);


            if (DocSys.checkIsInPolygon(poligono, coord))
            {
                MessageBox.Show("Coordenada se encuentra dentro del poligono");
            }
            else
            {
                MessageBox.Show("Coordenada NO se encuentra dentro del poligono");
            }
        }
    }
}
