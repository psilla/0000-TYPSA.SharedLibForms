using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class formLayoutEntities
    {
        public Rectangle ScreenSize;
        public int Spacing;
        public int UiWidth;
        public int UiHeight;

        public Label Header;
        public Button BtnNext;

        public int TextBoxOffsetX;
        public int ComboBoxOffsetX;
        public int TopReserved;
        public int BottomReserved;
        public int TopPadding;

        public int YOffsetCalc;
        public int MaxAllowedHeight;
        public bool NeedsScroll;

        public Control Container;
        public Panel ScrollPanel;

        public int MaxDescripcionWidth;
        public int MaxPropiedadWidth;
        public int MaxUnidadWidth;

        public int XPropiedad;
        public int XTextBox;
        public int XUnidad;

        public int TextBoxWidth;

        public List<(string descripcion, string propiedad, string unidad)> Campos;
    }

    public static class UIStyles
    {
        public static readonly Font Header = new Font("Helvetica", 8, FontStyle.Bold);
        public static readonly Font LabelBold = new Font("Helvetica", 8, FontStyle.Bold);
        public static readonly Font LabelItalic = new Font("Helvetica", 8, FontStyle.Italic);
        public static readonly Font LabelRegular = new Font("Helvetica", 8, FontStyle.Regular);
        public static readonly Font LabelBoldItalic = new Font("Helvetica", 8, FontStyle.Bold | FontStyle.Italic);
    }

    public static class cls_00_FormHelper
    {
        public static Panel CreateScrollPanel(
            Form form,
            int topReserved,
            int bottomReserved
        )
        {
            Panel scrollPanel = new Panel
            {
                AutoScroll = true,
                Location = new Point(0, topReserved),
                Size = new Size(
                    form.ClientSize.Width, form.ClientSize.Height - topReserved - bottomReserved
                ),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            form.Controls.Add(scrollPanel);
            return scrollPanel;
        }

        public static Rectangle ConfigureBaseForm(
            Form form,
            string formTitle,
            Rectangle screenSize,
            int? customWidth = null,
            int? customHeight = null
        )
        {
            // Configuración base
            form.Text = formTitle;
            form.BackColor = Color.White;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.TopMost = true;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Si no se pasa, usa el 50% de la pantalla
            form.Width = customWidth ?? (screenSize.Width/2);
            form.Height = customHeight ?? (screenSize.Height/2);

            // return
            return screenSize;
        }

        public static int GetMaxLabelWidthBase(
            Graphics g,
            IEnumerable<string> labels,
            Font font
        )
        {
            int maxLabelWidth = 0;
            // Iteramos
            foreach (var label in labels)
            {
                SizeF size = g.MeasureString(label, font);
                // Validamos
                if (size.Width > maxLabelWidth)
                    maxLabelWidth = (int)Math.Ceiling(size.Width);
            }
            // return
            return maxLabelWidth;
        }

        public static int GetMaxLabelWidth(
            Form form,
            IEnumerable<string> fields,
            Font font 
        )
        {
            using (Graphics g = form.CreateGraphics())
            {
                // return
                return GetMaxLabelWidthBase(g, fields, font);
            }
        }

        public static void InitializeBaseLayout(
            Form form,
            formLayoutEntities layout,
            string formTitle,
            string formMessage,
            EventHandler nextHandler,
            FormClosingEventHandler closingHandler
        )
        {
            // ============
            // CONFIGURACION POR DEFECTO
            // ============

            layout.ScreenSize = Screen.PrimaryScreen.WorkingArea;
            // Configuracion inicial
            layout.ScreenSize = ConfigureBaseForm(
                form, formTitle, layout.ScreenSize
            );
            // Forzar cierre
            form.FormClosing += closingHandler;

            // ============
            // DEFINIR INPUTS
            // ============

            layout.Spacing = 25;
            layout.UiWidth = form.ClientSize.Width;
            layout.UiHeight = form.ClientSize.Height;

            // ============
            // CREAMOS HEADER
            // ============

            layout.Header = Clases.label_Default(
                formMessage, layout.Spacing, layout.Spacing, UIStyles.Header
            );
            form.Controls.Add(layout.Header);

            // ============
            // BUTTON NEXT
            // ============

            layout.BtnNext = Clases.button_Next(
                layout.UiWidth, layout.Spacing, layout.UiHeight
            );
            layout.BtnNext.Click += nextHandler;
            layout.BtnNext.Location = new Point(
                layout.UiWidth - layout.BtnNext.Width - layout.Spacing,
                layout.UiHeight - layout.BtnNext.Height - layout.Spacing
            );
            form.Controls.Add(layout.BtnNext);
            form.AcceptButton = layout.BtnNext;
        }

        public static void CalculateLayoutReservedSpaces(
            formLayoutEntities layout
        )
        {
            // ============
            // RESERVAS
            // ============

            layout.TopReserved = layout.Header.Height + (layout.Spacing * 2);
            layout.BottomReserved = layout.BtnNext.Height + (layout.Spacing * 2);
        }

        public static void EvaluateScrollRequirement(
            formLayoutEntities layout
        )
        {
            // ============
            // VALIDAR ALTURA NECESARIA 
            // ============

            // Por defecto
            layout.MaxAllowedHeight = (layout.ScreenSize.Height / 2);
            // Aplicamos
            layout.NeedsScroll = layout.YOffsetCalc > layout.MaxAllowedHeight;
        }

        public static void CreateScrollableContainer(
            Form form,
            formLayoutEntities layout
        )
        {
            // ============
            // CREAR PANEL
            // ============

            // Validamos
            if (layout.NeedsScroll)
            {
                layout.ScrollPanel = CreateScrollPanel(
                    form, layout.TopReserved, layout.BottomReserved
                );

                layout.ScrollPanel.AutoScrollMinSize = new Size(0, layout.YOffsetCalc);
                layout.Container = layout.ScrollPanel;
            }
            else
            {
                layout.Container = form;
            }
        }

        public static formLayoutEntities BuildBaseLayout<T>(
            Form form,
            string formTitle,
            string formMessage,
            IEnumerable<T> fields,
            Func<T, string> labelSelector,
            Action<formLayoutEntities, int> setOffset,
            EventHandler nextHandler,
            FormClosingEventHandler closingHandler
        )
        {
            formLayoutEntities layout = new formLayoutEntities();

            // ============
            // HELPER
            // ============

            InitializeBaseLayout(
                form, layout, formTitle, formMessage, nextHandler, closingHandler
            );

            // ============
            // RESERVAS
            // ============

            CalculateLayoutReservedSpaces(layout);

            // ============
            // CALCULO ANCHO MAXIMO
            // ============

            int maxLabelWidth = GetMaxLabelWidth(
                form, fields.Select(labelSelector), UIStyles.LabelItalic
            );

            // ============
            // POSICION X SEGUN ANCHO MAXIMO
            // ============

            setOffset(layout, maxLabelWidth + (layout.Spacing * 2));

            // ============
            // CALCULAR ALTURA NECESARIA
            // ============

            // Altura padding visual
            int paddingHeight = form.Height - form.ClientSize.Height;
            // Asignamos
            layout.TopPadding = paddingHeight;

            int yOffsetCalc = 0;
            // Iteramos
            foreach (var field in fields)
            {
                int row = layout.Spacing;
                // Incrementamos
                yOffsetCalc += row + layout.Spacing;
            }
            // Asignamos
            layout.YOffsetCalc = layout.TopPadding + layout.TopReserved + (yOffsetCalc - layout.Spacing) + layout.BottomReserved;

            // ============
            // VALIDAR ALTURA NECESARIA 
            // ============

            EvaluateScrollRequirement(layout);

            // ============
            // CREAR PANEL
            // ============

            CreateScrollableContainer(form, layout);

            // return
            return layout;
        }

        public static void ApplyFormHeightAndCenter(
            Form form,
            formLayoutEntities layout,
            int yOffset
        )
        {
            // ============
            // APLICAR ALTURA FORM
            // ============

            // Altura real necesaria restando ultimo espacio del offset
            int realAlturaNecesaria = layout.TopPadding + (yOffset - layout.Spacing) + layout.BottomReserved;
            // Asignamos
            form.Height = layout.NeedsScroll ? layout.MaxAllowedHeight : realAlturaNecesaria;

            // ============
            // CENTRAR FORM
            // ============

            form.Location = Clases.centrar_Formulario(layout.ScreenSize, form.Width, form.Height);
        }

        public static void FinalizeLayoutControls<T, TControl>(
            Form form,
            formLayoutEntities layout,
            int yOffset,
            Dictionary<T, TControl> controls,
            Func<formLayoutEntities, int> getOffsetX
        )
        where TControl : Control
        {
            // ============
            // AJUSTE DE ANCHO SI HAY SCROLL
            // ============

            form.Shown += (s, e) =>
            {
                // Validamos
                if (layout.NeedsScroll && layout.ScrollPanel != null && layout.ScrollPanel.VerticalScroll.Visible)
                {
                    int panelWidth = layout.ScrollPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
                    int newWidth = panelWidth - getOffsetX(layout);
                    // Iteramos
                    foreach (var ctrl in controls.Values)
                    {
                        ctrl.Width = newWidth;
                    }
                }
            };

            // ============
            // HELPER
            // ============

            ApplyFormHeightAndCenter(form, layout, yOffset);
        }




    }
}
