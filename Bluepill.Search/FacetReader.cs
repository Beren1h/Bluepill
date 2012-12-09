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

        public IEnumerable<Facet> Read(string userName)
        {
            var list = new List<Facet>();
            var file = string.Format("{0}/bin/user configurations/{1}.xml", AppDomain.CurrentDomain.BaseDirectory, userName);
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
                            Id = facet.Attribute("id").Value,
                            Name = facet.Attribute("name").Value,
                            Top = (facet.Attribute("top") != null && facet.Attribute("top").Value == "yes"),
                            Aspects = from aspect in aspects orderby aspect.text select new Aspect 
                            { 
                                Value = aspect.value, 
                                Text = aspect.text, 
                                Facet = aspect.facet
                            }
                        });
            }

            return list;
        }

        //public XDocument Read(string name)
        //{
        //    return new XDocument();
        //}

        //public IEnumerable<Facet> BuildFacets(string userName)
        //{
        //    var list = new List<Facet>();
        //    var file = string.Format("{0}/bin/user configurations/{1}.xml", AppDomain.CurrentDomain.BaseDirectory, userName);

        //    var doc = XDocument.Load(file);

            
        //    //if (pointers == null)
        //    var pointers = doc.Root.Element("pointers").Elements("pointer");
        //    var facets = doc.Root.Element("facets").Elements("facet");

        //    return GetPointers(pointers, facets);
        //    //foreach (var pointer in pointers)
        //    //{
        //    //    var descendants = pointer.Elements("pointer");
        //    //    var facet = GetFacet(facets, pointer.Attribute("facetId").Value);

        //    //    facet.Display = (pointer.Attribute("display") != null) ? pointer.Attribute("display").Value : facet.Display;

        //    //    facet.Descendants = BuildFacets(userName, descendants);
        //    //    list.Add(facet);
        //    //};

        //    //return list;
        //}


        //public IEnumerable<Facet> GetPointers(IEnumerable<XElement> pointers, IEnumerable<XElement> facets)
        //{
        //    var list = new List<Facet>();
        //    //var file = string.Format("{0}me.xml", AppDomain.CurrentDomain.BaseDirectory);

        //    //var doc = XDocument.Load(file);

        //    //if (pointers == null)
        //    //    pointers = doc.Root.Element("pointers").Elements("pointer");

        //    //var facets = doc.Root.Element("facets").Elements("facet");

        //    foreach (var pointer in pointers)
        //    {
        //        var descendants = pointer.Elements("pointer");
        //        var facet = GetFacet(facets, pointer.Attribute("facetId").Value);

        //        facet.Display = (pointer.Attribute("display") != null) ? pointer.Attribute("display").Value : facet.Display;

        //        facet.Descendants = GetPointers(descendants, facets);
        //        list.Add(facet);
        //    };

        //    return list;
        //}

        //private Facet GetFacet(IEnumerable<XElement> facets, string id)
        //{
        //    //(from f in doc.Root.Element("facets").Elements("facet") where (f.Attribute("id") == pointer.Attribute("facetId")) select f).FirstOrDefault();

        //    var facet = (from f in facets where (f.Attribute("id").Value == id) select f).FirstOrDefault();

        //    var aspects = from aspect in facet.Elements("aspect")
        //                  select new
        //                  {
        //                      text = aspect.Attribute("text").Value,
        //                      value = aspect.Attribute("value").Value,
        //                  };

        //    return new Facet
        //    {
        //        Display = facet.Attribute("default-display").Value,
        //        Id = facet.Attribute("id").Value,
        //        Aspects = from aspect in aspects orderby aspect.text select Tuple.Create(aspect.text, aspect.value, false),
        //        Descendants = new List<Facet>()
        //    };
        //}

        //public IEnumerable<Facet> GetFacet(string name)
        //{
        //    return null;
        //}

        //public IEnumerable<Facet> GetFacet(long value)
        //{
        //    return null;
        //}


        //private Facet CreateFacet(XElement element)
        //{
        //    return new Facet { 
        //                        Name = element.Attribute("name").Value, 
        //                        Value = long.Parse(element.Attribute("value").Value),
        //                        Children = (from child in element.Elements() orderby child.Attribute("name").Value select CreateFacet(child)).ToList()
        //                     };
        //}





    }
}
