using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bluepill.Search
{
    public interface IFacet
    {
        IEnumerable<XElement> GetUserFacets(string userName);
    }
}
