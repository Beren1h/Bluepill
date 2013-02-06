using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Picture
{
    public interface IResize
    {
        Scale DetermineResizeScale(int sourceWidth, int sourceHeight, int resizedWidth, int resizedHeight);

        byte[] CreateResizedPicture(dynamic pic, Scale scale);
    }
}
