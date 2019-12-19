using System;
using System.Collections.Generic;
using System.IO;

namespace MarkovChains
{
    internal static class Program
    {
        private static void Main()
        {
            var startsList = new List<string>();
            var text = File.ReadAllText("data.txt");
            var sentences = TextParser.ParseSentences(text);
            var markovModel = MarkovModelMaker.CreateMarkovModel(sentences, startsList);
            int wordsCount;
            var random = new Random(System.Environment.TickCount);
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Backspace)
                    break;
                Console.WriteLine(TextGenerator.GenerateText(markovModel, startsList, random.Next(3, 20)));
                Console.WriteLine(TextGenerator.GenerateText(markovModel, startsList, random.Next(3, 20)));
                Console.WriteLine(TextGenerator.GenerateText(markovModel, startsList, random.Next(3, 20)));
            }
        }
    }
}