using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkovChains
{
    internal class Dictogram : Dictionary<string, int>
    {
        private readonly Random _random;
        private int _keysCount;
        private int _tokensCount;

        public Dictogram(string[] iterable = null)
        {
            _keysCount = 0;
            _tokensCount = 0;
            _random = new Random();
            if (iterable != null)
            {
                Update(iterable);
            }
        }

        public void Update(string[] iterable)
        {
            foreach (var word in iterable)
            {
                if (ContainsKey(word))
                {
                    this[word] += 1;
                    _tokensCount += 1;
                }
                else
                {
                    this[word] = 1;
                    _tokensCount += 1;
                    _keysCount += 1;
                }
            }
        }

        public int CountWord(string word)
        {
            if (ContainsKey(word))
            {
                return this[word];
            }

            return 0;
        }

        public string ReturnRandomWord()
        {
            return Keys.ToList()[_random.Next(0, Keys.ToList().Count - 1)];
        }

        public string ReturnWeightedRandomWord()
        {
            var randomIndex = _random.Next(0, _tokensCount - 1);
            var index = 0;
            var keysList = Keys.ToList();
            for (var i = 0; i < _keysCount; i++)
            {
                index += this[keysList[i]];
                if (index >= randomIndex)
                {
                    index = i;
                    break;
                }
            }

            return keysList[index];
        }
    }
}