using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class cls_00_InstaForm_TextBox
    {
        public static double? TextBoxFormOutAsDouble(
            string formMessage,
            string formTitle = "Selection Form",
            double? defaultValue = null
        )
        {
            using (DoubleInputForm ventana = new DoubleInputForm(
                formMessage, formTitle, defaultValue
            ))
            {
                // return
                return ventana.ShowDialog() == DialogResult.OK ? ventana.salida : null;
            }
        }

        public static string TextBoxFormOutAsStr(
            string formMessage,
            string formTitle = "Selection Form",
            string defaultValue = ""
        )
        {
            // Crear instancia del formulario con formMessage y valor por defecto
            TextBoxForm ventana = new TextBoxForm(
                formMessage, formTitle, defaultValue
            );

            // Mostrar el formulario de manera modal
            System.Windows.Forms.Application.Run(ventana);

            // return
            return ventana.salida;
        }

        public static string FilePathFormOut(
            string formMessage,
            string formTitle = "Selection Form"
        )
        {
            // Crear instancia del formulario
            FilePathForm ventana = new FilePathForm(formMessage, formTitle);

            // Mostrar el formulario de manera modal
            Application.Run(ventana);

            // Retornar la propiedad salida
            return ventana.salida;
        }

        public static Dictionary<string, string> TextBoxFormOut_NextToLabel(
            string formMessage,
            List<(string propiedad, string valorDefecto)> fields,
            int textBoxWidth = -1,
            string formTitle = "Selection Form"
        )
        {
            using (TextBoxForm_NextToLabel ventana = new TextBoxForm_NextToLabel(
                formMessage, fields, textBoxWidth, formTitle
            ))
            {
                // return
                return ventana.ShowDialog() == DialogResult.OK ? ventana.salida : null;
            }
        }

        public static Dictionary<string, string> TextBoxFormOut_NextToLabel_String(
            string formMessage,
            Dictionary<string, string> fieldsByDict,
            int textBoxWidth = -1,
            string formTitle = "Selection Form"
        )
        {
            // Convertimos a lista de strings para el formulario
            var fields = fieldsByDict
                .Select(kvp => (kvp.Key, kvp.Value))
                .ToList();

            using (TextBoxForm_NextToLabel ventana = new TextBoxForm_NextToLabel(
                formMessage, fields, textBoxWidth, formTitle)
            )
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

        public static Dictionary<string, double> TextBoxFormOut_NextToLabel_Double(
            string formMessage,
            Dictionary<string, string> fieldsByDict,
            int textBoxWidth = -1,
            string formTitle = "Selection Form"
        )
        {
            // Convertimos a lista de strings para el formulario
            var fields = fieldsByDict
                .Select(kvp => (kvp.Key, kvp.Value.ToString()))
                .ToList();

            using (TextBoxForm_NextToLabel ventana = new TextBoxForm_NextToLabel(
                formMessage, fields, textBoxWidth, formTitle)
            )
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
                            out double value)
                    )
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
            string formMessage,
            Dictionary<string, string> fieldsByDict,
            int textBoxWidth = -1,
            string formTitle = "Selection Form"
        )
        {
            // Convertimos a lista de strings para el formulario
            var fields = fieldsByDict
                .Select(kvp => (kvp.Key, kvp.Value.ToString()))
                .ToList();

            using (TextBoxForm_NextToLabel ventana = new TextBoxForm_NextToLabel(
                formMessage, fields, textBoxWidth, formTitle)
            )
            {
                // Validamos
                if (ventana.ShowDialog() != DialogResult.OK) return null;

                Dictionary<string, int> result = new Dictionary<string, int>();
                // Iteramos
                foreach (var kvp in ventana.salida)
                {
                    // Validamos
                    if (!int.TryParse(
                        kvp.Value, System.Globalization.NumberStyles.Integer,
                        System.Globalization.CultureInfo.InvariantCulture,out int value
                    ))
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

        







    }
}
