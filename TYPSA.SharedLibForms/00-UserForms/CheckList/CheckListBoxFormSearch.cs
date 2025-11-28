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
            string mensajeSel,
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

            string mensajeFinal = mensajeSel + "\n" + shortcutsMess;

            // --- FORM ---
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

            this.Location = Clases.centrar_Formulario(screenSize, this.Width, this.Height);

            // HEADER
            header = Clases.label_Header(mensajeFinal, spacing);
            this.Controls.Add(header);

            // BUSCADOR
            txtSearch = new TextBox();
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.Width = uiWidth - 60;
            txtSearch.Location = new Point(10, header.Bottom + 10);

            txtSearch.BackColor = Color.FromArgb(245, 245, 245);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;

            txtSearch.ForeColor = Color.Gray;
            txtSearch.Text = "Search...";

            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == "Search...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Search...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            txtSearch.TextChanged += TxtSearch_TextChanged;
            this.Controls.Add(txtSearch);

            picSearch = new PictureBox();
            picSearch.SizeMode = PictureBoxSizeMode.StretchImage;
            picSearch.Width = 24;
            picSearch.Height = 24;
            picSearch.Location = new Point(txtSearch.Right + 5, txtSearch.Top);
            picSearch.Image = SystemIcons.Information.ToBitmap();
            this.Controls.Add(picSearch);

            // BOTON NEXT
            btnNext = Clases.button_Next(uiWidth, spacing, uiHeight);
            btnNext.Click += OnButtonClick;
            btnNext.Click += NextButtonPressed;
            this.Controls.Add(btnNext);

            // LISTA PRINCIPAL
            chListBox = Clases.checkedListBox(
                txtSearch,
                btnNext,
                spacing,
                uiHeight,
                uiWidth,
                listInput.ToArray()
            );

            chListBox.CheckOnClick = true;

            // 👉 MUY IMPORTANTE: ahora usamos SelectedIndexChanged
            chListBox.SelectedIndexChanged += ChListBox_SelectedIndexChanged;

            this.Controls.Add(chListBox);

            allItems = new List<string>(listInput);

            // MARCAR ITEMS POR DEFECTO
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

            this.AcceptButton = btnNext;
        }

        // ============================================================
        // SELECCIÓN CORRECTA (NUNCA FALLA)
        // ============================================================
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

        // ============================================================
        // FILTRO DEL BUSCADOR
        // ============================================================
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

        // ============================================================
        // BOTÓN NEXT
        // ============================================================
        private void OnButtonClick(object sender, EventArgs e)
        {
            salida = selectedItems.ToList();
            this.Close();
        }

        private void NextButtonPressed(object sender, EventArgs e) => this.Close();

        // ============================================================
        // ATAJOS DE TECLADO
        // ============================================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CheckListBoxFormSearch
            // 
            this.ClientSize = new System.Drawing.Size(292, 212);
            this.Name = "CheckListBoxFormSearch";
            this.Load += new System.EventHandler(this.CheckListBoxFormSearch_Load);
            this.ResumeLayout(false);

        }

        private void CheckListBoxFormSearch_Load(object sender, EventArgs e)
        {

        }
    }
}

