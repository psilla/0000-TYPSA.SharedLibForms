using System.Collections.Generic;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class cls_00_InstaForm_CheckedListBox
    {
        public static List<string> CheckListBoxFormOut(
            string formMessage,
            List<string> options
        )
        {
            // Crear instancia del formulario
            CheckListBoxForm ventana = new CheckListBoxForm(formMessage, options);
            // Mostrar el formulario de manera modal
            Application.Run(ventana);
            // Validamos
            if (ventana.salida == null)
            {
                // Mensaje
                MessageBox.Show(
                    "No selection was made.\nReturning null.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                // Finalizamos
                return null;
            }
            // return
            return ventana.salida;
        }


        public static List<string> CheckListBoxFormSearchOut(
            string formMessage,
            List<string> options,
            List<string> defaultValues = null
        )
        {
            // Crear instancia del formulario
            CheckListBoxFormSearch ventana = new CheckListBoxFormSearch(
                formMessage, options, defaultValues
            );
            // Mostrar el formulario de manera modal
            Application.Run(ventana);
            // Validamos
            if (ventana.salida == null)
            {
                // Mensaje
                MessageBox.Show(
                    "No selection was made.\nReturning null.",
                    "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                // Finalizamos
                return null;
            }
            // return
            return ventana.salida;
        }

        public static List<string> CheckListBoxFormSelectedItemsOut(
            string formMessage, 
            List<string> options, 
            HashSet<string> defaultValues = null
        )
        {
            // Crear instancia del formulario
            using (CheckListBoxFormSelectedItems ventana = new CheckListBoxFormSelectedItems(
                formMessage, options, defaultValues
            ))
            {
                // Mostrar el formulario de manera modal
                DialogResult result = ventana.ShowDialog();

                // Retornar la propiedad salida
                return ventana.salida;
            }
        }

        public static string CheckListBoxFormUniqueSelectionOut(
            string formMessage, 
            List<string> options
        )
        {
            // Crear instancia del formulario
            using (CheckListBoxFormUniqueSelection ventana = new CheckListBoxFormUniqueSelection(
                formMessage, options
            ))
            {
                // Mostrar el formulario de manera modal
                DialogResult result = ventana.ShowDialog();

                // Retornar la propiedad salida
                return ventana.salida;
            }
        }

        public static string CheckListBoxFormUniqueSelectionSearchOut(
            string formMessage,
            List<string> options,
            string defaultValue = null
        )
        {
            // Crear instancia del formulario
            CheckListBoxFormUniqueSelectionSearch ventana = new CheckListBoxFormUniqueSelectionSearch(
                formMessage, options, defaultValue
            );
            // Mostrar el formulario de manera modal
            Application.Run(ventana);
            // Validamos
            if (ventana.salida == null)
            {
                // Mensaje
                MessageBox.Show(
                    "No selection was made.\nReturning null.",
                    "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                // Finalizamos
                return null;
            }
            // return
            return ventana.salida;
        }








    }
}
