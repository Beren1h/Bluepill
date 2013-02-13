using Bluepill.Search;
using Bluepill.Storage.StorageTypes;
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
    public class BoxStorage : IBoxStorage
    {
        private IStorageContext _context;
        private IQueryBuilder _queryBuilder;
        
        public BoxStorage(IQueryBuilder queryBuilder, IStorageContext context)
        {
            _context = context;
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
            var collection = _context.GetCollection(collectionName);

            var box = collection.FindAs<Box>(query).SetFields(new[] { Fields.GRIDFS_ID, Fields.IS_LARGE }).FirstOrDefault();

            if (box.IsLarge)
            {
                _context.GridFSDelete(box.GridFSId);
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
                using(var ms = new MemoryStream(box.Bytes))
                {
                    var gridFSItem = _context.GridFSUpload(ms);
                    box.GridFSId = gridFSItem.Id;
                    box.Bytes = null;
                }
            }

            var collection = _context.GetCollection(box.UserId);

            //make sure indexes on the searchable fields
            foreach (var item in box.MetaData)
            {
                collection.EnsureIndex(item.Name);
            }

            //make sure unique index on image hash (prevents duplicate images in db)
            collection.EnsureIndex(new IndexKeysBuilder().Ascending(Fields.HASH), IndexOptions.SetUnique(true));

            collection.Insert(box);
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
            var collection = _context.GetCollection(collectionName);
            
            var cursor = collection.FindAs<Box>(query).SetFields(fields);
            var box = cursor.FirstOrDefault();
            var retrieval = new Retrieval();
            if (box.IsLarge)
            {
                var file = _context.Database.GridFS.FindOneById(box.GridFSId);

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

            var query = _queryBuilder.Build(facets);
            var results = new List<Box>();
            var collection = _context.GetCollection(collectionName);

            var cursor = (query == null) ? collection.FindAllAs<Box>().SetFields(fields) : collection.FindAs<Box>(query).SetFields(fields); 

            cursor.Limit = perPage - 1;
            cursor.Skip = (page - 1) * perPage;

            return new Retrieval { Boxes = cursor.ToList(), Total = cursor.Count() };
        }
    }
}

