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
    public partial class frmListar : Form
    {
        public frmListar()
        {
            InitializeComponent();
        }
        cSQLserver nSQLserver = new cSQLserver();
        public bool Initialized = false;
        public bool conCodigo = true;

        private void cmbSucursales_SelectedValueChanged(object sender, EventArgs e)
        {
            if(Initialized)
                llenarListado();
        }

        private void frmListar_Load(object sender, EventArgs e)
        {
            llenarListado();
            cModul.EstaAbiertoFrmListado = true;
        }

        public void llenarListado()
        {
            // toma los datos del combobox
            string selecteVal = cmbSucursales.SelectedValue.ToString();
            string selecteTex = cmbSucursales.Text.ToString();
            // indica que surcursal esta mostando
            this.Text = "Existencias " + selecteTex;
            lblSucursalName.Text = selecteTex;
            for (int i = 0; i < cModul.mSucursales.Rows.Count; i++)
            {
                // Carga la ultima fecha registrada de actualización
                if (cModul.mSucursales.Rows[i]["id"].ToString() == selecteVal)
                {
                    lblActualizacion.Text = "Ultima Actualización " + cModul.mSucursales.Rows[i]["fecha_hora_actualizacion"].ToString();
                    // Revisa la conexión de la actual sucursal y muestra un semaforo de estatus.
                    if (nSQLserver.validar_conexion(cModul.mSucursales.Rows[i]["data_source"].ToString(),
                        cModul.mSucursales.Rows[i]["catalog"].ToString(),
                        cModul.mSucursales.Rows[i]["user_id"].ToString(),
                        cModul.mSucursales.Rows[i]["password"].ToString()))
                    {
                        pbxSemaforo.BackColor = Color.Green;
                    }
                    else
                        pbxSemaforo.BackColor = Color.Red;
                }
            }
            // Genera el filtrado de los datos que requiere mostrar y hace una copia
            DataTable sucursalExistencias = cModul.mExistencias.Select("sucursal_id = '" + selecteVal + "'").CopyToDataTable();

            // Elimina la información que no se requiere
            sucursalExistencias.Columns.Remove("sucursal_id");

            // carga y muestra la información en el dataGrid
            dgvListar.DataSource = crearPlantillaColumnas(sucursalExistencias);
            dgvListar.AutoResizeColumns();
            dgvListar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvListar.RowHeadersVisible = false;
        }

        private DataTable crearPlantillaColumnas(DataTable sucursalExistencias)
        {
            // Contamos el total de registros para repartir en 3 filas en el data grid
            int registros = sucursalExistencias.Rows.Count;
            // hacemos el calculo de cuanto será cada fila en el data grid
            double filas = Math.Ceiling(sucursalExistencias.Rows.Count / (double)3);
            // Agregamos los registros en blanco para que se completen 3 filas del mismo tamaño
            for (int i = 0; i < (filas * 3 - registros); i++)
            {
                DataRow Renglon;
                // Generamos registro en blanco para que se acompleten
                Renglon = sucursalExistencias.NewRow();
                sucursalExistencias.Rows.Add(Renglon);
            }
            // Generamos la tabla con el formato de 3 filas para el data grid
            DataTable Tabla = new DataTable();
            // Validamos si lleva codigo el reporte
            if (conCodigo)
            {
                // Asignamos las columnas de la plantilla a rellenar
                Tabla.Columns.Add(new DataColumn("Codigo"));
                Tabla.Columns.Add(new DataColumn("Descripción"));
                Tabla.Columns.Add(new DataColumn("Existencia"));
                Tabla.Columns.Add(new DataColumn("-"));
                Tabla.Columns.Add(new DataColumn("Codigo2"));
                Tabla.Columns.Add(new DataColumn("Descripción2"));
                Tabla.Columns.Add(new DataColumn("Existencia2"));
                Tabla.Columns.Add(new DataColumn("--"));
                Tabla.Columns.Add(new DataColumn("Codigo3"));
                Tabla.Columns.Add(new DataColumn("Descripción3"));
                Tabla.Columns.Add(new DataColumn("Existencia3"));
                //Llenamos el formato con las 3 filas para el data grid
                for (int i = 0; i < filas; i++)
                {
                    DataRow Renglon;
                    // Asignamos 3 registros a la vez.
                    Renglon = Tabla.NewRow();
                    Renglon[0] = sucursalExistencias.Rows[i]["Codigo"].ToString();
                    Renglon[1] = sucursalExistencias.Rows[i]["Descripción"].ToString();
                    Renglon[2] = sucursalExistencias.Rows[i]["Existencia"].ToString();
                    Renglon[4] = sucursalExistencias.Rows[i + (int)filas]["Codigo"].ToString();
                    Renglon[5] = sucursalExistencias.Rows[i + (int)filas]["Descripción"].ToString();
                    Renglon[6] = sucursalExistencias.Rows[i + (int)filas]["Existencia"].ToString();
                    Renglon[8] = sucursalExistencias.Rows[i + (int)filas * 2]["Codigo"].ToString();
                    Renglon[9] = sucursalExistencias.Rows[i + (int)filas * 2]["Descripción"].ToString();
                    Renglon[10] = sucursalExistencias.Rows[i + (int)filas * 2]["Existencia"].ToString();
                    Tabla.Rows.Add(Renglon);
                }
            }
            else
            {
                // Asignamos las columnas de la plantilla a rellenar
                Tabla.Columns.Add(new DataColumn("Descripción"));
                Tabla.Columns.Add(new DataColumn("Existencia"));
                Tabla.Columns.Add(new DataColumn("-"));
                Tabla.Columns.Add(new DataColumn("Descripción2"));
                Tabla.Columns.Add(new DataColumn("Existencia2"));
                Tabla.Columns.Add(new DataColumn("--"));
                Tabla.Columns.Add(new DataColumn("Descripción3"));
                Tabla.Columns.Add(new DataColumn("Existencia3"));
                //Llenamos el formato con las 3 filas para el data grid
                for (int i = 0; i < filas; i++)
                {
                    DataRow Renglon;
                    // Asignamos 3 registros a la vez.
                    Renglon = Tabla.NewRow();
                    Renglon[0] = sucursalExistencias.Rows[i]["Descripción"].ToString();
                    Renglon[1] = sucursalExistencias.Rows[i]["Existencia"].ToString();
                    Renglon[3] = sucursalExistencias.Rows[i + (int)filas]["Descripción"].ToString();
                    Renglon[4] = sucursalExistencias.Rows[i + (int)filas]["Existencia"].ToString();
                    Renglon[6] = sucursalExistencias.Rows[i + (int)filas * 2]["Descripción"].ToString();
                    Renglon[7] = sucursalExistencias.Rows[i + (int)filas * 2]["Existencia"].ToString();
                    Tabla.Rows.Add(Renglon);
                }
            }
            return Tabla;
        }

        private void frmListar_FormClosing(object sender, FormClosingEventArgs e)
        {
            cModul.EstaAbiertoFrmListado = false;
            //Hiding the window, because closing it makes the window unaccessible.
            this.Hide();
            this.Parent = null;
            e.Cancel = true; //hides the form, cancels closing event
        }
    }
}
