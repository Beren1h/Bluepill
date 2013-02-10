using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public class UserGateway
    {
        private MongoServer _server;
        private MongoDatabase _database;
        private MongoCollection<BluepillUser> _collection;

        private const string CONNECTION = "mongodb://localhost";
        private const string DATABASE = "bluepill";
        private const string USERS_COLLECTION = "users";
        

        public UserGateway()
        {
            _server = MongoServer.Create(CONNECTION);
            _database = _server.GetDatabase(DATABASE);
            _collection = _database.GetCollection<BluepillUser>(USERS_COLLECTION);
        }

        public void SaveUser(BluepillUser user)
        {
            _collection.Insert(user);
        }

        public BluepillUser GetUser(string uid)
        {
            var query = Query.EQ(Fields.BLUEPILL_USER_ID, uid);
            return _collection.FindAs<BluepillUser>(query).FirstOrDefault();
        }

    }
}
