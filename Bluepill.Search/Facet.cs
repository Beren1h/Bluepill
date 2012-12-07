using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Search
{
    public class Facet
    {
        public string Id { get; set; }
        public string Display { get; set; }
        public bool IsChecked { get; set; }
        public IEnumerable<Facet> Descendants { get; set; }
        public IEnumerable<Tuple<string, string, bool>> Aspects { get; set; }
    }
}
