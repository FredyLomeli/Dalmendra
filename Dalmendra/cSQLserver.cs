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


        public void ConsultarInventario(cSucursal Sucursal, out string fecha)
        {
            fecha = null;
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection();
            if (validar_conexion(Sucursal.data_source, Sucursal.catalog, Sucursal.user_id, Sucursal.password))
            {
                //definir la consulta sql server
                //CONSULTA SOLO GRUPO ALMACEN PANES 010
                //string sql = "SELECT i.idinsumo AS CODIGO, i.descripcion AS DESCRIPCION, " +
                //    "i.unidad AS UNIDAD, a.existencia AS EXISTENCIA FROM insumos AS i " +
                //    "INNER JOIN acumuladoinsumos AS a ON i.idinsumo=a.idinsumo " +
                //    "WHERE i.idgruposi = 010 AND a.idalmacen = 3";
                //CONSULTA A TODOS LOS ALMACENES 008 MISELANIOS y 010 PANES
                string sql = "SELECT i.idinsumo AS CODIGO, i.descripcion AS DESCRIPCION, " +
                    "i.unidad AS UNIDAD, a.existencia AS EXISTENCIA FROM insumos AS i " +
                    "INNER JOIN acumuladoinsumos AS a ON i.idinsumo=a.idinsumo " +
                    "WHERE a.idalmacen = 3";
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
                //Consultamos fecha y hora
                DateTime localDate = DateTime.Now;
                //Recorremos los valores del sistema para agregarlos a la BD local
                foreach (DataRow dr in dt.Rows)
                {
                    cExistencias nExist = new cExistencias();
                    // Asignamos los valores al registro de existencias
                    nExist.sucursal_id = Sucursal.id;
                    nExist.codigo = dr[0].ToString();
                    nExist.descripcion = dr[1].ToString();
                    nExist.existencia = dr[3].ToString();
                    nExist.unidad = dr[2].ToString();
                    nExist.fecha_actualizacion = localDate.ToString();
                    // Consultamos si ya existe un registor con ese codigo
                    DataRow[] rows = cModul.mExistencias.Select("codigo = '" + dr[0].ToString() + "' AND sucursal_id = '" + Sucursal.id + "'");
                    // Actualiza o inserta el registro segun corresponde.
                    if (rows.Count() > 0)
                        nExist.update();
                    else
                        nExist.insert();
                }
                // Elimina los registros que ya no existen
                nExistencias.delete(Sucursal.id, localDate.ToString());
                // Actualiza fecha actualización Servidor
                nSucursal.updateActualizacion(Sucursal.id,localDate.ToString());
                // retornamos la fecha de registro
                fecha = localDate.ToString();
            }
        }
    }
}
