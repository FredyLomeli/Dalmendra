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
        frmOrdenExistencias fOrdenExistencias = new frmOrdenExistencias();
        frmConfig fConfiguracion;
        cSucursal nSucursal = new cSucursal();
        cSQLserver nSQLserver = new cSQLserver();
        cExistencias nExistencias = new cExistencias();
        cCategoria nCategoria = new cCategoria();
        frmErrorSincronizacion fShowError = new frmErrorSincronizacion();

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            //fShowError.Show();
            fConfiguracion = new frmConfig(this, fListadoCategorias);
            // Verifica la base de datos en correcto estado.
            cSQLite.Up();
            // Consulta las sucursales registradas
            cModul.mSucursales = nSucursal.consulta();
            // Consulta las Existencias registradas
            cModul.mExistencias = nExistencias.consulta();
            // Consulta los datos de configuracion
            cDatos.ConsultaDatos();
            // Definimos si se activa el times de sincronizacion
            defineTimerSync();
            // Si es la primera vez abre la configuración de la Base de datos.
            if (cModul.ThisFirstTime)
            {
                fSucursales.ShowDialog();
            }
            // Si no es la primera vez valida las conexiones.
            else
            {
                //Actualiza el inventario
                actualizarExistencias();
                if (cModul.SucursalesActivas.Rows.Count > 0)
                {
                    //Carga las vista principal
                    switch (cModul.FirstReport)
                    {
                        case "PorCategorias":
                            OpenListadoCategorias();
                            break;
                        case "ListadoConCodigo":
                            openListadoConCodigo();
                            break;
                        case "ListadoSinCodigo":
                            openListadoSinCodigo();
                            break;
                    }
                }
                else
                {
                    // Avisa que no existe ninguna categoria para mostrar el reporte
                    MessageBox.Show(cModul.SinConexionConSucursales, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fSucursales.ShowDialog();
                }
            }
            //muestra si hay errores en la sincronizacion
            this.abreVentanaErrorSincronizacion();
        }

        private void actualizarExistencias()
        {
            // Generamos tabla combox
            DataTable Tabla = new DataTable();
            Tabla.Columns.Add(new DataColumn("id"));
            Tabla.Columns.Add(new DataColumn("nombre_sucursal"));
            Tabla.Columns.Add(new DataColumn("fecha_actualizacion"));
            Tabla.Columns.Add(new DataColumn("color"));
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
                            Renglon[3] = dr[8].ToString();
                            // Asigna las variables para la consulta
                            cSucursal newSucursal = new cSucursal();
                            newSucursal.id = dr[0].ToString();
                            newSucursal.nombre_sucursal = dr[1].ToString();
                            newSucursal.data_source = dr[2].ToString();
                            newSucursal.catalog = dr[3].ToString();
                            newSucursal.user_id = dr[4].ToString();
                            newSucursal.password = dr[5].ToString();
                            newSucursal.fecha_hora_actualizacion = dr[7].ToString();
                            // Actualiza las existencias
                            string fecha, error;
                            //nSQLserver.ConsultarInventario(nSucursal, out fecha);
                            // Actualización para evitar que el programa se rompa al tener problemas de conexión 
                            if (nSQLserver.ConsultarInventario(newSucursal, out fecha, out error))
                                // Avisa que no existe ninguna categoria para mostrar el reporte
                                //MessageBox.Show(error, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // reune los errores a mostrar
                                cModul.ErrorSincronizacion = cModul.ErrorSincronizacion + error;
                            // Carga la fecha de actualización
                            Renglon[2] = fecha;
                            // Agrega el registro al combobox
                            Tabla.Rows.Add(Renglon);
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
                        //Si no hay conexion pero tiene fecha de actualizacion se muestra como sucursal activa
                        else if (!string.IsNullOrEmpty(dr[7].ToString().Trim()))
                        {
                            // Generamos el combo de
                            Renglon = Tabla.NewRow();
                            Renglon[0] = dr[0].ToString();
                            Renglon[1] = dr[1].ToString();
                            Renglon[2] = dr[7].ToString();
                            Renglon[3] = dr[8].ToString();
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
                //Cierra la ventana de error si esta abierta
                this.cierraVentanaErrorSincronizacion();
                //Actualiza las existencias de las sucursales
                this.actualizarExistencias();
                if(cModul.EstaAbiertoFrmListado)
                    this.fListar.llenarListado();
                if(cModul.EstaAbiertoFrmCategorias)
                {
                    this.fListadoCategorias.crearVistaCategorias();
                    this.fListadoCategorias.deSeleccionarDataGrids();
                }
                //Abre la ventana si hay errores
                this.abreVentanaErrorSincronizacion();
            }
        }

        private void cierraVentanaErrorSincronizacion()
        {
            if (cModul.BanErrorSincronizacion)
            {
                cModul.BanErrorSincronizacion = false;
                cModul.ErrorSincronizacion = "";
                this.fShowError.Hide();
            }
        }

        private void abreVentanaErrorSincronizacion()
        {
            if (cModul.BanErrorSincronizacion)
            {
                this.fShowError.Location = new Point(890, 520);
                this.fShowError.lblError.Text = cModul.ErrorSincronizacion;
                //this.fShowError.MdiParent = this;
                //this.fListar.WindowState = FormWindowState.Maximized;
                this.fShowError.Show();
                this.fShowError.Focus();
            }
        }

        private void tsmConCodigo_Click(object sender, EventArgs e)
        {
            actualizarExistencias();
            if (cModul.SucursalesActivas.Rows.Count > 0)
            {
                openListadoConCodigo();
            }
            else
            {
                // Avisa que no existe ninguna categoria para mostrar el reporte
                MessageBox.Show(cModul.SinConexionConSucursales, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                fSucursales.ShowDialog();
            }
        }

        public void openListadoConCodigo()
        {
            //actualizarExistencias();
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
            actualizarExistencias();
            if (cModul.SucursalesActivas.Rows.Count > 0)
            {
                openListadoSinCodigo();
            }
            else
            {
                // Avisa que no existe ninguna categoria para mostrar el reporte
                MessageBox.Show(cModul.SinConexionConSucursales, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                fSucursales.ShowDialog();
            }
        }

        public void openListadoSinCodigo()
        {
            //actualizarExistencias();
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
            if (cModul.SucursalesActivas.Rows.Count > 0)
            {
                OpenListadoCategorias();
            }
            else
            {
                // Avisa que no existe ninguna categoria para mostrar el reporte
                MessageBox.Show(cModul.SinConexionConSucursales, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                fSucursales.ShowDialog();
            }
        }

        private void sucursalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSucursales.ShowDialog();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fCategorias.ShowDialog();
        }

        private void articulosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fOrdenExistencias.ShowDialog();
        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fConfiguracion.ShowDialog();
        }

        private void OpenListadoCategorias()
        {
            //Consulta las categorias 
            cModul.mCategorias = nCategoria.consulta();
            //Valida el numero de Categorias con estado mayor activo
            int numberOfCartegorys = cModul.mCategorias.Select("estado = 1").Length;
            //Valida el numero de categorias activas que existen
            if (numberOfCartegorys < 1)
            {
                // Avisa que no existe ninguna categoria para mostrar el reporte
                MessageBox.Show(cModul.NoExistenCategoriasActivas, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Abre la pantalla para la captura de nuevas categorias
                fCategorias.ShowDialog();
            }
            else // Abre las categorias
            {
                //actualizarExistencias();
                this.fListadoCategorias.MdiParent = this;
                this.fListadoCategorias.WindowState = FormWindowState.Maximized;
                //this.fCategorias.conCodigo = false;
                //this.fCategorias.Initialized = false;
                //this.fCategorias.cmbSucursales.DataSource = cModul.SucursalesActivas;
                //this.fCategorias.cmbSucursales.DisplayMember = "nombre_sucursal";
                //this.fCategorias.cmbSucursales.ValueMember = "id";
                //this.fCategorias.Initialized = true;
                this.fListadoCategorias.Show();
                this.fListadoCategorias.deSeleccionarDataGrids();
                // Definimos si se actva el timer de cambio de sucursarl.
                this.fListadoCategorias.defineTimerChangeSucursal();
            }
        }

        //Define el tiempo en que se ejecuta el timer de sincronización
        public void defineTimerSync()
        {
            int time = Int32.Parse(cModul.TimeSyncSucursal);
            if (time > 0)
            {
                tmrActualizacion.Enabled = true;
                tmrActualizacion.Interval = cDatos.convertirMinToMiliseg(time);
            }
            else
                tmrActualizacion.Enabled = false;
        }
    }
}
