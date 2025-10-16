using System;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public partial class ExcelPathEntry : Form
    {
        public string ExcelPath { get; private set; }

        public ExcelPathEntry()
        {
            InitializeComponent();
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

        }
    }
}
