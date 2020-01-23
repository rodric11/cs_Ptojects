using Microsoft.Win32;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using static DictationaryParser.DictationParser;

namespace DictationaryParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Expectation idleWIndow = new Expectation();

        public static List<string> activeTypes = new List<string>();
        public List<string> DictStrings { get; set; }
        public MainWindow()
        {
            InitializeComponent();
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
            activeTypes = new List<string>();

            ActivatedTypesCHeck();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            fileDialog.ShowDialog();

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

        private void ParseDictationButton_Click(object sender, RoutedEventArgs e)
        {
            if (DictStrings != null || DictationRichBox.Document != null)
            {
                //Thread thread = new Thread(new ThreadStart(GetOpcorporaWords));
                
                idleWIndow.WindowStyle = WindowStyle.None;
                idleWIndow.ExpectationLabel.Content = "Выолняется подключение Opcorpora...";
                idleWIndow.ExpectationLabel.Visibility = Visibility.Visible;
                //thread.Start();
                BarLabel.Content = "Выолняется подключение Opcorpora...";
                //while (DictationParser.OpcorporaWords == null)
                //{
                //    //idlewindow.show();
                //    //idlewindow.showactivated = true;
                //    //idlewindow.activate();
                //    Thread.Sleep(1000);
                //    //idlewindow.hide();
                //}
                
                //Thread.Sleep(1000);
                GetOpcorporaWords();

                BarLabel.Content = "Выолняется обработка диктанта...";
                
                

                List<string> wordslist = GetWordsFromDictationary(DictStrings);

                BarLabel.Content = "Выолняется сопоставление слов из диктанта c Opcorpora...";
                AddNewWordsToExcel(MatchWordsFromOpcorporaAndDictationary(wordslist));
                //idleWIndow.Hide();
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
            PartOfSpeechChange partOfSpeechChange = new PartOfSpeechChange();

            partOfSpeechChange.Show();
        }
    }
}
