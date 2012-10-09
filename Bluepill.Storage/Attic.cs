using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public class Attic : IAttic
    {
        private MongoServer _server;
        private MongoDatabase _database;
                
        private const string CONNECTION = "mongodb://localhost";
        private const string DATABASE = "bluepill_test";
        
        public Attic()
        {
            _server = MongoServer.Create(CONNECTION);
            _database = _server.GetDatabase(DATABASE);
        }

        public void AddBox(Box box)
        {
            if (box.IsLarge)
            {
                using(var stream = new FileStream(box.file, FileMode.Open))
                {
                    var gridFSItem = _database.GridFS.Upload(stream, box.file);
                    box.GridFSId = gridFSItem.Id;
                }
            }

            _database.GetCollection<Box>("boxes").Insert(box);

        }

        public void Empty(string file)
        {
            _database.GetCollection<Box>("boxes").Drop();
            _database.GridFS.Delete(file);
        }
    }
}
