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
    public partial class frmListadoCategorias : Form
    {
        public frmListadoCategorias()
        {
            InitializeComponent();
        }
        cCategoria nCategoria = new cCategoria();
        int columna = 0;
        int conteoDataGrid = 1;
        int panelPxIni = 0;
        int panelPxFin = 0;
        int panelLargo = 0;
        int gridLargo = 0;
        string panelName = "No Name";

        int maxPx = 640;
        int colDescripcionLargo = 380;
        int colExistenciaLargo = 60;
        int celdaExistenciaLargo = 22;
        int panelAncho = 450;
        int sizeLetra = 17;
        int posicionVerticalDataGrid = 23;
        int espacioInferiorPanel = 30;

        //int maxPx = 620;
        //int colDescripcionLargo = 280;
        //int colExistenciaLargo = 90;
        //int celdaExistenciaLargo = 22;
        //int panelAncho = 380;
        //int sizeLetra = 12;
        //int posicionVerticalDataGrid = 18;
        //int espacioInferiorPanel = 23;

        // Valores de los botones de cada sucursal
        string btnSucursal1 = "0";
        string btnSucursal2 = "0";
        string btnSucursal3 = "0";
        string btnSucursal4 = "0";
        string btnSucursal5 = "0";
        string btnSucursal6 = "0";
        string btnSucursal7 = "0";
        string btnSucursal8 = "0";
        string btnSucursal9 = "0";
        string btnSucursal10 = "0";

        private void frmListadoCategorias_Load(object sender, EventArgs e)
        {
            cModul.EstaAbiertoFrmCategorias = true;
            crearVistaCategorias();
            defineTimerChangeSucursal();
        }

        // Valida que el ID asingado este entre las sucursales activas,
        // De lo contrario le asigna el primer ID de la lista segun su orden
        public void asignDbId()
        {
            // Valida que exita la Base de datos seleccionada y este activa
            int numberOfRecords = cModul.SucursalesActivas.Select("id = '" + cModul.IdDbSelect + "'").Length;
            if (numberOfRecords == 0)
            {
                // si no esta activa la base de datos seleccionada valida que tengamos mas 1 una sucursal activa
                if (cModul.SucursalesActivas.Rows.Count > 0)
                {
                    // Si hay mas de una DB activa asigna la primera como seleccionada
                    cModul.IdDbSelect = cModul.SucursalesActivas.Rows[0][0].ToString();
                }
                else
                {
                    // Avisa que no existe ninguna categoria para mostrar el reporte
                    MessageBox.Show(cModul.SinConexionConSucursales, cModul.NombreDelPrograma, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        // Cambio de SelectedDbID segun el temporisador
        public void changeDbId()
        {
            // Valida que existan mas de 2 sucursales activas
            if (cModul.SucursalesActivas.Rows.Count > 1)
            {
                // Bandera para identificar el valor a seleccionar
                bool banNextId = false;
                // recorremos todas las sucursales activas
                foreach (DataRow dr in cModul.SucursalesActivas.Rows)
                {
                    // si esta activa la bandera asina la sucursal
                    if (banNextId)
                    {
                        // asigna el ID de la sucursal
                        cModul.IdDbSelect = dr[0].ToString();
                        // desactiva la bandera de la sucursal
                        banNextId = false;
                    }
                    // Una vez identificado la sucursal activa habilita la bandera para asignar el siguiente registor como selecicionado
                    else if (dr[0].ToString() == cModul.IdDbSelect)
                        banNextId = true;
                }
                //si era el ultimo registro y quedo activa la bandera se asigna el primer registor solo DB seleccionada
                if(banNextId)
                    cModul.IdDbSelect = cModul.SucursalesActivas.Rows[0][0].ToString();
                // Despues de Cambiar el DB ID se dibuja la tabla
                crearVistaCategorias();
            }
        }

        public void ListarBtnSucursal()
        {
            //Ocultamos los botones
            ocultarBtnStatusBar();
            // Definimos la variable contador del foreach
            int i = 1;
            // Recorremos las sucursales activas
            foreach (DataRow dr in cModul.SucursalesActivas.Rows)
            {
                // Si el ID es el seleccionado muestra el estatus y marca el boton
                if (dr[0].ToString() == cModul.IdDbSelect)
                {
                    // Titula el formulario con el nombre de la sucursal
                    this.Text = dr[1].ToString();
                    // Muestra la ultima actualizacion de la sucursal
                    tsslBarraEstado.Text = "Sucursal " + dr[1].ToString() +
                        " Ultima Actualización " + dr[2].ToString();
                    // Crea el boton en la barra de estado y le da valor seleccionado
                    mostrarBtnStatusBar(i, dr, true);
                    // Muestra la configuracion de sincronizacion
                    if(cModul.TimeSyncSucursal == "0")
                        tsslSincronizacion.Text = " | Sincronización Inactiva.";
                    else
                        tsslSincronizacion.Text = " | Sincronización cada " + cModul.TimeSyncSucursal + " min.";
                    //Muestra la configuracion de cambio de sucursal
                    if (cModul.TimeChangeSucursal == "0")
                        tsslChambioSucursal.Text = " | Cambio de sucursal automatico Inactivo.";
                    else
                        tsslChambioSucursal.Text = " | Cambio de sucursal automatico cada " + cModul.TimeChangeSucursal + " min";
                    //Carga el color de fondo de la sucursal.
                    this.BackColor = cDatos.getColorFromArgb(dr[3].ToString());
                }
                // Agrega la sucursal a los botones
                else mostrarBtnStatusBar(i, dr, false);
                // Incrementa en 1 el valor que recorre las sucursales
                i++;
            }
        }

        public void crearVistaCategorias()
        {
            // Oculta los paneles activos para volver a reacalcularlos
            this.ocultarTablas();
            // Asigna el ID de sucursal seleccionado
            this.asignDbId();
            // Lista las sucursales activas
            this.ListarBtnSucursal();
            //Revisa las categorias activas
            DataTable data = cModul.mCategorias.Select("estado = 1").CopyToDataTable();
            //Recorre las categorias
            foreach (DataRow dr in data.Rows)
            {
                // consulta el numero de registros que concuerden con la regla
                int numberOfRecords = cModul.mExistencias.Select("sucursal_id = '" + cModul.IdDbSelect +
                        "' AND descripción LIKE '" + dr[2].ToString() + "%'").Length;
                // Valida cuantas existencias hay con esa palabra cable
                if (numberOfRecords > 0)
                {
                    // Genera el filtrado de los datos que requiere mostrar y hace una copia
                    DataTable categoriaExistencias = cModul.mExistencias.Select("sucursal_id = '" + cModul.IdDbSelect + 
                        "' AND descripción LIKE '" + dr[2].ToString() + "%'").CopyToDataTable();
                    //asignamos el nombre que tomarà el panel
                    this.panelName = dr[1].ToString();
                    //Calculamos el largo de tamaño que tenga la grid = al numeor de registros
                    this.gridLargo = 3 + (celdaExistenciaLargo * categoriaExistencias.Rows.Count);
                    //Calculamos el largo que tendra el panel en relaciòn al tamaño de la grid
                    this.panelLargo = this.gridLargo + this.espacioInferiorPanel;
                    //Valida si aun cabe la grilla en la columna actual.
                    if (this.panelPxFin + this.panelLargo <= this.maxPx)
                    {
                        this.panelPxIni = this.panelPxFin + 5;
                        this.panelPxFin = this.panelPxIni + panelLargo;
                    }
                    //Si no cabe cambia la columna a la siguiente
                    else
                    {
                        this.columna = this.columna + 1;
                        this.panelPxIni = 5;
                        this.panelPxFin = this.panelPxIni + panelLargo;
                    }
                    //Genera la tabla de la categoria seleccionada y la imprime
                    imprimirTablas(generarListado(categoriaExistencias, dr[2].ToString()));
                }
            }
            // al terminar el recorrido reinicia los controladores
            this.columna = 0;
            this.conteoDataGrid = 1;
            this.panelPxIni = 0;
            this.panelPxFin = 0;
            this.panelLargo = 0;
            this.gridLargo = 0;
        }

        private DataTable generarListado(DataTable ExistenciasCategorias, string palabra_clave)
        {
            //Ordenamos los datos por la columna orden
            ExistenciasCategorias.DefaultView.Sort = "orden ASC";
            ExistenciasCategorias = ExistenciasCategorias.DefaultView.ToTable();
            // Generamos la tabla con el formato de 2 filas para el data grid
            DataTable Tabla = new DataTable();
            // Asignamos las columnas de la plantilla a rellenar
            Tabla.Columns.Add(new DataColumn("Descripción"));
            Tabla.Columns.Add(new DataColumn("Existencia"));
            //Llenamos el formato con las 3 filas para el data grid
            foreach (DataRow dr in ExistenciasCategorias.Rows)
            {
                DataRow Renglon;
                // Asignamos 3 registros a la vez.
                Renglon = Tabla.NewRow();
                Renglon[0] = dr["Descripción"].ToString().Replace(palabra_clave, "").Trim();
                //string cadena = "Hola mundo!!!";
                //cadena = cadena.Replace("!!!", ".");
                //Hola Mundo.
                Renglon[1] = dr["Existencia"].ToString();
                Tabla.Rows.Add(Renglon);
            }
            // devuelve la tabla generada
            return Tabla;
        }

        private void ocultarTablas()
        {
            this.gpb1.Visible = false;
            this.gpb2.Visible = false;
            this.gpb3.Visible = false;
            this.gpb4.Visible = false;
            this.gpb5.Visible = false;
            this.gpb6.Visible = false;
            this.gpb7.Visible = false;
            this.gpb8.Visible = false;
            this.gpb9.Visible = false;
            this.gpb10.Visible = false;
            this.gpb11.Visible = false;
            this.gpb12.Visible = false;
            this.gpb13.Visible = false;
            this.gpb14.Visible = false;
            this.gpb15.Visible = false;
            this.gpb16.Visible = false;
            this.gpb17.Visible = false;
            this.gpb18.Visible = false;
            this.gpb19.Visible = false;
            this.gpb20.Visible = false;
            this.gpb21.Visible = false;
        }

        // Imprimir datos en valores
        private void imprimirTablas(DataTable valores) 
        {
            switch (this.conteoDataGrid)
            {
                case 1:
                    // asigna los valores del panel
                    this.gpb1.Visible = true;
                    // Tamaño del texto
                    this.gpb1.Font = new Font("Arial",this.sizeLetra);
                    // define el ancho
                    this.gpb1.Width = this.panelAncho;
                    // define el largo
                    this.gpb1.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb1.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb1.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv1.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv1.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv1.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv1.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv1.DataSource = valores;
                    this.dgv1.ColumnHeadersVisible = false;
                    this.dgv1.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv1.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv1.AutoResizeColumns();
                    //this.dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv1.RowHeadersVisible = false;
                    
                    break;
                case 2:
                    // asigna los valores del panel
                    this.gpb2.Visible = true;
                    // Tamaño del texto
                    this.gpb2.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb2.Width = this.panelAncho;
                    // define el largo
                    this.gpb2.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb2.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb2.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv2.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv2.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv2.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv2.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv2.DataSource = valores;
                    this.dgv2.ColumnHeadersVisible = false;
                    this.dgv2.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv2.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv2.AutoResizeColumns();
                    //this.dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv2.RowHeadersVisible = false;
                    break;
                case 3:
                    // asigna los valores del panel
                    this.gpb3.Visible = true;
                    // Tamaño del texto
                    this.gpb3.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb3.Width = this.panelAncho;
                    // define el largo
                    this.gpb3.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb3.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb3.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv3.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv3.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv3.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv3.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv3.DataSource = valores;
                    this.dgv3.ColumnHeadersVisible = false;
                    this.dgv3.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv3.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv3.AutoResizeColumns();
                    //this.dgv3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv3.RowHeadersVisible = false;
                    break;
                case 4:
                    // asigna los valores del panel
                    this.gpb4.Visible = true;
                    // Tamaño del texto
                    this.gpb4.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb4.Width = this.panelAncho;
                    // define el largo
                    this.gpb4.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb4.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb4.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv4.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv4.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv4.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv4.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv4.DataSource = valores;
                    this.dgv4.ColumnHeadersVisible = false;
                    this.dgv4.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv4.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv4.AutoResizeColumns();
                    //this.dgv4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv4.RowHeadersVisible = false;
                    break;
                case 5:
                    // asigna los valores del panel
                    this.gpb5.Visible = true;
                    // Tamaño del texto
                    this.gpb5.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb5.Width = this.panelAncho;
                    // define el largo
                    this.gpb5.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb5.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb5.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv5.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv5.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv5.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv5.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv5.DataSource = valores;
                    this.dgv5.ColumnHeadersVisible = false;
                    this.dgv5.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv5.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv5.AutoResizeColumns();
                    //this.dgv5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv5.RowHeadersVisible = false;
                    break;
                case 6:
                    // asigna los valores del panel
                    this.gpb6.Visible = true;
                    // Tamaño del texto
                    this.gpb6.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb6.Width = this.panelAncho;
                    // define el largo
                    this.gpb6.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb6.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb6.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv6.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv6.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv6.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv6.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv6.DataSource = valores;
                    this.dgv6.ColumnHeadersVisible = false;
                    this.dgv6.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv6.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv6.AutoResizeColumns();
                    //this.dgv6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv6.RowHeadersVisible = false;
                    break;
                case 7:
                    // asigna los valores del panel
                    this.gpb7.Visible = true;
                    // Tamaño del texto
                    this.gpb7.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb7.Width = this.panelAncho;
                    // define el largo
                    this.gpb7.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb7.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb7.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv7.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv7.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv7.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv7.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv7.DataSource = valores;
                    this.dgv7.ColumnHeadersVisible = false;
                    this.dgv7.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv7.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv7.AutoResizeColumns();
                    //this.dgv7.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv7.RowHeadersVisible = false;
                    break;
                case 8:
                    // asigna los valores del panel
                    this.gpb8.Visible = true;
                    // Tamaño del texto
                    this.gpb8.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb8.Width = this.panelAncho;
                    // define el largo
                    this.gpb8.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb8.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb8.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv8.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv8.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv8.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv8.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv8.DataSource = valores;
                    this.dgv8.ColumnHeadersVisible = false;
                    this.dgv8.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv8.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv8.AutoResizeColumns();
                    //this.dgv8.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv8.RowHeadersVisible = false;
                    break;
                case 9:
                    // asigna los valores del panel
                    this.gpb9.Visible = true;
                    // Tamaño del texto
                    this.gpb9.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb9.Width = this.panelAncho;
                    // define el largo
                    this.gpb9.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb9.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb9.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv9.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv9.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv9.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv9.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv9.DataSource = valores;
                    this.dgv9.ColumnHeadersVisible = false;
                    this.dgv9.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv9.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv9.AutoResizeColumns();
                    //this.dgv9.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv9.RowHeadersVisible = false;
                    break;
                case 10:
                    // asigna los valores del panel
                    this.gpb10.Visible = true;
                    // Tamaño del texto
                    this.gpb10.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb10.Width = this.panelAncho;
                    // define el largo
                    this.gpb10.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb10.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb10.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv10.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv10.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv10.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv10.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv10.DataSource = valores;
                    this.dgv10.ColumnHeadersVisible = false;
                    this.dgv10.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv10.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv10.AutoResizeColumns();
                    //this.dgv10.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv10.RowHeadersVisible = false;
                    break;
                case 11:
                    // asigna los valores del panel
                    this.gpb11.Visible = true;
                    // Tamaño del texto
                    this.gpb11.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb11.Width = this.panelAncho;
                    // define el largo
                    this.gpb11.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb11.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb11.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv11.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv11.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv11.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv11.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv11.DataSource = valores;
                    this.dgv11.ColumnHeadersVisible = false;
                    this.dgv11.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv11.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv11.AutoResizeColumns();
                    //this.dgv11.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv11.RowHeadersVisible = false;
                    break;
                case 12:
                    // asigna los valores del panel
                    this.gpb12.Visible = true;
                    // Tamaño del texto
                    this.gpb12.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb12.Width = this.panelAncho;
                    // define el largo
                    this.gpb12.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb12.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb12.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv12.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv12.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv12.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv12.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv12.DataSource = valores;
                    this.dgv12.ColumnHeadersVisible = false;
                    this.dgv12.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv12.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv12.AutoResizeColumns();
                    //this.dgv12.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv12.RowHeadersVisible = false;
                    break;
                case 13:
                    // asigna los valores del panel
                    this.gpb13.Visible = true;
                    // Tamaño del texto
                    this.gpb13.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb13.Width = this.panelAncho;
                    // define el largo
                    this.gpb13.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb13.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb13.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv13.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv13.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv13.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv13.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv13.DataSource = valores;
                    this.dgv13.ColumnHeadersVisible = false;
                    this.dgv13.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv13.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv13.AutoResizeColumns();
                    //this.dgv13.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv13.RowHeadersVisible = false;
                    break;
                case 14:
                    // asigna los valores del panel
                    this.gpb14.Visible = true;
                    // Tamaño del texto
                    this.gpb14.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb14.Width = this.panelAncho;
                    // define el largo
                    this.gpb14.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb14.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb14.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv14.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv14.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv14.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv14.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv14.DataSource = valores;
                    this.dgv14.ColumnHeadersVisible = false;
                    this.dgv14.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv14.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv14.AutoResizeColumns();
                    //this.dgv14.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv14.RowHeadersVisible = false;
                    break;
                case 15:
                    // asigna los valores del panel
                    this.gpb15.Visible = true;
                    // Tamaño del texto
                    this.gpb15.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb15.Width = this.panelAncho;
                    // define el largo
                    this.gpb15.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb15.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb15.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv15.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv15.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv15.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv15.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv15.DataSource = valores;
                    this.dgv15.ColumnHeadersVisible = false;
                    this.dgv15.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv15.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv15.AutoResizeColumns();
                    //this.dgv15.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv15.RowHeadersVisible = false;
                    break;
                case 16:
                    // asigna los valores del panel
                    this.gpb16.Visible = true;
                    // Tamaño del texto
                    this.gpb16.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb16.Width = this.panelAncho;
                    // define el largo
                    this.gpb16.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb16.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb16.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv16.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv16.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv16.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv16.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv16.DataSource = valores;
                    this.dgv16.ColumnHeadersVisible = false;
                    this.dgv16.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv16.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv16.AutoResizeColumns();
                    //this.dgv16.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv16.RowHeadersVisible = false;
                    break;
                case 17:
                    // asigna los valores del panel
                    this.gpb17.Visible = true;
                    // Tamaño del texto
                    this.gpb17.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb17.Width = this.panelAncho;
                    // define el largo
                    this.gpb17.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb17.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb17.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv17.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv17.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv17.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv17.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv17.DataSource = valores;
                    this.dgv17.ColumnHeadersVisible = false;
                    this.dgv17.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv17.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv17.AutoResizeColumns();
                    //this.dgv17.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv17.RowHeadersVisible = false;
                    break;
                case 18:
                    // asigna los valores del panel
                    this.gpb18.Visible = true;
                    // Tamaño del texto
                    this.gpb18.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb18.Width = this.panelAncho;
                    // define el largo
                    this.gpb18.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb18.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb18.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv18.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv18.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv18.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv18.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv18.DataSource = valores;
                    this.dgv18.ColumnHeadersVisible = false;
                    this.dgv18.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv18.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv18.AutoResizeColumns();
                    //this.dgv18.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv18.RowHeadersVisible = false;
                    break;
                case 19:
                    // asigna los valores del panel
                    this.gpb19.Visible = true;
                    // Tamaño del texto
                    this.gpb19.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb19.Width = this.panelAncho;
                    // define el largo
                    this.gpb19.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb19.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb19.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv19.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv19.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv19.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv19.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv19.DataSource = valores;
                    this.dgv19.ColumnHeadersVisible = false;
                    this.dgv19.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv19.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv19.AutoResizeColumns();
                    //this.dgv19.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv19.RowHeadersVisible = false;
                    break;
                case 20:
                    // asigna los valores del panel
                    this.gpb20.Visible = true;
                    // Tamaño del texto
                    this.gpb20.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb20.Width = this.panelAncho;
                    // define el largo
                    this.gpb20.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb20.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb20.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv20.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv20.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv20.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv20.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv20.DataSource = valores;
                    this.dgv20.ColumnHeadersVisible = false;
                    this.dgv20.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv20.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv20.AutoResizeColumns();
                    //this.dgv20.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv20.RowHeadersVisible = false;
                    break;
                case 21:
                    // asigna los valores del panel
                    this.gpb21.Visible = true;
                    // Tamaño del texto
                    this.gpb21.Font = new Font("Arial", this.sizeLetra);
                    // define el ancho
                    this.gpb21.Width = this.panelAncho;
                    // define el largo
                    this.gpb21.Height = this.panelLargo;
                    // posiciona el control horizontal, vertical
                    this.gpb21.Location = new Point((this.columna * this.panelAncho) + 5, this.panelPxIni);
                    // Nombra al panel como la categoria
                    this.gpb21.Text = this.panelName;
                    // asigna los valores de la grilla
                    // Tamaño del texto
                    this.dgv21.Font = new Font("Arial", this.sizeLetra);
                    // ancho fijo de la grilla
                    this.dgv21.Width = this.panelAncho - 10;
                    // 22 pixeles por registro
                    //define el largo por el numero de registros
                    this.dgv21.Height = this.gridLargo;
                    // indica en que coordenadas se imprime
                    this.dgv21.Location = new Point(5, this.posicionVerticalDataGrid);
                    //preparando para mostrar el datagrid
                    this.dgv21.DataSource = valores;
                    this.dgv21.ColumnHeadersVisible = false;
                    this.dgv21.Columns[0].Width = this.colDescripcionLargo;
                    this.dgv21.Columns[1].Width = this.colExistenciaLargo;
                    //this.dgv21.AutoResizeColumns();
                    //this.dgv21.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dgv21.RowHeadersVisible = false;
                    break;
                default:
                    Console.Write("Se ingreso un valor fuera de rango");
                    break;
            }
            // posiciona la siguiente grilla para imprimir
            this.conteoDataGrid = this.conteoDataGrid + 1;
        }

        private void frmListadoCategorias_FormClosing(object sender, FormClosingEventArgs e)
        {
            cModul.EstaAbiertoFrmCategorias = false;
            //Hiding the window, because closing it makes the window unaccessible.
            this.Hide();
            this.Parent = null;
            e.Cancel = true; //hides the form, cancels closing event
        }

        private void tmrCambioSucursal_Tick(object sender, EventArgs e)
        {
            // Cambia al siguiente ID en el listado de sucursales activas
            changeDbId();
            // Crea la vista en relación al DB ID seleccionado
            crearVistaCategorias();
            // Elimina la selección de las sucursales
            this.deSeleccionarDataGrids();
        }

        //Define el tiempo en que se ejecuta el timer de cambio de sucursal
        public void defineTimerChangeSucursal()
        {
            int time = Int32.Parse(cModul.TimeChangeSucursal);
            if (time > 0)
            {
                tmrCambioSucursal.Enabled = true;
                tmrCambioSucursal.Interval = cDatos.convertirMinToMiliseg(time);
            }
            else
                tmrCambioSucursal.Enabled = false;
        }

        private void mostrarBtnStatusBar(int btnNumber, DataRow dr, bool banCheck)
        {
            switch (btnNumber)
            {
                case 1:
                    tsmItem1.Visible = true;
                    tsmItem1.Text = dr[1].ToString();
                    btnSucursal1 = dr[0].ToString();
                    if (banCheck)
                        tsmItem1.Checked = true;
                    break;
                case 2:
                    tsmItem2.Visible = true;
                    tsmItem2.Text = dr[1].ToString();
                    btnSucursal2 = dr[0].ToString();
                    if (banCheck)
                        tsmItem2.Checked = true;
                    break;
                case 3:
                    tsmItem3.Visible = true;
                    tsmItem3.Text = dr[1].ToString();
                    btnSucursal3 = dr[0].ToString();
                    if (banCheck)
                        tsmItem3.Checked = true;
                    break;
                case 4:
                    tsmItem4.Visible = true;
                    tsmItem4.Text = dr[1].ToString();
                    btnSucursal4 = dr[0].ToString();
                    if (banCheck)
                        tsmItem4.Checked = true;
                    break;
                case 5:
                    tsmItem5.Visible = true;
                    tsmItem5.Text = dr[1].ToString();
                    btnSucursal5 = dr[0].ToString();
                    if (banCheck)
                        tsmItem5.Checked = true;
                    break;
                case 6:
                    tsmItem6.Visible = true;
                    tsmItem6.Text = dr[1].ToString();
                    btnSucursal6 = dr[0].ToString();
                    if (banCheck)
                        tsmItem6.Checked = true;
                    break;
                case 7:
                    tsmItem7.Visible = true;
                    tsmItem7.Text = dr[1].ToString();
                    btnSucursal7 = dr[0].ToString();
                    if (banCheck)
                        tsmItem7.Checked = true;
                    break;
                case 8:
                    tsmItem8.Visible = true;
                    tsmItem8.Text = dr[1].ToString();
                    btnSucursal8 = dr[0].ToString();
                    if (banCheck)
                        tsmItem8.Checked = true;
                    break;
                case 9:
                    tsmItem9.Visible = true;
                    tsmItem9.Text = dr[1].ToString();
                    btnSucursal9 = dr[0].ToString();
                    if (banCheck)
                        tsmItem9.Checked = true;
                    break;
                case 10:
                    tsmItem10.Visible = true;
                    tsmItem10.Text = dr[1].ToString();
                    btnSucursal10 = dr[0].ToString();
                    if (banCheck)
                        tsmItem10.Checked = true;
                    break;
            }
        }

        private void ocultarBtnStatusBar()
        {
            tsmItem1.Visible = false;
            tsmItem1.Checked = false;
            tsmItem2.Visible = false;
            tsmItem2.Checked = false;
            tsmItem3.Visible = false;
            tsmItem3.Checked = false;
            tsmItem4.Visible = false;
            tsmItem4.Checked = false;
            tsmItem5.Visible = false;
            tsmItem5.Checked = false;
            tsmItem6.Visible = false;
            tsmItem6.Checked = false;
            tsmItem7.Visible = false;
            tsmItem7.Checked = false;
            tsmItem8.Visible = false;
            tsmItem8.Checked = false;
            tsmItem9.Visible = false;
            tsmItem9.Checked = false;
            tsmItem10.Visible = false;
            tsmItem10.Checked = false;
        }

        public void deSeleccionarDataGrids()
        {
            this.dgv1.ClearSelection();
            this.dgv2.ClearSelection();
            this.dgv3.ClearSelection();
            this.dgv4.ClearSelection();
            this.dgv5.ClearSelection();
            this.dgv6.ClearSelection();
            this.dgv7.ClearSelection();
            this.dgv8.ClearSelection();
            this.dgv9.ClearSelection();
            this.dgv10.ClearSelection();
            this.dgv11.ClearSelection();
            this.dgv12.ClearSelection();
            this.dgv13.ClearSelection();
            this.dgv14.ClearSelection();
            this.dgv15.ClearSelection();
            this.dgv16.ClearSelection();
            this.dgv17.ClearSelection();
            this.dgv18.ClearSelection();
            this.dgv19.ClearSelection();
            this.dgv20.ClearSelection();
            this.dgv21.ClearSelection();
        }

        private void tsmItem1_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal1;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }

        private void tsmItem2_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal2;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }

        private void tsmItem3_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal3;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }

        private void tsmItem4_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal4;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }

        private void tsmItem5_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal5;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }

        private void tsmItem6_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal6;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }

        private void tsmItem7_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal7;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }

        private void tsmItem8_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal8;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }

        private void tsmItem9_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal9;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }

        private void tsmItem10_Click(object sender, EventArgs e)
        {
            cModul.IdDbSelect = btnSucursal10;
            crearVistaCategorias();
            this.deSeleccionarDataGrids();
        }
    }
}
