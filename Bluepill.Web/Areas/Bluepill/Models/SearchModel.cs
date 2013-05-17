using Bluepill.Search;
using System.Collections.Generic;

namespace Bluepill.Web.Areas.Administration.Models
{
    public class SearchModel
    {
        public IEnumerable<Facet> Facets { get; set; }
        public int Page { get; set; }
        public int PageModifier { get; set; }
        public int MaxIndex { get; set; }
        public double TotalPages { get; set; }
        public long TotalBoxes { get; set; }
        public int PageDelta { get; set; }
        public bool IsMobile { get; set; }
    }
}