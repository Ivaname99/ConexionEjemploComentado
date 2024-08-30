using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class CustomerRepository
    {
        // Método para obtener todos los clientes desde la base de datos
        public List<Customers> ObtenerTodos()
        {
            using (var conexion = DataBase.GetSqlConnection())
            { // Establece una conexión a la base de datos
                // Consulta SQL para seleccionar todos los campos de la tabla Customers
                String selectFrom = "SELECT [CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Region], [PostalCode], [Country], [Phone], [Fax] FROM [dbo].[Customers]";

                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                { // Crea un comando SQL
                    SqlDataReader reader = comando.ExecuteReader(); // Ejecuta el comando y obtiene un lector de datos
                    List<Customers> Customers = new List<Customers>(); // Crea una lista para almacenar los clientes

                    // Lee cada fila del resultado
                    while (reader.Read())
                    {
                        var customers = LeerDelDataReader(reader); // Convierte la fila en un objeto Customers
                        Customers.Add(customers); // Añade el cliente a la lista
                    }
                    return Customers; // Devuelve la lista de clientes
                }
            }
        }

        // Método para obtener un cliente específico por su ID
        public Customers ObtenerPorID(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            { // Establece una conexión a la base de datos
                // Consulta SQL para seleccionar un cliente por ID
                String selectForID = "SELECT [CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Region], [PostalCode], [Country], [Phone], [Fax] FROM [dbo].[Customers] WHERE CustomerID = @customerId";

                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                { // Crea un comando SQL
                    comando.Parameters.AddWithValue("customerId", id); // Añade el parámetro de ID a la consulta

                    var reader = comando.ExecuteReader(); // Ejecuta la consulta y obtiene un lector de datos
                    Customers customers = null; // Variable para almacenar el cliente encontrado

                    // Lee la primera fila del resultado (si existe)
                    if (reader.Read())
                    {
                        customers = LeerDelDataReader(reader); // Convierte la fila en un objeto Customers
                    }
                    return customers; // Devuelve el cliente encontrado o null
                }
            }
        }

        // Convierte una fila de datos del lector en un objeto Customers
        public Customers LeerDelDataReader(SqlDataReader reader)
        {
            Customers customers = new Customers(); // Crea un nuevo objeto Customers
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"]; // Asigna el valor del CustomerID, o un espacio si es null
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"]; // Asigna el valor de CompanyName, o una cadena vacía si es null
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"]; // Asigna el valor de ContactName, o una cadena vacía si es null
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"]; // Asigna el valor de ContactTitle, o una cadena vacía si es null
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"]; // Asigna el valor de Address, o una cadena vacía si es null
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"]; // Asigna el valor de City, o una cadena vacía si es null
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"]; // Asigna el valor de Region, o una cadena vacía si es null
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"]; // Asigna el valor de PostalCode, o una cadena vacía si es null
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"]; // Asigna el valor de Country, o una cadena vacía si es null
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"]; // Asigna el valor de Phone, o una cadena vacía si es null
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"]; // Asigna el valor de Fax, o una cadena vacía si es null
            return customers; // Devuelve el objeto Customers
        }

        // Método para insertar un nuevo cliente en la base de datos
        public int InsertarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            { // Establece una conexión a la base de datos
                // Consulta SQL para insertar un nuevo cliente
                String insertInto = "INSERT INTO [dbo].[Customers] ([CustomerID], [CompanyName], [ContactName], [ContactTitle], [Address], [City]) VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City)";

                using (var comando = new SqlCommand(insertInto, conexion))
                { // Crea un comando SQL
                    int insertados = parametrosCliente(customer, comando); // Añade los parámetros del cliente al comando y ejecuta la consulta
                    return insertados; // Devuelve el número de filas insertadas
                }
            }
        }

        // Método para actualizar un cliente existente en la base de datos
        public int ActualizarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            { // Establece una conexión a la base de datos
                // Consulta SQL para actualizar un cliente por ID
                String ActualizarCustomerPorID = "UPDATE [dbo].[Customers] SET [CustomerID] = @CustomerID, [CompanyName] = @CompanyName, [ContactName] = @ContactName, [ContactTitle] = @ContactTitle, [Address] = @Address, [City] = @City WHERE CustomerID = @CustomerID";

                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion))
                { // Crea un comando SQL
                    int actualizados = parametrosCliente(customer, comando); // Añade los parámetros del cliente al comando y ejecuta la consulta
                    return actualizados; // Devuelve el número de filas actualizadas
                }
            }
        }

        // Añade los parámetros del cliente al comando SQL y ejecuta la consulta
        public int parametrosCliente(Customers customer, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID); // Añade el parámetro CustomerID
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName); // Añade el parámetro CompanyName
            comando.Parameters.AddWithValue("ContactName", customer.ContactName); // Añade el parámetro ContactName
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactTitle); // Añade el parámetro ContactTitle
            comando.Parameters.AddWithValue("Address", customer.Address); // Añade el parámetro Address
            comando.Parameters.AddWithValue("City", customer.City); // Añade el parámetro City
            var insertados = comando.ExecuteNonQuery(); // Ejecuta la consulta y devuelve el número de filas afectadas
            return insertados; // Devuelve el número de filas afectadas
        }

        // Método para eliminar un cliente por su ID
        public int EliminarCliente(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            { // Establece una conexión a la base de datos
                // Consulta SQL para eliminar un cliente por ID
                String EliminarCliente = "DELETE FROM [dbo].[Customers] WHERE CustomerID = @CustomerID";

                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion))
                { // Crea un comando SQL
                    comando.Parameters.AddWithValue("@CustomerID", id); // Añade el parámetro de ID
                    int eliminados = comando.ExecuteNonQuery(); // Ejecuta la consulta y devuelve el número de filas eliminadas
                    return eliminados; // Devuelve el número de filas eliminadas
                }
            }
        }
    }
}
