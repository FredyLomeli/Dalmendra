using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Dalmendra
{
    class cMySQL
    {
        public string verificar_mysql_conexion()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dalmendra;";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            
            try
            {
                databaseConnection.Open();
                return "Conexion a Base de datos correcta";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                databaseConnection.Close();
            }
        }
    }
}
