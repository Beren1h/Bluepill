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
    public class StorageContext : IStorageContext
    {
        private MongoServer _server;
        private MongoDatabase _database;

        
        //private const string CONNECTION = "mongodb://appharbor_b0d57020-973d-4040-869e-acae8dc80def:6cpf1ku6483hkg5cb0sjjdo77q@ds043447.mongolab.com:43447/appharbor_b0d57020-973d-4040-869e-acae8dc80def";
        //private const string DATABASE = "appharbor_b0d57020-973d-4040-869e-acae8dc80def";
        
        //private const string CONNECTION = "mongodb://localhost";
        //private const string DATABASE = "bluepill";

        private const string CONNECTION = "mongodb://web:3$SilkyStrong@bluepill02.cloudapp.net/bluepill";
        private const string DATABASE = "bluepill";

        public StorageContext()
        {
            _server = MongoServer.Create(CONNECTION);
            _database = _server.GetDatabase(DATABASE);
        }

        public MongoDatabase Database { get { return _database; } }

        public MongoCollection<IStorageType> GetCollection(string name)
        {
            return _database.GetCollection<IStorageType>(name);
        }

        public MongoGridFSFileInfo GridFSUpload(MemoryStream ms)
        {
            return _database.GridFS.Upload(ms, new Guid().ToString());
        }

        public void GridFSDelete(BsonValue id)
        {
            _database.GridFS.DeleteById(id);
        }
       

    }
}
