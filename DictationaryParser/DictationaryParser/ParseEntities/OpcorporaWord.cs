using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictationaryParser
{
    class OpcorporaWord
    {
        public string Lexeme { get; set; }

        public string PartOfSpeech { get; set; }

        public List<string> AditionalWords { get; set; }


    }
}
