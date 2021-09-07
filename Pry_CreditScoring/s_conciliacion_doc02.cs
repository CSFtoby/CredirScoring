using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application
{
    public partial class s_conciliacion_doc02 : Form
    {
        public DataAccess da;
        public s_conciliacion_doc02()
        {
            InitializeComponent();
        }
        private void lLAbrir_archivo_cs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((!rbArchivo1.Checked) && (!rbArchivo2.Checked))
            {
                MessageBox.Show("Seleccione el tipo de archivo...");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccione un archivo ";
            //openFileDialog.InitialDirectory = Application.StartupPath + "\\logs";
            openFileDialog.InitialDirectory = "C:\\TransUnion";
            openFileDialog.Filter = "All Files (*.*)|*.*|Archivos CSV (*.csv)|*.csv";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtArchivo.Text = openFileDialog.FileName.ToString();
                if (rbArchivo1.Checked)
                {
                    if (cargaArchivo1_toGridView())
                    {
                        lLAbrir_archivo_cs.Visible = false;
                    }
                }
                if (rbArchivo2.Checked)
                {
                    if (cargaArchivo2_toGridView())
                    {
                        lLAbrir_archivo_cs.Visible = false;

                    }
                }
            }
        }
        private bool cargaArchivo1_toGridView()
        {
            bool vl_retorno = false;
            DataTable dt = new DataTable();
            dt.Columns.Add("application_id");
            dt.Columns.Add("fecha_creacion");
            dt.Columns.Add("fecha_actualizacion");
            dt.Columns.Add("estado");
            dt.Columns.Add("oficial_servicio");
            dt.Columns.Add("no_identificacion");
            dt.Columns.Add("filial");
            dt.Columns.Add("monto_solicitado");
            dt.Columns.Add("tasa");
            dt.Columns.Add("plazo");
            dt.Columns.Add("producto");
            string csvPath = txtArchivo.Text;
            try
            {
                if (System.IO.File.Exists(csvPath))
                {
                    System.IO.StreamReader fileReader = new System.IO.StreamReader(csvPath, false);

                    //Reading Data
                    while (fileReader.Peek() != -1)
                    {
                        var fileRow = fileReader.ReadLine();
                        var fileDataField = fileRow.Split(',');
                        dt.Rows.Add(fileDataField);
                    }
                    fileReader.Dispose();
                    fileReader.Close();
                    vl_retorno = true;
                }
                else
                {
                    MessageBox.Show("Error en la busqueda del archivo seleccionado en el disco de su PC.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                vl_retorno = false;
            }

            gvTabla.DataSource = dt;
            return vl_retorno;
        }
        private bool cargaArchivo2_toGridView()
        {
            bool vl_retorno = false;
            DataTable dt = new DataTable();
            dt.Columns.Add("col1");
            dt.Columns.Add("col2");
            dt.Columns.Add("col3");
            dt.Columns.Add("col4");
            dt.Columns.Add("col5");
            dt.Columns.Add("col6");
            dt.Columns.Add("col7");
            dt.Columns.Add("col8");
            dt.Columns.Add("col9");
            dt.Columns.Add("col10");
            dt.Columns.Add("idbitacorabusqueda");
            dt.Columns.Add("identificacion");
            dt.Columns.Add("nombres");
            dt.Columns.Add("apellidos");
            dt.Columns.Add("tipo");

            string csvPath = txtArchivo.Text;
            try
            {
                if (System.IO.File.Exists(csvPath))
                {
                    System.IO.StreamReader fileReader = new System.IO.StreamReader(csvPath, false);

                    //Reading Data
                    while (fileReader.Peek() != -1)
                    {
                        var fileRow = fileReader.ReadLine();
                        var fileDataField = fileRow.Split(',');
                        dt.Rows.Add(fileDataField);
                    }
                    fileReader.Dispose();
                    fileReader.Close();
                    vl_retorno = true;
                }
                else
                {
                    MessageBox.Show("Error en la busqueda del archivo seleccionado en el disco de su PC.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                vl_retorno = false;
            }

            gvTabla.DataSource = dt;
            return vl_retorno;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea subir el archivo de conciliacion ?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (rbArchivo1.Checked)
            {
                try
                {
                    Int32 reg = 0;
                    foreach (DataGridViewRow row in gvTabla.Rows)
                    {
                        string string1 = (string)row.Cells["application_id"].Value;
                        string string2 = (string)row.Cells["fecha_creacion"].Value;
                        string string3 = (string)row.Cells["no_identificacion"].Value;
                        string string4 = (string)row.Cells["monto_solicitado"].Value;

                        da.insertar_archivo1(string1, string2, string3, string4);
                    }
                    MessageBox.Show("Archivo ingresado a la base de datos...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (rbArchivo2.Checked)
            {
                try
                {
                    Int32 reg = 0;
                    foreach (DataGridViewRow row in gvTabla.Rows)
                    {
                        string string1 = (string)row.Cells["idbitacorabusqueda"].Value;
                        string string2 = (string)row.Cells["identificacion"].Value;
                        string string3 = (string)row.Cells["nombres"].Value;
                        string string4 = (string)row.Cells["apellidos"].Value;
                        string string5 = (string)row.Cells["tipo"].Value;

                        da.insertar_archivo2(string1, string2, string3, string4, string5);
                    }
                    MessageBox.Show("Archivo ingresado a la base de datos...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
