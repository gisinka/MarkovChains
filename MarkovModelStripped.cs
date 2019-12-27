using MarkovModelLib;
using MongoDB.Bson;
using System.Collections.Generic;

namespace MarkovChains
{
    class MarkovModelStripped
    {
        public ObjectId Id { get; set; }
        public Dictionary<string, Dictogram> DictogramModel { get; set; }
        public List<string> StartsList { get; set; }

        public MarkovModel ConvertToMarkovModel()
        {
            var markovModel = new MarkovModel("");
            markovModel.DictogramModel = DictogramModel;
            markovModel.StartsList = this.StartsList;
            foreach (var firstKey in markovModel.DictogramModel.Keys)
            {
                markovModel.DictogramModel[firstKey].KeysCount = markovModel.DictogramModel[firstKey].Keys.Count;
                markovModel.DictogramModel[firstKey].TokensCount = markovModel.DictogramModel[firstKey].Values.Count;
            }

            return markovModel;
        }
    }
}
