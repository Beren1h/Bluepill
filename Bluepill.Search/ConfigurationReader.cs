using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Bluepill.Search
{
    public class ConfigurationReader : IConfigurationReader
    {
        public XDocument GetSearchConfiguration(string userName)
        {
            var xmlFile = string.Format("{0}/bin/user configurations/{1}.xml", AppDomain.CurrentDomain.BaseDirectory, userName);
            var schemaFile = string.Format("{0}/bin/user configurations/configuration.xsd", AppDomain.CurrentDomain.BaseDirectory);

            IEnumerable<XElement> config = new List<XElement>();
            var doc = XDocument.Load(xmlFile);

            var schema = new XmlSchemaSet();
            schema.Add("", schemaFile);

            doc.Validate(schema, (sender, e) =>
            {
                //just get out at the first sign of trouble.  This won't capture multiple validation
                //errors but just provides an indication that your xml is messed up.
                throw new XmlSchemaException(e.Message);
            });

            return doc;

        }
    }
}
