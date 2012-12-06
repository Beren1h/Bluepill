using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Areas.Administration.Models
{
    public class SearchModel
    {
        public IEnumerable<Facet> Facets { get; set; }
        public int Page { get; set; }
        public int MaxIndex { get; set; }
        public double TotalPages { get; set; }
        public long TotalBoxes { get; set; }
        public int PageDelta { get; set; }
        public IEnumerable<long> Selects { get; set; }
    }
}