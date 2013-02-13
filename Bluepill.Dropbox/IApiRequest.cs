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
        Task<Dictionary<string, string>> GetToken(Uri uri, string token, string secret);
        Task<JObject> GetMetaData(Token token);
        Task<JObject> GetMedia(Token token, string path);
        Task<JObject> Delete(Token token, string path);
        Task<HttpResponseMessage> Upload(Token token, string path, byte[] file);
    }
}
