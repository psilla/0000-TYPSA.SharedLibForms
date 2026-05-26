using System;
using System.Windows.Forms;
using System.Drawing;

namespace TYPSA.SharedLib.UserForms
{
    public class Clases : Form
    {
        // CENTRAR FORMULARIO //

        public static Point centrar_Formulario(
            Rectangle screenSize, 
            int formWidth, 
            int formHeight
        )
        {
            // Calcular la posición de la ventana
            int x = (screenSize.Width - formWidth) / 2;
            int y = (screenSize.Height - formHeight) / 2;
            // return
            return new Point(x, y);
        }

        // GET LOCATION //

        public static Point get_location_button(
            Size formSize, 
            Size controlSize
        )
        {
            // Calcular la posición para centrar el control en la ventana
            int x = (formSize.Width - controlSize.Width) / 2;
            int y = (formSize.Height - controlSize.Height) / 2;
            // return
            return new Point(x, y);
        }

        // GET WIDTH //

        public static int get_width_textbox(
            Label label, 
            int uiWidth, 
            int spacing
        )
        {
            // Coordenada donde termina el Label
            int ptoIni = label.Location.X + label.Width;

            // Punto final de la ventana restando el margen
            int ptoFin = uiWidth - spacing;

            // Retornar el ancho del TextBox
            return ptoFin - ptoIni;
        }

        // CREATE LABEL // 

        public static Label label_Default(
            string labelMessage,
            int x,
            int y,
            Font labelFont
        )
        {
            // Crear el Label
            Label labelSel = new Label();

            // Estilo 
            labelSel.AutoSize = true;
            labelSel.Font = labelFont;
            labelSel.Text = labelMessage;
            labelSel.Location = new Point(x, y);
            
            // return
            return labelSel;
        }

        // CREATE TEXTBOX // 

        public static TextBox textBox_Default(
            int fixedWidth,
            int xOffset,
            int yOffset
        )
        {
            TextBox textBox = new TextBox
            {
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle,
                Font = UIStyles.LabelRegular,
                Size = new Size(fixedWidth, 25),
                Location = new Point(xOffset, yOffset)
            };
            // return
            return textBox;
        }

        // CREATE COMBOBOX //

        public static ComboBox comboBox_Default(
            int fixedWidth,
            int xOffset,
            int yOffset,
            object[] listInput
        )
        {
            ComboBox cBox = new ComboBox
            {
                BackColor = Color.FromArgb(245, 245, 245),
                FlatStyle = FlatStyle.Flat,
                Font = UIStyles.LabelRegular,
                Size = new Size(fixedWidth, 25),
                Location = new Point(xOffset, yOffset),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            // Agregamos elementos
            cBox.Items.AddRange(listInput);
            // return
            return cBox;
        }

        // CREATE CHECKEDLISTBOX //

        public static CheckedListBox checkedListBox(
            Label header, 
            Button btnNext, 
            int spacing, 
            int uiWidth, 
            string[] listInput
        )
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
            // return
            return chListBox;
        }

        public static CheckedListBox checkedListBox(
            Control controlAbove,
            Button btnNext,
            int spacing,
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
            // return
            return chListBox;
        }

        public static CheckedListBox checkedListBoxByItem(
            Label header, 
            Button btnNext, 
            int spacing, 
            int uiWidth, 
            string[] listInput
        )
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

        // CREATE LISTBOX //

        public ListBox listbox(
            Label header, 
            Button btnNext, 
            int spacing, 
            int uiHeight, 
            int uiWidth, 
            string[] listInput
        )
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

        // CREATE BUTTON //

        public static Button button_Next(
            int uiWidth, 
            int spacing, 
            int uiHeight
        )
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

        public static Button button_fileAndFolderPath(
            string textInput
        )
        {
            // Crear el botón "Seleccionar Ruta"
            Button btnSelect = new Button
            {
                Text = textInput,
                FlatStyle = FlatStyle.Flat, // Cambio estilo botón para cambio de borde
                BackColor = Color.FromArgb(240, 240, 240),
                Size = new Size(300, 50), // Tamaño del botón
                TextAlign = ContentAlignment.MiddleCenter // Centrar el texto en el botón
            };

            // Configurar estilo del borde
            btnSelect.FlatAppearance.BorderColor = Color.DarkGray; // Color del borde
            btnSelect.FlatAppearance.BorderSize = 2; // Grosor del borde

            // return
            return btnSelect;
        }

      
    }
}
