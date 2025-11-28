using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace TYPSA.SharedLib.UserForms
{
    public class DropDownFormAtenea : Form
    {
        private Label header;
        private Button btnNext;
        private System.Windows.Forms.ComboBox comboBox;
        public object salida = null; // Almacena la salida seleccionada

        public DropDownFormAtenea(string mensajeSel, object[] listInput, object defaultValue = null)
        {
            // Configuración del formulario
            this.Text = "Selection Form";
            this.BackColor = Color.White;
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

            // Botón Next
            btnNext = Clases.button_Next(uiWidth, spacing, uiHeight);
            btnNext.Click += NextButtonPressed;
            this.Controls.Add(btnNext);

            // Enter activa el botón Next
            this.AcceptButton = btnNext;

            // ComboBox
            comboBox = Clases.comboBox(header, spacing, uiHeight,  uiWidth, listInput);
            comboBox.SelectedIndexChanged += DropDownOutput;
            this.Controls.Add(comboBox);

            // Asignar valor por defecto si se encuentra en la lista
            if (defaultValue != null && listInput.Contains(defaultValue))
            {
                comboBox.SelectedItem = defaultValue;
            }

            // Cierre Form
            this.FormClosing += OnFormClosing;
        }

        // Eventos internos
        private void NextButtonPressed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DropDownOutput(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cb = sender as System.Windows.Forms.ComboBox;
            if (cb != null)
            {
                salida = cb.SelectedItem;
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DropDownForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "DropDownForm";
            this.Load += new System.EventHandler(this.DropDownForm_Load);
            this.ResumeLayout(false);

        }

        private void DropDownForm_Load(object sender, EventArgs e)
        {

        }
    }


}



