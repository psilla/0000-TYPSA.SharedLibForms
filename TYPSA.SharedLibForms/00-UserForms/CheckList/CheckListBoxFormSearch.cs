using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class CheckListBoxFormSearch : Form
    {
        private Label header;
        private CheckedListBox chListBox;
        private Button btnNext;

        // Buscador
        private TextBox txtSearch;
        private PictureBox picSearch;

        // Lista original completa
        private List<string> allItems;

        // Selecciones persistentes (NO SE PIERDEN)
        private HashSet<string> selectedItems = new HashSet<string>();

        // Flag para evitar filtrado al inicio
        private bool searchReady = false;

        // Salida final
        public List<string> salida = null;

        public CheckListBoxFormSearch(
            string formMessage,
            List<string> listInput,
            List<string> listInputByDefault = null
        )
        {
            // Mensaje con atajos
            string shortcutsMess =
                "(Ctrl + A = Select all, " +
                "Ctrl + D = Deselect all, " +
                "Ctrl + H = Hide non-selected, " +
                "Ctrl + M = Show all, " +
                "Ctrl + I = Invert selection)";
            formMessage = formMessage + "\n" + shortcutsMess;

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
            txtSearch.Text = "Search...";

            // Evento
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == "Search...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            // Evento
            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Search...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            // Evento
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // Añadimos
            this.Controls.Add(txtSearch);

            // -------------------------------------------------
            // PICTURE BOX
            // -------------------------------------------------

            picSearch = new PictureBox();
            picSearch.SizeMode = PictureBoxSizeMode.StretchImage;
            picSearch.Width = 24;
            picSearch.Height = 24;
            picSearch.Location = new Point(txtSearch.Right + 5, txtSearch.Top);
            picSearch.Image = SystemIcons.Information.ToBitmap();
            this.Controls.Add(picSearch);

            // -------------------------------------------------
            // BUTTON NEXT
            // -------------------------------------------------

            btnNext = Clases.button_Next(uiWidth, spacing, uiHeight);
            btnNext.Click += OnButtonClick;
            btnNext.Click += NextButtonPressed;
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
            chListBox.SelectedIndexChanged += ChListBox_SelectedIndexChanged;
            this.Controls.Add(chListBox);

            // -------------------------------------------------
            // DATOS POR DEFECTO
            // -------------------------------------------------

            allItems = new List<string>(listInput);
            // Valor por defecto (si existe)
            if (listInputByDefault != null)
            {
                foreach (var item in listInputByDefault)
                {
                    if (allItems.Contains(item))
                        selectedItems.Add(item);
                }

                for (int i = 0; i < chListBox.Items.Count; i++)
                {
                    string item = chListBox.Items[i].ToString();
                    if (selectedItems.Contains(item))
                        chListBox.SetItemChecked(i, true);
                }
            }

            // Activamos el filtro ahora que todo está cargado
            searchReady = true;

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

        // -----------------------------================================
        // SELECCIÓN CORRECTA
        // -----------------------------================================
        private void ChListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chListBox.SelectedIndex < 0) return;

            string item = chListBox.SelectedItem.ToString();
            bool isChecked = chListBox.GetItemChecked(chListBox.SelectedIndex);

            if (isChecked)
                selectedItems.Add(item);
            else
                selectedItems.Remove(item);
        }

        // -----------------------------================================
        // FILTRO DEL BUSCADOR
        // -----------------------------================================
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!searchReady) return;

            string search = txtSearch.Text.Trim().ToLower();

            var itemsSeleccionados = new HashSet<string>(selectedItems);

            chListBox.Items.Clear();

            IEnumerable<string> items =
                (string.IsNullOrEmpty(search) || search == "search...")
                ? allItems
                : allItems.Where(i => i.ToLower().Contains(search));

            foreach (var item in items)
                chListBox.Items.Add(item, itemsSeleccionados.Contains(item));
        }

        // -----------------------------================================
        // BUTTON NEXT
        // -----------------------------================================

        private void OnButtonClick(object sender, EventArgs e)
        {
            salida = selectedItems.ToList();
            this.Close();
        }

        private void NextButtonPressed(object sender, EventArgs e) => this.Close();

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

        // -----------------------------================================
        // ATAJOS DE TECLADO
        // -----------------------------================================

        protected override bool ProcessCmdKey(
            ref Message msg, 
            Keys keyData
        )
        {
            // Select All (solo los filtrados/visibles)
            if (keyData == (Keys.Control | Keys.A))
            {
                foreach (var obj in chListBox.Items)
                {
                    string item = obj.ToString();
                    selectedItems.Add(item);
                }

                // Refrescar visual
                TxtSearch_TextChanged(null, null);
                return true;
            }

            // Deselect All (solo los filtrados/visibles)
            if (keyData == (Keys.Control | Keys.D))
            {
                foreach (var obj in chListBox.Items)
                {
                    string item = obj.ToString();
                    selectedItems.Remove(item);
                }

                // Refrescar visual
                TxtSearch_TextChanged(null, null);
                return true;
            }

            // Hide non-selected
            if (keyData == (Keys.Control | Keys.H))
            {
                var visibles = allItems.Where(i => selectedItems.Contains(i)).ToList();
                chListBox.Items.Clear();
                foreach (var item in visibles)
                    chListBox.Items.Add(item, true);
                return true;
            }

            // Show all
            if (keyData == (Keys.Control | Keys.M))
            {
                TxtSearch_TextChanged(null, null);
                return true;
            }

            // Invert selection
            if (keyData == (Keys.Control | Keys.I))
            {
                foreach (var item in allItems)
                {
                    if (selectedItems.Contains(item))
                        selectedItems.Remove(item);
                    else
                        selectedItems.Add(item);
                }

                TxtSearch_TextChanged(null, null);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

       
    }
}

