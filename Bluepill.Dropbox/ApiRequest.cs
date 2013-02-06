using Bluepill.Storage;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Dropbox
{
    public class ApiRequest : IApiRequest
    {
        private ISignature _signature;
        private HttpClient _client;

        private const string METADATA = "https://api.dropbox.com/1/metadata/sandbox/";
        private const string MEDIA = "https://api.dropbox.com/1/media/sandbox/{0}";
        private const string DELETE = "https://api.dropbox.com/1/fileops/delete?root=sandbox&path={0}";
        private const string UPLOAD = "https://api-content.dropbox.com/1/files_put/sandbox/{0}";

        private const string REQUEST_TOKEN = "https://api.dropbox.com/1/oauth/request_token";
        private const string AUTHORIZATION_URL = "https://www.dropbox.com/1/oauth/authorize?oauth_token={0}";
        private const string ACCESS_TOKEN = "https://api.dropbox.com/1/oauth/access_token";

        public ApiRequest(ISignature signature)
        {
            _signature = signature;
            _client = new HttpClient();
        }

        public async Task<Dictionary<string, string>> GetToken(Uri uri, string token, string secret)
        {
            var client = new HttpClient();
            var parameters = _signature.GetQueryString(uri, token, secret);
            var request = string.Format("{0}?{1}", uri, parameters);
            var response = await client.GetAsync(request);
            var dictionary = new Dictionary<string, string>();

            response.EnsureSuccessStatusCode();

            var result = response.Content.ReadAsStringAsync().Result;

            foreach (var item in result.Split('&'))
            {
                var kvp = item.Split('=');
                dictionary.Add(kvp[0], kvp[1]);
            }

            return dictionary;
        }

        public async Task<JObject> GetMetaData(Token token)
        {
            var uri = new Uri(METADATA);
            var querystring = _signature.GetQueryString(uri, token.Value, token.Secret);
            var request = string.Format("{0}?{1}", uri, querystring);
            var response = await _client.GetAsync(request);

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync();

            return JObject.Parse(content.Result);
        }

        public async Task<JObject> GetMedia(Token token, string path)
        {
            var filename = path.Substring(1, path.Length - 1);

            var uri = new Uri(string.Format(MEDIA, _signature.PercentEncode(filename)));
            var querystring = _signature.GetQueryString(uri, token.Value, token.Secret);
            var request = string.Format("{0}?{1}", uri, querystring);
            var response = await _client.GetAsync(request);
            var content = response.Content.ReadAsStringAsync();
            
            return JObject.Parse(content.Result);
        }

        public async Task<JObject> Delete(Token token, string path)
        {
            var uri = new Uri(string.Format(DELETE, path));
            var querystring = _signature.GetQueryString(uri, token.Value, token.Secret);
            var request = string.Format("{0}&{1}", uri, querystring);
            var response = await _client.GetAsync(request);
            var content = response.Content.ReadAsStringAsync();

            return JObject.Parse(content.Result);
        }

        public async Task<HttpResponseMessage> Upload(Token token, string path, byte[] file)
        {
            var uri = new Uri(string.Format(UPLOAD, path));
            var querystring = _signature.GetQueryString(uri, token.Value, token.Secret, "PUT");
            var request = string.Format("{0}?{1}", uri, querystring);
            var content = new ByteArrayContent(file);

            return await _client.PutAsync(request, content);
        }
        //public async Task<JObject> GetMetaData(Uri uri, Token accessToken)
        //{
        //    var querystring = _signature.GetQueryString(uri, accessToken.Value, accessToken.Secret);
        //    var request = string.Format("{0}?{1}", uri, querystring);
        //    var response = await _client.GetAsync(request);

        //    response.EnsureSuccessStatusCode();

        //    var content = response.Content.ReadAsStringAsync();

        //    return JObject.Parse(content.Result);
        //}
    }
}
