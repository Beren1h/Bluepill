using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Areas.Administration.Models
{
    public class SearchModel
    {
        public IList<Facet> Facets { get; set; }
    }
}