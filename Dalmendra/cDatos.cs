using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dalmendra
{
    class cDatos
    {
        public static bool convertirIntToBool(int value)
        {
            if (value == 1)
                return true;
            else return false;
        }

        public static int convertirBoolToInt(bool value)
        {
            if (value)
                return 1;
            else return 0;
        }

        public static int convertirActivoToInt(string value)
        {
            if (value == "Activo")
                return 1;
            else return 0;
        }

        public static string convertirIntToActivo(string value)
        {
            if (value == "1")
                return "Activo";
            else return "Inactivo";
        }

        public static string convertirBoolToActivo(bool value)
        {
            if (value)
                return "Activo";
            else return "Inactivo";
        }

        public static bool convertirActivoToBool(string value)
        {
            if (value == "Activo")
                return true;
            else return false;
        }

        public static int convertirMinToMiliseg(int value)
        {
            return value * 60000;
        }

        public static void ConsultaDatos()
        {
            DataTable dt = consulta();
            // Consulta el ID
            DataRow[] IdDbSelectRow = dt.Select("descripcion = 'IdDbSelect' ");
            cModul.IdDbSelect = IdDbSelectRow[0]["value"].ToString();
            DataRow[] TimeSyncSucursalRow = dt.Select("descripcion = 'TimeSyncSucursal' ");
            cModul.TimeSyncSucursal = TimeSyncSucursalRow[0]["value"].ToString();
            DataRow[] TimeChangeSucursalRow = dt.Select("descripcion = 'TimeChangeSucursal' ");
            cModul.TimeChangeSucursal = TimeChangeSucursalRow[0]["value"].ToString();
            DataRow[] FirstReportRow = dt.Select("descripcion = 'FirstReport' ");
            cModul.FirstReport = FirstReportRow[0]["value"].ToString();
        }

        public static void UpdateDato(string descripcion, string value)
        {
            switch (descripcion)
            {
                case "IdDbSelect":
                    cModul.IdDbSelect = value;
                break;
                case "TimeSyncSucursal":
                    cModul.TimeSyncSucursal = value;
                break;
                case "TimeChangeSucursal":
                    cModul.TimeChangeSucursal = value;
                break;
                case "FirstReport":
                    cModul.FirstReport = value;
                break;
            }
        }

        public static void update(string descripcion, string value)
        {
            using (var ctx = cSQLite.GetInstance())
            {
                ctx.Open();
                // Edita el registro de la sucursal
                var query = "UPDATE configuracion SET value = :value " +
                    "WHERE descripcion = :descripcion ;";
                using (SQLiteCommand command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("value", value));
                    command.Parameters.Add(new SQLiteParameter("descripcion", descripcion));
                    command.ExecuteNonQuery();
                }
                ctx.Close();
                ctx.Dispose();
            }
            UpdateDato(descripcion, value);
        }

        public static DataTable consulta()
        {
            using (var ctx = cSQLite.GetInstance())
            {
                var query = "SELECT * FROM configuracion;";
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

        public static Color getColorFromArgb(string argbColor)
        {
            string[] mkColor = argbColor.Split('|');
            if (mkColor.Length == 4)
            {
                // Create a blue color using the FromArgb static method.
                Color myArgbColor = new Color();
                myArgbColor = Color.FromArgb(Int32.Parse(mkColor[0]),
                    Int32.Parse(mkColor[1]), Int32.Parse(mkColor[2]), Int32.Parse(mkColor[3]));
                return myArgbColor;
            }
            else
                return SystemColors.Control;
        }

    }
}
