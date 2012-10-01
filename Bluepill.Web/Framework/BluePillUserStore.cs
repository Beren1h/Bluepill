using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Framework
{
    public class BluePillUserStore : IBluePillUserStore
    {
        private Dictionary<string, BluePillUser> _users;

        public BluePillUserStore()
        {
            _users = new Dictionary<string, BluePillUser>();

            _users.Add("uid", new BluePillUser { 
                UserName = "uid", 
                Password = "pwd", 
                Collections = new List<string> {"collection1", "collection2", "collection3", "collection4", "collection5", "collection6", "collection7" } 
            });

        }

        public BluePillUser GetUser(string userName)
        {
            BluePillUser user;

            userName = userName ?? "";

            _users.TryGetValue(userName, out user);

            return user;
        }
    }
}