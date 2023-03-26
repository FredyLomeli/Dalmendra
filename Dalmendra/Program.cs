using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dalmendra
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        
        //CODIGO PARA VARIAS EJECUCIONES DE INSTANCIA
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new frmPrincipal());
        //}

        //CODIGO PARA UNA EJECUCION DE UNA SOLA INSTANCIA A LA VEZ
        [STAThread]
        public static void Main()
        {
            bool nuevaInstancia;
            Mutex mutex = new Mutex(true, "DalmendraInventarioSis", out nuevaInstancia);
            if (nuevaInstancia)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmPrincipal());
            }
            else
            {
                MessageBox.Show("Ya se encuentra abierto el programa.", "Dalmendra", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }   
        }
    }
}
