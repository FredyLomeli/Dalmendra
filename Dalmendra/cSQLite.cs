using System;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dalmendra
{
    class cSQLite
    {
        // Nombre del archivo de base de datos
        private const string DBName = "dalmendra.sqlite";
        private static bool IsDbRecentlyCreated = false;
        public static SQLiteConnection DBCON;

        public static void Up()
        {
            // Crea la base de datos y registra usuario solo una vez
            if (!File.Exists(Path.GetFullPath(DBName)))
            {
                SQLiteConnection.CreateFile(DBName);
                IsDbRecentlyCreated = true;
            }
            // Genera la instancia de SQLite
            using (var ctx = GetInstance())
            {
                ctx.Open();
                if (IsDbRecentlyCreated)
                {
                    cModul.ThisFirstTime = true;
                    // Si es la primera vez genera la tabla de consulta.
                    var query = "CREATE TABLE \"sucursales\" (\"id\" integer NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                        "\"nombre_sucursal\" text(200),\"data_source\" text(200) NOT NULL,\"catalog\" text(200) NOT NULL, " +
                        "\"user_id\" text(200) NOT NULL,\"password\" text(200) NOT NULL, " + 
                        "\"orden\" integer NOT NULL DEFAULT 0, \"fecha_hora_actualizacion\" text(10), " +
                        "\"color\" text(16) DEFAULT NULL);";
                    using (var command = new SQLiteCommand(query, ctx))
                    {
                        command.ExecuteNonQuery();
                    }
                    // Si es la primera vez genera la tabla de consulta.
                    var query2 = "CREATE TABLE \"existencias\" (\"id\" integer NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                        "\"sucursal_id\" integer, \"codigo\" text(10), \"descripcion\" text(250), " +
                        "\"existencia\" real, \"orden\" integer NOT NULL DEFAULT 0, \"fecha_actualizacion\" text(100));";
                    using (var command = new SQLiteCommand(query2, ctx))
                    {
                        command.ExecuteNonQuery();
                    }
                    // Si es la primera vez genera la tabla de consulta.
                    var query3 = "CREATE TABLE \"categorias\" (\"id\" integer NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                        "\"descripcion\" text(50), \"palabra_clave\" text(20), \"orden\" integer NOT NULL DEFAULT 0, " +
                        "\"estado\" text(1) NOT NULL DEFAULT \"1\");";
                    using (var command = new SQLiteCommand(query3, ctx))
                    {
                        command.ExecuteNonQuery();
                    }
                    // Si es la primera vez genera la tabla de consulta.
                    var query4 = "CREATE TABLE \"datos\" (\"id\" integer NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                        "\"descripcion\" text(50), \"value\" text(20), \"orden\" integer NOT NULL DEFAULT 0, " +
                        "\"estado\" text(1) NOT NULL DEFAULT \"1\");";
                    using (var command = new SQLiteCommand(query4, ctx))
                    {
                        command.ExecuteNonQuery();
                    }
                    // Si es la primera vez genera la tabla de consulta.
                    var query5 = "CREATE TABLE \"configuracion\" (\"id\" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                      "\"descripcion\" TEXT(250) NOT NULL DEFAULT \"config\", \"value\" TEXT(250) DEFAULT NULL);";
                    using (var command = new SQLiteCommand(query5, ctx))
                    {
                        command.ExecuteNonQuery();
                    }
                    // Agrega el registro de sucursal 1
                    var cadena = "INSERT INTO sucursales (nombre_sucursal, data_source, catalog, " +
                        "user_id, password, orden) VALUES (?, ?, ?, ?, ?, 1)";
                    using (var command = new SQLiteCommand(cadena, ctx))
                    {
                        command.Parameters.Add(new SQLiteParameter("nombre_sucursal", "Sucursal Prueba"));
                        command.Parameters.Add(new SQLiteParameter("data_source", "server"));
                        command.Parameters.Add(new SQLiteParameter("catalog", "DB"));
                        command.Parameters.Add(new SQLiteParameter("user_id", "sa"));
                        command.Parameters.Add(new SQLiteParameter("password", "***"));
                        command.ExecuteNonQuery();
                    }
                    // Agrega el registro de sucursal 1
                    var cadena2 = "INSERT INTO configuracion (descripcion, value) " +
                        "VALUES (?, ?),(?, ?),(?, ?),(?, ?)";
                    using (var command = new SQLiteCommand(cadena2, ctx))
                    {
                        // ID de la sucursal a mostrar en el reporte seleccionado
                        command.Parameters.Add(new SQLiteParameter("name", "IdDbSelect"));
                        command.Parameters.Add(new SQLiteParameter("value", "1"));
                        // Tiempo en minutos a sincronizar las sucursales 0 es inactivo
                        command.Parameters.Add(new SQLiteParameter("name2", "TimeSyncSucursal"));
                        command.Parameters.Add(new SQLiteParameter("value2", "1"));
                        // Tiempo en minutos para cambiar de una sucursal a otra 0 es inactivo
                        command.Parameters.Add(new SQLiteParameter("name3", "TimeChangeSucursal"));
                        command.Parameters.Add(new SQLiteParameter("value3", "15"));
                        // Reporte que muestra por defecto al ingresar al programa
                        // PorCategorias, ListadoConCodigo, ListadoSinCodigo
                        command.Parameters.Add(new SQLiteParameter("name4", "FirstReport"));
                        command.Parameters.Add(new SQLiteParameter("value4", "PorCategorias"));
                        command.ExecuteNonQuery();
                    }
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public static SQLiteConnection GetInstance()
        {
            //Genera la coneción SQLite
            var db = new SQLiteConnection(
                string.Format("Data Source={0};Version=3;", DBName)
            );
            //Abre la conexión
            //db.Open();
            //Devuelve la conexión
            return db;
        }

    }
}
