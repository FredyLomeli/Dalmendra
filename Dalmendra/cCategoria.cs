using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dalmendra
{
    class cCategoria
    {
        public string id;
        public string descripcion;
        public string palabra_clave;
        public string estado;

        public void insert()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Agrega el registro de la sucursal
                var cadena = "INSERT INTO categorias (descripcion, palabra_clave, " +
                    "estado) VALUES (?, ?, ?)";
                using (var command = new SQLiteCommand(cadena, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("descripcion", this.descripcion));
                    command.Parameters.Add(new SQLiteParameter("palabra_clave", this.palabra_clave));
                    command.Parameters.Add(new SQLiteParameter("estado", this.estado));
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
                var query = "UPDATE categorias SET descripcion = :descripcion, palabra_clave = :palabra_clave," +
                    " estado = :estado WHERE id = :id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("descripcion", this.descripcion));
                    command.Parameters.Add(new SQLiteParameter("palabra_clave", this.palabra_clave));
                    command.Parameters.Add(new SQLiteParameter("estado", this.estado));
                    command.Parameters.Add(new SQLiteParameter("id", this.id));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public void delete()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "DELETE FROM categorias WHERE id = :id;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("id", this.id));
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
                var query = "SELECT id AS ID, descripcion AS \"Descripción\", " +
                    "palabra_clave AS \"Clave\", estado AS \"Estado\" FROM categorias;";
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
                var query = "SELECT * FROM categorias;";
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

        public void clean()
        {
            this.id = null;
            this.descripcion = null;
            this.palabra_clave = null;
            this.estado = null;
        }

        public void up(string descripcion, string palabra_clave, string estado, string id = null)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.palabra_clave = palabra_clave;
            this.estado = estado;
        }
    }
}
