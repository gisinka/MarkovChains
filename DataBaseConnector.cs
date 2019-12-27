using System.Runtime.CompilerServices;
using MarkovModelLib;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MarkovChains
{
    public static class DataBaseConnector
    {
        public static void AddToDataBase(this MarkovModel markovModel, string connectionString, string databaseName, string collectionName, int dialogId)
        {
            var client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            var document = BsonDocument.Parse(JsonConvert.SerializeObject(markovModel));
            document.Add(new BsonElement("dialogId", dialogId));
            var filter = Builders<BsonDocument>.Filter.Eq("dialogId", dialogId);
            collection.ReplaceOne(filter, document, new ReplaceOptions() { IsUpsert = true });
        }

        public static MarkovModel GetFromDataBase(string connectionString, string databaseName, string collectionName, int dialogId)
        {
            var client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("dialogId", dialogId);
            var document = collection.Find(filter).FirstOrDefault();
            document.Remove("_id");
            document.Remove("dialogId");
            return JsonConvert.DeserializeObject<MarkovModelStripped>(document.ToJson()).ConvertToMarkovModel();
        }
    }
}
