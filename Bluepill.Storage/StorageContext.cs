using Bluepill.Storage.StorageTypes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        private string CONNECTION = ConfigurationManager.ConnectionStrings["bluepill"].ConnectionString;
        //private string CONNECTION = System.Environment.GetEnvironmentVariable("CUSTOMCONNSTR_bluepill");
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
