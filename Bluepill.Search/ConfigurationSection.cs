using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Search
{
    public sealed class ConfigurationSection
    {
        private readonly string _section;

        //these string values must match the xml elements in the configuration file
        public static readonly ConfigurationSection FACETS = new ConfigurationSection("facets");
        public static readonly ConfigurationSection COLLECTIONS = new ConfigurationSection("collections");

        private ConfigurationSection(string section)
        {
            _section = section;
        }

        public override string ToString()
        {
            return _section;
        }
    }
}
