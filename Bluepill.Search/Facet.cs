using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Search
{
    public class Facet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Top { get; set; }
        //public IEnumerable<Facet> Descendants { get; set; }
        //public IEnumerable<Tuple<string, string, bool>> Aspects { get; set; }
        public IEnumerable<Aspect> Aspects { get; set; }
    }
}
