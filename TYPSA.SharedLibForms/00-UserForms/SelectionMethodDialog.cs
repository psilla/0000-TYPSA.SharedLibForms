using System;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public partial class SelectionMethodDialog : Form
    {
        private string projectCode;
        public string SelectedOption { get; private set; } // Propiedad para devolver la selección

        public SelectionMethodDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Obtén el valor ingresado en el cuadro de texto
            string userInput = textBox1.Text.Trim().ToUpper(); // Convertimos a mayúsculas

            // Verifica si el valor es "A" o "B"
            if (userInput == "A" || userInput == "B")
            {
                SelectedOption = userInput; // Almacena la opción seleccionada
                this.DialogResult = DialogResult.OK; // Cierra el formulario y marca como OK
            }
            else
            {
                // Mensaje indicando selección inválida
                MessageBox.Show("Invalid selection. Please enter 'A' or 'B'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Si este evento no es necesario, puedes dejarlo vacío
            MessageBox.Show("Label clicked!", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Si necesitas manejar el texto mientras se escribe, implementa aquí la lógica
            Console.WriteLine($"Current text: {textBox1.Text}");
        }

        private void SelectionMethodDialog_Load(object sender, EventArgs e)
        {
            // Lógica al cargar el formulario (opcional)
            Console.WriteLine("SelectionMethodDialog loaded.");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

