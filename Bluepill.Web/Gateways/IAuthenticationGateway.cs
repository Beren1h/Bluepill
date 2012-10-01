using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Gateways
{
    public interface IAuthenticationGateway
    {
        bool Authenticate(string userName, string passWord);
    }
}