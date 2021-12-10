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
        int colDescripcionLargo = 330;
        int colExistenciaLargo = 110;
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


        private void frmListadoCategorias_Load(object sender, EventArgs e)
        {
            //Consulta las categorias 
            cModul.mCategorias = nCategoria.consulta();
            //Revisa las categorias activas
            DataTable data = cModul.mCategorias.Select("estado = 1").CopyToDataTable(); ;
            //Recorre las categorias
            foreach (DataRow dr in data.Rows)
            {
                // consulta el numero de registros que concuerden con la regla
                int numberOfRecords = cModul.mExistencias.Select("sucursal_id = '1' AND descripción LIKE '" + dr[2].ToString() + "%'").Length;
                // Valida cuantas existencias hay con esa palabra cable
                if (numberOfRecords > 0)
                {
                    // Genera el filtrado de los datos que requiere mostrar y hace una copia
                    DataTable categoriaExistencias = cModul.mExistencias.Select("sucursal_id = '1' AND descripción LIKE '" + dr[2].ToString() + "%'").CopyToDataTable();
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

        private void consultaCategorias()
        {
 
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
                default:
                    Console.Write("Se ingreso un valor fuera de rango");
                    break;
            }
            // posiciona la siguiente grilla para imprimir
            this.conteoDataGrid = this.conteoDataGrid + 1;
        }

        private void frmListadoCategorias_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Hiding the window, because closing it makes the window unaccessible.
            this.Hide();
            this.Parent = null;
            e.Cancel = true; //hides the form, cancels closing event
        }
    }
}
