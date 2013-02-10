using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Bluepill.Web.Framework;
using Bluepill.Storage;
using BCrypt.Net;

namespace Bluepill.Web.Gateways
{
    public class AuthenticationGateway : IAuthenticationGateway
    {
        //private List<KeyValuePair<string, string>> _credentials;
        //private IBluePillUserStore _users;
        private UserGateway _gateway;

        //public AuthenticationGateway(IBluePillUserStore users)
        public AuthenticationGateway()
        {
            //_credentials = new List<KeyValuePair<string, string>>();
            //_credentials = new List<KeyValuePair<string, string>>();
            //_credentials.Add(new KeyValuePair<string, string>("uid", "pwd"));
            //_users = users;
            _gateway = new UserGateway();


        }

        public bool Authenticate(string userName, string password)
        {
            var user = _gateway.GetUser(userName);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Hash))
                return true;

            return false;

            //var user = _users.GetUser(userName);


            //if (user != null && user.Password == password)
            //    return true;

            //return false;

            //return _credentials.Any(a => a.Key == userName && a.Value == passWord);
        }
    }
}