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
        //private BluepillUserStorage _gateway;
        private IBluepillUserStorage _storage;

        public AuthenticationGateway(IBluepillUserStorage storage)
        {
            //_gateway = null; //new BluepillUserStorage(new StorageContext());
            _storage = storage;
        }

        public bool Authenticate(string userName, string password)
        {
            var user = _storage.GetUser(userName);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Hash))
                return true;

            return false;
        }
    }
}