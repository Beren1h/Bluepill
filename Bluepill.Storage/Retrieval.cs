using Bluepill.Storage.StorageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public class Retrieval
    {
        public Retrieval()
        {
            Boxes = new List<Box>();
        }

        public long Total { get; set; }
        public IList<Box> Boxes { get; set; }
        public byte[] BigPicutre { get; set; }
    }
}
