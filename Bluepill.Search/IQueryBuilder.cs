using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Search
{
    public interface IQueryBuilder
    {
        IList<IMongoQuery> Build(IEnumerable<Facet> facets);
        
    }
}
