using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Dropbox
{
    public interface IApiRequestParameters
    {
        string GetParameters(Uri uri, string token, string tokenSecret);
        string GetParameters2(Uri uri, string token, string tokenSecret);

    }
}
