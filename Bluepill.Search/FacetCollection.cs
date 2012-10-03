using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Search
{
    public class FacetCollection
    {
        public string Name { get; set; }
        public IList<Facet> Facets { get; set; }
    }
}
