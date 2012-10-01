using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Gateways
{
    public interface IPictureGateway
    {
        byte[] GetBytes(string file);
        bool Compare(byte[] iamge1, byte[] image2);
        byte[] Resize(string file, int newWidth, int newHeight);
    }
}