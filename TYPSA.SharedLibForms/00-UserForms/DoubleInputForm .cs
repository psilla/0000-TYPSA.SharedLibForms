using System;
using System.Windows.Forms;
using System.Drawing;

namespace TYPSA.SharedLib.UserForms
{
    public class DoubleInputForm : Form
    {
        private Label header;
        private Label label;
        private System.Windows.Forms.TextBox textBox;
        private Button btnNext;
        public double? salida = null; // Salida de tipo double (puede ser null si no es válido)

        public DoubleInputForm(string mensajeSel, string formText, double? defaultValue = null)
        {
            // Configuración del formulario
            this.Text = formText;
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Dimensiones y espaciado
            var screenSize = Screen.PrimaryScreen.WorkingArea;
            this.Width = screenSize.Width / 2;
            this.Height = screenSize.Height / 5;
            int spacing = 10;
            int uiWidth = this.ClientSize.Width;
            int uiHeight = this.ClientSize.Height;

            // Añadimos controles
            this.Location = Clases.centrar_Formulario(screenSize, this.Width, this.Height);

            // Header
            header = Clases.label_Header(mensajeSel, spacing);
            this.Controls.Add(header);

            // Label
            label = Clases.label_TextBox(this.ClientSize, spacing);
            this.Controls.Add(label);

            // TextBox
            int fixedWidth = 150;
            textBox = Clases.textBox_NextToLabel(fixedWidth, label);
            // ← aquí se activa la validación de entrada
            textBox.KeyPress += TextBox_KeyPress;
            // Mostrar el valor por defecto formateado
            if (defaultValue.HasValue)
                textBox.Text = defaultValue.Value.ToString("0.##"); 
            this.Controls.Add(textBox);

            // Botón Next
            btnNext = Clases.button_Next(uiWidth, spacing, uiHeight);
            btnNext.Click += OnButtonClick;
            this.Controls.Add(btnNext);

            // 💡 Enter activa el botón Next
            this.AcceptButton = btnNext;
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
                this.DialogResult = DialogResult.OK; // ✔ Importante para que funcione en la otra función
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Clear();
                textBox.Focus();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DoubleInputForm
            // 
            this.ClientSize = new System.Drawing.Size(292, 212);
            this.Name = "DoubleInputForm";
            this.Load += new System.EventHandler(this.DoubleInputForm_Load);
            this.ResumeLayout(false);

        }

        private void DoubleInputForm_Load(object sender, EventArgs e)
        {

        }
    }
}
