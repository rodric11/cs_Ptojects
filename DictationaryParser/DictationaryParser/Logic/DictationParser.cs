using DictationaryParser.ParseEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

[assembly: InternalsVisibleTo("UnitTests")]
namespace DictationaryParser
{

    internal class DictationParser
    {
        public static Expectation idleWIndow = new Expectation();

        public static List<string> partOfSpeeches = new List<string>();
        public static List<OpcorporaWord> OpcorporaWords { get; set; }

        public static List<string> GetStrings(string filePath)
        {
            string[] dictationStrings = File.ReadAllLines(filePath, Encoding.Default);

            List<string> stringsList = new List<string>();

            foreach (var str in dictationStrings)
            {
                if (str != "")
                {
                    stringsList.Add(str);
                }
            }
            return stringsList;
        }

        public static void GetOpcorporaWords()
        {


            //idleWIndow.WindowStyle = WindowStyle.None;
            //idleWIndow.ExpectationLabel.Content = ;

            //ShowWindow("Выолняется подключение Opcorpora...");

            //string xmlFileName = "\\dict.opcorpora.xml";

            string xmlFolderName = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Documentation\\dict.opcorpora.xml";

            XmlDocument xDoc = new XmlDocument();

            xDoc.Load(xmlFolderName);

            XmlElement xRoot = xDoc.DocumentElement;

            XmlNodeList lemmataLvl = xRoot.SelectNodes("lemmata");

            XmlNode lemmata = lemmataLvl[0];  // Уровень lemmata

            XmlNodeList lemmaLvl = lemmata.SelectNodes("lemma");  // Уровень lemma

            OpcorporaWords = new List<OpcorporaWord>();

            foreach (XmlNode xmlNode in lemmaLvl)
            {
                XmlNode lNode = xmlNode.FirstChild; // Уровень l
                XmlNode gNode = lNode.FirstChild;  // Уровень g
                XmlNode fNode = xmlNode.ChildNodes[1]; // f одуровень

                string str = gNode.SelectSingleNode("@v").Value;  // Значение "INFN"
                string value = lNode.SelectSingleNode("@t").Value; // лексема глагола

                OpcorporaWord opcorporaWord = new OpcorporaWord();
                opcorporaWord.AditionalWords = new List<string>();

                //string[] partOfSpeechSTR = Enum.GetNames(typeof (PartOfSpeech));

                //foreach (var pos in partOfSpeechSTR)
                //{
                //    partOfSpeeches.Add(pos);
                //}

                if (str != null)
                {
                    if (MainWindow.activeTypes.Contains(str) == true)
                    {
                        foreach (XmlNode chilnode in xmlNode.ChildNodes)
                        {
                            string aditionalWord = chilnode.SelectSingleNode("@t").Value;
                            opcorporaWord.AditionalWords.Add(aditionalWord);
                        }

                        opcorporaWord.Lexeme = value;
                        opcorporaWord.PartOfSpeech = str;

                        //if (OpcorporaWords.Find(x => x.Lexeme == opcorporaWord.Lexeme) == null)
                        //{

                        //}

                        OpcorporaWords.Add(opcorporaWord);
                    }

                }

            }

        }

