using MarkovModelLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarkovChains
{
    internal static class Program
    {
        private static void Main()
        {
            var random = new Random();
            var name = "testCol";
            var messageGenerator = DataBaseConnector.GetFromDataBase("mongodb://localhost:27017", "test", name, 5);
            while (true)
            {
                Console.WriteLine("Введите команду:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "/next":
                        Console.WriteLine(messageGenerator.Generate(random.Next(3, 50)));
                        break;
                    case "/exit":
                        messageGenerator.AddToDataBase("mongodb://localhost:27017", "test", name, 5);
                        Environment.Exit(0);
                        break;
                    case "/update":
                        Console.WriteLine("Введите текст для занесения в модель:");
                        messageGenerator.Update(Console.ReadLine());
                        break;
                    case "/count":
                        Console.WriteLine(messageGenerator.Count());
                        break;
                    case "/clear":
                        messageGenerator.Clear();
                        break;
                }
            }
        }
    }
}