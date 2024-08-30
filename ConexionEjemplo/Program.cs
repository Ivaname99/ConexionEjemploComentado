using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionEjemplo
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread] // Especifica que el hilo que ejecuta la aplicación debe usar un modelo de apartamento de un solo hilo (STA). Requerido para la mayoría de las aplicaciones de Windows Forms.
        static void Main()
        {
            // Habilita los estilos visuales para la aplicación. Esto asegura que los controles de Windows Forms se dibujen con el estilo visual de la versión de Windows en ejecución.
            Application.EnableVisualStyles();

            // Establece el formato de renderizado de texto predeterminado para los controles de Windows Forms. Si es false, se usa el renderizado de texto heredado.
            Application.SetCompatibleTextRenderingDefault(false);

            // Ejecuta la aplicación y muestra el formulario Form1.
            Application.Run(new Form1());
        }
    }
}
