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
    public partial class frmCategorias : Form
    {
        public frmCategorias()
        {
            InitializeComponent();
        }
        // Instancia que se usa para el CRUD
        cCategoria nCategoria = new cCategoria();
        // 0 sin estado
        // 1 nuevo
        // 2 edición
        // 3 Ordenando
        string status = "0";

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            cModul.mCateoriasListado = nCategoria.consultaTodo();
            ConsultarCategorias();
            cModul.banActualizacion = false;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            limpiarCampos();
            status = "1";
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            status = "2";
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            if (validarCampos(txtDescripcion.Text.Trim(), txtPalabraClave.Text.Trim(),
                cDatos.convertirBoolToInt(ckbEstado.Checked).ToString(),lblID.Text.Trim()))
            {
                GuardarSucursal();
                ConsultarCategorias();
            }
            else MessageBox.Show(cModul.SucursalValidateError, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tsbdelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblID.Text.Trim()))
            {
                DialogResult dialogResult = MessageBox.Show(cModul.PreguntaEliminar(txtDescripcion.Text.Trim()),
                    cModul.NombreDelPrograma, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    nCategoria.delete();
                    cModul.mCateoriasListado = nCategoria.consultaTodo();
                    ConsultarCategorias();

                    MessageBox.Show(cModul.SucursalEliminadaCorrecto, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                inhabilitarCampos();
                limpiarCampos();
            }
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            inhabilitarCampos();
            limpiarCampos();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            inhabilitarCampos();
            limpiarCampos();
        }

        private void frmCategorias_FormClosed(object sender, FormClosedEventArgs e)
        {
            cModul.mCategorias = nCategoria.consulta();
            cModul.banActualizacion = true;
        }

        private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (status == "0" && dgvCategorias.RowCount > 0)
                {
                    // Asigna valor a los campos
                    lblID.Text = dgvCategorias.Rows[dgvCategorias.SelectedRows[0].Index].Cells[0].Value.ToString();
                    txtDescripcion.Text = dgvCategorias.Rows[dgvCategorias.SelectedRows[0].Index].Cells[1].Value.ToString();
                    txtPalabraClave.Text = dgvCategorias.Rows[dgvCategorias.SelectedRows[0].Index].Cells[2].Value.ToString();
                    ckbEstado.Checked = cDatos.convertirActivoToBool(dgvCategorias.Rows[dgvCategorias.SelectedRows[0].Index].Cells[3].Value.ToString());
                    tsbEditar.Enabled = true;
                    tsbdelete.Enabled = true;
                    tsbCancelar.Enabled = true;
                    // Cargamos los datos al modelos en uso
                    nCategoria.up(txtDescripcion.Text, txtPalabraClave.Text, cDatos.convertirBoolToInt(ckbEstado.Checked).ToString(), lblID.Text);
                }
                else if (status == "3")
                {
                    validaFuncionesOrden();
                }
            }
            catch (Exception exe)
            {
            }
        }

        private void validaFuncionesOrden()
        {
            // Valida el registro seleccionado sea el primero
            if (dgvCategorias.SelectedRows[0].Index == 0)
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
            if (dgvCategorias.SelectedRows[0].Index == cModul.mCateoriasListado.Rows.Count - 1)
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

        // Carga los valores que se tienen en la base de datos y se  muestran en la grilla
        private void ConsultarCategorias(bool ban = true)
        {
            cModul.mCateoriasListado.DefaultView.Sort = "orden ASC";
            cModul.mCateoriasListado = cModul.mCateoriasListado.DefaultView.ToTable();
            DataTable dt = cModul.mCateoriasListado.Copy();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Estado"] = cDatos.convertirIntToActivo(dt.Rows[i]["Estado"].ToString());
            }
            dt.Columns.Remove("orden");
            dgvCategorias.DataSource = dt;
            //Evita que se pueda ordenar las columnas
            for (int i = 0; i < dgvCategorias.Columns.Count; i++)
            {
                dgvCategorias.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvCategorias.AutoResizeColumns();
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // Consulta las sucursales registradas
            if (ban)
                cModul.mCategorias = nCategoria.consulta();
        }

        private void GuardarSucursal()
        {
            if (status == "1")
            {
                nCategoria.insert();
                MessageBox.Show(cModul.CategoriaGuardadaCorrecto, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (status == "2")
            {
                nCategoria.update();
                MessageBox.Show(cModul.CategoriaGuardadaCorrecto, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cModul.mCateoriasListado = nCategoria.consultaTodo();
            inhabilitarCampos();
            limpiarCampos();
        }

        private void habilitarCampos()
        {
            txtDescripcion.Enabled = true;
            txtPalabraClave.Enabled = true;
            ckbEstado.Enabled = true;
            tsbNuevo.Enabled = false;
            tsbEditar.Enabled = false;
            tsbGuardar.Enabled = true;
            tsbdelete.Enabled = false;
            tsbCancelar.Enabled = true;
        }

        private void inhabilitarCampos()
        {
            txtDescripcion.Enabled = false;
            txtPalabraClave.Enabled = false;
            ckbEstado.Enabled = false;
            tsbNuevo.Enabled = true;
            tsbEditar.Enabled = false;
            tsbGuardar.Enabled = false;
            tsbdelete.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbInicio.Enabled = false;
            tsbFinal.Enabled = false;
            tsbSubir.Enabled = false;
            tsbBajar.Enabled = false;
            tsbOrdenar.Enabled = true;
        }

        private void limpiarCampos()
        {
            txtDescripcion.Text = "";
            txtPalabraClave.Text = "";
            ckbEstado.Checked = true;
            lblID.Text = "";
            status = "0";
        }

        private bool validarCampos(string descripcion, string palabra_clave, string estado, string id = null)
        {
            if (String.IsNullOrEmpty(descripcion))
            {
                nCategoria.clean();
                return false;
            }
            else if (String.IsNullOrEmpty(palabra_clave))
            {
                nCategoria.clean();
                return false;
            }
            else if (String.IsNullOrEmpty(estado))
            {
                nCategoria.clean();
                return false;
            }
            else if (status == "2" && String.IsNullOrEmpty(id))
            {
                nCategoria.clean();
                return false;
            }
            nCategoria.up(descripcion, palabra_clave, estado, id);
            return true;
        }

        private void tsbOrdenar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.Rows.Count > 1)
            {
                inhabilitarCampos();
                limpiarCampos();
                this.tsbBajar.Enabled = true;
                this.tsbInicio.Enabled = true;
                this.tsbSubir.Enabled = true;
                this.tsbFinal.Enabled = true;
                this.tsbOrdenar.Enabled = false;
                this.tsbNuevo.Enabled = false;
                this.tsbCancelar.Enabled = true;
                //this.tsbGuardar.Enabled = true;
                this.status = "3";
                validaFuncionesOrden();
            }
            else
                MessageBox.Show(cModul.OrdenDebeSerMayorQueUno, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsbInicio_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvCategorias.SelectedRows[0].Index;
            // Variable que controla el orden de los valores
            int newOrden = 2;
            // Recorreo el listado para signar el orden.
            for (int i = 0; i < cModul.mCateoriasListado.Rows.Count; i++)
            {
                // Si es el indice seleccionado lo marca como primero en la lista
                if (i == indexSeleccted)
                {
                    cModul.mCateoriasListado.Rows[i]["orden"] = 1;
                    nCategoria.updateOrden(cModul.mCateoriasListado.Rows[i]["ID"].ToString(), 1);
                }
                // Si no es el indice seleccionado lo marca como un consecutivo
                else
                {
                    cModul.mCateoriasListado.Rows[i]["orden"] = newOrden;
                    nCategoria.updateOrden(cModul.mCateoriasListado.Rows[i]["ID"].ToString(), newOrden);
                    newOrden++;
                }
            }
            // Actualiza la tabla offline   
            ConsultarCategorias(false);
            dgvCategorias.Rows[0].Selected = true;
        }

        private void tsbSubir_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvCategorias.SelectedRows[0].Index;
            int ordenDesplazado = Int32.Parse(cModul.mCateoriasListado.Rows[indexSeleccted - 1]["orden"].ToString());
            string idOrdenDesplazado = cModul.mCateoriasListado.Rows[indexSeleccted - 1]["ID"].ToString();
            int ordenMover = Int32.Parse(cModul.mCateoriasListado.Rows[indexSeleccted]["orden"].ToString());
            string idOrdenMover = cModul.mCateoriasListado.Rows[indexSeleccted]["ID"].ToString();
            cModul.mCateoriasListado.Rows[dgvCategorias.SelectedRows[0].Index - 1]["orden"] = ordenMover;
            cModul.mCateoriasListado.Rows[dgvCategorias.SelectedRows[0].Index]["orden"] = ordenDesplazado;
            // Actualiza los datos en la DB
            nCategoria.updateOrden(idOrdenDesplazado, ordenMover);
            nCategoria.updateOrden(idOrdenMover, ordenDesplazado);
            // Actualiza la tabla offline   
            ConsultarCategorias(false);
            dgvCategorias.Rows[indexSeleccted - 1].Selected = true;
        }

        private void tsbBajar_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvCategorias.SelectedRows[0].Index;
            int ordenDesplazado = Int32.Parse(cModul.mCateoriasListado.Rows[indexSeleccted + 1]["orden"].ToString());
            string idOrdenDesplazado = cModul.mCateoriasListado.Rows[indexSeleccted + 1]["ID"].ToString();
            int ordenMover = Int32.Parse(cModul.mCateoriasListado.Rows[indexSeleccted]["orden"].ToString());
            string idOrdenMover = cModul.mCateoriasListado.Rows[indexSeleccted]["ID"].ToString();
            cModul.mCateoriasListado.Rows[dgvCategorias.SelectedRows[0].Index + 1]["orden"] = ordenMover;
            cModul.mCateoriasListado.Rows[dgvCategorias.SelectedRows[0].Index]["orden"] = ordenDesplazado;
            // Actualiza los datos en la DB
            nCategoria.updateOrden(idOrdenDesplazado, ordenMover);
            nCategoria.updateOrden(idOrdenMover, ordenDesplazado);
            // Actualiza la tabla offline   
            ConsultarCategorias(false);
            dgvCategorias.Rows[indexSeleccted + 1].Selected = true;
        }

        private void tsbFinal_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvCategorias.SelectedRows[0].Index;
            // Variable que controla el orden de los valores
            int newOrden = 1;
            // Recorreo el listado para signar el orden.
            for (int i = 0; i < cModul.mCateoriasListado.Rows.Count; i++)
            {
                // Si es el indice seleccionado lo marca como primero en la lista
                if (i == indexSeleccted)
                {
                    cModul.mCateoriasListado.Rows[i]["orden"] = cModul.mCateoriasListado.Rows.Count;
                    nCategoria.updateOrden(cModul.mCateoriasListado.Rows[i]["ID"].ToString(),
                        cModul.mCateoriasListado.Rows.Count);
                }
                // Si no es el indice seleccionado lo marca como un consecutivo
                else
                {
                    cModul.mCateoriasListado.Rows[i]["orden"] = newOrden;
                    nCategoria.updateOrden(cModul.mCateoriasListado.Rows[i]["ID"].ToString(), newOrden);
                    newOrden++;
                }
            }
            // Actualiza la tabla offline   
            ConsultarCategorias(false);
            dgvCategorias.Rows[cModul.mCateoriasListado.Rows.Count - 1].Selected = true;
        }
    }
}
