using Bluepill.Search;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public class Attic : IAttic
    {
        private MongoServer _server;
        private MongoDatabase _database;
        private IQueryBuilder _queryBuilder;
        private IFacetCollectionReader _reader;
                
        private const string CONNECTION = "mongodb://localhost";
        private const string DATABASE = "bluepill_test";
        
        public Attic(IQueryBuilder queryBuilder)
        {
            _server = MongoServer.Create(CONNECTION);
            _database = _server.GetDatabase(DATABASE);
            _queryBuilder = queryBuilder;
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
            GetIndexedCollection(box).Insert(box);
        }

        //public IList<Box> GetBoxes(IList<Facet> facets, int perPage, int startIndex, string[] fields = null)
        //{
        //    var query = _queryBuilder.Build(facets);
        //    var results = new List<Box>();
        //    var upperBound = startIndex + perPage;
        //    var collection = 

        //    //if (facets.Count == 0)
        //    //{
        //    //    results = 
        //    //}
        //    //else
        //    //{
        //    //}

        //}




        private MongoCollection<Box> GetIndexedCollection(Box box)
        {
            var collection = _database.GetCollection<Box>(box.UserId);

            var options = new IndexOptionsDocument();
            options.Add(new BsonDocument("unique", true));

            var ensureUnique = new IndexKeysDocument();
            ensureUnique.Add(Fields.HASH, box.Hash);

            var keys = new IndexKeysDocument();
            foreach (var item in box.MetaData)
            {
                collection.EnsureIndex(item.Name);
            }

            collection.EnsureIndex(ensureUnique, options);

            return collection;
        }








        public void Empty()
        {
            foreach (var collection in _database.GetCollectionNames())
            {
                _database.GetCollection<Box>(collection).Drop();
            }

            _database.GridFS.Files.Drop();
        }


    }
}
