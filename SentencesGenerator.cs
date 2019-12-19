using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace MarkovChains
{
    internal class SentencesGenerator
    {
        public static string GenerateRandomStart(Dictionary<ImmutableArray<string>, Dictogram> model)
        {
            if (model.ContainsKey(ImmutableArray.Create("END")))
            {
                var seedWord = "END";
                while (seedWord == "END")
                    seedWord = model[ImmutableArray.Create("END")].ReturnWeightedRandomWord();
                return seedWord;
            }

            var random = new Random(Environment.TickCount);
            var count = model.Keys.Count;
            return model.Keys.ToList()[random.Next(0, count)].ToString();
        }

        public static string GenerateRandomStart(Dictionary<string, Dictogram> model)
        {
            var seedWord = "";
            if (model.ContainsKey("END"))
            {
                seedWord = "END";
                while (seedWord == "END")
                    seedWord = model["END"].ReturnWeightedRandomWord();
                return seedWord;
            }

            var random = new Random(Environment.TickCount);
            var count = model.Keys.Count;
            return model.Keys.ToList()[random.Next(0, count)];
        }

        public static string GenerateRandomSentence(int length, Dictionary<string, Dictogram> markovModel)
        {
            var sentence = new List<string>();
            var currentWord = GenerateRandomStart(markovModel);
            var firstWord = new StringBuilder(currentWord);
            firstWord[0] = char.ToUpper(firstWord[0]);
            sentence.Add(firstWord.ToString());
            for (var i = 0; i < length; i++)
            {
                var currentDictogram = markovModel[currentWord];
                var randomWeightedWord = currentDictogram.ReturnWeightedRandomWord();
                currentWord = randomWeightedWord;
                sentence.Add(currentWord);
            }

            return string.Join(' ', sentence) + '.';
        }
    }
}