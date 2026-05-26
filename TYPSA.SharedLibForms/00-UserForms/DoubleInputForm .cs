using System;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class DoubleInputForm : Form
    {
        private Label header;
        private Label label;
        private System.Windows.Forms.TextBox textBox;
        private Button btnNext;
        public double? salida = null; // Salida de tipo double

        public DoubleInputForm(
            string formMessage,
            string formTitle = "Selection Form",
            double? defaultValue = null
        )
        {
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
            // Activamos validacion de entrada
            textBox.KeyPress += TextBox_KeyPress;
            // Mostrar el valor por defecto formateado
            if (defaultValue.HasValue)
                textBox.Text = defaultValue.Value.ToString("0.##"); 
            this.Controls.Add(textBox);

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

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal y tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            // Intentar convertir el texto a un número decimal
            if (double.TryParse(textBox.Text, out double valor))
            {
                salida = valor;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Clear();
                textBox.Focus();
            }
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
