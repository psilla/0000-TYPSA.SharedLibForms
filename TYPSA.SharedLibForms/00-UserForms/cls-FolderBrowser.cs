using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public class FolderBrowser
    {
        public string[] SelectedFiles { get; private set; }

        public static string SelectFolder()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                var result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    return folderBrowserDialog.SelectedPath;
                }
            }

            return null; // User canceled the folder selection
        }

        public static List<string> SelectMultipleRvtFilesFromFolder(string folderPath)
        {
            List<string> selectedFiles = new List<string>();

            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Revit Files (*.rvt)|*.rvt";
                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = folderPath; // Establecer el directorio inicial

                var result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Obtener los nombres de archivo seleccionados
                    selectedFiles.AddRange(openFileDialog.FileNames);

                    string filesToAnalyze = string.Join(Environment.NewLine, selectedFiles);
                    MessageBox.Show("Selected files:" + Environment.NewLine + filesToAnalyze, "Name of the selected files", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            return selectedFiles;
        }
    }
}
