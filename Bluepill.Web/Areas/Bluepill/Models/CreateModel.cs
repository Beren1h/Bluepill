using Bluepill.Search;
using Bluepill.Web.Areas.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Areas.Administration.Models
{
    public class CreateModel
    {
        public IEnumerable<Facet> Facets { get; set; }
        public int TotalFileCount { get; set; }
        public string File { get; set; }
        public int ResizedWidth { get; set; }
        public int ResizedHeight { get; set; }
        public IEnumerable<long> Selects { get; set; }

        //public List<long> Facets { get; set; }
        //public int TotalFileCount { get; set; }
        //public string File { get; set; }
        //public int ResizedWidth { get; set; }
        //public int ResizedHeight { get; set; }

    }
}