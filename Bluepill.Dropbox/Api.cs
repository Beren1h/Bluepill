using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Dropbox
{
    public static class Api
    {
        public const string REQUEST_TOKEN = "https://api.dropbox.com/1/oauth/request_token";

        public const string AUTHORIZATION_URL = "https://www.dropbox.com/1/oauth/authorize?oauth_token={0}";

        public const string ACCESS_TOKEN = "https://api.dropbox.com/1/oauth/access_token";

        public const string METADATA = "https://api.dropbox.com/1/metadata/sandbox/";

        public const string MEDIA = "https://api.dropbox.com/1/media/sandbox{0}";

        public const string DELETE = "https://api.dropbox.com/1/fileops/delete?root=sandbox&path={0}";

        public const string UPLOAD = "https://api-content.dropbox.com/1/files_put/sandbox/{0}";
    }
}
