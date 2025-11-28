using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public partial class ElemPathEntry : System.Windows.Forms.Form
    {
        public List<string> SelectedFiles { get; private set; } = new List<string>();

        public ElemPathEntry()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userPath = textBox1.Text;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = userPath,
                Title = "Select Files to Analyze",
                Filter = "Revit Files (*.rvt)|*.rvt|All Files (*.*)|*.*",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                SelectedFiles = selectedFiles.ToList();

                if (SelectedFiles != null && SelectedFiles.Count > 0)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("No files selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PinningElementsPathEntry_Load(object sender, EventArgs e)
        {

        }
    }
}
