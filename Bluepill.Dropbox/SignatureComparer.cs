using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Dropbox
{
    public class SignatureComparer : IComparer<Tuple<string, string, string>>
    {
        public int Compare(Tuple<string, string, string> x, Tuple<string, string, string> y)
        {
            if (x.Item2 == y.Item2)
            {
                return string.CompareOrdinal(x.Item3, y.Item3);
            }

            return string.CompareOrdinal(x.Item2, y.Item2);
        }
    }
}
