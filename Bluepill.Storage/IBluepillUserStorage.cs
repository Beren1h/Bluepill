using Bluepill.Storage.StorageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public interface IBluepillUserStorage
    {
        void SaveUser(BluepillUser user);
        BluepillUser GetUser(string uid);
    }
}
