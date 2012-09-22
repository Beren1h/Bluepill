using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Application.Models
{
    public class SettingsModel
    {
        public string DefaultCollection { get; set; }
        public List<string> Collections { get; set; }
        public SelectList CollectionsDropDown { get; set; }
        public string UserName { get; set; }
    }
}