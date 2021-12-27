using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dalmendra
{
    public partial class frmConfig : Form
    {
        frmPrincipal fPrincipal;
        frmListadoCategorias fListadoCategorias;

        public frmConfig(frmPrincipal fPrincipal, frmListadoCategorias fListadoCategorias)
        {
            InitializeComponent();
            this.fPrincipal = fPrincipal;
            this.fListadoCategorias = fListadoCategorias;
        }

        private void config_Load(object sender, EventArgs e)
        {
            cModul.banActualizacion = false;
            cmbReporte.SelectedItem = cModul.FirstReport;
            nudSync.Value = Convert.ToDecimal(cModul.TimeSyncSucursal);
            nudCambio.Value = Convert.ToDecimal(cModul.TimeChangeSucursal);
        }

        private void frmConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            cModul.banActualizacion = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            cDatos.update("FirstReport", cmbReporte.SelectedItem.ToString());
            cDatos.update("TimeSyncSucursal",nudSync.Value.ToString());
            fPrincipal.defineTimerSync();
            cDatos.update("TimeChangeSucursal", nudCambio.Value.ToString());
            fListadoCategorias.defineTimerChangeSucursal();
            MessageBox.Show(cModul.ConfigSave, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
