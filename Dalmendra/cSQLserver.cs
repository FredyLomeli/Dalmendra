using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dalmendra
{
    class cSQLserver
    {
        string connetionString;
        SqlConnection conn;
        cExistencias nExistencias = new cExistencias();
        cSucursal nSucursal = new cSucursal();

        public string verificar_conexion(string nombre_sucursal, string data_source, string catalog,
            string user_id, string password)
        {
            try
            {
                connetionString = @"Data Source=" + data_source + ";Initial Catalog=" +
                    catalog + ";Integrated Security=False;User ID=" + user_id + ";Password=" + password + ";";
                conn = new SqlConnection(connetionString);
                conn.Open();
                return "Conexion a Base de datos "+ nombre_sucursal + " correcta";
            }
            catch (SqlException e)
            {
                return e.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool validar_conexion(string data_source, string catalog,
            string user_id, string password)
        {
            connetionString = @"Data Source=" + data_source + ";Initial Catalog=" +
                    catalog + ";Integrated Security=False;User ID=" + user_id + ";Password=" + password + ";";
            conn = new SqlConnection(connetionString);
            try
            {
                conn.Open();
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
            finally 
            {
                conn.Close();
            }
        }


        public bool ConsultarInventario(cSucursal Sucursal, out string fecha, out string error)
        {
            fecha = Sucursal.fecha_hora_actualizacion;
            error = "Conexión intermitente con la sucursal: " + Sucursal.nombre_sucursal + "\n" + "Metodo ConsultarInventarios" + "\n";
            DataTable dt = new DataTable();
            try
            {
                if (validar_conexion(Sucursal.data_source, Sucursal.catalog, Sucursal.user_id, Sucursal.password))
                {
                    //CONSULTA A TODOS LOS ALMACENES NO CONSULTA UNIDAD
                    string sql = "SELECT i.idinsumo AS CODIGO, i.descripcion AS DESCRIPCION, " +
                        "a.existencia AS EXISTENCIA FROM insumos AS i " +
                        "INNER JOIN acumuladoinsumos AS a ON i.idinsumo=a.idinsumo " +
                        "WHERE a.idalmacen = 3";
                    //Generamos la cadena de conexion
                    connetionString = @"Data Source=" + Sucursal.data_source + ";Initial Catalog=" +
                        Sucursal.catalog + ";Integrated Security=False;User ID=" + Sucursal.user_id +
                        ";Password=" + Sucursal.password + ";";
                    SqlConnection cn = new SqlConnection(connetionString);
                    //definimos el adaptador para almacenar la información
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cn);
                    //cargamos la tabla en memoria "data table" con la información del adaptador    
                    dataAdapter.Fill(dt);
                    cn.Close();
                    cn.Dispose();
                    error = "Error al validar las unidades vendidas: " + Sucursal.nombre_sucursal + "\n" + "Metodo ConsultarInventarios" + "\n";
                    dt = ConsultarVentas(Sucursal, dt);
                    //Consultamos fecha y hora
                    DateTime localDate = DateTime.Now;
                    //Definimos el total de los datos para dar orden
                    //int orden = cModul.mExistencias.Select("sucursal_id = '" + Sucursal.id + "'").Length;
                    error = "Error al sincronisar los datos con la DB local sucursal: " + Sucursal.nombre_sucursal + "\n" + "Metodo ConsultarInventarios" + "\n";
                    //Recorremos los valores del sistema para agregarlos a la BD local
                    foreach (DataRow dr in dt.Rows)
                    {
                        cExistencias nExist = new cExistencias();
                        // Asignamos los valores al registro de existencias
                        nExist.sucursal_id = Sucursal.id;
                        nExist.codigo = dr[0].ToString();
                        nExist.descripcion = dr[1].ToString();
                        nExist.existencia = dr[2].ToString();
                        nExist.fecha_actualizacion = localDate.ToString();
                        // Guardamos la información del registro
                        nExist.save();
                    }
                    error = "Error al terminar la sincronización con la DB local sucursal: " + Sucursal.nombre_sucursal + "\n" + "Metodo ConsultarInventarios" + "\n";
                    // Elimina los registros que ya no existen
                    nExistencias.delete(Sucursal.id, localDate.ToString());
                    // Actualiza fecha actualización Servidor
                    nSucursal.updateActualizacion(Sucursal.id, localDate.ToString());
                    // retornamos la fecha de registro
                    fecha = localDate.ToString();
                    //Eliminamos los errores
                    error = "";
                }
                return false;
            }
            catch (SqlException e)
            {
                cModul.BanErrorSincronizacion = true;
                return true;
            }
        }

        public DataTable ConsultarVentas(cSucursal Sucursal, DataTable DtConsulta)
        {
            DataTable Data = DtConsulta;
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection();
            if (validar_conexion(Sucursal.data_source, Sucursal.catalog, Sucursal.user_id, Sucursal.password))
            {
                // Consulta para rebajar lo vendido total
                //string sql = "SELECT c.idinsumo AS Insumo, SUM(t.cantidad * c.cantidad) AS Existencias " +
                //    "FROM tempcheqdet AS t INNER JOIN costos AS c ON t.idproducto = c.idproducto " +
                //    "GROUP BY c.idinsumo;";
                // Consulta para rebajar lo vendido excluyendo los tikets cancelados
                //string sql = "SELECT c.idinsumo AS Insumo, SUM(t.cantidad * c.cantidad) AS Existencias " +
                //    "FROM tempcheqdet AS t INNER JOIN costos AS c ON t.idproducto = c.idproducto " +
                //    "JOIN tempcheques AS f ON t.foliodet = f.folio WHERE f.cancelado = 0 " +
                //    "GROUP BY c.idinsumo;";
                // CONSULTA QUE AGREGA DESCRIPCIÖN AL TOTAL DE PRODUCTOS VENDIDOS
                string sql = "SELECT c.idinsumo AS Insumo, SUM(t.cantidad * c.cantidad) AS Existencias, " +
                        "(SELECT i.descripcion FROM insumos AS i WHERE c.idinsumo = i.idinsumo) AS Descripcion " +
                        "FROM tempcheqdet AS t INNER JOIN costos AS c ON t.idproducto = c.idproducto " +
                        "JOIN tempcheques AS f ON t.foliodet = f.folio " +
                        "WHERE f.cancelado = 0 GROUP BY c.idinsumo;";
                //Generamos la cadena de conexion
                connetionString = @"Data Source=" + Sucursal.data_source + ";Initial Catalog=" +
                    Sucursal.catalog + ";Integrated Security=False;User ID=" + Sucursal.user_id +
                    ";Password=" + Sucursal.password + ";";
                cn = new SqlConnection(connetionString);
                //definimos el adaptador para almacenar la información
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cn);
                //cargamos la tabla en memoria "data table" con la información del adaptador    
                dataAdapter.Fill(dt);
                cn.Close();
                //Recorremos los valores del sistema para agregarlos a la BD local
                foreach (DataRow dr in dt.Rows)
                {
                    string consulta = "CODIGO='" + dr[0].ToString() + "'";
                    // Valida si existe el registro en la tabla
                    int existencias = Data.Select(consulta).Length;
                    //Actualiza los datos de la tabla de datos offline
                    if (existencias > 0)
                    {
                        DataRow[] MRow = Data.Select(consulta);
                        MRow[0]["EXISTENCIA"] = Double.Parse(MRow[0]["EXISTENCIA"].ToString()) - Double.Parse(dr[1].ToString());
                    }
                    else
                    {
                        DataRow row = Data.NewRow();
                        row["CODIGO"] = dr[0].ToString();
                        row["DESCRIPCION"] = dr[2].ToString();
                        row["EXISTENCIA"] = Double.Parse(dr[1].ToString()) * -1;
                        Data.Rows.Add(row);
                    }
                }
            }
            return Data;
        }
    }
}
