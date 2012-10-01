using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Bluepill.Search
{
    public class Facet : IFacet
    {
        public IEnumerable<XElement> GetUserFacets(string userName)
        {
            //Yes, the datasource here is an xml file that is generated from a spreadsheet held by the business users
            var xmlFile = string.Format("{0}/bin/user facets/{1}.xml", AppDomain.CurrentDomain.BaseDirectory, userName);
            var schemaFile = string.Format("{0}/bin/user facets/facet.xsd", AppDomain.CurrentDomain.BaseDirectory);

            IEnumerable<XElement> facets = new List<XElement>();

            try
            {
                var doc = XDocument.Load(xmlFile);

                var schema = new XmlSchemaSet();
                schema.Add("", schemaFile);

                doc.Validate(schema, (sender, e) =>
                {
                    //just get out at the first sign of trouble.  This won't capture multiple validation
                    //errors but just provides an indication that your xml is messed up.
                    throw new XmlSchemaException(e.Message);
                });

                facets = from f in doc.Root.Elements() select f;
            }
            catch (Exception ex)
            {
                //for whatever reason we couldn't get the store elements
                //log the error and we'll return an empty list
                //_logger.LogException(ex.Message, ex);
            }

            return facets;

        }
    }
}
