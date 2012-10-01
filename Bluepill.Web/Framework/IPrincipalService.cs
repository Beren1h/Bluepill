using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Bluepill.Web.Framework
{
    public interface IPrincipalService
    {
        IPrincipal GetPrincipal();
    }
}