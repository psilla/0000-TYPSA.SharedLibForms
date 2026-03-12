using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class InstanciarFormularios
    {
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
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                // Finalizamos
                return null;
            }
            // return
            return ventana.salida;
        }

        public static string CheckListBoxFormUniqueSelectionSearchOut(
            string mensaje,
            List<string> listInput,
            string defaultSelectedItem = null
        )
        {
            // Crear instancia del formulario
            CheckListBoxFormUniqueSelectionSearch ventana =
                new CheckListBoxFormUniqueSelectionSearch(mensaje, listInput, defaultSelectedItem);
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

        public static List<string> CheckListBoxFormSelectedItemsOut(
            string mensaje, 
            List<string> listInput, 
            HashSet<string> itemsMarcadosPorDefecto = null
        )
        {
            // Crear instancia del formulario
            using (CheckListBoxFormSelectedItems ventana =
                new CheckListBoxFormSelectedItems(mensaje, listInput, itemsMarcadosPorDefecto))
            {
                // Mostrar el formulario de manera modal
                DialogResult result = ventana.ShowDialog();

                // Retornar la propiedad salida
                return ventana.salida;
            }
        }

        public static string CheckListBoxFormUniqueSelectionOut(
            string mensaje, 
            List<string> listInput
        )
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

        public static double? DoubleInputFormOut(
            string mensaje, 
            string formText, 
            double? defaultValue = null
        )
        {
            using (DoubleInputForm ventana =
                new DoubleInputForm(mensaje, formText, defaultValue))
            {
                // return
                return ventana.ShowDialog() == DialogResult.OK ? ventana.salida : null;
            }
        }

        public static object DropDownFormOut(
            string mensaje, 
            bool? valorPorDefecto = null
        )
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

        public static object DropDownFormOutWithFormText(
            string mensaje, 
            string formText, 
            bool? valorPorDefecto = null
        )
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

        public static string DropDownFormListOut(
            string mensaje, 
            List<string> listInput, 
            string formText, 
            string defaultValue = null
        )
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

        public static string FilePathFormOut(
            string mensaje
        )
        {
            // Crear instancia del formulario
            FilePathForm ventana = new FilePathForm(mensaje);

            // Mostrar el formulario de manera modal
            Application.Run(ventana);

            // Retornar la propiedad salida
            return ventana.salida;
        }

        public static string TextBoxFormOut(
            string mensaje, 
            string defaultValue = ""
        )
        {
            // Crear instancia del formulario con mensaje y valor por defecto
            TextBoxForm ventana = new TextBoxForm(mensaje, defaultValue);

            // Mostrar el formulario de manera modal
            System.Windows.Forms.Application.Run(ventana);

            // Retornar la propiedad salida
            return ventana.salida;
        }

        public static Dictionary<string, string> TextBoxFormOut_NextToLabel(
            string mensaje,
            List<(string propiedad, string valorDefecto)> props
        )
        {
            using (TextBoxForm_NextToLabel ventana = new TextBoxForm_NextToLabel(mensaje, props))
            {
                return ventana.ShowDialog() == DialogResult.OK ? ventana.salida : null;
            }
        }

        public static Dictionary<string, double> TextBoxFormOut_NextToLabel_Double(
            string mensaje,
            Dictionary<string, double> valoresIniciales
        )
        {
            // Convertimos a lista de strings para el formulario
            var props = valoresIniciales
                .Select(kvp => (kvp.Key, kvp.Value.ToString()))
                .ToList();

            using (TextBoxForm_NextToLabel ventana = new TextBoxForm_NextToLabel(mensaje, props))
            {
                // Validamos
                if (ventana.ShowDialog() != DialogResult.OK) return null;

                Dictionary<string, double> result = new Dictionary<string, double>();
                // Iteramos
                foreach (var kvp in ventana.salida)
                {
                    // Validamos
                    if (!double.TryParse(
                            kvp.Value,
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out double value))
                    {
                        // Mensaje
                        MessageBox.Show(
                            $"Invalid numeric value for '{kvp.Key}': {kvp.Value}", "Invalid input",
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                        // Finalizamos
                        return null;
                    }
                    // Almacenamos
                    result[kvp.Key] = value;
                }
                // return
                return result;
            }
        }

        public static Dictionary<string, int> TextBoxFormOut_NextToLabel_Integer(
            string mensaje,
            Dictionary<string, int> valoresIniciales
        )
        {
            // Convertimos a lista de strings para el formulario
            var props = valoresIniciales
                .Select(kvp => (kvp.Key, kvp.Value.ToString()))
                .ToList();

            using (TextBoxForm_NextToLabel ventana = new TextBoxForm_NextToLabel(mensaje, props))
            {
                // Validamos
                if (ventana.ShowDialog() != DialogResult.OK) return null;

                Dictionary<string, int> result = new Dictionary<string, int>();
                // Iteramos
                foreach (var kvp in ventana.salida)
                {
                    // Validamos
                    if (!int.TryParse(
                            kvp.Value,
                            System.Globalization.NumberStyles.Integer,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out int value))
                    {
                        // Mensaje
                        MessageBox.Show(
                            $"Invalid numeric value for '{kvp.Key}': {kvp.Value}", "Invalid input",
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                        // Finalizamos
                        return null;
                    }
                    // Almacenamos
                    result[kvp.Key] = value;
                }
                // return
                return result;
            }
        }

        public static Dictionary<string, string> TextBoxFormOut_NextToLabel_String(
            string mensaje,
            Dictionary<string, string> valoresIniciales,
            int textBoxWidth = 100
        )
        {
            // Convertimos a lista de strings para el formulario
            var props = valoresIniciales
                .Select(kvp => (kvp.Key, kvp.Value))
                .ToList();

            using (TextBoxForm_NextToLabel ventana = new TextBoxForm_NextToLabel(mensaje, props, textBoxWidth))
            {
                // Validamos
                if (ventana.ShowDialog() != DialogResult.OK) return null;

                Dictionary<string, string> result = new Dictionary<string, string>();
                // Iteramos 
                foreach (var kvp in ventana.salida)
                {
                    // Permitimos string vacío, pero no null
                    result[kvp.Key] = kvp.Value?.Trim() ?? string.Empty;
                }

                // return
                return result;
            }
        }







    }
}
