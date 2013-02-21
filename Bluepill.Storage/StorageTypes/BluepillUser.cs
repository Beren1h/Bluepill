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
    public class BluepillUser : IStorageType
    {
        public ObjectId _id { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public byte[] SearchConfiguration { get; set; }
        public IEnumerable<Facet> Facets { get; set; }
        public int WorkFactor { get; set; }
    }
}
