using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public enum FieldType
    {
        TextBox,
        ComboBox
    }

    public class ComboTextBoxForm_NextToLabel : Form
    {
        private Button btnNext;

        public Dictionary<string, string> salida = null;

        private Dictionary<string, Control> propertyControls =
            new Dictionary<string, Control>();

        public class FieldDefinition
        {
            public string Propiedad { get; set; }
            public FieldType Type { get; set; }

            public string ValorDefecto { get; set; }

            public List<string> Opciones { get; set; } = new List<string>();
        }

        public ComboTextBoxForm_NextToLabel(
            string formMessage,
            List<FieldDefinition> fields,
            int controlWidth = -1,
            string formTitle = "Selection Form"
        )
        {
            // =========================
            // HELPER
            // =========================

            formLayoutEntities layout = cls_00_FormHelper.BuildBaseLayout(
                this, formTitle, formMessage, fields.Select(f => f.Propiedad), f => f,
                (l, offset) => l.TextBoxOffsetX = offset, NextButtonPressed, OnFormClosing
            );

            // ============
            // CREAR ENTIDADES
            // ============

            int availableWidth = this.ClientSize.Width - layout.TextBoxOffsetX - layout.Spacing;
            // Usamos por defecto cuando el arg no es == -1
            int finalWidth = controlWidth > 0 ? controlWidth : availableWidth;
            // Calculamos desfase segun scroll o no
            int yOffset = layout.NeedsScroll ? 0 : layout.TopReserved;
            // Iteramos
            foreach (var campo in fields)
            {
                // label
                Label labelPropiedad = Clases.label_Default(
                    campo.Propiedad, layout.Spacing, yOffset, UIStyles.LabelItalic
                );
                layout.Container.Controls.Add(labelPropiedad);

                // Definimos como nulo
                Control control = null;

                // ======================
                // TEXTBOX
                // ======================

                if (campo.Type == FieldType.TextBox)
                {
                    // textBox
                    TextBox tb = Clases.textBox_Default(
                        finalWidth, layout.TextBoxOffsetX, yOffset
                    );

                    // Asignar valor por defecto
                    tb.Text = campo.ValorDefecto ?? "";

                    control = tb;
                }

                // ======================
                // COMBOBOX
                // ======================

                else if (campo.Type == FieldType.ComboBox)
                {
                    // comboBox
                    ComboBox cb = Clases.comboBox_Default(
                        finalWidth, layout.TextBoxOffsetX, yOffset, campo.Opciones.ToArray()
                    );

                    // Asignar valor por defecto
                    if (!string.IsNullOrEmpty(campo.ValorDefecto) &&
                        campo.Opciones.Contains(campo.ValorDefecto))
                    {
                        cb.SelectedItem = campo.ValorDefecto;
                    }
                    else if (campo.Opciones.Any())
                    {
                        cb.SelectedIndex = 0;
                    }

                    control = cb;
                }

                // Añadimos
                layout.Container.Controls.Add(control);

                // Almacenamos
                propertyControls[campo.Propiedad] = control;

                // Incrementar Y para la siguiente fila
                yOffset += Math.Max(labelPropiedad.Height, control.Height) + layout.Spacing;
            }

            // ============
            // HELPER
            // ============

            cls_00_FormHelper.FinalizeLayoutControls(
                this, layout, yOffset, propertyControls, l => l.TextBoxOffsetX
            );
        }

        private void NextButtonPressed(object sender, EventArgs e)
        {
            salida = new Dictionary<string, string>();

            foreach (var pair in propertyControls)
            {
                string value = "";

                if (pair.Value is TextBox tb)
                    value = tb.Text.Trim();

                else if (pair.Value is ComboBox cb)
                    value = cb.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(value))
                    salida[pair.Key] = value;
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