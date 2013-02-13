using Bluepill.Search;
using Bluepill.Storage.StorageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public interface IBoxPacker
    {
        Box PackBox(byte[] bytes, string userName, IEnumerable<Facet> factes);
    }
}
