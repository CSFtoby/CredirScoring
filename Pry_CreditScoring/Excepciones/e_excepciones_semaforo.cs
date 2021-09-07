using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wfcModel;

namespace Docsis_Application.Excepciones
{
	public partial class e_excepciones_semaforo : Form
	{
		public e_excepciones_semaforo(DataTable datos, TipoSemaforo semaforo)
		{
			InitializeComponent();
			switch (semaforo)
			{
				case TipoSemaforo.Verde:
					this.labelSemaforo.Text = "Excepciones en Verde";
					break;
				case TipoSemaforo.Amarillo:
					this.labelSemaforo.Text = "Excepciones en Amarillo";
					break;
				case TipoSemaforo.Rojo:
					this.labelSemaforo.Text = "Excepciones en Rojo";
					break;
				default:
					break;
			}

			this.dgvExcepciones.DataSource = null;
			this.dgvExcepciones.DataSource = datos;
		}
	}
}
