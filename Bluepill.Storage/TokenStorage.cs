using Bluepill.Storage.StorageTypes;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public class TokenStorage : ITokenStorage
    {
        private IStorageContext _context;

        public TokenStorage(IStorageContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        public void AddToken(Token token)
        {
            var collection = _context.Database.GetCollection<Token>(Fields.TOKEN_COLLECTION);
            collection.EnsureIndex(new IndexKeysBuilder().Ascending(Fields.TOKEN_USER_ID), IndexOptions.SetUnique(true));
            collection.Insert(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Token GetToken(string user)
        {
            var query = Query.EQ(Fields.TOKEN_USER_ID, user);
            var collection = _context.Database.GetCollection<Token>(Fields.TOKEN_COLLECTION);
            return collection.FindAs<Token>(query).FirstOrDefault();
        }

    }
}
