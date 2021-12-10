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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        frmListar fListar = new frmListar();
        frmSucursales fSucursales = new frmSucursales();
        frmCategorias fCategorias = new frmCategorias();
        frmListadoCategorias fListadoCategorias = new frmListadoCategorias();
        cSucursal nSucursal = new cSucursal();
        cSQLserver nSQLserver = new cSQLserver();
        cExistencias nExistencias = new cExistencias();

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            // Verifica la base de datos en correcto estado.
            cSQLite.Up();
            // Consulta las sucursales registradas
            cModul.mSucursales = nSucursal.consulta();
            // Consulta las Existencias registradas
            cModul.mExistencias = nExistencias.consulta();
            // Si es la primera vez abre la configuración de la Base de datos.
            if (cModul.ThisFirstTime)
            {
                fSucursales.ShowDialog();
            }
            // Si no es la primera vez valida las conexiones.
            else
            {
                openListadoSinCodigo();
            }
            
        }
        private void sucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSucursales.ShowDialog();
        }

        private void actualizarExistencias()
        {
            // Generamos tabla combox
            DataTable Tabla = new DataTable();
            Tabla.Columns.Add(new DataColumn("id"));
            Tabla.Columns.Add(new DataColumn("nombre_sucursal"));
            if (cModul.mSucursales != null && cModul.mSucursales.Rows.Count > 0)
            {
                foreach (DataRow dr in cModul.mSucursales.Rows)
                {
                    // Generamos el objeto a agregar combo
                    DataRow Renglon;
                    // Si no es la conexión de prueba
                    if (dr[5].ToString() != "***" && dr[5].ToString() != null)
                    {
                        // Valida la conexion con la base de datos
                        if (nSQLserver.validar_conexion(dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString()))
                        {
                            // Generamos el combo de
                            Renglon = Tabla.NewRow();
                            Renglon[0] = dr[0].ToString();
                            Renglon[1] = dr[1].ToString();
                            // Agrega el registro al combobox
                            Tabla.Rows.Add(Renglon);
                            // Asigna las variables para la consulta
                            cSucursal newSucursal = new cSucursal();
                            newSucursal.id = dr[0].ToString();
                            newSucursal.nombre_sucursal = dr[1].ToString();
                            newSucursal.data_source = dr[2].ToString();
                            newSucursal.catalog = dr[3].ToString();
                            newSucursal.user_id = dr[4].ToString();
                            newSucursal.password = dr[5].ToString();
                            newSucursal.fecha_hora_actualizacion = dr[6].ToString();
                            // Actualiza las existencias
                            string fecha;
                            nSQLserver.ConsultarInventario(newSucursal, out fecha);
                            //actualiza la fecha del registor de sucursales
                            for (int i = 0; i < cModul.mSucursalesListado.Rows.Count; i++)
                            {
                                if (cModul.mSucursales.Rows[i]["id"].ToString() == dr[0].ToString())
                                    cModul.mSucursales.Rows[i]["fecha_hora_actualizacion"] = fecha;
                            }
                            // Consulta las sucursales registradas
                            cModul.mSucursales = nSucursal.consulta();
                            // Consulta las Existencias registradas
                            cModul.mExistencias = nExistencias.consulta();
                        }
                        else if (dr[6].ToString() != null)
                        {
                            // Generamos el combo de
                            Renglon = Tabla.NewRow();
                            Renglon[0] = dr[0].ToString();
                            Renglon[1] = dr[1].ToString();
                            Tabla.Rows.Add(Renglon);
                        }
                    }
                }
                cModul.mExistencias = nExistencias.consulta();
                //MessageBox.Show(cModul.ErrorProbocado, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // habre la configuracion de Sucursales
            else
                fSucursales.ShowDialog();
            cModul.SucursalesActivas = Tabla;
            
        }

        private void tmrActualizacion_Tick(object sender, EventArgs e)
        {
            if (cModul.banActualizacion)
            {
                actualizarExistencias();
                this.fListar.llenarListado();
            }

        }

        private void tsmConCodigo_Click(object sender, EventArgs e)
        {
            actualizarExistencias();
            this.fListar.MdiParent = this;
            this.fListar.WindowState = FormWindowState.Maximized;
            this.fListar.conCodigo = true;
            this.fListar.Initialized = false;
            this.fListar.cmbSucursales.DataSource = cModul.SucursalesActivas;
            this.fListar.cmbSucursales.DisplayMember = "nombre_sucursal";
            this.fListar.cmbSucursales.ValueMember = "id";
            this.fListar.Initialized = true;
            this.fListar.Show();
        }

        private void tsmSinCodigo_Click(object sender, EventArgs e)
        {
            openListadoSinCodigo();
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fCategorias.ShowDialog();
        }

        public void openListadoSinCodigo()
        {
            actualizarExistencias();
            this.fListar.MdiParent = this;
            this.fListar.WindowState = FormWindowState.Maximized;
            this.fListar.conCodigo = false;
            this.fListar.Initialized = false;
            this.fListar.cmbSucursales.DataSource = cModul.SucursalesActivas;
            this.fListar.cmbSucursales.DisplayMember = "nombre_sucursal";
            this.fListar.cmbSucursales.ValueMember = "id";
            this.fListar.Initialized = true;
            this.fListar.Show();
        }

        private void porCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actualizarExistencias();
            this.fListadoCategorias.MdiParent = this;
            this.fListadoCategorias.WindowState = FormWindowState.Maximized;
            //this.fCategorias.conCodigo = false;
            //this.fCategorias.Initialized = false;
            //this.fCategorias.cmbSucursales.DataSource = cModul.SucursalesActivas;
            //this.fCategorias.cmbSucursales.DisplayMember = "nombre_sucursal";
            //this.fCategorias.cmbSucursales.ValueMember = "id";
            //this.fCategorias.Initialized = true;
            this.fListadoCategorias.Show();
            this.fListadoCategorias.dgv1.ClearSelection();
            this.fListadoCategorias.dgv2.ClearSelection();
            this.fListadoCategorias.dgv3.ClearSelection();
            this.fListadoCategorias.dgv4.ClearSelection();
            this.fListadoCategorias.dgv5.ClearSelection();
            this.fListadoCategorias.dgv6.ClearSelection();
            this.fListadoCategorias.dgv7.ClearSelection();
            this.fListadoCategorias.dgv8.ClearSelection();
            this.fListadoCategorias.dgv9.ClearSelection();
            this.fListadoCategorias.dgv10.ClearSelection();
            this.fListadoCategorias.dgv11.ClearSelection();
            this.fListadoCategorias.dgv12.ClearSelection();
            this.fListadoCategorias.dgv13.ClearSelection();
            this.fListadoCategorias.dgv14.ClearSelection();
        }

    }
}
