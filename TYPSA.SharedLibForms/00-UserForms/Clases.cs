using System;
using System.Windows.Forms;
using System.Drawing;

namespace TYPSA.SharedLib.UserForms
{
    public class Clases : Form
    {
        public static Point get_location_label_textBox(Size formSize, Size controlSize, int spacing)
        {
            // Calcular la posición X
            int x = spacing;

            // Calcular la posición Y centrada verticalmente
            int y = (formSize.Height - controlSize.Height) / 2;

            // Retornar la ubicación como un Point
            return new Point(x, y);
        }

        public static Point get_location_label_button(Size formSize, Size controlSize, int spacing)
        {
            // Cálculo para colocar el Label en 1/5 de la altura de la ventana
            int x = spacing;
            int y = (formSize.Height - controlSize.Height) / 5;

            return new Point(x, y);
        }

        public static Point get_location_textbox(Label label, Size formSize, Size controlSize)
        {
            // Coordenada donde termina el Label
            int x = label.Location.X + label.Width;

            // Coordenada Y centrada verticalmente
            int y = (formSize.Height - controlSize.Height) / 2;

            // Retornar la posición del TextBox
            return new Point(x, y);
        }

        public static Point get_location_textbox_NextToLabel(Label label)
        {
            // Alinear en X a la derecha del Label con un margen de 10px
            int x = label.Location.X + label.Width + 10;
            // Usar la misma posición Y del Label
            int y = label.Location.Y;
            // return
            return new Point(x, y);
        }

        public static int get_width_textbox(Label label, int uiWidth, int spacing)
        {
            // Coordenada donde termina el Label
            int ptoIni = label.Location.X + label.Width;

            // Punto final de la ventana restando el margen
            int ptoFin = uiWidth - spacing;

            // Retornar el ancho del TextBox
            return ptoFin - ptoIni;
        }

        public static int get_width_textbox_NextToLabel(int fixedWidth)
        {
            // Devolver el ancho fijo del TextBox
            return fixedWidth;
        }

        public static Point get_location_button(Size formSize, Size controlSize)
        {
            // Calcular la posición para centrar el control en la ventana
            int x = (formSize.Width - controlSize.Width) / 2;
            int y = (formSize.Height - controlSize.Height) / 2;
            return new Point(x, y);
        }

        public static Point centrar_Formulario(Rectangle screenSize, int formWidth, int formHeight)
        {
            // Calcular la posición de la ventana
            int x = (screenSize.Width - formWidth) / 2;
            int y = (screenSize.Height - formHeight) / 2;

            // Establecer la posición de la ventana
            return new Point(x, y);
        }

        public static Label label_Header(string mensajeSel, int spacing)
        {
            // Crear el Label
            Label labelSel = new Label();

            // Estilo de texto para el encabezado
            Font font = new Font("Helvetica", 8, FontStyle.Bold);
            labelSel.Font = font;
            labelSel.Text = mensajeSel;

            // Configurar la ubicación y tamaño
            labelSel.Location = new Point(spacing, spacing);
            labelSel.AutoSize = true; // Permitir que el tamaño del label se adapte al texto

            return labelSel;
        }

        public static Label label_TextBox(Size formSize, int spacing)
        {
            // Crear el Label
            Label label = new Label();

            // Configurar propiedades del Label
            label.Font = new Font("Helvetica", 8);
            label.Text = "Enter a value:";
            label.AutoSize = true; // Permitir que el tamaño del Label se adapte al texto

            // Calcular ubicación usando GetLocationLabelTextBox
            label.Location = get_location_label_textBox(formSize, label.PreferredSize, spacing);

            return label;
        }

        public static Label label_Button(Size formSize, int spacing, string textInput)
        {
            // Crear el Label
            Label label = new Label();

            // Configurar propiedades del Label
            label.Font = new Font("Helvetica", 8);
            label.Text = textInput;
            label.AutoSize = true; // Permitir que el tamaño del Label se adapte al texto

            // Calcular la ubicación usando GetLocationLabelButton
            label.Location = get_location_label_button(formSize, label.PreferredSize, spacing);

            return label;
        }

        public static TextBox textBox(int uiWidth, int spacing, Label label, Size formSize)
        {
            // Crear y configurar el TextBox
            TextBox textBox = new TextBox
            {
                BackColor = Color.FromArgb(245, 245, 245), // Gris más claro, estilo moderno
                BorderStyle = BorderStyle.FixedSingle,     // Borde fino
                Font = new Font("Segoe UI", 9),            // Fuente moderna y estándar
                Size = new Size(get_width_textbox(label, uiWidth, spacing), 50), // Establecer tamaño
                Location = get_location_textbox(label, formSize, label.PreferredSize) // Calcular ubicación
            };

            return textBox;
        }

        public static TextBox textBox_NextToLabel(int fixedWidth, Label label)
        {
            TextBox textBox = new TextBox
            {
                BackColor = Color.FromArgb(245, 245, 245), // Gris más claro, estilo moderno
                BorderStyle = BorderStyle.FixedSingle,     // Borde fino
                Font = new Font("Segoe UI", 9),            // Fuente moderna y estándar
                Size = new Size(get_width_textbox_NextToLabel(fixedWidth), 24), // Más compacto
                Location = get_location_textbox_NextToLabel(label)
            };
            // return
            return textBox;
        }

