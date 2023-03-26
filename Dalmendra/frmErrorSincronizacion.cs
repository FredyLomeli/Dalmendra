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
    public partial class frmErrorSincronizacion : Form
    {
        public frmErrorSincronizacion()
        {
            InitializeComponent();
        }


        private void frmErrorSincronizacion_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap();
        }

        private void frmErrorSincronizacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            cModul.BanErrorSincronizacion = false;
            cModul.ErrorSincronizacion = "";
            //Hiding the window, because closing it makes the window unaccessible.
            this.Hide();
            e.Cancel = true; //hides the form, cancels closing event
        }
    }
}
