using System.Collections.Generic;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class cls_00_InstaForm_ComboBox
    {
        public static object ComboBoxFormOut(
            string formMessage,
            string formText = "Selection Form",
            bool? defaultValue = null

        )
        {
            object[] options = { true, false };
            // Crear instancia del formulario
            DropDownForm ventana = new DropDownForm(
                formMessage, options, formText, defaultValue
            );

            // Mostrar el formulario de manera modal
            System.Windows.Forms.Application.Run(ventana);

            // return
            return ventana.salida;
        }

        public static string ComboBoxFormListOut(
            string formMessage, 
            List<string> options,
            string formText = "Selection Form",
            string defaultValue = null
        )
        {
            // Crear instancia del formulario
            DropDownFormList ventana = new DropDownFormList(
                formMessage, options, formText, defaultValue
            );

            // Mostrar el formulario de manera modal
            System.Windows.Forms.Application.Run(ventana);

            // return
            return ventana.salida as string ?? string.Empty;
        }

        public static object ComboBoxFormListOut_Atenea(
            string formMessage,
            object[] options,
            string formTitle = "Selection Form",
            object defaultValue = null
        )
        {
            // Crear instancia del formulario
            DropDownFormAtenea ventana = new DropDownFormAtenea(
                formMessage, options, formTitle, defaultValue
            );
            // Mostrar el formulario de manera modal
            Application.Run(ventana);
            // Validamos
            if (ventana.salida == null)
            {
                // Mensaje
                MessageBox.Show(
                    "No selection was made.\nReturning null.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                // Finalizamos
                return null;
            }
            // return
            return ventana.salida;
        }

        public static Dictionary<string, string> ComboBoxFormOut_NextToLabel(
            string formMessage,
            List<(string propiedad, List<string> options, string valorDefecto)> fields,
            int comboBoxWidth = -1,
            string formTitle = "Selection Form"
        )
        {
            using (ComboBoxForm_NextToLabel ventana = new ComboBoxForm_NextToLabel(
                formMessage, fields, comboBoxWidth, formTitle
            ))
            {
                // return
                return ventana.ShowDialog() == DialogResult.OK ? ventana.salida : null;
            }
        }

    






    }
}
