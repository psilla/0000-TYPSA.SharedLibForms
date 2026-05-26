using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class TextBoxForm_NextToLabel : Form
    {
        private Label header;
        private Button btnNext;

        // Diccionario de salida
        public Dictionary<string, string> salida = null;
        // Diccionario interno de TextBox asociados a cada propiedad
        private Dictionary<string, TextBox> propertyTextBoxes = new Dictionary<string, TextBox>();

        public TextBoxForm_NextToLabel(
            string formMessage,
            List<(string propiedad, string valorDefecto)> fields,
            int textBoxWidth = -1,
            string formTitle = "Selection Form"
        )
        {
            // ============
            // HELPER
            // ============

            formLayoutEntities layout = cls_00_FormHelper.BuildBaseLayout(
                this, formTitle, formMessage, fields.Select(f => f.propiedad), f => f,
                (l, offset) => l.TextBoxOffsetX = offset, NextButtonPressed, OnFormClosing
            );

            // ============
            // CREAR ENTIDADES
            // ============

            int availableWidth = this.ClientSize.Width - layout.TextBoxOffsetX - layout.Spacing;
            // Usamos por defecto cuando el arg no es == -1
            int finalWidth = textBoxWidth > 0 ? textBoxWidth : availableWidth;
            // Calculamos desfase segun scroll o no
            int yOffset = layout.NeedsScroll ? 0 : layout.TopReserved;
            // Iteramos
            foreach (var campo in fields)
            {
                // label
                Label labelPropiedad = Clases.label_Default(
                    campo.propiedad, layout.Spacing, yOffset, UIStyles.LabelItalic
                );
                layout.Container.Controls.Add(labelPropiedad);

                // textBox
                TextBox textBox = Clases.textBox_Default(
                    finalWidth, layout.TextBoxOffsetX, yOffset
                );
                layout.Container.Controls.Add(textBox);

                // Asignar valor por defecto
                textBox.Text = campo.valorDefecto;

                // Almacenamos
                propertyTextBoxes[campo.propiedad] = textBox;

                // Incrementar Y para la siguiente fila
                yOffset += Math.Max(labelPropiedad.Height, textBox.Height) + layout.Spacing;
            }

            // ============
            // HELPER
            // ============

            cls_00_FormHelper.FinalizeLayoutControls(
                this, layout, yOffset, propertyTextBoxes, l => l.TextBoxOffsetX
            );
        }

        private void NextButtonPressed(object sender, EventArgs e)
        {
            salida = new Dictionary<string, string>();
            foreach (var pair in propertyTextBoxes)
            {
                string propertyName = pair.Key;
                string textBoxValue = pair.Value.Text.Trim();
                if (!string.IsNullOrEmpty(textBoxValue))
                {
                    salida[propertyName] = textBoxValue;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (salida == null)
            {
                MessageBox.Show(
                    "The form has been closed without saving the data.",
                    "Warning"
                );
            }
        }

       
    }
}

