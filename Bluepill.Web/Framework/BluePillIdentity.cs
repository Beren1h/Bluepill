﻿using Bluepill.Search;
using Bluepill.Storage;
using Bluepill.Storage.StorageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Bluepill.Web.Framework
{
    public class BluePillIdentity : IIdentity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AuthenticationType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Collections { get; set; }

        public Token AccessToken { get; set; }

        public IEnumerable<Facet> Facets { get; set; }

        public bool IsMobile { get; set; }
        
    }
}