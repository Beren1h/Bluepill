using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bluepill.Search
{
    public class FacetReader : IFacetReader
    {
        private readonly IConfigurationReader _configurationReader;

        public FacetReader(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        public IEnumerable<Facet> Read(string userName)
        {
            var list = new List<Facet>();
            var file = string.Format("{0}/user configurations/{1}.xml", AppDomain.CurrentDomain.BaseDirectory, userName);
            var doc = XDocument.Load(file);
            var facets = doc.Root.Elements("facet");

            foreach (var facet in facets)
            {
                 var aspects = from aspect in facet.Elements("aspect")
                               select new
                               {
                                   text = aspect.Attribute("text").Value,
                                   value = aspect.Attribute("value").Value,
                                   facet = (aspect.Attribute("facet") !=null ) ? aspect.Attribute("facet").Value : null, 
                               };

                 list.Add(new Facet
                        {
                            Id = long.Parse(facet.Attribute("id").Value),
                            Name = facet.Attribute("name").Value,
                            Top = (facet.Attribute("top") != null && facet.Attribute("top").Value == "yes"),
                            Aspects = from aspect in aspects orderby aspect.text select new Aspect 
                            { 
                                Value = long.Parse(aspect.value), 
                                Text = aspect.text, 
                                Facet = aspect.facet
                            }
                        });
            }

            return list;
        }
    }
}
