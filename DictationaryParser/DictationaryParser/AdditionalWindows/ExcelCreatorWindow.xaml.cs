using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DictationaryParser.AdditionalWindows
{
    /// <summary>
    /// Логика взаимодействия для ExcelCreatorWindow.xaml
    /// </summary>
    public partial class ExcelCreatorWindow : Window
    {
        public ExcelCreatorWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExcelNameTextBox.Text != "" && ExcelSheetTextBox.Text != "")
            {
                MainWindow.currentFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\DictationaryParser\\Documentation\\" + ExcelNameTextBox.Text;
                MainWindow.currentSheet = ExcelSheetTextBox.Text;
                if (ExcelNameTextBox.Text.EndsWith(".xlsx") == false)
                {
                    MainWindow.currentFile += ".xlsx";
                }
                
                this.Hide();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!");
            }
        }
    }
}
