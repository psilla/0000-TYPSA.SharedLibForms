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
            string formMessage,
            List<string> listInput,
            string defaultSelectedItem = null
        )
        {
            // Texto con atajos
            string shortcuts = "(Please select only one option)";
            formMessage = formMessage + "\n" + shortcuts;

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

            this.FormClosing += OnFormClosing;

            // -------------------------------------------------
            // HEADER
            // -------------------------------------------------

            header = Clases.label_Default(
                formMessage, spacing, spacing, UIStyles.Header
            );
            this.Controls.Add(header);

            // -------------------------------------------------
            // BUSCADOR
            // -------------------------------------------------

            txtSearch = new TextBox();
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.Width = uiWidth - (spacing*2);
            txtSearch.Location = new Point(spacing, header.Bottom + spacing);
            txtSearch.BackColor = Color.FromArgb(245, 245, 245);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.ForeColor = Color.Gray;

            // Evento
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // Añadimos
            this.Controls.Add(txtSearch);

            // -------------------------------------------------
            // BUTTON NEXT
            // -------------------------------------------------

            btnNext = Clases.button_Next(uiWidth, spacing, uiHeight);
            btnNext.Click += OnButtonClick;
            this.Controls.Add(btnNext);
            this.AcceptButton = btnNext;

            // -------------------------------------------------
            // CREAR ENTIDADES
            // -------------------------------------------------

            chListBox = Clases.checkedListBox(
                txtSearch, btnNext, spacing, uiWidth, listInput.ToArray()
            );
            chListBox.CheckOnClick = true;

            // Eventos
            chListBox.ItemCheck += OnItemCheck;
            chListBox.KeyDown += ChListBox_KeyDown;
            this.Controls.Add(chListBox);

            // -------------------------------------------------
            // DATOS POR DEFECTO
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

            // -------------------------------------------------
            // AJUSTAR ALTURA
            // -------------------------------------------------

            int itemHeight = chListBox.ItemHeight;
            // maximos items visibles
            int visibleItems = Math.Min(allItems.Count, 20);

            // altura base por items
            int listHeight = visibleItems * itemHeight;

            // altura total del form
            int newHeight = (this.Height - this.ClientSize.Height) + header.Height + txtSearch.Height + listHeight + btnNext.Height + (spacing * 5);

            // altura maxima y mnima permitidas
            int maxHeight = screenSize.Height / 2;
            int minHeight = screenSize.Height / 3;

            // altura final del form
            this.Height = Math.Max(minHeight, Math.Min(newHeight, maxHeight));

            // Ajustamos checklist al espacio disponible
            int availableHeight = btnNext.Top - (txtSearch.Bottom + spacing);
            chListBox.Height = availableHeight;

            // -------------------------------------------------
            // CENTRAR FORMULARIO
            // -------------------------------------------------

            this.Location = Clases.centrar_Formulario(
                screenSize, this.Width, this.Height
            );
        }

        // -----------------------------=========================
        // SELECCIÓN ÚNICA
        // -----------------------------=========================
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

        // -----------------------------=========================
        // BUSCADOR
        // -----------------------------=========================

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

        // -----------------------------=========================
        // ATAJOS EN LISTA
        // -----------------------------=========================

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

        // -----------------------------=========================
        // BUTTON NEXT
        // -----------------------------=========================

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

        // -----------------------------=========================
        // CIERRE FORM
        // -----------------------------=========================

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
