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

            //if (NOUN.IsChecked == true )
            //{
            //    checkBoxes[0].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[0].IsChecked = false;
            //}

            //if (ADJF.IsChecked == true)
            //{
            //    checkBoxes[1].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[1].IsChecked = false;
            //}

            //if (ADJS.IsChecked == true)
            //{
            //    checkBoxes[2].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[2].IsChecked = false;
            //}

            //if (COMP.IsChecked == true)
            //{
            //    checkBoxes[3].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[3].IsChecked = false;
            //}

            //if (VERB.IsChecked == true)
            //{
            //    checkBoxes[4].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[4].IsChecked = false;
            //}

            //if (INFN.IsChecked == true)
            //{
            //    checkBoxes[5].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[5].IsChecked = false;
            //}

            //if (PRTF.IsChecked == true)
            //{
            //    checkBoxes[6].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[6].IsChecked = false;
            //}

            //if (PRTS.IsChecked == true)
            //{
            //    checkBoxes[7].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[7].IsChecked = false;
            //}

            //if (GRND.IsChecked == true)
            //{
            //    checkBoxes[8].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[8].IsChecked = false;
            //}

            //if (NUMR.IsChecked == true)
            //{
            //    checkBoxes[9].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[9].IsChecked = false;
            //}

            //if (ADVB.IsChecked == true)
            //{
            //    checkBoxes[10].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[10].IsChecked = false;
            //}

            //if (NPRD.IsChecked == true)
            //{
            //    checkBoxes[11].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[11].IsChecked = false;
            //}

            //if (PRED.IsChecked == true)
            //{
            //    checkBoxes[12].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[12].IsChecked = false;
            //}

            //if (PREP.IsChecked == true)
            //{
            //    checkBoxes[13].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[13].IsChecked = false;
            //}

            //if (CONJ.IsChecked == true)
            //{
            //    checkBoxes[14].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[14].IsChecked = false;
            //}

            //if (PRCL.IsChecked == true)
            //{
            //    checkBoxes[15].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[15].IsChecked = false;
            //}

            //if (INTJ.IsChecked == true)
            //{
            //    checkBoxes[16].IsChecked = true;
            //}
            //else
            //{
            //    checkBoxes[16].IsChecked = false;
            //}


            #endregion
        }

        private void ConfirmTypesButton_Click(object sender, RoutedEventArgs e)
        {
            if (NOUN.IsChecked == true)
            {
                checkBoxes[0].IsChecked = true;
                NOUN.IsChecked = true;
            }
            else
            {
                checkBoxes[0].IsChecked = false;
                NOUN.IsChecked = false;
            }

            if (ADJF.IsChecked == true)
            {
                checkBoxes[1].IsChecked = true;
                ADJF.IsChecked = true;
            }
            else
            {
                checkBoxes[1].IsChecked = false;
                ADJF.IsChecked = false;
            }

            if (ADJS.IsChecked == true)
            {
                checkBoxes[2].IsChecked = true;
                ADJS.IsChecked = true;
            }
            else
            {
                checkBoxes[2].IsChecked = false;
                ADJS.IsChecked = false;
            }

            if (COMP.IsChecked == true)
            {
                checkBoxes[3].IsChecked = true;
                COMP.IsChecked = true;
            }
            else
            {
                checkBoxes[3].IsChecked = false;
                COMP.IsChecked = false ;
            }

            if (VERB.IsChecked == true)
            {
                checkBoxes[4].IsChecked = true;
                VERB.IsChecked = true;
            }
            else
            {
                checkBoxes[4].IsChecked = false;
                VERB.IsChecked = false;
            }

            if (INFN.IsChecked == true)
            {
                checkBoxes[5].IsChecked = true;
                INFN.IsChecked = true;
            }
            else
            {
                checkBoxes[5].IsChecked = false;
                INFN.IsChecked = false;
            }

            if (PRTF.IsChecked == true)
            {
                checkBoxes[6].IsChecked = true;
                PRTF.IsChecked = true;
            }
            else
            {
                checkBoxes[6].IsChecked = false;
                PRTF.IsChecked = false;
            }

            if (PRTS.IsChecked == true)
            {
                checkBoxes[7].IsChecked = true;
                PRTS.IsChecked = true;
            }
            else
            {
                checkBoxes[7].IsChecked = false;
                PRTS.IsChecked = false;
            }

            if (GRND.IsChecked == true)
            {
                checkBoxes[8].IsChecked = true;
                GRND.IsChecked = true;
            }
            else
            {
                checkBoxes[8].IsChecked = false;
                GRND.IsChecked = false;
            }

            if (NUMR.IsChecked == true)
            {
                checkBoxes[9].IsChecked = true;
                NUMR.IsChecked = true;
            }
            else
            {
                checkBoxes[9].IsChecked = false;
                NUMR.IsChecked = false;
            }

            if (ADVB.IsChecked == true)
            {
                checkBoxes[10].IsChecked = true;
                ADVB.IsChecked = true;
            }
            else
            {
                checkBoxes[10].IsChecked = false;
                ADVB.IsChecked = false;
            }

            if (NPRD.IsChecked == true)
            {
                checkBoxes[11].IsChecked = true;
                NPRD.IsChecked = true;
            }
            else
            {
                checkBoxes[11].IsChecked = false;
                NPRD.IsChecked = false;
            }

            if (PRED.IsChecked == true)
            {
                checkBoxes[12].IsChecked = true;
                PRED.IsChecked = true;
            }
            else
            {
                checkBoxes[12].IsChecked = false;
                PRED.IsChecked = false;
            }

            if (PREP.IsChecked == true)
            {
                checkBoxes[13].IsChecked = true;
                PREP.IsChecked = true;
            }
            else
            {
                checkBoxes[13].IsChecked = false;
                PREP.IsChecked = false;
            }

            if (CONJ.IsChecked == true)
            {
                checkBoxes[14].IsChecked = true;
                CONJ.IsChecked = true;
            }
            else
            {
                checkBoxes[14].IsChecked = false;
                CONJ.IsChecked = false;
            }

            if (PRCL.IsChecked == true)
            {
                checkBoxes[15].IsChecked = true;
                PRCL.IsChecked = true;
            }
            else
            {
                checkBoxes[15].IsChecked = false;
                PRCL.IsChecked = false;
            }

            if (INTJ.IsChecked == true)
            {
                checkBoxes[16].IsChecked = true;
                INTJ.IsChecked = true;
            }
            else
            {
                checkBoxes[16].IsChecked = false;
                INTJ.IsChecked = false;
            }

            //new PartOfSpeechChange();
            this.Hide();
        }
    }
}
