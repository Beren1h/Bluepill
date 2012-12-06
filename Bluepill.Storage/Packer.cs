using Bluepill.Picture;
using Bluepill.Search;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bluepill.Storage
{
    public class Packer : IPacker
    {
        private IResize _resize;

        public Packer(IResize resize)
        {
            _resize = resize;
        }

        public Box PackBox(string file, string userName, IEnumerable<long> facets)
        {
            var box = new Box();
            var metadata = new BsonDocument();

            //foreach (var facet in facets)
            //{
            //    var aspectValues = new List<long>();
            //    aspectValues.AddRange(from aspect in facet.Aspects where aspect.IsChecked select aspect.Value);

            //    if (aspectValues.Count > 0)
            //        metadata.Add(facet.Name, new BsonArray(aspectValues));

            //}

            //metadata.Add("facets", new BsonArray((from v in facets select v.Value).ToList()));
            metadata.Add("facets", new BsonArray(facets));

            box.MetaData = metadata;
            box.UserId = userName;
            
            using(var source = new Bitmap(file))
            {
                using (var ms = new MemoryStream())
                {
                    source.Save(ms, ImageFormat.Png);
                    box.Bytes = ms.ToArray();
                }

                var reducedScale = _resize.DetermineResizeScale(source.Width, source.Height, 200, 200);

                box.ReducedBytes = _resize.CreateResizedPicture(file, reducedScale);

                box.ReducedBytesWidth = reducedScale.Width;
                box.ReducedBytesHeight = reducedScale.Height;
            }

            if (box.Bytes.Length > 16777216)
            {
                box.Bytes = null;
                box.IsLarge = true;
                box.file = file;
            }

            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                box.Hash = Convert.ToBase64String(sha1.ComputeHash(box.ReducedBytes));
            }

            return box;
            
            

        }
    }
}

