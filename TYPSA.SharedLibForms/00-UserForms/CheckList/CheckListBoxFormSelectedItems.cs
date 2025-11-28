using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace TYPSA.SharedLib.UserForms
{
    public class CheckListBoxFormSelectedItems : Form
    {
        private Label header;
        private CheckedListBox chListBox;
        private Button btnNext;

        // Lista completa original
        private List<string> listaOriginal; 
        public List<string> Salida { get; private set; }

        public CheckListBoxFormSelectedItems(
            string mensajeSel,
            List<string> listInput, 
            HashSet<string> itemsMarcadosPorDefecto = null
        )
        {
            this.Text = "Selection Form";
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Salida = null;

            // Habilitar captura de teclado
            this.KeyPreview = true;
            this.KeyDown += OnKeyDown;
            listaOriginal = new List<string>(listInput);

            var screenSize = Screen.PrimaryScreen.WorkingArea;
            this.Width = screenSize.Width / 2;
            this.Height = screenSize.Height / 2;
            int spacing = 10;
            int uiWidth = this.ClientSize.Width;
            int uiHeight = this.ClientSize.Height;

            this.Location = Clases.centrar_Formulario(screenSize, this.Width, this.Height);

            header = Clases.label_Header(mensajeSel, spacing);
            this.Controls.Add(header);

            btnNext = Clases.button_Next(uiWidth, spacing, uiHeight);
            btnNext.Click += OnButtonClick;
            this.AcceptButton = btnNext;
            this.Controls.Add(btnNext);

            chListBox = Clases.checkedListBox(
                header, btnNext, spacing, uiHeight, uiWidth, listInput.ToArray()
            );
            this.Controls.Add(chListBox);

            // ✔️ Marcar por defecto los items especificados
            if (itemsMarcadosPorDefecto != null)
            {
                for (int i = 0; i < chListBox.Items.Count; i++)
                {
                    string item = chListBox.Items[i].ToString();
                    if (itemsMarcadosPorDefecto.Contains(item))
                    {
                        chListBox.SetItemChecked(i, true);
                    }
                }
            }

            this.FormClosing += OnFormClosing;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            Salida = chListBox.CheckedItems.Cast<object>().Select(item => item.ToString()).ToList();

            if (Salida.Count == 0)
            {
                MessageBox.Show("Please select at least one item.", "Warning");
                return;
            }

            this.Close();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (Salida == null && e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "No option was selected. Do you want to cancel the process?",
                    "Confirmation"
                );

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // CTRL + H → Ocultar NO seleccionados
            if (e.Control && e.KeyCode == Keys.H)
            {
                OcultarNoSeleccionados();
                e.SuppressKeyPress = true;
            }

            // CTRL + M → Mostrar todo
            if (e.Control && e.KeyCode == Keys.M)
            {
                MostrarTodo();
                e.SuppressKeyPress = true;
            }

            // CTRL + I → Toggle (invertir)
            if (e.Control && e.KeyCode == Keys.I)
            {
                ToggleOcultar();
                e.SuppressKeyPress = true;
            }
        }

        private void OcultarNoSeleccionados()
        {
            var seleccionados = chListBox.CheckedItems
                .Cast<object>()
                .Select(x => x.ToString())
                .ToHashSet();

            chListBox.Items.Clear();

            foreach (var item in listaOriginal)
            {
                if (seleccionados.Contains(item))
                    chListBox.Items.Add(item, true);
            }
        }

        private void MostrarTodo()
        {
            var seleccionados = chListBox.CheckedItems
                .Cast<object>()
                .Select(x => x.ToString())
                .ToHashSet();

            chListBox.Items.Clear();

            foreach (var item in listaOriginal)
                chListBox.Items.Add(item, seleccionados.Contains(item));
        }

        private void ToggleOcultar()
        {
            if (chListBox.Items.Count == listaOriginal.Count)
                OcultarNoSeleccionados();
            else
                MostrarTodo();
        }



        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(292, 212);
            this.Name = "CheckListBoxForm";
            this.Load += new System.EventHandler(this.CheckListBoxForm_Load);
            this.ResumeLayout(false);
        }

        private void CheckListBoxForm_Load(object sender, EventArgs e)
        {
        }
    }
}
