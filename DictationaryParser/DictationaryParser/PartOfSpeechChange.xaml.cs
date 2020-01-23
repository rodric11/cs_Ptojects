using System;
using System.Collections.Generic;
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

namespace DictationaryParser
{
    /// <summary>
    /// Логика взаимодействия для PartOfSpeechChange.xaml
    /// </summary>
    public partial class PartOfSpeechChange : Window
    {
        public static List<CheckBox> checkBoxes = new List<CheckBox>();
        public PartOfSpeechChange()
        {
            InitializeComponent();

            if (checkBoxes.Count == 0)
            {
                checkBoxes.Add(NOUN);
                checkBoxes.Add(ADJF);
                checkBoxes.Add(ADJS);
                checkBoxes.Add(COMP);
                checkBoxes.Add(VERB);
                checkBoxes.Add(INFN);
                checkBoxes.Add(PRTF);
                checkBoxes.Add(PRTS);
                checkBoxes.Add(GRND);

                checkBoxes.Add(NUMR);
                checkBoxes.Add(ADVB);
                checkBoxes.Add(NPRD);
                checkBoxes.Add(PRED);
                checkBoxes.Add(PREP);
                checkBoxes.Add(CONJ);
                checkBoxes.Add(PRCL);
                checkBoxes.Add(INTJ);
            }

            #region Костыли

            if (checkBoxes[0].IsChecked == true)
            {
                NOUN.IsChecked = true;
            }

            if (checkBoxes[1].IsChecked == true)
            {
                ADJF.IsChecked = true;
            }

            if (checkBoxes[2].IsChecked == true)
            {
                ADJS.IsChecked = true;
            }

            if (checkBoxes[3].IsChecked == true)
            {
                COMP.IsChecked = true;
            }

            if (checkBoxes[4].IsChecked == true)
            {
                VERB.IsChecked = true;
            }

            if (checkBoxes[5].IsChecked == true)
            {
                INFN.IsChecked = true;
            }

            if (checkBoxes[6].IsChecked == true)
            {
                PRTF.IsChecked = true;
            }

            if (checkBoxes[7].IsChecked == true)
            {
                PRTS.IsChecked = true;
            }

            if (checkBoxes[8].IsChecked == true)
            {
                GRND.IsChecked = true;
            }

            if (checkBoxes[9].IsChecked == true)
            {
                NUMR.IsChecked = true;
            }

            if (checkBoxes[10].IsChecked == true)
            {
                ADVB.IsChecked = true;
            }

            if (checkBoxes[11].IsChecked == true)
            {
                NPRD.IsChecked = true;
            }

            if (checkBoxes[12].IsChecked == true)
            {
                PRED.IsChecked = true;
            }

            if (checkBoxes[13].IsChecked == true)
            {
                PREP.IsChecked = true;
            }

            if (checkBoxes[14].IsChecked == true)
            {
                CONJ.IsChecked = true;
            }

            if (checkBoxes[15].IsChecked == true)
            {
                PRCL.IsChecked = true;
            }

            if (checkBoxes[16].IsChecked == true)
            {
                INTJ.IsChecked = true;
            }


            #endregion
        }

        private void ConfirmTypesButton_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
        }
    }
}
