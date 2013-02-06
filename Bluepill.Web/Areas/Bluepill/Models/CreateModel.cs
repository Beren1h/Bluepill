using Bluepill.Search;
using System.Collections.Generic;

namespace Bluepill.Web.Areas.Administration.Models
{
    public class CreateModel
    {
        public IEnumerable<Facet> Facets { get; set; }
        public int TotalFileCount { get; set; }
        public string File { get; set; }
        public int ResizedWidth { get; set; }
        public int ResizedHeight { get; set; }
        public string Url { get; set; }
    }
}