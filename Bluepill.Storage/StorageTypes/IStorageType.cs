using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage.StorageTypes
{
    public interface IStorageType
    {
        ObjectId _id { get; set; }
    }
}
