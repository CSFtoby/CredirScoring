using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Docsis_Application
{
    public partial class s_adjuntos_doc : Form
    {
        public s_adjuntos_doc()
        {
            InitializeComponent();
            p_llenar_adjuntos(99);
        }
        private void p_llenar_adjuntos(int p_no_solicitud)
        {
            string vl_sql;
            try
            {
                vl_sql = @"Select no_archivo,
                                  nombre_archivo,
                                  extension,
                                  descripcion 
                             From dcs_archivos_adjuntos a,
                                  dcs_wf_tipo_documentos b 
                            Where a.documento_id=b.documento_id 
                              and no_solicitud=:pa_no_solicitud 
                            Order by no_archivo ";

                if (DocSys.connOracle.State.ToString().ToUpper() == "CLOSED")
                {
                    DocSys.connOracle.Open();
                }
                OracleCommand cmd2 = new OracleCommand(vl_sql, DocSys.connOracle);
                cmd2.CommandType = CommandType.Text;

                //───────────────────
                OracleParameter pa_parametro1 = new OracleParameter("pa_no_solicitud", OracleType.Int32);
                cmd2.Parameters.Add(pa_parametro1);
                pa_parametro1.Direction = ParameterDirection.Input;
                pa_parametro1.Value = p_no_solicitud;
                OracleDataReader dr = cmd2.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Columns.Add("no_archivo");
                tabla.Columns.Add("nombre_archivo");
                tabla.Columns.Add("extension");
                tabla.Columns.Add("descripcion");
                while (dr.Read())
                {
                    tabla.Rows.Add(dr["no_archivo"].ToString(),
                                dr["nombre_archivo"].ToString(),
                                dr["extension"].ToString(),
                                dr["descripcion"].ToString());
                }

                // Suspending automatic refreshes as items are added/removed.
                list_adjuntos.BeginUpdate();
                list_adjuntos.SmallImageList = imagesSmall;
                list_adjuntos.LargeImageList = imagesLarge;
                list_adjuntos.Clear();
                foreach (DataRow fila in tabla.Rows)
                {
                    ListViewItem listItem = new ListViewItem(fila["descripcion"].ToString());
                    listItem.ImageIndex = 3;

                    /*if (global_bandera)
                    {
                        imagesSmall.Images.Add("tempo",pictureBox4.Image);
                        listItem.ImageKey = "tempo";
                    }
                    */

                    /*if (fila["extension"].ToString().ToUpper() == "DOC" | fila["extension"].ToString().ToUpper() == "DOCX")
                        listItem.ImageIndex = 3;

                    if (fila["extension"].ToString().ToUpper() == "XLS" | fila["extension"].ToString().ToUpper() == "XLSX")
                        listItem.ImageIndex = 3;

                    if (fila["extension"].ToString().ToUpper() == "PDF")
                        listItem.ImageIndex = 3;*/

                    // Add sub-items for Details view.
                    listItem.SubItems.Add(fila["extension"].ToString());
                    listItem.SubItems.Add(fila["no_archivo"].ToString());
                    list_adjuntos.Items.Add(listItem);
                }

                // Add column headers for Details view.
                list_adjuntos.Columns.Add("Nombre Archivo", 180, HorizontalAlignment.Left);
                list_adjuntos.Columns.Add("Ext", 60, HorizontalAlignment.Left);
                list_adjuntos.Columns.Add("No", 60, HorizontalAlignment.Left);

                // Re-enable the display.
                list_adjuntos.EndUpdate();
                list_adjuntos.Sort();
                list_adjuntos.View = View.Tile;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en p_llenar_adjuntos : " + ex.Message + " " + ex.Source, "Aviso DocSis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }

}
