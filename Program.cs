using System;
using System.IO;

namespace MarkovChains
{
    internal static class Program
    {
        private static void Main()
        {
            var random = new Random();
            var messageGenerator = new MarkovModelLib.MarkovModel(File.ReadAllText("data.txt"));
            var isWork = true;
            while (isWork)
            {
                Console.WriteLine("Введите команду:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "/next":
                        Console.WriteLine(messageGenerator.Generate(random.Next(3, 30)));
                        break;
                    case "/exit":
                        isWork = false;
                        break;
                    case "/update":
                        Console.WriteLine("Введите текст для занесения в модель:");
                        messageGenerator.Update(Console.ReadLine());
                        break;
                    case "/count":
                        Console.WriteLine(messageGenerator.Count());
                        break;
                }
            }
        }
    }
}