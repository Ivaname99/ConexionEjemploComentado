using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DatosLayer;
using System.Net;
using System.Reflection;


namespace ConexionEjemplo
{
    public partial class Form1 : Form
    {
        // Repositorio para manejar datos de clientes
        CustomerRepository customerRepository = new CustomerRepository();

        public Form1()
        {
            InitializeComponent(); // Inicializa los componentes del formulario
        }

        // Carga los datos de todos los clientes al hacer clic en el botón "Cargar"
        private void btnCargar_Click(object sender, EventArgs e)
        {
            var Customers = customerRepository.ObtenerTodos(); // Obtiene todos los clientes
            dataGrid.DataSource = Customers; // Muestra los clientes en el DataGrid
        }

        // Método vacío para manejar cambios en el textBox1 (actualmente no hace nada)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Método vacío que se llama cuando se carga el formulario (actualmente no hace nada)
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Busca un cliente por ID cuando se hace clic en el botón "Buscar"
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var cliente = customerRepository.ObtenerPorID(txtBuscar.Text); // Obtiene el cliente por ID
            // Llena los campos del formulario con los datos del cliente
            tboxCustomerID.Text = cliente.CustomerID;
            tboxCompanyName.Text = cliente.CompanyName;
            tboxContacName.Text = cliente.ContactName;
            tboxContactTitle.Text = cliente.ContactTitle;
            tboxAddress.Text = cliente.Address;
            tboxCity.Text = cliente.City;
        }

        // Método vacío para manejar clics en el label4 (actualmente no hace nada)
        private void label4_Click(object sender, EventArgs e)
        {

        }

        // Inserta un nuevo cliente al hacer clic en el botón "Insertar"
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            var resultado = 0; // Variable para almacenar el resultado de la inserción

            var nuevoCliente = ObtenerNuevoCliente(); // Crea un nuevo cliente con los datos del formulario

            // Verifica si algún campo está vacío
            if (validarCampoNull(nuevoCliente) == false)
            {
                resultado = customerRepository.InsertarCliente(nuevoCliente); // Inserta el cliente
                MessageBox.Show("Guardado" + "Filas modificadas = " + resultado); // Muestra mensaje con el resultado
            }
            else
            {
                MessageBox.Show("Debe completar los campos por favor"); // Muestra mensaje si algún campo está vacío
            }
        }

        // Verifica si algún campo del objeto es nulo o vacío
        public Boolean validarCampoNull(Object objeto)
        {
            foreach (PropertyInfo property in objeto.GetType().GetProperties())
            {
                object value = property.GetValue(objeto, null);
                if ((string)value == "")
                {
                    return true; // Devuelve true si encuentra un campo vacío
                }
            }
            return false; // Devuelve false si todos los campos están completos
        }

        // Método vacío para manejar clics en el label5 (actualmente no hace nada)
        private void label5_Click(object sender, EventArgs e)
        {

        }

        // Actualiza los datos de un cliente al hacer clic en el botón "Modificar"
        private void btModificar_Click(object sender, EventArgs e)
        {
            var actualizarCliente = ObtenerNuevoCliente(); // Crea un cliente con los datos actualizados
            int actualizadas = customerRepository.ActualizarCliente(actualizarCliente); // Actualiza el cliente
            MessageBox.Show($"Filas actualizadas = {actualizadas}"); // Muestra mensaje con el número de filas actualizadas
        }

        // Obtiene un nuevo cliente con los datos del formulario
        private Customers ObtenerNuevoCliente()
        {
            var nuevoCliente = new Customers
            {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContacName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddress.Text,
                City = tboxCity.Text
            };

            return nuevoCliente;
        }

        // Elimina un cliente al hacer clic en el botón "Eliminar"
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int elimindas = customerRepository.EliminarCliente(tboxCustomerID.Text); // Elimina el cliente por ID
            MessageBox.Show("Filas eliminadas = " + elimindas); // Muestra mensaje con el número de filas eliminadas
        }
    }
}
