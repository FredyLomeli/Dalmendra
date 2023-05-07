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
    class cOrden
    {
        public string id;
        public string sucursal_id;
        public string codigo;
        public int orden;

        public cOrden()
        {
            //Constructor vacio
        }

        public cOrden(cExistencias nExistencias)
        {
            this.sucursal_id = nExistencias.sucursal_id;
            this.codigo = nExistencias.codigo;
            this.orden = nExistencias.orden;
        }

        public DataTable consulta()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                var query = "SELECT sucursal_id, codigo, orden FROM orden ORDER BY orden DESC;";
                ctx.Open();
                // Adaptador de datos, DataSet y tabla
                using (SQLiteDataAdapter db = new SQLiteDataAdapter(query, ctx))
                {
                    DataSet ds = new DataSet();
                    ds.Reset();
                    DataTable dt = new DataTable();
                    db.Fill(ds);
                    // y la mostramos en el DataGridView
                    dt = ds.Tables[0];
                    ctx.Close();
                    ctx.Dispose();
                    return dt;
                }
            }
        }

        public void insert()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Agrega el registro del orden
                var cadena = "INSERT INTO orden (sucursal_id, codigo, orden) " + 
                    " VALUES (:sucursal_id, :codigo, :orden)";
                using (var command = new SQLiteCommand(cadena, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("sucursal_id", this.sucursal_id));
                    command.Parameters.Add(new SQLiteParameter("codigo", this.codigo));
                    command.Parameters.Add(new SQLiteParameter("orden", this.orden));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
        }

        public void update(int orden, string codigo, string sucursal_id)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "UPDATE orden SET orden = :orden WHERE codigo = :codigo AND sucursal_id = :sucursal_id;";
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

        public void addRegToDataTable()
        {
            DataRow Renglon;
            Renglon = cModul.mOrden.NewRow();
            Renglon[0] = this.sucursal_id;
            Renglon[1] = this.codigo;
            Renglon[2] = this.orden;
            cModul.mOrden.Rows.Add(Renglon);
        }

        public int getOrden()
        {
            //Consulta en la variable si ya se tiene un orden a dicho articulo
            DataRow[] ordenRow = cModul.mOrden.Select("sucursal_id = '" + this.sucursal_id + "' AND codigo = '" + this.codigo + "'");
            //Si ya tiene un orden, se regresa el valor
            if (ordenRow.Length > 0)
                this.orden = Int32.Parse(ordenRow[0]["orden"].ToString());
            //Si no tiene un orden, le genera uno nuevo
            else
            {
                // ordenamos los valores de manera desendente
                cModul.mOrden.DefaultView.Sort = "orden DESC";
                cModul.mOrden = cModul.mOrden.DefaultView.ToTable();
                // filtramos por la sucursal que estamos editando
                DataRow[] ordenTemp = cModul.mOrden.Select("sucursal_id = '" + this.sucursal_id + "'");
                // Si ya hay registros en la tabla de orden de esa sucursal
                if (ordenTemp.Length > 0)
                    // Tomamos el valor de orden de la sucursal mas alto y le aumentamos 1
                    this.orden = Int32.Parse(ordenTemp[0]["orden"].ToString()) + 1;
                // Si no hay registros en la tabla de orden de esa sucursal
                else this.orden = 1;

                // Guardamos en la base de datos el nuevo valor de orden
                this.insert();
                // Cargamos el nuevo valor de orden al DataTable para evitar consulta
                this.addRegToDataTable();
            }
            return this.orden;
        }
    }
}
