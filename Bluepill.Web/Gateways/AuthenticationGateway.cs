using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Bluepill.Web.Framework;

namespace Bluepill.Web.Gateways
{
    public class AuthenticationGateway : IAuthenticationGateway
    {
        //private List<KeyValuePair<string, string>> _credentials;
        private IBluePillUserStore _users;

        public AuthenticationGateway(IBluePillUserStore users)
        {
            //_credentials = new List<KeyValuePair<string, string>>();
            //_credentials = new List<KeyValuePair<string, string>>();
            //_credentials.Add(new KeyValuePair<string, string>("uid", "pwd"));
            _users = users;


        }

        public bool Authenticate(string userName, string password)
        {
            var user = _users.GetUser(userName);

            if (user != null && user.Password == password)
                return true;

            return false;

            //return _credentials.Any(a => a.Key == userName && a.Value == passWord);
        }
    }
}