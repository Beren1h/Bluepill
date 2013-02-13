using Bluepill.Storage.StorageTypes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public interface IStorageContext
    {
        MongoDatabase Database { get; }
        MongoCollection<IStorageType> GetCollection(string name);
        MongoGridFSFileInfo GridFSUpload(MemoryStream ms);
        void GridFSDelete(BsonValue id);
    }
}