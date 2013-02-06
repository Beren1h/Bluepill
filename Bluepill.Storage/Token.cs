using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public class Token
    {
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public string Value { get; set; }
        public string Secret { get; set; }
    }
}
