using System.Collections.Generic;
using System.Windows.Forms;
using TYPSA.MC.RibbonButton.Revit.UserForms;

namespace TYPSA.SharedLib.UserForms
{
    public class InstanciarFormularios
    {
        public static List<string> CheckListBoxFormOut(
            string mensaje, 
            List<string> listInput
        )
        {
            // Crear instancia del formulario
            CheckListBoxForm ventana =
                new CheckListBoxForm(mensaje, listInput);

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
            string mensaje,
            List<string> listInput,
            List<string> listInputByDefualt = null
        )
        {
            // Crear instancia del formulario
            CheckListBoxFormSearch ventana =
                new CheckListBoxFormSearch(mensaje, listInput, listInputByDefualt);

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

        public static List<string> CheckListBoxFormSelectedItemsOut(
            string mensaje, List<string> listInput, HashSet<string> itemsMarcadosPorDefecto = null
        )
        {
            // Crear instancia del formulario
            using (CheckListBoxFormSelectedItems ventana =
                new CheckListBoxFormSelectedItems(mensaje, listInput, itemsMarcadosPorDefecto))
            {
                // Mostrar el formulario de manera modal
                DialogResult result = ventana.ShowDialog();

                // Retornar la propiedad salida
                return ventana.Salida;
            }
        }

        public static string CheckListBoxFormUniqueSelectionOut(string mensaje, List<string> listInput)
        {
            // Crear instancia del formulario
            using (CheckListBoxFormUniqueSelection ventana =
                new CheckListBoxFormUniqueSelection(mensaje, listInput))
            {
                // Mostrar el formulario de manera modal
                DialogResult result = ventana.ShowDialog();

                // Retornar la propiedad salida
                return ventana.salida;
            }
        }

        public static double? DoubleInputFormOut(string mensaje, string formText, double? defaultValue = null)
        {
            using (DoubleInputForm ventana =
                new DoubleInputForm(mensaje, formText, defaultValue))
            {
                // return
                return ventana.ShowDialog() == DialogResult.OK ? ventana.salida : null;
            }
        }

        public static object DropDownFormOut(string mensaje, bool? valorPorDefecto = null)
        {
            // Opciones para el ComboBox True/False
            object[] opciones = { true, false };

            // Crear instancia del formulario
            DropDownForm ventana =
                new DropDownForm(mensaje, opciones, valorPorDefecto);

            // Mostrar el formulario de manera modal
            System.Windows.Forms.Application.Run(ventana);

            // Retornar la propiedad salida
            return ventana.salida;
        }

        public static object DropDownFormOutWithFormText(string mensaje, string formText, bool? valorPorDefecto = null)
        {
            // Opciones para el ComboBox True/False
            object[] opciones = { true, false };

            // Crear instancia del formulario
            DropDownFormWithFormText ventana =
                new DropDownFormWithFormText(mensaje, opciones, formText, valorPorDefecto);

            // Mostrar el formulario de manera modal
            System.Windows.Forms.Application.Run(ventana);

            // Retornar la propiedad salida
            return ventana.salida;
        }

        public static string DropDownFormListOut(string mensaje, List<string> listInput, string formText, string defaultValue = null)
        {

            // Crear instancia del formulario
            DropDownFormList ventana =
                new DropDownFormList(mensaje, listInput, formText, defaultValue);

            // Mostrar el formulario de manera modal
            System.Windows.Forms.Application.Run(ventana);

            // Retornar la propiedad salida convertida a string o un valor predeterminado
            return ventana.salida as string ?? string.Empty;
        }

        public static object DropDownFormAteneaOut(
            string mensaje,
            object[] lista,
            object defaultValue = null
        )
        {
            // Crear instancia del formulario
            DropDownFormAtenea ventana = 
                new DropDownFormAtenea(mensaje, lista, defaultValue);

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

        public static string FilePathFormOut(string mensaje)
        {
            // Crear instancia del formulario
            FilePathForm ventana = new FilePathForm(mensaje);

            // Mostrar el formulario de manera modal
            Application.Run(ventana);

            // Retornar la propiedad salida
            return ventana.salida;
        }

        public static string TextBoxFormOut(string mensaje, string defaultValue = "")
        {
            // Crear instancia del formulario con mensaje y valor por defecto
            TextBoxForm ventana = new TextBoxForm(mensaje, defaultValue);

            // Mostrar el formulario de manera modal
            System.Windows.Forms.Application.Run(ventana);

            // Retornar la propiedad salida
            return ventana.salida;
        }





    }
}
