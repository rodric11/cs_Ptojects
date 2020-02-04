using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DictationaryParser;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ExcelAddingTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            OpcorporaWord word1 = new OpcorporaWord();

            word1.Lexeme = "test1";
            word1.PartOfSpeech = "testSpeech1";

            OpcorporaWord word2 = new OpcorporaWord();

            word2.Lexeme = "test2";
            word2.PartOfSpeech = "testSpeec2";

            List<OpcorporaWord> words = new List<OpcorporaWord>();

            words.Add(word1);
            words.Add(word2);

            DictationParser.AddNewWordsToExcel(words, "asdasd");

        }
    }
}
