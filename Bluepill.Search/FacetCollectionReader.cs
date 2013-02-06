using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Bluepill.Search
{
    public class FacetCollectionReader : IFacetCollectionReader
    {
        private IConfigurationReader _configurationReader;
        private const string FACET_COLLECTION_CACHE_KEY = "collections";

        private const string COLLECTIONS_ROOT_ELEMENT = "collections";
        private const string COLLECTION_ELEMENT = "collection";
        private const string FACET_ROOT_ELEMENT = "facets";
        private const string FACET_ELEMENT = "facet";
        private const string COLLECTION_NAME_ATTRIBUTE = "name";
        private const string FACET_NAME_ATTRIBUTE = "name";
        private const string ASPECT_NAME_ATTRIBUTE = "name";
        private const string ASPECT_VALUE_ATTRIBUTE = "value";

        public FacetCollectionReader(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        public IList<FacetCollection> GetFacetCollections(string userName, HttpSessionStateBase session)
        {
            return null;

            //var collectionList = (List<FacetCollection>)session[FACET_COLLECTION_CACHE_KEY] ?? new List<FacetCollection>();

            //if (collectionList.Count == 0)
            //{
            //    var config = _configurationReader.GetSearchConfiguration(userName);

            //    var collections = config.Root.Elements(COLLECTIONS_ROOT_ELEMENT);
            //    var facets = config.Root.Elements(FACET_ROOT_ELEMENT);

            //    foreach (var collection in collections.Elements(COLLECTION_ELEMENT))
            //    {
            //        var collectionName = collection.Attribute(COLLECTION_NAME_ATTRIBUTE).Value;
            //        var list = new List<Facet>();

            //        foreach (var collectionFacet in collection.Descendants(FACET_ELEMENT))
            //        {
            //            var facet = facets.Elements(FACET_ELEMENT).Where(f => f.Attribute(FACET_NAME_ATTRIBUTE).Value == collectionFacet.Attribute(FACET_NAME_ATTRIBUTE).Value).FirstOrDefault();

            //            var facetName = facet.Attribute(FACET_NAME_ATTRIBUTE).Value;

            //            var aspects = from element in facet.Descendants()
            //                          select new
            //                          {
            //                              name = element.Attribute(ASPECT_NAME_ATTRIBUTE).Value,
            //                              value = element.Attribute(ASPECT_VALUE_ATTRIBUTE).Value,
            //                          };

            //            list.Add(new Facet
            //            {
            //                Name = facetName,
            //                Aspects = from a in aspects orderby a.name select new Aspect { Name = a.name, Value = long.Parse(a.value), FacetName = facetName }
            //            });

                        
            //        }

            //        collectionList.Add(new FacetCollection { Name = collectionName, Facets = list });
                    
            //    }

            //    session.Add(FACET_COLLECTION_CACHE_KEY, collectionList);
            //}

            //return collectionList;

        }

        //public IList<FacetCollection> GetFacets(string userName, HttpSessionStateBase session)
        //{
        //    var collectionList = (List<FacetCollection>)session[FACET_CACHE_KEY] ?? new List<FacetCollection>();
            
        //    if (collectionList.Count == 0)
        //    {
        //        var config = _reader.GetSection(userName, ConfigurationSection.COLLECTIONS);
        //        var facets = _reader.GetSection(userName, ConfigurationSection.FACETS);

        //        foreach (var collection in config.Descendants("collection"))
        //        {
        //            var collectionName = collection.Attribute("name").Value;

        //            foreach (var collectionFacet in collection.Descendants("facet"))
        //            {
        //                var list = new List<Facet>();

        //                foreach (var facet in facets.Descendants())
        //                {
        //                    if (facet.Attribute("name").Value == collectionFacet.Attribute("name").Value)
        //                    {
        //                        var facetName = facet.Attribute("name").Value;


        //                        var aspects = from element in facet.Descendants()
        //                                      select new
        //                                      {
        //                                          name = element.Attribute("name").Value,
        //                                          value = element.Attribute("value").Value,
        //                                      };

        //                        list.Add(new Facet
        //                        {
        //                            Name = facetName,
        //                            Aspects = from a in aspects select new Aspect { Name = a.name, Value = long.Parse(a.value), FacetName = facetName }
        //                        });

        //                    }
        //                }

        //                collectionList.Add(new FacetCollection { Name = collectionName, Facets = list });
        //            }
        //        }
        //    }

        //    return collectionList;

        //}











    }
}
