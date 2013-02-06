using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Dropbox
{
    public interface ISignature
    {
        string GetQueryString(Uri uri, string token, string tokenSecret, string method = "GET");
        string PercentEncode(string value);
    }
}
