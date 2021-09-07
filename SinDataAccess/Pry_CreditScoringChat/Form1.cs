using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pry_CreditScoringChat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void p_llenar_usuarios_disponibles()
        {
            //il.ImageSize = new Size(int.Parse(picAncho.Value.ToString()), int.Parse(picAlto.Value.ToString()));
            //il.ColorDepth = ColorDepth.Depth32Bit;

            //lvfotos.Clear();
            //lvfotos.Groups.Clear();
            //il.Images.Clear();
            //il2.Images.Clear();
            //DataTable dtFotos = new DataTable();
            //dtFotos.Columns.Add("foto", typeof(Image));
            //dtFotos.Columns.Add("no_emple");
            //dtFotos.Columns.Add("nombre");
            //dtFotos.Columns.Add("filial");
            //dtFotos.Columns.Add("oficial_servicio");
            //dtFotos.Clear();
            //string campo = "d.descri";


            //DataTable dt = da.P_Obtener_listado_fotos(texto, campo, cbSoloempleados.Checked);

            //for (int x = 0; x < dt.Rows.Count; x++)
            //{
            //    Image col1 = p_CopyDataToBitmap((byte[])dt.Rows[x]["foto"]);
            //    var col2 = dt.Rows[x]["codigo_cliente"].ToString();
            //    var col3 = dt.Rows[x]["nombre"].ToString();
            //    var col4 = dt.Rows[x]["filial"].ToString();
            //    var col5 = dt.Rows[x]["oficial_servicio"].ToString();

            //    dtFotos.Rows.Add(col1, col2, col3, col4, col5);
            //}

            //panelCargando.Visible = false;
            
            //lvfotos.View = View.LargeIcon;
            //lvfotos.LargeImageList = il;
            //lvfotos.SmallImageList = il2;


            //string vl_depto = "";
            //int i = 0;
            //bool inicio = true;
            //int indgrupo = 0;
            //foreach (DataRow fila in dtFotos.Rows)
            //{
            //    ListViewItem lvi = new ListViewItem();
            //    il.Images.Add(fila["no_emple"].ToString(), (Image)fila["foto"]);
            //    il2.Images.Add(fila["no_emple"].ToString(), (Image)fila["foto"]);
            //    lvi.ImageIndex = i;

            //    lvi.Tag = fila["no_emple"].ToString();
            //    lvi.ToolTipText = fila["nombre"].ToString() + "\n" + "Oficial de servicio :" + fila["oficial_servicio"].ToString() + "\n" + "Filial :" + fila["filial"].ToString();
            //    lvi.Text = fila["no_emple"].ToString() + "\n" + fila["nombre"].ToString() + "\n";



            //    if (cbAgruparxfilial.Checked)
            //    {
            //        if (inicio)
            //        {
            //            indgrupo = 0;
            //            lvfotos.Groups.Add(new ListViewGroup(fila["filial"].ToString(), HorizontalAlignment.Left));
            //            vl_depto = fila["filial"].ToString();
            //            inicio = false;
            //        }
            //        if (fila["filial"].ToString() != vl_depto)
            //        {
            //            lvfotos.Groups.Add(new ListViewGroup(fila["filial"].ToString(), HorizontalAlignment.Left));
            //            vl_depto = fila["filial"].ToString();
            //            indgrupo++;
            //        }
            //        lvi.Group = lvfotos.Groups[indgrupo];
            //    }
            //    lvfotos.Items.Add(lvi);
            //    i++;
            //}


            //lvfotos.EndUpdate();
            //lvfotos.Sort();

            //if (rbVistaFotos1.Checked)
            //    lvfotos.View = View.LargeIcon;

            //if (rbVistasFotos2.Checked)
            //{
            //    lvfotos.View = View.Tile;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s_chatdoc_01 forma = new s_chatdoc_01();
            forma.ShowDialog();
        }
    }
}
