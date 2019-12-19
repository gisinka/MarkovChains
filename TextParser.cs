using System;
using System.Collections.Generic;
using System.Text;

namespace MarkovChains
{
    static class TextParser
    {
        private static readonly char[] Separators = {'.', '!', '?', ':', ';', '(', ')'};

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            foreach (var sentence in text.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
            {
                var sentenceDivided = DivideWords(sentence);
                if (sentenceDivided.Count > 0)
                    sentencesList.Add(sentenceDivided);
            }

            return sentencesList;
        }

        private static List<string> DivideWords(string sentence)
        {
            var wordsList = new List<string>();
            var word = new StringBuilder();
            for (var i = 0; i < sentence.Length; i++)
                if (sentence[i] == '\'' || char.IsLetter(sentence[i]))
                {
                    word.Append(sentence[i]);
                }
                else if (word.Length > 0)
                {
                    wordsList.Add(word.ToString().ToLowerInvariant());
                    word.Clear();
                }

            if (word.Length > 0)
                wordsList.Add(word.ToString().ToLowerInvariant());
            return wordsList;
        }
    }
}