using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bluepill.Web.Areas.layout.Models
{
    public class CreateModel
    {
        public string WorkingCollection { get; set; }
        public List<string> Collections { get; set; }
        public SelectList CollectionsDropDown { get; set; }
        public string UserName { get; set; }
    }
}