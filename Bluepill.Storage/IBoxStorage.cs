//using Bluepill.Dropbox;
using Bluepill.Search;
using Bluepill.Storage.StorageTypes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public interface IBoxStorage
    {
        void AddBox(Box box);
        Retrieval GetBoxes(IEnumerable<Facet> facets, int perPage, int page, string collectionName, string[] fields = null);
        Retrieval GetBox(ObjectId id, string collectionName, string[] fields = null);
        void RemoveBox(ObjectId id, string collectionName);
    }
}
