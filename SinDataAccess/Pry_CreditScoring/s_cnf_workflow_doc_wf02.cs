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
    public partial class s_cnf_workflow_doc_wf02 : Form
    {
        public DataAccess da;
        int gworkflowid = 0;
        Int16 gcodigo_sub_app = 0;
        Int16 gdocumento_id = 0;
        public s_cnf_workflow_doc_wf02(int workflowid, Int16 codigo_sub_app, Int16 documento_id)
        {
            InitializeComponent();
            gworkflowid = workflowid;
            gcodigo_sub_app = codigo_sub_app;
            gdocumento_id = documento_id;
            
        }

        private void s_cnf_workflow_doc_wf02_Load(object sender, EventArgs e)
        {
            numericUpDownOrden.Value = da.ObtenerOrderByDoc(gworkflowid, gcodigo_sub_app, gdocumento_id);
        }

        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            da.PonerOrderByDoc(gworkflowid, gcodigo_sub_app, gdocumento_id, Int16.Parse(numericUpDownOrden.Value.ToString()));
            this.DialogResult = DialogResult.OK;
        }

        
    }
}
