using System;
using System.Windows.Forms;
using System.Drawing;

namespace TYPSA.SharedLib.UserForms
{
    public class DropDownForm : Form
    {
        private Label header;
        private Button btnNext;
        private System.Windows.Forms.ComboBox comboBox;
        public object salida = null; // Almacena la salida seleccionada

        public DropDownForm(
            string formMessage, 
            object[] options,
            string formTitle = "Selection Form",
            bool? defaultValue = null
        )
        {
            // Configuramos salida
            this.salida = null;

            formLayoutEntities layout = new formLayoutEntities();

            // ============
            // HELPER
            // ============

            cls_00_FormHelper.InitializeBaseLayout(
                this, layout, formTitle, formMessage, NextButtonPressed, OnFormClosing
            );

            // ============
            // RESERVAS
            // ============

            cls_00_FormHelper.CalculateLayoutReservedSpaces(layout);

            // ============
            // CREAR ENTIDADES
            // ============

            int comboWidth = layout.UiWidth - (layout.Spacing * 2);
            // Calculamos desfase
            int yOffset = layout.TopReserved;
            // comboBox
            comboBox = Clases.comboBox_Default(
                comboWidth, layout.Spacing, yOffset, options
            );
            // Añadimos
            this.Controls.Add(comboBox);

            // Establecer valor por defecto si existe
            if (defaultValue.HasValue)
            {
                comboBox.SelectedItem = defaultValue.Value;
            }

            // ============
            // CALCULAR ALTURA NECESARIA
            // ============

            // Altura padding visual
            int paddingHeight = this.Height - this.ClientSize.Height;
            // Asignamos
            layout.TopPadding = paddingHeight;

            int yOffsetCalc = comboBox.Height;
            // Asignamos
            layout.YOffsetCalc = layout.TopPadding + layout.TopReserved + yOffsetCalc + layout.BottomReserved;

            // ============
            // APLICAR ALTURA FORM
            // ============

            this.Height = layout.YOffsetCalc;

            // ============
            // CENTRAR FORM
            // ============

            this.Location = Clases.centrar_Formulario(layout.ScreenSize, this.Width, this.Height);
        }

        // Eventos internos
        private void NextButtonPressed(object sender, EventArgs e)
        {
            if (comboBox.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select an option before continuing.",
                    "Selection Required"
                );
                return;
            }

            salida = comboBox.SelectedItem;
            this.Close();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // Si no se seleccionó nada, y el cierre es por el usuario (no por código)
            if (salida == null && e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "No option was selected. Do you want to cancel the process?",
                    "Confirmation"
                );

                if (result == DialogResult.No)
                {
                    // Cancela el cierre
                    e.Cancel = true;
                }
            }
        }

       
    }

}







