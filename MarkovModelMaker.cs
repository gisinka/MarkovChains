using System.Collections.Generic;
using System.Text;

namespace MarkovChains
{
    class MarkovModelMaker
    {
        public static Dictionary<string, Dictogram> CreateMarkovModel(List<List<string>> text, List<string> startsList)
        {
            var markovModel = new Dictionary<string, Dictogram>();
            foreach (var sentence in text)
            {
                CreateNGramKeys(sentence, markovModel, 2, startsList);
                CreateNGramKeys(sentence, markovModel, 3, startsList);
            }
            return markovModel;
        }

        private static void CreateNGramKeys(List<string> sentence, Dictionary<string, Dictogram> markovModel,
            int gramDimension, List<string> startsList)
        {
            var firstKey = new StringBuilder();
            var secondKey = new StringBuilder();
            for (var i = 0; i < sentence.Count - gramDimension + 1; i++)
            {
                for (var m = 0; m < gramDimension - 1; m++)
                {
                    firstKey.Append(sentence[m + i] + " ");
                    if (i == 0 && m == gramDimension - 2)
                        startsList.Add(firstKey.ToString().Substring(0, firstKey.Length - 1));
                    if (m == gramDimension - 2)
                        secondKey.Append(sentence[m + i + 1]);
                }
                firstKey.Remove(firstKey.Length - 1, 1);
                AddNGram(markovModel, firstKey.ToString(), secondKey.ToString());
                firstKey.Clear();
                secondKey.Clear();
            }
        }

        private static void AddNGram(Dictionary<string, Dictogram> markovModel, string firstKey, string secondKey)
        {
            if (markovModel.ContainsKey(firstKey))
                markovModel[firstKey].Update(new string[] {secondKey});
            else
                markovModel[firstKey] = new Dictogram(new string[] { secondKey });
        }
    }
}
