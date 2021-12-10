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
    class cExistencias
    {
        public string id;
        public string sucursal_id;
        public string codigo;
        public string descripcion;
        public string existencia;
        public string unidad;
        public string fecha_actualizacion;

        public DataTable consultaExistencias(string sucursal)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                var query = "SELECT codigo AS \"Codigo\", descripcion AS Descripción, existencia || \" \" " +
                    "|| unidad AS \"Existencia\" FROM existencias WHERE sucursal_id = :sucursal ORDER BY descripcion DESC;";
                ctx.Open();
                // Adaptador de datos, DataSet y tabla
                using (SQLiteDataAdapter db = new SQLiteDataAdapter(query, ctx))
                {
                    db.SelectCommand.Parameters.Add(new SQLiteParameter(":sucursal", sucursal));
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
                var query = "SELECT sucursal_id, codigo AS \"Codigo\", descripcion AS Descripción, existencia || \" \" " +
                    "|| unidad AS \"Existencia\" FROM existencias ORDER BY descripcion DESC;";
                ctx.Open();
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

        public bool codigoExitente(string id, string codigo)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                // Adaptador de datos, DataSet y tabla
                using (SQLiteCommand SQLcmd = new SQLiteCommand())
                {
                    SQLcmd.Connection = ctx;   // Set default connection
                    SQLcmd.CommandText = "SELECT * FROM existencias WHERE sucursal_id = :id AND codigo = :codigo;";
                    SQLcmd.Parameters.Add(new SQLiteParameter("id", id));
                    SQLcmd.Parameters.Add(new SQLiteParameter("codigo", codigo));
                    ctx.Open();
                    using (SQLiteDataReader result = SQLcmd.ExecuteReader())
                    {
                        ctx.Close();
                        ctx.Dispose();
                        if (result.HasRows)
                            return true;
                        return false;
                    }
                }
            }
        }

        public void insert()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Agrega el registro de la sucursal
                var cadena = "INSERT INTO existencias (sucursal_id, codigo, descripcion, " +
                    "existencia, unidad, fecha_actualizacion) VALUES (:sucursal_id, :codigo," +
                    " :descripcion, :existencia, :unidad, :fecha_actualizacion)";
                using (var command = new SQLiteCommand(cadena, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("sucursal_id", this.sucursal_id));
                    command.Parameters.Add(new SQLiteParameter("codigo", this.codigo));
                    command.Parameters.Add(new SQLiteParameter("descripcion", this.descripcion));
                    command.Parameters.Add(new SQLiteParameter("existencia", this.existencia));
                    command.Parameters.Add(new SQLiteParameter("unidad", this.unidad));
                    command.Parameters.Add(new SQLiteParameter("fecha_actualizacion", this.fecha_actualizacion));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public void update()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "UPDATE existencias SET descripcion = :descripcion," +
                    " existencia = :existencia, unidad = :unidad, fecha_actualizacion = :fecha_actualizacion " +
                    " WHERE codigo = :codigo AND sucursal_id = :sucursal_id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("descripcion", this.descripcion));
                    command.Parameters.Add(new SQLiteParameter("existencia", this.existencia));
                    command.Parameters.Add(new SQLiteParameter("unidad", this.unidad));
                    command.Parameters.Add(new SQLiteParameter("fecha_actualizacion", this.fecha_actualizacion));
                    command.Parameters.Add(new SQLiteParameter("codigo", this.codigo));
                    command.Parameters.Add(new SQLiteParameter("sucursal_id", this.sucursal_id));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public void delete(string sucursal_id, string fecha_actualizacion)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "DELETE FROM existencias WHERE sucursal_id = :sucursal_id AND fecha_actualizacion != :fecha;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("sucursal_id", sucursal_id));
                    command.Parameters.Add(new SQLiteParameter("fecha", fecha_actualizacion));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public void delete(string sucursal_id)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "DELETE FROM existencias WHERE sucursal_id = :sucursal_id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("sucursal_id", sucursal_id));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }
    }
}
