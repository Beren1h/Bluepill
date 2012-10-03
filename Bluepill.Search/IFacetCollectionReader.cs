using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace Bluepill.Search
{
    public interface IFacetCollectionReader
    {
        IList<FacetCollection> GetFacetCollections(string userName, HttpSessionStateBase session);
    }
}
