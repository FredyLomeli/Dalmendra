using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Dalmendra
{
    public static class cModul
    {
        public static string NombreDelPrograma = "Dalmendra";
        public static string SucursalActulizadaCorrecto = "El registro se ha actualizado con exito.";
        public static string SucursalGuardadaCorrecto = "El registro se ha guardado con exito.";
        public static string CategoriaGuardadaCorrecto = "El registro se ha guardado con exito.";
        public static string SucursalValidateError = "Error al Guardar, no se puede dejar ningun campo vacio.";
        public static string SucursalEliminadaCorrecto = "El registro se ha eliminado correctamente.";
        public static string RegistroNoEncontrado = "El registro no se ha encontrado.";
        public static string NoExistenCategoriasActivas = "Registra al menos una categoria para mostrar este reporte";
        public static string OrdenDebeSerMayorQueUno = "Para Ordenar los registros debes tener mas de 1 registro.";
        public static string NoExistenRegistros = "No existen registros vinculados a esta busqueda.";
        public static string ConfigSave = "Se han Guardado correctamenta las configuraciones.";
        public static string SinConexionConSucursales = "No existe ninguna conexión con las sucursales, revisa las conexiones existentes o genera una nueva.";

        //Formularios Abiertos
        public static bool EstaAbiertoFrmListado = false;
        public static bool EstaAbiertoFrmCategorias = false;

        public static bool ThisFirstTime = false;

        public static string GuardarSinConexion = "No hay conexion con la base de datos, ¿Deseas guardar los datos? ";

        public static string PreguntaEliminar(string xNombreProceso)
        {
            return "¿Deseas eliminar el registro \"" + xNombreProceso + "\"?.";
        }

        public static DataTable mSucursales = new DataTable();
        public static DataTable mExistencias = new DataTable();
        public static DataTable mSucursalesListado = new DataTable();
        public static DataTable mCateoriasListado = new DataTable();
        public static DataTable mCategorias = new DataTable();
        public static DataTable SucursalesActivas = new DataTable();
        public static bool banActualizacion = true;
        public static DataTable CatalogoTemporal = new DataTable();

        // ID de la sucursal a mostrar en el reporte seleccionado
        public static string IdDbSelect = "1";
        // Tiempo en minutos a sincronizar las sucursales 0 es inactivo
        public static string TimeSyncSucursal = "1";
        // Tiempo en minutos para cambiar de una sucursal a otra 0 es inactivo
        public static string TimeChangeSucursal = "15";
        // Reporte que muestra por defecto al ingresar al programa
        // PorCategorias, ListadoConCodigo, ListadoSinCodigo
        public static string FirstReport = "PorCategorias";

        public static string ErrorProbocado = "Error al intentar mostrar el reporte, revisa la version del sistema operativo y NetFramework valido.\n Error: 314058 Descripción de la Consola de recuperación de Windows 7";
        // Variables para controlar los errores con la sincronización
        public static bool BanErrorSincronizacion = false;
        public static string ErrorSincronizacion = "";
    }
}
