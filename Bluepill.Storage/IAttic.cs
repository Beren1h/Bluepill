using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public interface IAttic
    {
        void AddBox(Box box);
        void Empty(string file);
    }
}
