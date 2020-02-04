using DictationaryParser.AdditionalWindows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
//using static DictationaryParser.DictationParser;

namespace DictationaryParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Expectation idleWIndow = new Expectation();

        public static List<string> activeTypes;
        private static List<OpcorporaWord> PreparedForExcelAddingList { get; set; }

        private PartOfSpeechChange partOfSpeechChange = new PartOfSpeechChange();
        public List<string> DictStrings { get; set; }

        public List<string> wordslist;

        public static string currentFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\DictationaryParser\\Documentation\\Words.xlsx";
        public static string currentSheet = "";


        public MainWindow()
        {
            InitializeComponent();
            LoadDictationButton.IsEnabled = false;
            ParseDictationButton.IsEnabled = false;
            WriteWordsToExcelButton.IsEnabled = false;
        }

        private void WordsFromDictationRichBox_Loaded(object sender, RoutedEventArgs e)
        {
            WordsFromDictationRichBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            WordsFromDictationRichBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void DictationRichBox_Loaded(object sender, RoutedEventArgs e)
        {
            DictationRichBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            DictationRichBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void LoadDictationButton_Click(object sender, RoutedEventArgs e)
        {
            

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            fileDialog.ShowDialog();

            if (fileDialog.FileName != "")
            {

                DictStrings = DictationParser.GetStrings(fileDialog.FileName);

            
                FlowDocument flowDocument = new FlowDocument();
                Paragraph paragraph = new Paragraph();

                foreach (var textString in DictStrings)
                {
                    paragraph.Inlines.Add(textString);
                    flowDocument.Blocks.Add(paragraph);
                    paragraph.Inlines.Add("\n");
                    flowDocument.Blocks.Add(paragraph);
                }

                DictationRichBox.Document = flowDocument;
            }
            

        }

        private void ParseDictationButton_Click(object sender, RoutedEventArgs e)
        {
            activeTypes = new List<string>();

            ActivatedTypesCHeck();


            if (DictStrings != null && DictStrings.Count != 0 && DictationRichBox.Document != null)
            {
                TypesChengeButton.IsEnabled = false;
                LoadDictationButton.IsEnabled = false;
                ParseDictationButton.IsEnabled = false;
                WriteWordsToExcelButton.IsEnabled = false;

                //
                
                Thread thread = new Thread(new ThreadStart(FindExistingWordsInOpcorpora));
                thread.Name = "ProcessingThread";
                thread.Start();

                MessageBox.Show("Выполняется обработка слов, подождте...", "IDLE", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                BarLabel.Foreground = Brushes.Red;
                BarLabel.Content = "Идет обработка...";

                
            }
            else if (DictStrings == null)
            {
                FlowDocument flowDocument = new FlowDocument();
                Paragraph paragraph = new Paragraph();

                flowDocument = DictationRichBox.Document;

                DictStrings = new List<string>();

               
                string mystring = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd).Text;
                char[] mystringChar = mystring.ToCharArray();
                string newstring = "";

                for (int i = 0; i < mystringChar.Length; i++)
                {
                    if (mystringChar[i] != '\n' && mystringChar[i] != '\r' && mystringChar[i] != '\\')
                    {
                        newstring += mystringChar[i];
                    }
                    else
                    {
                        i++;
                        if (newstring != "")
                        {
                            DictStrings.Add(newstring);
                        }
                        newstring = "";
                    }
                }
                TypesChengeButton.IsEnabled = false;
                LoadDictationButton.IsEnabled = false;
                ParseDictationButton.IsEnabled = false;
                WriteWordsToExcelButton.IsEnabled = false;

                Thread thread = new Thread(new ThreadStart(FindExistingWordsInOpcorpora));
                thread.Name = "ProcessingThread";
                thread.Start();

                MessageBox.Show("Выполняется обработка слов, подождте...", "IDLE", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                BarLabel.Foreground = Brushes.Red;
                BarLabel.Content = "Идет обработка...";
            }
            else
            {
                {
                    MessageBox.Show("Нет данных для преобразования!");
                }
            }
            
        }

        public static void ActivatedTypesCHeck()
        {
            foreach (var chBox in PartOfSpeechChange.checkBoxes)
            {
                if (chBox.IsChecked == true)
                {
                    activeTypes.Add(chBox.Name.ToString());
                }
            }
        }


        private void TypesChengeButton_Click(object sender, RoutedEventArgs e)
        {
            

            bool? check = partOfSpeechChange.ShowDialog();

                LoadDictationButton.IsEnabled = true;
                ParseDictationButton.IsEnabled = true;
          
           
        }

        private void WriteWordsToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile != "" && currentFile.EndsWith(".xlsx"))
            {
                TypesChengeButton.IsEnabled = false;
                LoadDictationButton.IsEnabled = false;
                ParseDictationButton.IsEnabled = false;
                WriteWordsToExcelButton.IsEnabled = false;

                Thread thread = new Thread(new ThreadStart(WriteToExcel));
                thread.Name = "ProcessingThread";
                thread.Start();

                BarLabel.Foreground = Brushes.Red;
                BarLabel.Content = $"Идет запись в Excel...";
            }
            

        }

        private void FindExistingWordsInOpcorpora()
        {

            DictationParser.GetOpcorporaWords();

            wordslist = DictationParser.GetWordsFromDictationary(DictStrings);

            PreparedForExcelAddingList = DictationParser.MatchWordsFromOpcorporaAndDictationary(wordslist);

            Dispatcher.InvokeAsync(() =>
            {
                TypesChengeButton.IsEnabled = true;
                LoadDictationButton.IsEnabled = true;
                ParseDictationButton.IsEnabled = true;
                WriteWordsToExcelButton.IsEnabled = true;

                BarLabel.Foreground = Brushes.Green;
                BarLabel.Content = $"Слов найдено: {PreparedForExcelAddingList.Count}";

                MessageBox.Show($"Обработка слов закончена. Найдено: {PreparedForExcelAddingList.Count}.\n Для записи в Excel нажмите \"Записать слова\".", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            });

        }

        private void WriteToExcel()
        {
            int counter = DictationParser.AddNewWordsToExcel(PreparedForExcelAddingList, currentFile);

            Dispatcher.InvokeAsync(() =>
            {
                TypesChengeButton.IsEnabled = true;
                LoadDictationButton.IsEnabled = true;
                ParseDictationButton.IsEnabled = true;
                WriteWordsToExcelButton.IsEnabled = true;

                BarLabel.Foreground = Brushes.Green;
                BarLabel.Content = $"Успешная запись...";
                
                MessageBox.Show($"Запись в Excel закончена успешно. Записано: {counter}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        private void AddNewExcelButton_Click(object sender, RoutedEventArgs e)
        {
            ExcelCreatorWindow excelCreatorWindow = new ExcelCreatorWindow();
            excelCreatorWindow.ShowDialog();

            if (currentFile != "" && currentSheet != "")
            {
                DictationParser.CreateNewExcelFile(currentSheet, currentFile);
            }

            string fileName = Path.GetFileName(currentFile);

            IsLoadedFileLabel.Foreground = Brushes.Green;
            IsLoadedFileLabel.Content = fileName;

        }

        private void UseOldExcelButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            fileDialog.ShowDialog();

            if (fileDialog.FileName != "")
            {
                currentFile = fileDialog.FileName;
            }
            
            IsLoadedFileLabel.Foreground = Brushes.Green;
            IsLoadedFileLabel.Content = Path.GetFileName(currentFile);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
