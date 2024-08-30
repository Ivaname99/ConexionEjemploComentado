using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DatosLayer
{
    public class DataBase
    {
        public static string ConnectionString // Establecemos una propiedad que obtiene la cadena de conexión para la base de datos
        {
            get
            {
                // Obtiene la cadena de conexión desde el archivo de configuración de la aplicación
                string CadenaConexion = ConfigurationManager
                    .ConnectionStrings["NWConnection"]
                    .ConnectionString;

                // Crea un objeto SqlConnectionStringBuilder para modificar la cadena de conexión
                SqlConnectionStringBuilder conexionBuilder =
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Asigna el nombre de la aplicación en la cadena de conexión, si se ha especificado
                conexionBuilder.ApplicationName =
                    ApplicationName ?? conexionBuilder.ApplicationName;

                // Establece el tiempo de espera de conexión, si se ha especificado; de lo contrario, usa el valor predeterminado
                conexionBuilder.ConnectTimeout = (ConnectionTimeout > 0)
                    ? ConnectionTimeout : conexionBuilder.ConnectTimeout;

                
                return conexionBuilder.ToString();// Devuelve la cadena de conexión modificada como una cadena de texto
            }
        }

        // Propiedad para establecer el tiempo de espera de la conexión en segundos
        public static int ConnectionTimeout { get; set; }

        // Propiedad para establecer el nombre de la aplicación en la cadena de conexión
        public static string ApplicationName { get; set; }

        // Método para obtener una conexión SQL abierta a la base de datos
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection conexion = new SqlConnection(ConnectionString); // Crea una nueva instancia de SqlConnection utilizando la cadena de conexión proporcionada
            conexion.Open(); // Abre la conexión a la base de datos
            return conexion; // Devuelve la conexión abierta
        }
    }
}
