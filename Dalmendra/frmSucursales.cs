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
    public partial class frmSucursales : Form
    {
        public frmSucursales()
        {
            InitializeComponent();
        }
        cSQLserver nSQLserver = new cSQLserver();
        cSucursal nSucursal = new cSucursal();
        cExistencias nExistencias = new cExistencias();
        // 0 sin estado
        // 1 nuevo
        // 2 edición
        string status = "0";

        private void frmSucursales_Load(object sender, EventArgs e)
        {
            cModul.mSucursalesListado = nSucursal.consultaTodo();
            ConsultarSucursales();
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
            if (validarCampos(txtNombreSucursal.Text.Trim(), txtServidor.Text.Trim(),
                txtDB.Text.Trim(), txtUsuario.Text.Trim(), txtContraseña.Text.Trim()))
            {
                if (nSQLserver.validar_conexion(txtServidor.Text.Trim(), txtDB.Text.Trim(), txtUsuario.Text.Trim(),
                    txtContraseña.Text.Trim()))
                {
                    GuardarSucursal();
                    string fecha;
                    nSQLserver.ConsultarInventario(nSucursal, out fecha);
                    for (int i = 0; i < cModul.mSucursalesListado.Rows.Count; i++)
                    {
                        if (cModul.mSucursalesListado.Rows[i]["ID"].ToString() == nSucursal.id)
                            cModul.mSucursalesListado.Rows[i]["Enlace"] = fecha;
                    }
                    // Consulta las Existencias registradas
                    cModul.mExistencias = nExistencias.consulta();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(cModul.GuardarSinConexion,
                        cModul.NombreDelPrograma, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        GuardarSucursal();
                    }
                }
                // Actualiza el listado de sucursales despues de guardar o actualizar
                ConsultarSucursales();
            }
            else MessageBox.Show(cModul.SucursalValidateError, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void tsbdelete_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(lblID.Text.Trim()))
            {
                DialogResult dialogResult = MessageBox.Show(cModul.PreguntaEliminar(txtNombreSucursal.Text.Trim()),
                    cModul.NombreDelPrograma, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    nSucursal.delete(lblID.Text.Trim());
                    nExistencias.delete(lblID.Text.Trim());
                    cModul.mSucursalesListado = nSucursal.consultaTodo();
                    ConsultarSucursales();

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

        private void tsbTest_Click(object sender, EventArgs e)
        {
            if (status == "0" && !String.IsNullOrEmpty(lblID.Text.Trim()))
            {
                DataRow[] rows = cModul.mSucursales.Select("id ='" + lblID.Text.Trim() + "'");
                if (rows.Count() > 0)
                {
                    MessageBox.Show(nSQLserver.verificar_conexion(txtNombreSucursal.Text.Trim(),
                        txtServidor.Text.Trim(), txtDB.Text.Trim(), txtUsuario.Text.Trim(),
                        rows[0][5].ToString()), cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(cModul.RegistroNoEncontrado, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ConsultarSucursales();
                    inhabilitarCampos();
                    limpiarCampos();
                }
                //string pass = "";
                //if (nSucursal.consulta(out pass, lblID.Text.Trim()))
                //{
                //    MessageBox.Show(nSQLserver.verificar_conexion(txtNombreSucursal.Text.Trim(),
                //        txtServidor.Text.Trim(), txtDB.Text.Trim(), txtUsuario.Text.Trim(),
                //        pass), cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    MessageBox.Show(cModul.RegistroNoEncontrado, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    ConsultarSucursales();
                //    inhabilitarCampos();
                //    limpiarCampos();
                //}
            }
            else
                MessageBox.Show(nSQLserver.verificar_conexion(txtNombreSucursal.Text.Trim(), 
                    txtServidor.Text.Trim(), txtDB.Text.Trim(), txtUsuario.Text.Trim(), 
                    txtContraseña.Text.Trim()), cModul.NombreDelPrograma, MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            inhabilitarCampos();
            limpiarCampos();
        }

        private void habilitarCampos()
        {
            txtNombreSucursal.Enabled = true;
            txtServidor.Enabled = true;
            txtDB.Enabled = true;
            txtUsuario.Enabled = true;
            txtContraseña.Enabled = true;
            tsbNuevo.Enabled = false;
            tsbEditar.Enabled = false;
            tsbGuardar.Enabled = true;
            tsbdelete.Enabled = false;
            tsbCancelar.Enabled = true;
            tsbTest.Enabled = true;
        }

        private void inhabilitarCampos()
        {
            txtNombreSucursal.Enabled = false;
            txtServidor.Enabled = false;
            txtDB.Enabled = false;
            txtUsuario.Enabled = false;
            txtContraseña.Enabled = false;
            tsbNuevo.Enabled = true;
            tsbEditar.Enabled = false;
            tsbGuardar.Enabled = false;
            tsbdelete.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbTest.Enabled = false;
        }

        private void limpiarCampos()
        {
            txtNombreSucursal.Text = "";
            txtServidor.Text = "";
            txtDB.Text = "";
            txtUsuario.Text = "";
            txtContraseña.Text = "";
            lblID.Text = "";
            status = "0";
        }

        private bool validarCampos(string nombre_sucursal, string data_source, string catalog,
            string user_id, string password)
        {
            if (String.IsNullOrEmpty(nombre_sucursal))
                return false;
            else if (String.IsNullOrEmpty(data_source))
                return false;
            else if (String.IsNullOrEmpty(catalog))
                return false;
            else if (String.IsNullOrEmpty(user_id))
                return false;
            else if (String.IsNullOrEmpty(password) && status == "1")
                return false;
            return true;
        }

        private void dgvSucursales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (status == "0")
            {
                // Asigna valor a los campos
                lblID.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[0].Value.ToString();
                txtNombreSucursal.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[1].Value.ToString();
                txtServidor.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[2].Value.ToString();
                txtDB.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[3].Value.ToString();
                txtUsuario.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[4].Value.ToString();
                tsbEditar.Enabled = true;
                tsbdelete.Enabled = true;
                tsbCancelar.Enabled = true;
                tsbTest.Enabled = true;
            }
        }

        private void GuardarSucursal()
        {
            if (status == "1")
            {
                nSucursal.insert(txtNombreSucursal.Text.Trim(), txtServidor.Text.Trim(),
                    txtDB.Text.Trim(), txtUsuario.Text.Trim(), txtContraseña.Text.Trim());
                MessageBox.Show(cModul.SucursalGuardadaCorrecto, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (status == "2")
            {
                if (String.IsNullOrEmpty(txtContraseña.Text.Trim()))
                    nSucursal.updateSinPassword(lblID.Text.Trim(), txtNombreSucursal.Text.Trim(), txtServidor.Text.Trim(),
                        txtDB.Text.Trim(), txtUsuario.Text.Trim());
                else
                    nSucursal.update(lblID.Text.Trim(), txtNombreSucursal.Text.Trim(), txtServidor.Text.Trim(),
                        txtDB.Text.Trim(), txtUsuario.Text.Trim(), txtContraseña.Text.Trim());
                MessageBox.Show(cModul.SucursalActulizadaCorrecto, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cModul.mSucursalesListado = nSucursal.consultaTodo();
            if (status == "1")
            {
                nSucursal.id = cModul.mSucursalesListado.Rows[cModul.mSucursalesListado.Rows.Count - 1]["ID"].ToString();
                nSucursal.data_source = txtServidor.Text.Trim();
                nSucursal.catalog = txtDB.Text.Trim();
                nSucursal.user_id = txtUsuario.Text.Trim();
                nSucursal.password = txtContraseña.Text.Trim();
            }
            else if (status == "2")
            {
                nSucursal.id = lblID.Text.Trim();
                nSucursal.data_source = txtServidor.Text.Trim();
                nSucursal.catalog = txtDB.Text.Trim();
                nSucursal.user_id = txtUsuario.Text.Trim();
                nSucursal.password = txtContraseña.Text.Trim();
            }
            inhabilitarCampos();
            limpiarCampos();
            
        }

        private void ConsultarSucursales() 
        {
            dgvSucursales.DataSource = cModul.mSucursalesListado;
            dgvSucursales.AutoResizeColumns();
            dgvSucursales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // Consulta las sucursales registradas
            cModul.mSucursales = nSucursal.consulta();
        }

        private void frmSucursales_FormClosed(object sender, FormClosedEventArgs e)
        {
            cModul.banActualizacion = true;
        }
    }
}
