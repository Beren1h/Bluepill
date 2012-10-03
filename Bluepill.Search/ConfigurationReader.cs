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

            //try
            //{
            var doc = XDocument.Load(xmlFile);

            var schema = new XmlSchemaSet();
            schema.Add("", schemaFile);

            doc.Validate(schema, (sender, e) =>
            {
                //just get out at the first sign of trouble.  This won't capture multiple validation
                //errors but just provides an indication that your xml is messed up.
                throw new XmlSchemaException(e.Message);
            });

            //config = from c in doc.Root.Elements(section.ToString()) select c;
            //config = from c in doc.Root.Elements() select c;

            //}
            //catch (Exception ex)
            //{
            //    //for whatever reason we couldn't get the store elements
            //    //log the error and we'll return an empty list
            //    //_logger.LogException(ex.Message, ex);
            //}

            //return config;

            return doc;

        }

        public IEnumerable<XElement> GetSection(string userName, ConfigurationSection section)
        {
            var xmlFile = string.Format("{0}/bin/user configurations/{1}.xml", AppDomain.CurrentDomain.BaseDirectory, userName);
            var schemaFile = string.Format("{0}/bin/user configurations/configuration.xsd", AppDomain.CurrentDomain.BaseDirectory);

            IEnumerable<XElement> config = new List<XElement>();

            //try
            //{
            var doc = XDocument.Load(xmlFile);

            var schema = new XmlSchemaSet();
            schema.Add("", schemaFile);

            doc.Validate(schema, (sender, e) =>
            {
                //just get out at the first sign of trouble.  This won't capture multiple validation
                //errors but just provides an indication that your xml is messed up.
                throw new XmlSchemaException(e.Message);
            });

            //config = from c in doc.Root.Elements(section.ToString()) select c;
            config = from c in doc.Root.Elements() select c;

            //}
            //catch (Exception ex)
            //{
            //    //for whatever reason we couldn't get the store elements
            //    //log the error and we'll return an empty list
            //    //_logger.LogException(ex.Message, ex);
            //}

            return config;

        }

        //public IEnumerable<XElement> GetCollections(string userName)
        //{
        //}
    }
}
