using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace ConsoleApp3
{
   // [BsonIgnoreExtraElements]
    class Item
    {
        public ObjectId Id { get; set; }
        public string itemHeader { get; set; }
        public string itemPrice { get; set; }
     //   public string itemImage { get; set; }
        public Dictionary<string, string> itemProps { get; set; }
    }
}
