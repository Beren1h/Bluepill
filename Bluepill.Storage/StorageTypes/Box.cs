using Bluepill.Search;
using Bluepill.Storage.StorageTypes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage.StorageTypes
{
    public class Box : IStorageType
    {
        public ObjectId _id { get; set; }
        public BsonValue GridFSId { get; set; }
        public bool IsLarge {get; set;}
        public BsonDocument MetaData { get; set; }
        public int ReducedBytesWidth { get; set; }
        public int ReducedBytesHeight { get; set; }
        public byte[] ReducedBytes { get; set; }
        public byte[] Bytes { get; set; }
        public string UserId { get; set; }
        public string file { get; set; }
        public string Hash { get; set; }
    }
}
