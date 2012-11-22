using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Search
{
    public class Facet
    {
        public string Name { get; set; }
        public IEnumerable<Aspect> Aspects { get; set; }
    }
}
