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
        cMySQL nMySQL = new cMySQL();
        int conteo = 0;
        // 0 sin estado
        // 1 nuevo
        // 2 edición
        // 3 Ordenando
        string status;

        private void frmSucursales_Load(object sender, EventArgs e)
        {
            cModul.mSucursalesListado = nSucursal.consultaTodo();
            ConsultarSucursales();
            cModul.banActualizacion = false;
            this.conteo = cModul.mSucursalesListado.Rows.Count;
            status = "0";
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
                if(String.IsNullOrEmpty(txtContraseña.Text.Trim()) && status == "2")
                {
                    GuardarSucursal();
                }
                else if (nSQLserver.validar_conexion(txtServidor.Text.Trim(), txtDB.Text.Trim(), txtUsuario.Text.Trim(),
                    txtContraseña.Text.Trim()))
                {
                    GuardarSucursal();
                    string fecha, error;
                    //nSQLserver.ConsultarInventario(nSucursal, out fecha);
                    // Actualización para evitar que el programa se rompa al tener problemas de conexión 
                    if (nSQLserver.ConsultarInventario(nSucursal, out fecha, out error))
                        // Avisa que no existe ninguna categoria para mostrar el reporte
                        //MessageBox.Show(error, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // reune los errores a mostrar
                        cModul.ErrorSincronizacion = cModul.ErrorSincronizacion + error;

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
                    nExistencias.deletePorSucursal(lblID.Text.Trim());
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
            this.status = "";
        }

        private void habilitarCampos()
        {
            this.txtNombreSucursal.Enabled = true;
            this.txtServidor.Enabled = true;
            this.txtDB.Enabled = true;
            this.txtUsuario.Enabled = true;
            this.txtContraseña.Enabled = true;
            this.tsbNuevo.Enabled = false;
            this.tsbEditar.Enabled = false;
            this.tsbGuardar.Enabled = true;
            this.tsbdelete.Enabled = false;
            this.tsbCancelar.Enabled = true;
            this.tsbTest.Enabled = true;
            this.btnColor.Enabled = true;
        }

        private void inhabilitarCampos()
        {
            this.txtNombreSucursal.Enabled = false;
            this.txtServidor.Enabled = false;
            this.txtDB.Enabled = false;
            this.txtUsuario.Enabled = false;
            this.txtContraseña.Enabled = false;
            this.tsbNuevo.Enabled = true;
            this.tsbEditar.Enabled = false;
            this.tsbGuardar.Enabled = false;
            this.tsbdelete.Enabled = false;
            this.tsbCancelar.Enabled = false;
            this.tsbTest.Enabled = false;
            this.tsbSubir.Enabled = false;
            this.tsbBajar.Enabled = false;
            this.tsbOrdenar.Enabled = true;
            this.tsbFinal.Enabled = false;
            this.tsbInicio.Enabled = false;
            this.btnColor.Enabled = false;
            this.pbxColorSuc.BackColor = cDatos.getColorFromArgb("NOCOLOR");
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

        private void dgvSucursales_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (status == "0")
                {
                    // Asigna valor a los campos
                    lblID.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[0].Value.ToString();
                    txtNombreSucursal.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[1].Value.ToString();
                    txtServidor.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[2].Value.ToString();
                    txtDB.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[3].Value.ToString();
                    txtUsuario.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[4].Value.ToString();
                    pbxColorSuc.BackColor = cDatos.getColorFromArgb(dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[6].Value.ToString());
                    lblSetColor.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[6].Value.ToString();
                    tsbEditar.Enabled = true;
                    tsbdelete.Enabled = true;
                    tsbCancelar.Enabled = true;
                    tsbTest.Enabled = true;
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
            if (dgvSucursales.SelectedRows[0].Index == 0)
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
            if (dgvSucursales.SelectedRows[0].Index == cModul.mSucursalesListado.Rows.Count - 1)
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

        private void GuardarSucursal()
        {
            if (status == "1")
            {
               this.conteo = this.conteo + 1;
                nSucursal.insert(txtNombreSucursal.Text.Trim(), txtServidor.Text.Trim(), txtDB.Text.Trim(),
                    txtUsuario.Text.Trim(), txtContraseña.Text.Trim(), this.conteo, lblSetColor.Text.Trim());
                MessageBox.Show(cModul.SucursalGuardadaCorrecto, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (status == "2")
            {
                if (String.IsNullOrEmpty(txtContraseña.Text.Trim()))
                    nSucursal.updateSinPassword(lblID.Text.Trim(), txtNombreSucursal.Text.Trim(), txtServidor.Text.Trim(),
                        txtDB.Text.Trim(), txtUsuario.Text.Trim(), lblSetColor.Text.Trim());
                else
                    nSucursal.update(lblID.Text.Trim(), txtNombreSucursal.Text.Trim(), txtServidor.Text.Trim(),
                        txtDB.Text.Trim(), txtUsuario.Text.Trim(), txtContraseña.Text.Trim(), lblSetColor.Text.Trim());
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

        private void ConsultarSucursales(bool ban = true) 
        {
            cModul.mSucursalesListado.DefaultView.Sort = "orden ASC";
            cModul.mSucursalesListado = cModul.mSucursalesListado.DefaultView.ToTable();
            DataTable dt = cModul.mSucursalesListado.Copy();
            dt.Columns.Remove("orden");
            dgvSucursales.DataSource = dt;
            //Evita que se pueda ordenar las columnas
            for (int i = 0; i < dgvSucursales.Columns.Count; i++)
            {
                dgvSucursales.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvSucursales.AutoResizeColumns();
            dgvSucursales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // Consulta las sucursales registradas
            if(ban)
                cModul.mSucursales = nSucursal.consulta();
        }

        private void frmSucursales_FormClosed(object sender, FormClosedEventArgs e)
        {
            cModul.banActualizacion = true;
            this.status = "";
        }

        private void tsbOrdenar_Click(object sender, EventArgs e)
        {
            if (dgvSucursales.Rows.Count > 1)
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

        private void tsbBajar_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvSucursales.SelectedRows[0].Index;
            int ordenDesplazado = Int32.Parse(cModul.mSucursalesListado.Rows[indexSeleccted + 1]["orden"].ToString());
            string idOrdenDesplazado = cModul.mSucursalesListado.Rows[indexSeleccted + 1]["ID"].ToString();
            int ordenMover = Int32.Parse(cModul.mSucursalesListado.Rows[indexSeleccted]["orden"].ToString());
            string idOrdenMover = cModul.mSucursalesListado.Rows[indexSeleccted]["ID"].ToString();
            cModul.mSucursalesListado.Rows[dgvSucursales.SelectedRows[0].Index + 1]["orden"] = ordenMover;
            cModul.mSucursalesListado.Rows[dgvSucursales.SelectedRows[0].Index]["orden"] = ordenDesplazado;
            // Actualiza los datos en la DB
            nSucursal.updateOrden(idOrdenDesplazado, ordenMover);
            nSucursal.updateOrden(idOrdenMover, ordenDesplazado);
            // Actualiza la tabla offline   
            ConsultarSucursales(false);
            dgvSucursales.Rows[indexSeleccted + 1].Selected = true;
        }

        private void tsbSubir_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvSucursales.SelectedRows[0].Index;
            int ordenDesplazado = Int32.Parse(cModul.mSucursalesListado.Rows[ indexSeleccted -1]["orden"].ToString());
            string idOrdenDesplazado = cModul.mSucursalesListado.Rows[indexSeleccted - 1]["ID"].ToString();
            int ordenMover = Int32.Parse(cModul.mSucursalesListado.Rows[indexSeleccted]["orden"].ToString());
            string idOrdenMover = cModul.mSucursalesListado.Rows[indexSeleccted]["ID"].ToString();
            cModul.mSucursalesListado.Rows[dgvSucursales.SelectedRows[0].Index - 1]["orden"] = ordenMover;
            cModul.mSucursalesListado.Rows[dgvSucursales.SelectedRows[0].Index]["orden"] = ordenDesplazado;
            // Actualiza los datos en la DB
            nSucursal.updateOrden(idOrdenDesplazado, ordenMover);
            nSucursal.updateOrden(idOrdenMover, ordenDesplazado);
            // Actualiza la tabla offline   
            ConsultarSucursales(false);
            dgvSucursales.Rows[indexSeleccted - 1].Selected = true;
        }

        private void tsbInicio_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvSucursales.SelectedRows[0].Index;
            // Variable que controla el orden de los valores
            int newOrden = 2;
            // Recorreo el listado para signar el orden.
            for (int i = 0; i < cModul.mSucursalesListado.Rows.Count; i++)
            {
                // Si es el indice seleccionado lo marca como primero en la lista
                if (i == indexSeleccted)
                {
                    cModul.mSucursalesListado.Rows[i]["orden"] = 1;
                    nSucursal.updateOrden(cModul.mSucursalesListado.Rows[i]["ID"].ToString(), 1);
                }
                // Si no es el indice seleccionado lo marca como un consecutivo
                else
                {
                    cModul.mSucursalesListado.Rows[i]["orden"] = newOrden;
                    nSucursal.updateOrden(cModul.mSucursalesListado.Rows[i]["ID"].ToString(), newOrden);
                    newOrden++;
                }
            }   
            // Actualiza la tabla offline   
            ConsultarSucursales(false);
            dgvSucursales.Rows[0].Selected = true;
        }

        private void tsbFinal_Click(object sender, EventArgs e)
        {
            // Intercambia el orden de los valores seleccionados.
            int indexSeleccted = dgvSucursales.SelectedRows[0].Index;
            // Variable que controla el orden de los valores
            int newOrden = 1;
            // Recorreo el listado para signar el orden.
            for (int i = 0; i < cModul.mSucursalesListado.Rows.Count; i++)
            {
                // Si es el indice seleccionado lo marca como primero en la lista
                if (i == indexSeleccted)
                {
                    cModul.mSucursalesListado.Rows[i]["orden"] = cModul.mSucursalesListado.Rows.Count;
                    nSucursal.updateOrden(cModul.mSucursalesListado.Rows[i]["ID"].ToString(),
                        cModul.mSucursalesListado.Rows.Count);
                }
                // Si no es el indice seleccionado lo marca como un consecutivo
                else
                {
                    cModul.mSucursalesListado.Rows[i]["orden"] = newOrden;
                    nSucursal.updateOrden(cModul.mSucursalesListado.Rows[i]["ID"].ToString(), newOrden);
                    newOrden++;
                }
            }
            // Actualiza la tabla offline   
            ConsultarSucursales(false);
            dgvSucursales.Rows[cModul.mSucursalesListado.Rows.Count - 1].Selected = true;
        }

        private void dgvSucursales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (status == "0" && lblID.Text == "")
                {
                    // Asigna valor a los campos
                    lblID.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[0].Value.ToString();
                    txtNombreSucursal.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[1].Value.ToString();
                    txtServidor.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[2].Value.ToString();
                    txtDB.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[3].Value.ToString();
                    txtUsuario.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[4].Value.ToString();
                    pbxColorSuc.BackColor = cDatos.getColorFromArgb(dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[6].Value.ToString());
                    lblSetColor.Text = dgvSucursales.Rows[dgvSucursales.SelectedRows[0].Index].Cells[6].Value.ToString();
                    tsbEditar.Enabled = true;
                    tsbdelete.Enabled = true;
                    tsbCancelar.Enabled = true;
                    tsbTest.Enabled = true;
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

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (cdgColor.ShowDialog() == DialogResult.OK)
            {
                //btnColor.BackColor = cdgColor.Color;
                pbxColorSuc.BackColor = cdgColor.Color;
                lblSetColor.Text = pbxColorSuc.BackColor.A.ToString() + "|" +
                    pbxColorSuc.BackColor.R.ToString() + "|" +
                    pbxColorSuc.BackColor.G.ToString() + "|" +
                    pbxColorSuc.BackColor.B.ToString();
                //pbxColorSuc.BackColor = cDatos.getColorFromArgb(lblColor.Text.Trim());
            }  
        }

        private void btnMysql_Click(object sender, EventArgs e)
        {
            MessageBox.Show(nMySQL.verificar_mysql_conexion(), cModul.NombreDelPrograma,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
