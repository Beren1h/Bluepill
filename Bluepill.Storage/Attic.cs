using Bluepill.Search;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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
                
        private const string CONNECTION = "mongodb://localhost";
        private const string DATABASE = "bluepill";
        
        public Attic(IQueryBuilder queryBuilder)
        {
            _server = MongoServer.Create(CONNECTION);
            _database = _server.GetDatabase(DATABASE);
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collectionName"></param>
        public void RemoveBox(ObjectId id, string collectionName)
        {
            var query = _queryBuilder.Build(id);
            var collection = _database.GetCollection<Box>(collectionName);
            var box = collection.FindAs<Box>(query).SetFields(new []{Fields.GRIDFS_ID, Fields.IS_LARGE, Fields.BYTES, Fields.OBJECT_ID}).FirstOrDefault();

            if (box.IsLarge)
            {
                _database.GridFS.DeleteById(box.GridFSId);
            }
            
            collection.Remove(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collectionName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Retrieval GetBox(ObjectId id, string collectionName, string[] fields)
        {
            var query = _queryBuilder.Build(id);
            var collection = _database.GetCollection<Box>(collectionName);
            var cursor = collection.FindAs<Box>(query).SetFields(fields);
            var box = cursor.FirstOrDefault();
            var retrieval = new Retrieval();
            if (box.IsLarge)
            {
                var file = _database.GridFS.FindOneById(box.GridFSId);

                using (var stream = file.OpenRead())
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);

                    using (var ms = new MemoryStream())
                    {
                        ms.Write(bytes, 0, bytes.Length);
                        box.Bytes = ms.ToArray();
                    }
                }

                retrieval.Boxes.Add(box);
            }

            retrieval.Boxes.Add(box);
            retrieval.Total = 1;

            return retrieval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facets"></param>
        /// <param name="perPage"></param>
        /// <param name="page"></param>
        /// <param name="collectionName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Retrieval GetBoxes(IEnumerable<Facet> facets, int perPage, int page, string collectionName, string[] fields = null)
        {
            if(fields == null)
                fields = new [] { Fields.METADATA, Fields.OBJECT_ID, Fields.REDUCED_BYTES, Fields.REDUCED_BYTES_WIDTH, Fields.REDUCED_BYTES_HEIGHT };

            //var query = _queryBuilder.Build((from v in facets select v.Value).ToList());
            var query = _queryBuilder.Build(facets);
            var results = new List<Box>();
            var collection = _database.GetCollection<Box>(collectionName);

            var cursor = (query == null) ? collection.FindAllAs<Box>().SetFields(fields) : collection.FindAs<Box>(query).SetFields(fields); 

            cursor.Limit = perPage - 1;
            cursor.Skip = (page - 1) * perPage;

            return new Retrieval { Boxes = cursor.ToList(), Total = cursor.Count() };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
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

