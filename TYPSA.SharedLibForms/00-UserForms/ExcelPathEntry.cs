using System;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public partial class ExcelPathEntry : Form
    {
        public string ExcelPath { get; private set; }

        //public ExcelPathEntry()
        //{
        //    InitializeComponent();
        //}

        public ExcelPathEntry(
            string labelText = null,
            string defaultPath = null
        )
        {
            InitializeComponent();

            // Texto por defecto o texto recibido
            if (!string.IsNullOrEmpty(labelText))
            {
                this.label1.Text = labelText;
            }

            // Ruta por defecto en textbox
            if (!string.IsNullOrEmpty(defaultPath))
            {
                this.textBox1.Text = defaultPath;
            }
                
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExcelPath = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ExcelPathEntry_Load(object sender, EventArgs e)
        {
            // Forzamos cálculo del layout
            this.label1.AutoSize = true;
            this.label1.PerformLayout();

            // Centramos el label REALMENTE
            this.label1.Left = (this.ClientSize.Width - this.label1.Width) / 2;
        }

    }
}
