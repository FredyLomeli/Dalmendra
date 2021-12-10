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
            cModul.banActualizacion = true;
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
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
        }

        // Carga los valores que se tienen en la base de datos y se  muestran en la grilla
        private void ConsultarCategorias()
        {
            DataTable dt = cModul.mCateoriasListado;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Estado"] = cDatos.convertirIntToActivo(dt.Rows[i]["Estado"].ToString());
            }
            dgvCategorias.DataSource = dt;
            dgvCategorias.AutoResizeColumns();
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
    }
}
