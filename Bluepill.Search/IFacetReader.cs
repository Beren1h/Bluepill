using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bluepill.Search
{
    public interface IFacetReader
    {
        XDocument Read(string name);
        IEnumerable<Facet> BuildFacets(string userName);
        //IEnumerable<XElement> GetFacet(string name);
        //IEnumerable<XElement> GetFacet(long value);
    }
}
