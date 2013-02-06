using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Areas.Application.Models
{
    public class DropboxModel
    {
        public string AuthorizationToken { get; set; }
        public string AuthorizationSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessSecret { get; set; }
        public string AuthorizationUrl { get; set; }
    }
}