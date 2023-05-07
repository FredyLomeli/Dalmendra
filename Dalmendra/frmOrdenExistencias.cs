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
        string selectedSucursal;
        string selectedCategoria;

        cCategoria nCategoria = new cCategoria();
        cExistencias nExistencias = new cExistencias();
        cOrden nOrden = new cOrden();

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
            // Limpia los valores seleccionados de los combos
            this.selectedSucursal = null;
            this.selectedCategoria = null;
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
            setInicioFinItem(true);
        }

        private void tsbSubir_Click(object sender, EventArgs e)
        {
            setSubirBajarItem(true);
        }

        private void tsbBajar_Click(object sender, EventArgs e)
        {
            setSubirBajarItem(false);
        }

        private void tsbFinal_Click(object sender, EventArgs e)
        {
            setInicioFinItem(false);
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
            // setea los registros seleccionados
            this.selectedSucursal = cmbSucursales.SelectedValue.ToString();
            this.selectedCategoria = cmbCategorias.SelectedValue.ToString();
            // consulta el numero de registros que concuerden con la regla
            int numberOfRecords = cModul.mExistencias.Select("sucursal_id = '" + this.selectedSucursal + "' " +
                "AND descripción LIKE '" + this.selectedCategoria + "%'").Length;
            // Revisa el numero de registros que existen de la categoria
            if (numberOfRecords < 1)
            {
                // Muestra el mensaje indicando que no hay resgitros pertenecientes a esa categoria
                MessageBox.Show(cModul.NoExistenRegistros, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                // regresamos a nullos los valores de los combos
                this.selectedSucursal = null;
                this.selectedCategoria = null;
            }
            else
            {
                // Llenamos la tabla
                llenarTabla();
                // Dehabilita los controles para realizar otra busqueda
                this.btnConsultar.Enabled = false;
                this.cmbCategorias.Enabled = false;
                this.cmbSucursales.Enabled = false;
                // habilita la opción de ordenado
                this.tsbOrdenar.Enabled = true;
            }
        }

        private void llenarTabla()
        {
            // Ordena por la columna de orden los datos de existencia
            cModul.mExistencias.DefaultView.Sort = "orden ASC";
            cModul.mExistencias = cModul.mExistencias.DefaultView.ToTable();
            // Genera el filtrado de los datos que requiere mostrar y hace una copia
            cModul.CatalogoTemporal = cModul.mExistencias.Select("sucursal_id = '" + this.selectedSucursal + "' " +
                "AND descripción LIKE '" + this.selectedCategoria + "%'").CopyToDataTable();
            // Genera otra copia para cargar solo la información que requiere el datagrid
            cModul.CatalogoTemporal.DefaultView.Sort = "orden ASC";
            cModul.CatalogoTemporal = cModul.CatalogoTemporal.DefaultView.ToTable();
            DataTable dt = cModul.CatalogoTemporal.Copy();
            dt.Columns.Remove("orden");
            dt.Columns.Remove("sucursal_id");
            dt.Columns.Remove("id");
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
            inhabilitarCampos();
            limpiarCampos();
            cModul.mCategorias = nCategoria.consulta();
            cModul.banActualizacion = true;
        }

        private void actualizarOrden(int orden, string id)
        {
            // Actualiza el orden de la tabla existencias
            nExistencias.updateOrden(orden, id);
            // Actualiza el orden en los datos offline
            DataRow[] HRow = cModul.mExistencias.Select("id = " + id + "");
            HRow[0]["orden"] = orden;
            // consultamos el codigo del articulo
            string codigo = HRow[0]["Codigo"].ToString();
            // Actualiza el orden de la tabla orden
            nOrden.update(orden, codigo, this.selectedSucursal);
        }

        private void setSubirBajarItem(bool subir)
        {
            // Tomamos el index del item seleccionado
            int indexSelected = dgvOrdenExistencias.SelectedRows[0].Index;
            //seteamos la variable que indicará si sube o baja el item
            int indexForChange;
            // Si sube el articulo, le resta 1 al indice seleccionado
            if (subir)
                indexForChange = dgvOrdenExistencias.SelectedRows[0].Index - 1;
            // Si sube el articulo, le resta 1 al indice seleccionado
            else
                indexForChange = dgvOrdenExistencias.SelectedRows[0].Index + 1;
            // toma el id seleccionado
            string idSelected = cModul.CatalogoTemporal.Rows[indexSelected]["id"].ToString();
            int ordenSelected = Int32.Parse(cModul.CatalogoTemporal.Rows[indexSelected]["orden"].ToString());
            // toma el id a intercambiar
            string idForChange = cModul.CatalogoTemporal.Rows[indexForChange]["id"].ToString();
            int ordenForChange = Int32.Parse(cModul.CatalogoTemporal.Rows[indexForChange]["orden"].ToString());
            // Asctualizamos los ordenes de ambos items
            actualizarOrden(ordenForChange, idSelected);
            actualizarOrden(ordenSelected, idForChange);
            // Actualiza la tabla offline   
            llenarTabla();
            // Seleccionado el item en la tabla
            dgvOrdenExistencias.Rows[indexForChange].Selected = true;
        }

        private void setInicioFinItem(bool inicio)
        {
            // bandera para el numero de registro a setear cuando no es el seleccionado
            int banNumSetOrden;
            // bandera par a indicar si el item seleccinado se va al inicio o al fin del listado
            int banSelectedOrden;

            // Define las variables para indicar si el item se mandará al inicio o al fin de la lista
            if (inicio)
            {
                // en orden desde que numero se le ortorgaran los orden a los items que no estan seleccionados
                banNumSetOrden = 1;
                // el item seleccionado tomará el primer orden del listado ya que lo enviaremos al inicio
                banSelectedOrden = 0;
            }
            // Si sube el articulo, le resta 1 al indice seleccionado
            else
            {
                // en orden desde que numero se le ortorgaran los orden a los items que no estan seleccionados
                banNumSetOrden = 0;
                // el item seleccionado tomará el ultimo orden del listado ya que lo enviaremos al final
                banSelectedOrden = cModul.CatalogoTemporal.Rows.Count - 1;
            }
            // toma el id seleccionado
            int indexSelected = dgvOrdenExistencias.SelectedRows[0].Index;

            // variable que guarda el nuevo orden
            int newOrden = 0;
            // Recorreo el listado para asignar el orden.
            for (int i = 0; i < cModul.CatalogoTemporal.Rows.Count; i++)
            {
                //Toma el ID del for a validar
                string idRowEdit = cModul.CatalogoTemporal.Rows[i]["id"].ToString();
                // Si es el indice seleccionado lo marca como primero en la lista
                if (indexSelected == i)
                    // seteamos el nuevo orden a editar si es el valor seleccionado
                    newOrden = Int32.Parse(cModul.CatalogoTemporal.Rows[banSelectedOrden]["orden"].ToString());
                // Si no es el indice seleccionado lo marca como un consecutivo
                else
                {
                    // seteamos el nuevo orden a editar que sigue en el for
                    newOrden = Int32.Parse(cModul.CatalogoTemporal.Rows[banNumSetOrden]["orden"].ToString());
                    // Aumenta el numero del orden
                    banNumSetOrden++;
                }
                //Actualiza el orden
                actualizarOrden(newOrden, idRowEdit);
            }
            // Actualiza la tabla offline   
            llenarTabla();
            //Selecciona el primer registro del DGV
            dgvOrdenExistencias.Rows[banSelectedOrden].Selected = true;
        }
    }
}
