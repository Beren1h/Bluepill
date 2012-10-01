using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Framework
{
    public class BluePillUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Collections { get; set; }
    }
}