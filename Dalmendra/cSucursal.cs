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
    class cSucursal
    {
        public string id;
        public string nombre_sucursal;
        public string data_source;
        public string catalog;
        public string user_id;
        public string password;
        public string fecha_hora_actualizacion;

        public void insert(string nombre_sucursal, string data_source, string catalog,
            string user_id, string password)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Agrega el registro de la sucursal
                var cadena = "INSERT INTO sucursales (nombre_sucursal, data_source, " +
                    "catalog, user_id, password) VALUES (?, ?, ?, ?, ?)";
                using (var command = new SQLiteCommand(cadena, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("nombre_sucursal", nombre_sucursal));
                    command.Parameters.Add(new SQLiteParameter("data_source", data_source));
                    command.Parameters.Add(new SQLiteParameter("catalog", catalog));
                    command.Parameters.Add(new SQLiteParameter("user_id", user_id));
                    command.Parameters.Add(new SQLiteParameter("password", password));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public void update(string id, string nombre_sucursal, string data_source, string catalog,
            string user_id, string password)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "UPDATE sucursales SET nombre_sucursal = :nombre_sucursal, data_source = :data_source," +
                    " catalog = :catalog, user_id = :user_id, password = :password WHERE id = :id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("nombre_sucursal", nombre_sucursal));
                    command.Parameters.Add(new SQLiteParameter("data_source", data_source));
                    command.Parameters.Add(new SQLiteParameter("catalog", catalog));
                    command.Parameters.Add(new SQLiteParameter("user_id", user_id));
                    command.Parameters.Add(new SQLiteParameter("password", password));
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public void updateSinPassword(string id, string nombre_sucursal, string data_source, string catalog,
            string user_id)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "UPDATE sucursales SET nombre_sucursal = :nombre_sucursal, data_source = :data_source," +
                    " catalog = :catalog, user_id = :user_id WHERE id = :id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("nombre_sucursal", nombre_sucursal));
                    command.Parameters.Add(new SQLiteParameter("data_source", data_source));
                    command.Parameters.Add(new SQLiteParameter("catalog", catalog));
                    command.Parameters.Add(new SQLiteParameter("user_id", user_id));
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public void delete(string id)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "DELETE FROM sucursales WHERE id = :id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public DataTable consultaTodo()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "SELECT id AS ID, nombre_sucursal AS \"Nombre Sucursal\", " +
                    "data_source AS Servidor, catalog AS \"Base de datos\", " +
                    "user_id AS Usuario, fecha_hora_actualizacion AS Enlace FROM sucursales;";
                // Adaptador de datos, DataSet y tabla
                using (SQLiteDataAdapter db = new SQLiteDataAdapter(query, ctx))
                {
                    DataSet ds = new DataSet();
                    ds.Reset();
                    DataTable dt = new DataTable();
                    db.Fill(ds);
                    //Asigna al DataTable la primer tabla (ciudades) 
                    // y la mostramos en el DataGridView
                    dt = ds.Tables[0];
                    ctx.Close();
                    ctx.Dispose();
                    return dt;
                }
            }
        }

        public DataTable consulta()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "SELECT * FROM sucursales;";
                // Adaptador de datos, DataSet y tabla
                using (SQLiteDataAdapter db = new SQLiteDataAdapter(query, ctx))
                {
                    DataSet ds = new DataSet();
                    ds.Reset();
                    DataTable dt = new DataTable();
                    db.Fill(ds);
                    //Asigna al DataTable la primer tabla (ciudades) 
                    // y la mostramos en el DataGridView
                    dt = ds.Tables[0];
                    ctx.Close();
                    ctx.Dispose();
                    return dt;
                }
            }
        }

        public bool consultaPass(out string password, string id)
        {
            password = "0";
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                try
                {
                    string quer = "SELECT * FROM sucursales WHERE id = :id;";
                    SQLiteCommand cmd = new SQLiteCommand(quer, ctx);
                    cmd.Parameters.Add(new SQLiteParameter("id", id));
                    SQLiteDataReader result = cmd.ExecuteReader();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            password = result[5].ToString();
                        }
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    ctx.Close();
                    ctx.Dispose();
                }
            }
        }

        public void ultimoRegistro()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                string quer = "SELECT * FROM sucursales ORDER BY id DESC LIMIT 1 ;";
                SQLiteCommand cmd = new SQLiteCommand(quer, ctx);
                try
                {
                    using (SQLiteDataReader registro = cmd.ExecuteReader())
                    {
                        if (registro.Read())
                        {
                            this.id = registro["id"].ToString();
                            this.nombre_sucursal = registro["nombre_sucursal"].ToString();
                            this.data_source = registro["data_source"].ToString();
                            this.catalog = registro["catalog"].ToString();
                            this.user_id = registro["user_id"].ToString();
                            this.password = registro["password"].ToString();
                        }
                        ctx.Close();
                        ctx.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void updateActualizacion(string id, string fecha_actualizacion)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "UPDATE sucursales SET fecha_hora_actualizacion = :fecha WHERE id = :id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("fecha", fecha_actualizacion));
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }
    }
}