        public static ComboBox comboBox(Label header, int spacing, int uiHeight, int uiWidth, object[] listInput)
        {
            // Construir el ComboBox
            ComboBox cBox = new ComboBox
            {
                Location = new Point(spacing, header.Bottom + spacing), // Posición
                Width = uiWidth - (spacing * 2), // Ancho
                DropDownStyle = ComboBoxStyle.DropDownList // Configurar estilo
            };

            // Agregar elementos al ComboBox
            cBox.Items.AddRange(listInput);

            return cBox;
        }

        public static CheckedListBox checkedListBox(Label header, Button btnNext, int spacing, int uiHeight, int uiWidth, string[] listInput)
        {
            // Crear CheckedListBox
            CheckedListBox chListBox = new CheckedListBox
            {
                Location = new Point(spacing, header.Bottom + spacing), // Posición
                Width = uiWidth - (spacing * 2), // Ancho
                Height = (btnNext.Top - spacing) - (header.Bottom + spacing), // Altura
                CheckOnClick = true // Activar con un click
            };

            // Agregar elementos al CheckedListBox
            foreach (var item in listInput)
            {
                chListBox.Items.Add(item);
            }

            return chListBox;
        }

        public static CheckedListBox checkedListBox(
            Control controlAbove,
            Button btnNext,
            int spacing,
            int uiHeight,
            int uiWidth,
            string[] listInput
        )
        {
            // Crear CheckedListBox
            CheckedListBox chListBox = new CheckedListBox
            {
                Location = new Point(spacing, controlAbove.Bottom + spacing), // Debajo del control indicado
                Width = uiWidth - (spacing * 2), // Ancho total
                Height = (btnNext.Top - spacing) - (controlAbove.Bottom + spacing) // Altura dinámica
            };

            // Agregar elementos
            foreach (var item in listInput)
                chListBox.Items.Add(item);

            return chListBox;
        }

        public static CheckedListBox checkedListBoxByItem(Label header, Button btnNext, int spacing, int uiHeight, int uiWidth, string[] listInput)
        {
            // Altura por ítem y máximo visible
            int rowHeight = 25;
            int maxVisibleItems = 15;
            int maxHeight = rowHeight * maxVisibleItems;

            // Altura ideal según número de ítems
            int calculatedHeight = listInput.Length * rowHeight;
            int finalHeight = Math.Min(calculatedHeight, maxHeight);

            // Crear CheckedListBox
            CheckedListBox chListBox = new CheckedListBox
            {
                Location = new Point(spacing, header.Bottom + spacing), // Posición
                Width = uiWidth - (spacing * 2), // Ancho
                Height = finalHeight, // Altura dinámica
                CheckOnClick = true, // Activar con un click
                ScrollAlwaysVisible = true // Mostrar scroll si es necesario
            };

            // Agregar elementos
            foreach (var item in listInput)
            {
                chListBox.Items.Add(item);
            }

            return chListBox;
        }

        public ListBox listbox(Label header, Button btnNext, int spacing, int uiHeight, int uiWidth, string[] listInput)
        {
            // Crear el ListBox
            ListBox listBox = new ListBox
            {
                SelectionMode = SelectionMode.MultiExtended, // Selección múltiple
                Location = new Point(spacing, header.Bottom + spacing), // Posición
                Width = uiWidth - (spacing * 2), // Ancho
                Height = (btnNext.Top - spacing) - (header.Bottom + spacing) // Altura
            };

            // Agregar elementos al ListBox
            listBox.Items.AddRange(listInput);

            return listBox;
        }

        public static Button button_Next(int uiWidth, int spacing, int uiHeight)
        {
            // Crear botón "Next"
            Button btnNext = new Button
            {
                Text = "Next", // Texto del botón
                AutoSize = true, // Hacer que el tamaño del botón se adapte al texto
                TextAlign = ContentAlignment.MiddleCenter, // Centrar el texto
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right // Anclar el botón en la esquina inferior derecha
            };

            // Configurar la posición del botón
            btnNext.Location = new Point(
                uiWidth - (btnNext.PreferredSize.Width + spacing), // X: alineado a la derecha con espaciado
                uiHeight - (btnNext.PreferredSize.Height + spacing) // Y: alineado abajo con espaciado
            );

            return btnNext;
        }

        public static Button button_fileAndFolderPath(string textInput, Size formSize)
        {
            // Crear el botón "Seleccionar Ruta"
            Button btnSelect = new Button
            {
                Text = textInput,
                FlatStyle = FlatStyle.Flat, // Cambio estilo botón para cambio de borde
                BackColor = Color.FromArgb(255, 192, 192), // Color del botón
                Size = new Size(300, 50), // Tamaño del botón
                TextAlign = ContentAlignment.MiddleCenter // Centrar el texto en el botón
            };

            // Configurar estilo del borde del botón
            btnSelect.FlatAppearance.BorderColor = Color.Red; // Color del borde
            btnSelect.FlatAppearance.BorderSize = 2; // Grosor del borde

            // Usar el método para centrar el botón en el formulario
            btnSelect.Location = get_location_button(formSize, btnSelect.Size);

            return btnSelect;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Clases
            // 
            this.ClientSize = new System.Drawing.Size(292, 212);
            this.Name = "Clases";
            this.Load += new System.EventHandler(this.Clases_Load);
            this.ResumeLayout(false);

        }

        private void Clases_Load(object sender, EventArgs e)
        {

        }
    }
}
