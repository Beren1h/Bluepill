using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Framework
{
    public interface IBluePillUserStore
    {
        BluePillUser GetUser(string userName);
    }
}