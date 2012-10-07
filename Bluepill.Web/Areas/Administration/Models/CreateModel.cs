using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Areas.Administration.Models
{
    public class CreateModel
    {
        public IList<Facet> Facets { get; set; }
        public int TotalFileCount { get; set; }
        public string File { get; set; }
    }
}