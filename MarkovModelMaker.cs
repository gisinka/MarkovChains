using System.Collections.Generic;
using System.Collections.Immutable;

namespace MarkovChains
{
    class MarkovModelMaker
    {
        public static Dictionary<string, Dictogram> MakeMarkovModel(string[] data)
        {
            var markovModel = new Dictionary<string, Dictogram>();
            for (var i = 0; i < data.Length - 1; i++)
            {
                if (markovModel.ContainsKey(data[i]))
                    markovModel[data[i]].Update(new string[] {data[i+1]});
                else
                    markovModel[data[i]] = new Dictogram(new string[] { data[i + 1] });
            }

            return markovModel;
        }

        public static Dictionary<ImmutableArray<string>, Dictogram> MakeHigherOrderMarkovModel(int order, string[] data)
        {
            var markovModel = new Dictionary<ImmutableArray<string>, Dictogram>();
            for (var i = 0; i < data.Length - order; i++)
            {
                var window = ImmutableArray.Create(data, i, order);
                if (markovModel.ContainsKey(window))
                    markovModel[window].Update(new string[] {data[i+order]});
                else
                    markovModel[window] = new Dictogram(new string[] { data[i + order] });
            }

            return markovModel;
        }
    }
}
