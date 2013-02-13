using Bluepill.Storage.StorageTypes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public class BluepillUserStorage : IBluepillUserStorage
    {
        private MongoCollection<BluepillUser> _collection;
        private IStorageContext _context;

        public BluepillUserStorage(IStorageContext context)
        {
            _context = context;
            _collection = _context.Database.GetCollection<BluepillUser>(Fields.USERS_COLLECTION);
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
