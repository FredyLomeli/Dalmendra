using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
