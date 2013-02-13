using Bluepill.Storage;
using Bluepill.Storage.StorageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bluepill.Dropbox
{
    public class Signature : ISignature
    {
        private ITokenStorage _storage;
        private Random _random;
        private Token _app;
        private const string UNRESERVED_CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        public Signature(ITokenStorage storage)
        {
            _storage = storage;
            _random = new Random();
            _app = _storage.GetToken("bluepill");
        }

        public string GetQueryString(Uri uri, string token, string tokenSecret, string method = "GET")
        {
            var hash = new HMACSHA1() { Key = GetHashKey(_app.Secret, tokenSecret) };
            var parameters = InitializeQueryString(uri, token);

            var signature = new StringBuilder();
            signature.Append(method);
            signature.Append("&" + WebUtility.UrlEncode(string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, uri.AbsolutePath)));
            signature.Append("&" + WebUtility.UrlEncode(parameters.ToString()));

            byte[] dataBuffer = Encoding.ASCII.GetBytes(signature.ToString());
            byte[] hashBytes = hash.ComputeHash(dataBuffer);

            parameters.AppendFormat("&oauth_signature={0}", HttpUtility.UrlEncode(Convert.ToBase64String(hashBytes)));

            return parameters.ToString();
        }


        private string ParameterEncode(string parameter)
        {
            if (parameter.Substring(0, 1) == "\\")
            {
                var encodedFilename = PercentEncode(parameter.Substring(1, parameter.Length - 1));
                return WebUtility.UrlEncode(parameter.Substring(0, 1) + encodedFilename);
            }

            return PercentEncode(parameter);
        }

        public string PercentEncode(string value)
        {
            var input = new StringBuilder();
            foreach (char symbol in value)
            {
                if (UNRESERVED_CHARACTERS.IndexOf(symbol) != -1)
                {
                    input.Append(symbol);
                }
                else
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(symbol.ToString());
                    foreach (byte b in bytes)
                    {
                        input.AppendFormat("%{0:X2}", b);
                    }
                }
            }

            return input.ToString();
        }

        //public string UpperCaseUrlEncode(string s)
        //{
        //    char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
        //    for (int i = 0; i < temp.Length - 2; i++)
        //    {
        //        if (temp[i] == '%')
        //        {
        //            temp[i + 1] = char.ToUpper(temp[i + 1]);
        //            temp[i + 2] = char.ToUpper(temp[i + 2]);
        //        }
        //    }
        //    return new string(temp);
        //}

        private StringBuilder InitializeQueryString(Uri uri, string token)
        {
            //var nonce = "2194858";
            //var timestamp = "1359515166";
            var nonce = _random.Next(123400, 9999999).ToString();
            var timestamp = GetTimestamp();

            var list = new List<Tuple<string, string, string>>();

            list.Add(Tuple.Create("", "oauth_consumer_key", _app.Value));
            list.Add(Tuple.Create("&", "oauth_nonce", nonce));
            list.Add(Tuple.Create("&", "oauth_signature_method", "HMAC-SHA1"));
            list.Add(Tuple.Create("&", "oauth_timestamp", timestamp));

            if (!string.IsNullOrEmpty(token))
            {
                list.Add(Tuple.Create("&", "oauth_token", token));
            }

            list.Add(Tuple.Create("&", "oauth_version", "1.0"));

            var collection = HttpUtility.ParseQueryString(uri.Query);
            foreach (var key in collection.AllKeys)
            {
                list.Add(Tuple.Create<string, string, string>("&", key, ParameterEncode(collection[key])));
            }

            list.Sort(new SignatureComparer());

            return ConvertToQueryString(list);
        }

        private string GetTimestamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        private byte[] GetHashKey(string appSecret, string tokenSecret)
        {
            var token = (string.IsNullOrEmpty(tokenSecret)) ? "" : WebUtility.UrlEncode(tokenSecret);
            var key = string.Format("{0}&{1}", WebUtility.UrlEncode(appSecret), token);

            return Encoding.ASCII.GetBytes(key);
        }

        private StringBuilder ConvertToQueryString(List<Tuple<string, string, string>> list)
        {
            var sb = new StringBuilder();

            foreach (var item in list)
            {
                sb.AppendFormat("{0}{1}={2}", item.Item1, item.Item2, item.Item3);
            }

            return sb;
        }

    }
}
