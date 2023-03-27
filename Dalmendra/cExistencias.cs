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
        public int orden;
        public string fecha_actualizacion;

        public DataTable consultaExistencias(string sucursal)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                var query = "SELECT codigo AS \"Codigo\", descripcion AS Descripción, " +
                    "existencia AS \"Existencia\", orden FROM existencias " + 
                    "WHERE sucursal_id = :sucursal ORDER BY orden ASC;";
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
                var query = "SELECT sucursal_id, codigo AS \"Codigo\", descripcion AS Descripción, " +
                    "existencia AS \"Existencia\", orden, id FROM existencias ORDER BY orden ASC;";
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
                    SQLcmd.CommandText = "SELECT * FROM existencias " +
                        "WHERE sucursal_id = :id AND codigo = :codigo ORDER BY orden ASC;";
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
                    "existencia, orden, fecha_actualizacion) VALUES (:sucursal_id, :codigo," +
                    " :descripcion, :existencia, :orden, :fecha_actualizacion)";
                using (var command = new SQLiteCommand(cadena, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("sucursal_id", this.sucursal_id));
                    command.Parameters.Add(new SQLiteParameter("codigo", this.codigo));
                    command.Parameters.Add(new SQLiteParameter("descripcion", this.descripcion));
                    command.Parameters.Add(new SQLiteParameter("existencia", this.existencia));
                    command.Parameters.Add(new SQLiteParameter("orden", this.orden));
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
                var query = "UPDATE existencias SET descripcion = :descripcion, " +
                    "existencia = :existencia, fecha_actualizacion = :fecha_actualizacion, " +
                    "orden = :orden WHERE id = :id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("descripcion", this.descripcion));
                    command.Parameters.Add(new SQLiteParameter("existencia", this.existencia));
                    command.Parameters.Add(new SQLiteParameter("orden", this.orden));
                    command.Parameters.Add(new SQLiteParameter("fecha_actualizacion", this.fecha_actualizacion));
                    command.Parameters.Add(new SQLiteParameter("codigo", this.codigo));
                    command.Parameters.Add(new SQLiteParameter("id", this.id));
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

        public void updateOrden(int orden, string codigo, string sucursal_id)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "UPDATE existencias SET orden = :orden " +
                    "WHERE codigo = :codigo AND sucursal_id = :sucursal_id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("orden", orden));
                    command.Parameters.Add(new SQLiteParameter("codigo", codigo));
                    command.Parameters.Add(new SQLiteParameter("sucursal_id", sucursal_id));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public void deletePorSucursal(string sucursal_id)
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

        public void save()
        {
            cOrden nOrden = new cOrden(this);
            // Si existe le asignamos un orden, si no, le creamos uno nuevo
            this.orden = nOrden.getOrden();

            //Validamos si el articulo ya existe en la base de datos para deifinir si actualizamos o insertamos
            DataRow[] existenciasTemp = cModul.mExistencias.Select("codigo = '" + this.codigo + "' AND sucursal_id = '" + this.sucursal_id + "'");
            //Si ya se tiene el registro
            if (existenciasTemp.Length > 0)
            {
                // Tomamos el ID
                this.id = existenciasTemp[0]["id"].ToString();
                //Actualizamos
                this.update();
            }
            //Si no se tiene le registro se crea uno nuevo
            else this.insert();
        }
    }
}
