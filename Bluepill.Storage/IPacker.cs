using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public interface IPacker
    {
        Box PackBox(string file, string userName, IList<Facet> factes);
    }
}
