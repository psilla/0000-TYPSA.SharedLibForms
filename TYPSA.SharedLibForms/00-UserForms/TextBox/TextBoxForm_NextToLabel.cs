using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class TextBoxForm_NextToLabel : Form
    {
        private Label header;
        private Button btnNext;

        public Dictionary<string, string> salida = null;
        private Dictionary<string, TextBox> propertyTextBoxes = new Dictionary<string, TextBox>();

        public TextBoxForm_NextToLabel(
            string mensajeSel,
            List<(string propiedad, string valorDefecto)> props
        )
        {
            this.Text = "Selection Form";
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.FormClosing += OnFormClosing;

            var screenSize = Screen.PrimaryScreen.WorkingArea;
            this.Width = screenSize.Width / 3;
            int spacing = 25;
            int uiWidth = this.ClientSize.Width;
            int uiHeight = this.ClientSize.Height;

            header = Clases.label_Header(mensajeSel, spacing);
            this.Controls.Add(header);

            int yOffset = header.Location.Y + header.Height + spacing;

            // ============
            // CÁLCULO DE ANCHOS MÁXIMOS
            // ============
            int maxDescripcionWidth = 0;
            int maxPropiedadWidth = 0;

            using (Graphics g = this.CreateGraphics())
            {
                foreach (var campo in props)
                {
                    SizeF propSize = g.MeasureString(campo.propiedad, new Font("Segoe UI", 9, FontStyle.Bold));

                    if (propSize.Width > maxPropiedadWidth)
                        maxPropiedadWidth = (int)Math.Ceiling(propSize.Width);
                }
            }

            // ============
            // POSICIONES X SEGÚN ANCHO MÁXIMO
            // ============
            int xDescripcion = 10;
            int xPropiedad = xDescripcion + maxDescripcionWidth + 10;
            int xTextBox = xPropiedad + maxPropiedadWidth + 15;

            // ============
            // CREACIÓN DE CONTROLES
            // ============
            foreach (var campo in props)
            {
                // Propiedad (en negrita)
                Label labelPropiedad = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Text = campo.propiedad,
                    Location = new Point(xPropiedad, yOffset)
                };
                this.Controls.Add(labelPropiedad);

                // TextBox asociado
                TextBox textBox = Clases.textBox_NextToLabel(100, labelPropiedad);
                textBox.Location = new Point(xTextBox, yOffset);
                textBox.Text = campo.valorDefecto;
                this.Controls.Add(textBox);

                // Guardamos referencia
                propertyTextBoxes[campo.propiedad] = textBox;

                // Avanzamos verticalmente
                yOffset += Math.Max(labelPropiedad.Height, textBox.Height) + spacing;
            }

            // ============
            // BOTÓN SIGUIENTE
            // ============
            btnNext = Clases.button_Next(uiWidth, spacing, uiHeight);
            btnNext.Click += NextButtonPressed;
            btnNext.Location = new Point(
                this.ClientSize.Width - btnNext.Width - 20,
                this.ClientSize.Height - btnNext.Height - 20
            );
            this.Controls.Add(btnNext);

            int totalAlturaNecesaria = yOffset + btnNext.Height + spacing;
            this.Height = Math.Min(totalAlturaNecesaria, screenSize.Height - 100);
            this.AcceptButton = btnNext;
        }

        private void NextButtonPressed(object sender, EventArgs e)
        {
            salida = new Dictionary<string, string>();

            foreach (var pair in propertyTextBoxes)
            {
                string propertyName = pair.Key;
                string textBoxValue = pair.Value.Text.Trim();
                salida[propertyName] = textBoxValue;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (salida == null)
            {
                MessageBox.Show(
                    "The form has been closed without saving the data.",
                    "Warning"
                );
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(292, 212);
            this.Name = "TextBoxForm_Solar";
            this.Load += new System.EventHandler(this.TextBoxForm_Solar_Load);
            this.ResumeLayout(false);
        }

        private void TextBoxForm_Solar_Load(object sender, EventArgs e)
        {

        }
    }
}

