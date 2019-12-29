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
            var markovModel = new Dictionary<string, Dictogram>();
            MarkovModelMaker.UpdateMarkovModel(sentences, startsList, markovModel);
            var random = new Random(Environment.TickCount);
            var isWork = true;
            while (isWork)
            {
                Console.WriteLine("Введите команду:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "/next":
                        Console.WriteLine(TextGenerator.GenerateText(markovModel, startsList, random.Next(3, 50)));
                        break;
                    case "/exit":
                        isWork = false;
                        break;
                    case "/update":
                        Console.WriteLine("Введите текст для занесения в модель:");
                        var newText = TextParser.ParseSentences(Console.ReadLine());
                        MarkovModelMaker.UpdateMarkovModel(newText, startsList, markovModel);
                        break;
                    case "/count":
                        var count = 0;
                        foreach (var value in markovModel.Values) count += value.Keys.Count;
                        Console.WriteLine("Пар начало-продолжение: {0}", count);
                        break;
                }
            }
        }
    }
}