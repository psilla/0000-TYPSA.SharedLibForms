using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TYPSA.MC.RibbonButton.Revit.UserForms
{
    public partial class ElemSelectionForm : Form
    {
        public List<string> SelectedItems { get; private set; } = new List<string>();

        public ElemSelectionForm()
        {
            InitializeComponent();
            PopulateCheckedListBox();
        }

        private void PopulateCheckedListBox()
        {
            checkedListBox1.Items.Clear(); // Clear any existing items to avoid duplicates

            checkedListBox1.Items.Add("Views Not on Sheets");
            checkedListBox1.Items.Add("Legends Not on Sheets");
            checkedListBox1.Items.Add("Schedules Not on Sheets");
            checkedListBox1.Items.Add("Unused Line Styles");
            checkedListBox1.Items.Add("Unused Line Patterns");
            checkedListBox1.Items.Add("Unused Loadable Families");
            checkedListBox1.Items.Add("Unused Materials");
            checkedListBox1.Items.Add("Unused Sheets");
            checkedListBox1.Items.Add("Unused View Filters");
            checkedListBox1.Items.Add("Unused View Templates");
            checkedListBox1.Items.Add("Tags without Host");
            checkedListBox1.Items.Add("Scope Boxes");
            checkedListBox1.Items.Add("Ungroup Model Groups");
            checkedListBox1.Items.Add("Imported CAD Files");
            checkedListBox1.Items.Add("Imported Raster Images");

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (object item in checkedListBox1.CheckedItems)
            {
                SelectedItems.Add(item.ToString());
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PurgeElementsSelectionForm_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

