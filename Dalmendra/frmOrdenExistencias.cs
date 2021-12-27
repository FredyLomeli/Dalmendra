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
    public partial class frmOrdenExistencias : Form
    {
        public frmOrdenExistencias()
        {
            InitializeComponent();
        }

        string status;

        cCategoria nCategoria = new cCategoria();
        cExistencias nExistencias = new cExistencias();

        private void frmOrdenExistencias_Load(object sender, EventArgs e)
        {
            cModul.banActualizacion = false;
            cModul.mCategorias = nCategoria.consulta();
            CargarCmbSucursales();
            CargarCmbCategorias();
            this.status = "0";
        }

        private void CargarCmbSucursales()
        {
            this.cmbSucursales.DataSource = cModul.SucursalesActivas;
            this.cmbSucursales.DisplayMember = "nombre_sucursal";
            this.cmbSucursales.ValueMember = "id";
        }

        private void CargarCmbCategorias()
        {
            this.cmbCategorias.DataSource = cModul.mCategorias;
            this.cmbCategorias.DisplayMember = "descripcion";
            this.cmbCategorias.ValueMember = "palabra_clave";
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            inhabilitarCampos();
            limpiarCampos();
        }

        private void inhabilitarCampos()
        {
            // deshabilita la operación de ordenamiento
            tsbOrdenar.Enabled = false;
            tsbBajar.Enabled = false;
            tsbInicio.Enabled = false;
            tsbSubir.Enabled = false;
            tsbFinal.Enabled = false;
            // habilita la consulta
            cmbSucursales.Enabled = true;
            cmbCategorias.Enabled = true;
            btnConsultar.Enabled = true;
        }

        private void limpiarCampos()
        {
            // Limpia la variable temporal
            cModul.CatalogoTemporal = null;
            // Limpia el datadrig view
            dgvOrdenExistencias.DataSource = cModul.CatalogoTemporal;
        }

        private void tsbOrdenar_Click(object sender, EventArgs e)
        {
            if (dgvOrdenExistencias.Rows.Count > 1)
            {
                this.tsbBajar.Enabled = true;
                this.tsbInicio.Enabled = true;
                this.tsbSubir.Enabled = true;
                this.tsbFinal.Enabled = true;
                this.tsbOrdenar.Enabled = false;
                //this.tsbGuardar.Enabled = true;
                this.status = "3";
                validaFuncionesOrden();
            }
            else
                MessageBox.Show(cModul.OrdenDebeSerMayorQueUno, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void validaFuncionesOrden()
        {
            // Valida el registro seleccionado sea el primero
            if (dgvOrdenExistencias.SelectedRows[0].Index == 0)
            {
                this.tsbInicio.Enabled = false;
                this.tsbSubir.Enabled = false;
            }
            else
            {
                this.tsbInicio.Enabled = true;
                this.tsbSubir.Enabled = true;
            }
            // Valida el registro seleccionado sea el ultimo
            if (dgvOrdenExistencias.SelectedRows[0].Index == cModul.CatalogoTemporal.Rows.Count - 1)
            {
                this.tsbFinal.Enabled = false;
                this.tsbBajar.Enabled = false;
            }
            else
            {
                this.tsbFinal.Enabled = true;
                this.tsbBajar.Enabled = true;
            }
        }

        private void tsbInicio_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvOrdenExistencias.SelectedRows[0].Index;
            // Variable que controla el orden de los valores
            int newOrden = 2;
            // Valida los registros seleccionados
            string selecteSucursal = cmbSucursales.SelectedValue.ToString();
            string selecteCategoria = cmbCategorias.SelectedValue.ToString();
            // Recorreo el listado para signar el orden.
            for (int i = 0; i < cModul.CatalogoTemporal.Rows.Count; i++)
            {
                // Si es el indice seleccionado lo marca como primero en la lista
                if (i == indexSeleccted)
                {
                    // Actualiza el orden en la tabla temporal
                    //cModul.CatalogoTemporal.Rows[i]["orden"] = 1;
                    //Actualiza el orden en la base de datos
                    nExistencias.updateOrden(1 ,cModul.CatalogoTemporal.Rows[i]["Codigo"].ToString(), selecteSucursal);
                    // Actualiza el orden en los datos offline
                    DataRow[] HRow = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                    "AND codigo = '" + cModul.CatalogoTemporal.Rows[i]["Codigo"].ToString() + "'");
                    HRow[0]["orden"] = 1;
                }
                // Si no es el indice seleccionado lo marca como un consecutivo
                else
                {
                    // Actualiza el orden en la tabla temporal
                    //cModul.CatalogoTemporal.Rows[i]["orden"] = newOrden;
                    //Actualiza el orden en la base de datos
                    nExistencias.updateOrden(newOrden, cModul.CatalogoTemporal.Rows[i]["Codigo"].ToString(), selecteSucursal);
                    // Actualiza el orden en los datos offline
                    DataRow[] HRow = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                    "AND codigo = '" + cModul.CatalogoTemporal.Rows[i]["Codigo"].ToString() + "'");
                    HRow[0]["orden"] = newOrden;
                    // Aumenta el numero del orden
                    newOrden++;
                }
            }
            // Actualiza la tabla offline   
            llenarTabla(selecteSucursal, selecteCategoria);
            dgvOrdenExistencias.Rows[0].Selected = true;
        }

        private void tsbSubir_Click(object sender, EventArgs e)
        {
            // Valida los registros seleccionados
            string selecteSucursal = cmbSucursales.SelectedValue.ToString();
            string selecteCategoria = cmbCategorias.SelectedValue.ToString();
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvOrdenExistencias.SelectedRows[0].Index;
            int ordenDesplazado = Int32.Parse(cModul.CatalogoTemporal.Rows[indexSeleccted - 1]["orden"].ToString());
            string codigoOrdenDesplazado = cModul.CatalogoTemporal.Rows[indexSeleccted - 1]["Codigo"].ToString();
            int ordenMover = Int32.Parse(cModul.CatalogoTemporal.Rows[indexSeleccted]["orden"].ToString());
            string codigoOrdenMover = cModul.CatalogoTemporal.Rows[indexSeleccted]["Codigo"].ToString();
            //Actualiza los datos de la tabla de datos offline
            DataRow[] MRow = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                    "AND codigo = '" + codigoOrdenDesplazado + "'");
            MRow[0]["orden"] = ordenMover;
            DataRow[] DRow = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                    "AND codigo = '" + codigoOrdenMover + "'");
            DRow[0]["orden"] = ordenDesplazado;
            // Actualiza los datos en la DB
            nExistencias.updateOrden(ordenMover, codigoOrdenDesplazado, selecteSucursal);
            nExistencias.updateOrden(ordenDesplazado, codigoOrdenMover, selecteSucursal);
            // Actualiza la tabla offline   
            llenarTabla(selecteSucursal, selecteCategoria);
            dgvOrdenExistencias.Rows[indexSeleccted - 1].Selected = true;
        }

        private void tsbBajar_Click(object sender, EventArgs e)
        {
            // Valida los registros seleccionados
            string selecteSucursal = cmbSucursales.SelectedValue.ToString();
            string selecteCategoria = cmbCategorias.SelectedValue.ToString();
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvOrdenExistencias.SelectedRows[0].Index;
            int ordenDesplazado = Int32.Parse(cModul.CatalogoTemporal.Rows[indexSeleccted + 1]["orden"].ToString());
            string codigoOrdenDesplazado = cModul.CatalogoTemporal.Rows[indexSeleccted + 1]["Codigo"].ToString();
            int ordenMover = Int32.Parse(cModul.CatalogoTemporal.Rows[indexSeleccted]["orden"].ToString());
            string codigoOrdenMover = cModul.CatalogoTemporal.Rows[indexSeleccted]["Codigo"].ToString();
            //Actualiza los datos de la tabla de datos offline
            DataRow[] MRow = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                    "AND codigo = '" + codigoOrdenDesplazado + "'");
            MRow[0]["orden"] = ordenMover;
            DataRow[] DRow = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                    "AND codigo = '" + codigoOrdenMover + "'");
            DRow[0]["orden"] = ordenDesplazado;
            // Actualiza los datos en la DB
            nExistencias.updateOrden(ordenMover, codigoOrdenDesplazado, selecteSucursal);
            nExistencias.updateOrden(ordenDesplazado, codigoOrdenMover, selecteSucursal);
            // Actualiza la tabla offline   
            llenarTabla(selecteSucursal, selecteCategoria);
            dgvOrdenExistencias.Rows[indexSeleccted + 1].Selected = true;
        }

        private void tsbFinal_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvOrdenExistencias.SelectedRows[0].Index;
            // Variable que controla el orden de los valores
            int newOrden = 1;
            // Valida los registros seleccionados
            string selecteSucursal = cmbSucursales.SelectedValue.ToString();
            string selecteCategoria = cmbCategorias.SelectedValue.ToString();
            // Recorreo el listado para signar el orden.
            for (int i = 0; i < cModul.CatalogoTemporal.Rows.Count; i++)
            {
                // Si es el indice seleccionado lo marca como primero en la lista
                if (i == indexSeleccted)
                {
                    // Actualiza el orden en la tabla temporal
                    //cModul.CatalogoTemporal.Rows[i]["orden"] = 1;
                    //Actualiza el orden en la base de datos
                    nExistencias.updateOrden(cModul.CatalogoTemporal.Rows.Count, 
                        cModul.CatalogoTemporal.Rows[i]["Codigo"].ToString(), selecteSucursal);
                    // Actualiza el orden en los datos offline
                    DataRow[] HRow = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                    "AND codigo = '" + cModul.CatalogoTemporal.Rows[i]["Codigo"].ToString() + "'");
                    HRow[0]["orden"] = cModul.CatalogoTemporal.Rows.Count;
                }
                // Si no es el indice seleccionado lo marca como un consecutivo
                else
                {
                    // Actualiza el orden en la tabla temporal
                    //cModul.CatalogoTemporal.Rows[i]["orden"] = newOrden;
                    //Actualiza el orden en la base de datos
                    nExistencias.updateOrden(newOrden, cModul.CatalogoTemporal.Rows[i]["Codigo"].ToString(), selecteSucursal);
                    // Actualiza el orden en los datos offline
                    DataRow[] HRow = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                    "AND codigo = '" + cModul.CatalogoTemporal.Rows[i]["Codigo"].ToString() + "'");
                    HRow[0]["orden"] = newOrden;
                    // Aumenta el numero del orden
                    newOrden++;
                }
            }
            // Actualiza la tabla offline   
            llenarTabla(selecteSucursal, selecteCategoria);
            dgvOrdenExistencias.Rows[cModul.CatalogoTemporal.Rows.Count - 1].Selected = true;
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            inhabilitarCampos();
            limpiarCampos();
        }

        private void dgvOrdenExistencias_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                 if (status == "3")
                {
                    validaFuncionesOrden();
                }
            }
            catch (Exception exe)
            {
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // Valida los registros seleccionados
            string selecteSucursal = cmbSucursales.SelectedValue.ToString();
            string selecteCategoria = cmbCategorias.SelectedValue.ToString();
            // consulta el numero de registros que concuerden con la regla
            int numberOfRecords = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                "AND descripción LIKE '" + selecteCategoria + "%'").Length;
            // Revisa el numero de registros que existen de la categoria
            if (numberOfRecords < 1)
                MessageBox.Show(cModul.NoExistenRegistros, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                // Llenamos la tabla
                llenarTabla(selecteSucursal, selecteCategoria);
                // Dehabilita los controles para realizar otra busqueda
                this.btnConsultar.Enabled = false;
                this.cmbCategorias.Enabled = false;
                this.cmbSucursales.Enabled = false;
                // habilita la opción de ordenado
                this.tsbOrdenar.Enabled = true;
            }
        }

        private void llenarTabla(string selecteSucursal, string selecteCategoria)
        {
            // Ordena por la columna de orden los datos de existencia
            cModul.mExistencias.DefaultView.Sort = "orden ASC";
            cModul.mExistencias = cModul.mExistencias.DefaultView.ToTable();
            // Genera el filtrado de los datos que requiere mostrar y hace una copia
            cModul.CatalogoTemporal = cModul.mExistencias.Select("sucursal_id = '" + selecteSucursal + "' " +
                "AND descripción LIKE '" + selecteCategoria + "%'").CopyToDataTable();
            // Genera otra copia para cargar solo la información que requiere el datagrid
            cModul.CatalogoTemporal.DefaultView.Sort = "orden ASC";
            cModul.CatalogoTemporal = cModul.CatalogoTemporal.DefaultView.ToTable();
            DataTable dt = cModul.CatalogoTemporal.Copy();
            dt.Columns.Remove("orden");
            dt.Columns.Remove("sucursal_id");
            //Carga la información a la grilla para mostrar y ordenar
            dgvOrdenExistencias.DataSource = dt;
            //Evita que se pueda ordenar las columnas
            for (int i = 0; i < dgvOrdenExistencias.Columns.Count; i++)
            {
                dgvOrdenExistencias.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Toma el tama;o de las columnas
            dgvOrdenExistencias.AutoResizeColumns();
            dgvOrdenExistencias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void frmOrdenExistencias_FormClosing(object sender, FormClosingEventArgs e)
        {
            cModul.mCategorias = nCategoria.consulta();
            cModul.banActualizacion = true;
        }
    }
}
