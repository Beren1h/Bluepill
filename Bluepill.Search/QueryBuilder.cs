﻿using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Search
{
    public class QueryBuilder : IQueryBuilder
    {
        public IMongoQuery Build(IEnumerable<long> facets)
        {
            var queries = new List<IMongoQuery>();

            //foreach (var facet in facets)
            //{
            //    var aspects = new List<long>();

            //    aspects.AddRange(from a in facet.Aspects where a.IsChecked select a.Value);

            //    if (aspects.Count > 0)
            //    {
            //        queries.Add(Query.In("MetaData." + facet.Name, new BsonArray(aspects)));
            //    }
            //}

            //queries.Add(Query.In("MetaData.facets", new BsonArray(facets)));

            //foreach(var facet in facets)
            //{
            //    queries.Add(Query.And();
            //}

            //queries.Add(Query.And(

            return (queries.Count == 0) ? null : Query.And(queries);
            
        }

        public IMongoQuery Build(ObjectId id)
        {
            return Query.EQ("_id", id);
        }
    }
}
