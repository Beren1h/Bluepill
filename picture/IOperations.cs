using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picture
{
    public interface IOperations
    {
        //byte[] Resize(string file, int width, int height);
        //byte[] Resize(byte[] bytes, int width, int height);
        bool Compare(byte[] image1, byte[] image2);
        Scale GetScale (byte[] bytes);
        byte[] GetBytes(string file);
    }
}
