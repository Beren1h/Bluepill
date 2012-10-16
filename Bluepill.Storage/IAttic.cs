using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public interface IAttic
    {
        void AddBox(Box box);
        //IList<Box> GetBoxes(IList<Facet> facets, string userName, int perPage, int startIndex, string[] fields = null);

        void Empty();
    }
}
