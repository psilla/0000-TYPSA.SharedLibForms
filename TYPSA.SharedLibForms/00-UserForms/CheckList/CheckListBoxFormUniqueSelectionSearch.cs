using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class CheckListBoxFormUniqueSelectionSearch : Form
    {
        private Label header;
        private CheckedListBox chListBox;
        private Button btnNext;

        // Buscador
        private TextBox txtSearch;

        // Datos
        private List<string> allItems;
        private string selectedItem = null;

        // Salida
        public string salida { get; private set; }

        public CheckListBoxFormUniqueSelectionSearch(
            string mensajeSel,
            List<string> listInput,
            string defaultSelectedItem = null
        )
        {
            // Texto con atajos
            string shortcuts =
                "(Ctrl + F = Search, Esc = Clear search, Enter = Confirm)";

            string mensajeFinal = mensajeSel + "\n" + shortcuts;

            // -------------------------------------------------
            // FORM
            // -------------------------------------------------
            this.Text = "Selection Form";
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            var screenSize = Screen.PrimaryScreen.WorkingArea;
            this.Width = screenSize.Width / 2;
            this.Height = screenSize.Height / 2;

            int spacing = 10;
            int uiWidth = this.ClientSize.Width;
            int uiHeight = this.ClientSize.Height;

            this.Location = Clases.centrar_Formulario(
                screenSize,
                this.Width,
                this.Height
            );

            // -------------------------------------------------
            // HEADER
            // -------------------------------------------------
            header = Clases.label_Header(mensajeFinal, spacing);
            this.Controls.Add(header);

            // -------------------------------------------------
            // BUSCADOR
            // -------------------------------------------------
            txtSearch = new TextBox();
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.Width = uiWidth - 20;
            txtSearch.Location = new Point(10, header.Bottom + 10);
            txtSearch.TextChanged += TxtSearch_TextChanged;
            this.Controls.Add(txtSearch);

            // -------------------------------------------------
            // BOTÓN NEXT
            // -------------------------------------------------
            btnNext = Clases.button_Next(uiWidth, spacing, uiHeight);
            btnNext.Click += OnButtonClick;
            this.Controls.Add(btnNext);

            // -------------------------------------------------
            // LISTA
            // -------------------------------------------------
            chListBox = Clases.checkedListBox(
                txtSearch,
                btnNext,
                spacing,
                uiHeight,
                uiWidth,
                listInput.ToArray()
            );

            chListBox.CheckOnClick = true;
            chListBox.ItemCheck += OnItemCheck;
            chListBox.KeyDown += ChListBox_KeyDown;

            this.Controls.Add(chListBox);

            // -------------------------------------------------
            // DATOS
            // -------------------------------------------------
            allItems = new List<string>(listInput);

            // Valor por defecto (si existe)
            if (!string.IsNullOrWhiteSpace(defaultSelectedItem) &&
                allItems.Contains(defaultSelectedItem))
            {
                selectedItem = defaultSelectedItem;
            }

            // Marcar valor por defecto visualmente
            for (int i = 0; i < chListBox.Items.Count; i++)
            {
                if (chListBox.Items[i].ToString() == selectedItem)
                {
                    chListBox.SetItemChecked(i, true);
                    chListBox.SelectedIndex = i;
                    break;
                }
            }

            this.AcceptButton = btnNext;
            this.FormClosing += OnFormClosing;
        }

        // =====================================================
        // SELECCIÓN ÚNICA
        // =====================================================
        private void OnItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < chListBox.Items.Count; i++)
                {
                    if (i != e.Index)
                        chListBox.SetItemChecked(i, false);
                }

                selectedItem = chListBox.Items[e.Index].ToString();
            }
            else if (chListBox.Items[e.Index].ToString() == selectedItem)
            {
                selectedItem = null;
            }
        }

        // =====================================================
        // BUSCADOR
        // =====================================================
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();

            chListBox.Items.Clear();

            IEnumerable<string> items =
                string.IsNullOrEmpty(search)
                ? allItems
                : allItems.Where(i => i.ToLower().Contains(search));

            foreach (var item in items)
                chListBox.Items.Add(item, item == selectedItem);
        }

        // =====================================================
        // ATAJOS EN LISTA
        // =====================================================
        private void ChListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && chListBox.SelectedIndex >= 0)
            {
                int i = chListBox.SelectedIndex;
                chListBox.SetItemChecked(i, true);
                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Foco en buscador
            if (keyData == (Keys.Control | Keys.F))
            {
                txtSearch.Focus();
                txtSearch.SelectAll();
                return true;
            }

            // Limpiar buscador
            if (keyData == Keys.Escape)
            {
                txtSearch.Clear();
                chListBox.Focus();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =====================================================
        // BOTÓN NEXT
        // =====================================================
        private void OnButtonClick(object sender, EventArgs e)
        {
            salida = selectedItem;

            if (string.IsNullOrWhiteSpace(salida))
            {
                MessageBox.Show(
                    "Please select one item.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            this.Close();
        }

        // =====================================================
        // CIERRE FORM
        // =====================================================
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (salida == null && e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "No option was selected. Do you want to cancel the process?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}
