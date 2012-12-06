using System;
using System.Collections.Generic;
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

        public XDocument Read(string name)
        {
            return new XDocument();
        }
                
        public IEnumerable<Facet> BuildFacets(string userName)
        {
            var doc = _configurationReader.GetSearchConfiguration(userName);

            //var file = string.Format("{0}/bin/user configurations/test.xml", AppDomain.CurrentDomain.BaseDirectory);

            //var doc = XDocument.Load(file);

            var list = (from element in doc.Root.Elements() orderby element.Attribute("name").Value select CreateFacet(element)).ToList();

            return list;
        }

        //public IEnumerable<Facet> GetFacet(string name)
        //{
        //    return null;
        //}

        //public IEnumerable<Facet> GetFacet(long value)
        //{
        //    return null;
        //}


        private Facet CreateFacet(XElement element)
        {
            return new Facet { 
                                Name = element.Attribute("name").Value, 
                                Value = long.Parse(element.Attribute("value").Value),
                                Children = (from child in element.Elements() orderby child.Attribute("name").Value select CreateFacet(child)).ToList()
                             };
        }





    }
}
