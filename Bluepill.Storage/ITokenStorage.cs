using Bluepill.Storage.StorageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public interface ITokenStorage
    {
        void AddToken(Token token);
        Token GetToken(string user);
    }
}
