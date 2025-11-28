using System;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class FilePathForm : Form
    {
        private Label header;
        private Label label;
        private Button btnSelect;
        private Button btnNext;
        public string salida = null; // Almacena la ruta seleccionada

        public FilePathForm(string mensajeSel)
        {
            // Configuración del formulario
            this.Text = "Selection Form";
            this.BackColor = System.Drawing.Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.salida = null;

            // Dimensiones y espaciado
            var screenSize = Screen.PrimaryScreen.WorkingArea;
            this.Width = screenSize.Width / 2;
            this.Height = screenSize.Height / 4;
            int spacing = 10;
            int uiWidth = this.ClientSize.Width;
            int uiHeight = this.ClientSize.Height;

            // Añadimos controles
            this.Location = Clases.centrar_Formulario(screenSize, this.Width, this.Height);

            // Header
            header = Clases.label_Header(mensajeSel, spacing);
            this.Controls.Add(header);

            // Label
            label = Clases.label_Button(this.ClientSize, spacing, "Select a file");
            this.Controls.Add(label);

            // Botón Select
            btnSelect = Clases.button_fileAndFolderPath("Select File", this.ClientSize);
            btnSelect.Click += OnButtonClick;
            this.Controls.Add(btnSelect);

            // Botón Next
            btnNext = Clases.button_Next(uiWidth, spacing, uiHeight);
            btnNext.Click += NextButtonPressed;
            this.Controls.Add(btnNext);

        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            // Método para acceder a los archivos
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtener la ruta
                    string selectedPath = dialog.FileName;

                    // Modificar el texto del label
                    label.Text = $"Selected File: {selectedPath}";
                    label.AutoSize = true; // Autoajuste al texto

                    // Guardar la ruta seleccionada
                    salida = selectedPath;
                }
            }
        }

        private void NextButtonPressed(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }

}



