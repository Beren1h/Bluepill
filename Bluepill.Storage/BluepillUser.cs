using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public class BluepillUser
    {
        public ObjectId _id { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public int WorkFactor { get; set; }
    }
}
