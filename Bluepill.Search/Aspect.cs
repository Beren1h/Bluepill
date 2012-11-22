using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Search
{
    public class Aspect
    {
        public string Name { get; set; }
        public long Value { get; set; }
        public string FacetName { get; set; }
        public bool IsChecked { get; set; }
    }
}
