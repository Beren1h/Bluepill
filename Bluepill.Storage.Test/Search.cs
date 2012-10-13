using Bluepill.Search;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace Bluepill.Storage.Test
{
    [TestFixture]
    public class Search
    {
        [SetUp]
        public void SetUp() { }

        [TearDown]
        public void TearDown() { }

        [Test]
        public void Reader()
        {
            var file = "c:\\solutions\\bluepill\\bluepill.search\\bin\\debug\\user configurations\\me.xml";
            var doc = XDocument.Load(file);
            var values = from v in doc.Root.Elements("facets").Elements("facet").Elements("aspect").Attributes("value") select v;
            var max = (from v in values select int.Parse(v.Value)).Max();

            Assert.AreEqual(0, max, string.Format("max value is {0}", max));
        }
    }
}
