using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MarkovChains
{
    static class TextParser
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            if (text.Length > 0 || !char.IsPunctuation(text[^1]))
                text += ".";
            foreach (Match sentence in Regex.Matches(text, @"[А-ЯЁ][\S\s]+?(?:[\S][^А-ЯЁ\.]){1,}(?:\.+|[?!])(?!(\s*[а-яё)\-""«0-9\.]))", RegexOptions.Multiline))
            {
                var sentenceDivided = DivideWords(sentence.Value);
                if (sentenceDivided.Count > 0)
                {
                    sentencesList.Add(sentenceDivided);
                }
            }

            return sentencesList;
        }

        private static List<string> DivideWords(string sentence)
        {
            var wordsList = new List<string>();
            var word = new StringBuilder();
            for (var i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == '\'' || char.IsLetter(sentence[i]))
                {
                    word.Append(sentence[i]);
                }
                else if (word.Length > 0)
                {
                    wordsList.Add(word.ToString());
                    word.Clear();
                }
            }

            if (word.Length > 0)
            {
                wordsList.Add(word.ToString());
            }

            return wordsList;
        }
    }
}