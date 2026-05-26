using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class ComboBoxForm_NextToLabel : Form
    {
        private Label header;
        private Button btnNext;

        // Diccionario de salida
        public Dictionary<string, string> salida = null;

        // Diccionario interno de ComboBox
        private Dictionary<string, ComboBox> propertyComboBoxes = new Dictionary<string, ComboBox>();

        public ComboBoxForm_NextToLabel(
            string formMessage,
            List<(string propiedad, List<string> opciones, string valorDefecto)> fields,
            int comboBoxWidth = -1,
            string formTitle = "Selection Form"
        )
        {
            // ============
            // HELPER
            // ============

            formLayoutEntities layout = cls_00_FormHelper.BuildBaseLayout(
                this, formTitle, formMessage, fields.Select(f => f.propiedad), f => f,
                (l, offset) => l.ComboBoxOffsetX = offset, NextButtonPressed, OnFormClosing
            );

            // ============
            // CREAR ENTIDADES
            // ============

            int availableWidth = this.ClientSize.Width - layout.ComboBoxOffsetX - layout.Spacing;
            // Usamos por defecto cuando el arg no es == -1
            int finalWidth = comboBoxWidth > 0 ? comboBoxWidth : availableWidth;
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

                // comboBox
                ComboBox comboBox = Clases.comboBox_Default(
                    finalWidth, layout.ComboBoxOffsetX, yOffset, campo.opciones.ToArray()
                );
                layout.Container.Controls.Add(comboBox);

                // Asignar valor por defecto
                if (!string.IsNullOrEmpty(campo.valorDefecto) && campo.opciones.Contains(campo.valorDefecto))
                {
                    comboBox.SelectedItem = campo.valorDefecto;
                }
                else if (campo.opciones.Any())
                {
                    // fallback
                    comboBox.SelectedIndex = 0; 
                }

                // Almacenamos
                propertyComboBoxes[campo.propiedad] = comboBox;

                // Incrementar Y para la siguiente fila
                yOffset += Math.Max(labelPropiedad.Height, comboBox.Height) + layout.Spacing;
            }

            // ============
            // HELPER
            // ============

            cls_00_FormHelper.FinalizeLayoutControls(
                this, layout, yOffset, propertyComboBoxes, l => l.ComboBoxOffsetX
            );
        }

        private void NextButtonPressed(object sender, EventArgs e)
        {
            salida = new Dictionary<string, string>();

            foreach (var pair in propertyComboBoxes)
            {
                string propertyName = pair.Key;
                string selectedValue = pair.Value.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    salida[propertyName] = selectedValue;
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ComboBoxForm_NextToLabel
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ComboBoxForm_NextToLabel";
            this.Load += new System.EventHandler(this.ComboBoxForm_NextToLabel_Load);
            this.ResumeLayout(false);

        }

        private void ComboBoxForm_NextToLabel_Load(object sender, EventArgs e)
        {

        }
    }
}