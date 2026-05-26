using System;
using System.Windows.Forms;
using System.Drawing;

namespace TYPSA.SharedLib.UserForms
{
    public class TextBoxForm : Form
    {
        private Label header;
        private Label label;
        private System.Windows.Forms.TextBox textBox;
        private Button btnNext;
        public string salida = null; // Almacena el texto ingresado

        public TextBoxForm(
            string formMessage,
            string formTitle = "Selection Form",
            string defaultValue = ""
        )
        {
            // Configuramos salida
            this.salida = null;

            formLayoutEntities layout = new formLayoutEntities();

            // ============
            // HELPER
            // ============

            cls_00_FormHelper.InitializeBaseLayout(
                this, layout, formTitle, formMessage, OnButtonClick, OnFormClosing
            );

            // ============
            // RESERVAS
            // ============

            cls_00_FormHelper.CalculateLayoutReservedSpaces(layout);

            // ============
            // CREAR ENTIDADES
            // ============

            int yOffset = layout.TopReserved;
            // label
            label = Clases.label_Default(
                "Enter a value:", layout.Spacing, yOffset, UIStyles.LabelItalic
            );
            this.Controls.Add(label);

            int textBoxWidth = Clases.get_width_textbox(label, layout.UiWidth, layout.Spacing);
            // textBox
            textBox = Clases.textBox_Default(
                textBoxWidth, label.Location.X + label.Width, yOffset
            );
            this.Controls.Add(textBox);
            // Asignamos valor
            textBox.Text = defaultValue;

            // ============
            // CALCULAR ALTURA NECESARIA
            // ============

            // Altura padding visual
            int paddingHeight = this.Height - this.ClientSize.Height;
            // Asignamos
            layout.TopPadding = paddingHeight;

            int yOffsetCalc = textBox.Height;
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

        private void OnButtonClick(object sender, EventArgs e)
        {
            salida = textBox.Text;
            // Validamos
            if (string.IsNullOrWhiteSpace(salida))
            {
                // Mensaje
                MessageBox.Show("" +
                    "Por favor, introduce un valor antes de continuar.",
                    "Campo vacío",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                // No cerramos el formulario
                return;
            }
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



