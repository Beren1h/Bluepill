using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Areas.Application.Models
{
    public class SearchInterfaceModel
    {
        public IList<Facet> Facets { get; set; }
        public int TotalFileCount { get; set; }
        public string File { get; set; }
        //public ImageAttributes ImageAttributes { get; set; }
        public int ResizedWidth { get; set; }
        public int ResizedHeight { get; set; }
    }
}