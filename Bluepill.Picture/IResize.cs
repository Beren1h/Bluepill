using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Picture
{
    public interface IResize
    {
        Scale DetermineResizeScale(int sourceWidth, int sourceHeight, int resizedWidth, int resizedHeight);
        //byte[] CreateResizedPicture(Bitmap source, Scale scale);
        byte[] CreateResizedPicture(string file, Scale scale);
    }
}