        public static List<string> GetWordsFromDictationary(List<string> dictStrings)
        {
            //CloseWindow();
            //ShowWindow("Выолняется обработка диктанта...");

            List<string> singleWords = new List<string>();

            string word = "";

            foreach (var @string in dictStrings)
            {
                char[] chars = @string.ToCharArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] >= '\u0410' && chars[i] <= '\u044F' || chars[i] == 'ё' || chars[i] == 'Ё')
                    {
                        word += chars[i];
                    }
                    if ((chars[i] == ' ' || chars[i] == '.' || chars[i] == ',' || chars[i] == ';' || chars[i] == ':' || chars[i] == '-'))
                    {
                        if (word != "")
                        {
                            word = word.ToLower();
                            singleWords.Add(word);
                            word = "";
                        }

                    }
                }
            }


            return singleWords;
        }

        public static List<OpcorporaWord> MatchWordsFromOpcorporaAndDictationary(List<string> singleWords)
        {
            //CloseWindow();

            //ShowWindow("Выолняется сопоставление слов из диктанта c Opcorpora...");

            List<OpcorporaWord> words = new List<OpcorporaWord>();

            foreach (var sWord in singleWords)
            {
                foreach (var oWord in OpcorporaWords)
                {
                    if (oWord.Lexeme == sWord)
                    {
                        OpcorporaWord newWord = new OpcorporaWord();

                        newWord.Lexeme = sWord;
                        newWord.PartOfSpeech = oWord.PartOfSpeech;

                        if (newWord.Lexeme.Length > 2)
                        {
                            words.Add(newWord);
                        }
                        
                    }
                    else
                    {
                        foreach (var aWord in oWord.AditionalWords)
                        {
                            if (aWord == sWord)
                            {
                                OpcorporaWord newWord = new OpcorporaWord();

                                newWord.Lexeme = sWord;
                                newWord.PartOfSpeech = oWord.PartOfSpeech;

                                if (words.Any(x => x.Lexeme == sWord) == false)
                                {
                                    if (newWord.Lexeme.Length > 2)
                                    {
                                        words.Add(newWord);
                                    }
                                    
                                }

                            }
                        }
                    }

                    // System.Console.WriteLine($"Обработано - {counter}, Найдено совпадений - {words.Count}");
                }
            }

            return words;
        }

        public static void AddNewWordsToExcel(List<OpcorporaWord> words)
        {
            Excel.Application objExcel = new Excel.Application();

            System.Diagnostics.Process[] excel = System.Diagnostics.Process.GetProcessesByName("excel");

            //string fileName = "\\Words.xlsx";
            string folder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\DictationaryParser\\Documentation\\Words.xlsx";
            //string fullFileName = folder + fileName;

            Excel.Workbook objWorkBook = objExcel.Workbooks.Open(folder, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            // TODO: Добавить возможность изменения в разных таблицах.
            Excel.Worksheet objWorkSheet = (Excel.Worksheet)objWorkBook.Worksheets["First"];

            int lastrow = objWorkSheet.Cells[objWorkSheet.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row + 1;

            try
            {
                if (objWorkSheet.Cells[2, "A"].Value != null)
                {
                    List<string> excelwords = new List<string>();

                    for (int i = 2; i < lastrow; i++)
                    {
                        excelwords.Add(objWorkSheet.Cells[i, "A"].Value.ToString());
                    }


                    foreach (var word in words)
                    {
                        foreach (var exw in excelwords)
                        {
                            if (words.Any(x => x.Lexeme == exw) == false)
                            {
                                objWorkSheet.Cells[lastrow, "A"].Value = (dynamic)word.Lexeme;
                                objWorkSheet.Cells[lastrow, "B"].Value = (dynamic)word.PartOfSpeech;

                                lastrow++;
                            }

                        }
                    }


                }
                else
                {
                    foreach (var word in words)
                    {
                        objWorkSheet.Cells[lastrow, "A"].Value = (dynamic)word.Lexeme;
                        objWorkSheet.Cells[lastrow, "B"].Value = (dynamic)word.PartOfSpeech;

                        lastrow++;
                    }
                }

                objWorkBook.Application.DisplayAlerts = false;
                objWorkBook.Close(true, folder, System.Reflection.Missing.Value);
                objExcel.Quit();

            }
            catch (Exception e)
            {
                foreach (var proc in excel)
                {
                    if (string.IsNullOrEmpty(proc.MainWindowTitle))
                    {
                        proc.Kill();
                    }
                }

                MessageBox.Show(e.Message);
            }

            // Закрываем процессы Excel
            foreach (var proc in excel)
            {
                if (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    proc.Kill();
                }
            }
        }
    }
}