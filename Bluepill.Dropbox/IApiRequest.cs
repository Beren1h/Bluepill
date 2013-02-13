using Bluepill.Storage;
using Bluepill.Storage.StorageTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Dropbox
{
    public interface IApiRequest
    {
        Task<Dictionary<string, string>> RequestAuthorizationToken();
        Task<Dictionary<string, string>> RequestAccessToken(string authorizationToken, string authorizationSecret);
        Task<JObject> GetMetaData(Token token);
        Task<JObject> GetMedia(Token token, string path);
        Task<JObject> Delete(Token token, string path);
        Task<HttpResponseMessage> Upload(Token token, string path, byte[] file);
        string GetAuthorizationUrl(string authorizationToken, string authorizationSecret);
    }
}
