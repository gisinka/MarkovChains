using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkovChains
{
    class Dictogram : Dictionary<string, int>
    {
        private int _keysCount;
        private int _tokensCount;
        private readonly Random _random;

        public Dictogram(string[] iterable = null)
        {
            this._keysCount = 0;
            this._tokensCount = 0;
            this._random = new Random(System.Environment.TickCount);
            if (iterable != null)
                this.Update(iterable);
        }

        public void Update(string[] iterable)
        {
            foreach (var word in iterable)
            {
                if (this.ContainsKey(word))
                {
                    this[word] += 1;
                    this._tokensCount += 1;
                }
                else
                {
                    this[word] = 1;
                    this._tokensCount += 1;
                    this._keysCount += 1;
                }
            }
        }

        public int CountWord(string word)
        {
            if (this.ContainsKey(word))
                return this[word];
            return 0;
        }

        public string ReturnRandomWord()
        {
            return this.Keys.ToList()[_random.Next(0, Keys.ToList().Count - 1)];
        }

        public string ReturnWeightedRandomWord()
        {
            var randomIndex = _random.Next(0, this._tokensCount - 1);
            var index = 0;
            var keysList = this.Keys.ToList();
            for (var i = 0; i < this._keysCount; i++)
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
