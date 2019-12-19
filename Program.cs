using System;
using System.Collections.Generic;
using System.IO;

namespace MarkovChains
{
    static class Program
    {
        static void Main()
        {
            var startsList = new List<string>();
            var text = File.ReadAllText("data.txt");
            var sentences = TextParser.ParseSentences(text);
            var markovModel = MyMarkovModelMaker.CreateMarkovModel(sentences, startsList);
            int wordsCount;
            try
            {
                wordsCount = int.Parse(Console.ReadLine());
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                wordsCount = 140;

            }
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Backspace)
                    break;
                else
                {
                    Console.WriteLine(TextGenerator.GenerateText(markovModel, startsList, wordsCount));
                }
            }
        }
    }
}
