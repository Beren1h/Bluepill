using MongoDB.Bson;
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
        public IList<IMongoQuery> Build(IEnumerable<Facet> facets)
        {
            //var queries = new List<IMongoQuery>{ Query.EQ("UserId", userName) };
            var queries = new List<IMongoQuery>();

            foreach (var facet in facets)
            {
                var aspects = new List<long>();

                aspects.AddRange(from a in facet.Aspects where a.IsChecked select a.Value);

                if (aspects.Count > 0)
                {
                    queries.Add(Query.In("MetaData." + facet.Name, new BsonArray(aspects)));
                }
            }

            return queries;
            
        }
    }
}
