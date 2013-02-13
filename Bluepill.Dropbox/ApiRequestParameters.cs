using Bluepill.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bluepill.Dropbox
{
    public class ApiRequestParameters : IApiRequestParameters
    {
        private Random _random;
        //private const string _appKey = "o4k9zx12fv6pzl3";
        //private const string _appSecret = "qkkwupwbdhr0wla";
        private ITokenStorage _storage;

        public ApiRequestParameters(ITokenStorage storage)
        {
            _random = new Random();
            _storage = storage;
        }

        public string GetParameters2(Uri uri, string token, string tokenSecret)
        {
            //var nonce = _random.Next(123400, 9999999).ToString();
            //var timestamp = GetTimestamp();

            var nonce = "2194858";
            var timestamp = "1359515166";


            var app = _storage.GetToken("bluepill");
            //var hash = new HMACSHA1() { Key = GetHashKey(_appSecret, tokenSecret) };
            var hash = new HMACSHA1() { Key = GetHashKey(app.Secret, tokenSecret) };

            var parameters = new StringBuilder();
            //parameters.AppendFormat("oauth_consumer_key={0}", _appKey);
            parameters.AppendFormat("oauth_consumer_key={0}", app.Value);

            if (!string.IsNullOrEmpty(token))
            {
                parameters.AppendFormat("&oauth_token={0}", token);
            }

            parameters.AppendFormat("&oauth_nonce={0}", nonce);
            parameters.AppendFormat("&oauth_timestamp={0}", timestamp);
            parameters.Append("&oauth_signature_method=HMAC-SHA1");


            parameters.Append("&oauth_version=1.0");

            var signature = new StringBuilder();
            signature.Append("GET");
            signature.Append("&" + UrlEncode(uri.ToString()));
            signature.Append("&" + UrlEncode(parameters.ToString()));

            byte[] dataBuffer = Encoding.ASCII.GetBytes(signature.ToString());
            byte[] hashBytes = hash.ComputeHash(dataBuffer);

            var sig = UrlEncode(Convert.ToBase64String(hashBytes));

            parameters.AppendFormat("&oauth_signature={0}", HttpUtility.UrlEncode(Convert.ToBase64String(hashBytes)));

            return parameters.ToString();
        }

        public string GetParameters(Uri uri, string token, string tokenSecret)
        {
            var nonce = _random.Next(123400, 9999999).ToString();
            var timestamp = GetTimestamp();
            var app = _storage.GetToken("bluepill");
            //var hash = new HMACSHA1() { Key = GetHashKey(_appSecret, tokenSecret) };
            var hash = new HMACSHA1() { Key = GetHashKey(app.Secret, tokenSecret) };

            var parameters = new StringBuilder();
            //parameters.AppendFormat("oauth_consumer_key={0}", _appKey);
            parameters.AppendFormat("oauth_consumer_key={0}", app.Value);
            parameters.AppendFormat("&oauth_nonce={0}", nonce);
            parameters.Append("&oauth_signature_method=HMAC-SHA1");
            parameters.AppendFormat("&oauth_timestamp={0}", timestamp);

            if (!string.IsNullOrEmpty(token))
            {
                parameters.AppendFormat("&oauth_token={0}", token);
            }

            parameters.Append("&oauth_version=1.0");

            var signature = new StringBuilder();
            signature.Append("GET");
            signature.Append("&" + UrlEncode(uri.ToString()));
            signature.Append("&" + UrlEncode(parameters.ToString()));

            byte[] dataBuffer = Encoding.ASCII.GetBytes(signature.ToString());
            byte[] hashBytes = hash.ComputeHash(dataBuffer);

            parameters.AppendFormat("&oauth_signature={0}", HttpUtility.UrlEncode(Convert.ToBase64String(hashBytes)));

            return parameters.ToString();
        }

        private string GetTimestamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public byte[] GetHashKey(string appSecret, string tokenSecret)
        {
            var token = (string.IsNullOrEmpty(tokenSecret)) ? "" : UrlEncode(tokenSecret);
            var key = string.Format("{0}&{1}", UrlEncode(appSecret), token);

            return Encoding.ASCII.GetBytes(key);
        }

        private string UrlEncode(string value)
        {
            var unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            StringBuilder result = new StringBuilder();

            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
                }
            }
            return result.ToString();
        }


    }
}
