﻿using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.GridFS;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Picture;
using MongoDB.Driver.Builders;

namespace Storage.Test
{
    [TestFixture]
    public class Mongo
    {
        private const string CONNECTION = "mongodb://localhost";
        private const string DATABASE = "bluepill_test";

        private MongoServer _server;
        private MongoDatabase _database;

        [SetUp]
        public void SetUp() 
        {
            _server = MongoServer.Create(CONNECTION);
            _database = _server.GetDatabase(DATABASE);
        }

        [TearDown]
        public void TearDown() { }


        [Test]
        public void CanInsert()
        {
            var file = "c:\\bluepill\\test_big.jpg";

            var metadata = new BsonDocument{ 
                { "dimension1", new BsonArray(new List<long>{1,2,3}) },  
                { "dimension2", new BsonArray(new List<long>{4,5,6}) }  
            };

            byte[] originalBytes;
            byte[] reducedBytes;
            byte[] comparisonBytes;

            using (var source = new Bitmap(file))
            {
                using (var ms = new MemoryStream())
                {
                    source.Save(ms, ImageFormat.Png);
                    originalBytes = ms.ToArray();
                }

                var resize = new Picture.Resize();
                var reducedScale = resize.DetermineResizeScale(source.Width, source.Height, 200, 200);
                reducedBytes = resize.CreateResizedPicture(file, reducedScale);

                var comparisonScale = resize.DetermineResizeScale(source.Width, source.Height, 50, 50);
                comparisonBytes = resize.CreateResizedPicture(file, comparisonScale);

            }

            //9920438
            //16777216
            //29268747

            var box = new Box { MetaData = metadata, Bytes = originalBytes, ReducedBytes = reducedBytes, ComparisonBytes = comparisonBytes, ReducedBytesHeight = 200, ReducedBytesWidth = 200 };

            var collection = _database.GetCollection<Box>("pictures");

            if (originalBytes.Length < 16777216)
            {
                collection.Insert(box);
            }
            else
            {
                box.Bytes = null;
                box.IsLarge = true;

                using (var stream = new FileStream(file, FileMode.Open))
                {
                    var gridFSItem = _database.GridFS.Upload(stream, file);
                    box.GridFSId = gridFSItem.Id;
                }

                collection.Insert(box);
            }

            byte[] bytes;

            var output = collection.FindOneAs<Box>(Query.EQ("_id", box._id));

            bytes = output.Bytes;

            if (output.IsLarge)
            {
                    var gridFSItem = _database.GridFS.FindOne(Query.EQ("_id", box.GridFSId));

                    using (var stream = gridFSItem.OpenRead())
                    {
                        bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, (int)stream.Length);
                    }
            }

            Assert.IsTrue(output.ComparisonBytes.SequenceEqual(box.ComparisonBytes));

            _database.GridFS.Delete(file);
            collection.Drop();
        }


        [Test]
        public void CanRemove()
        {
            var collection = _database.GetCollection<Box>("pictures");

        }
    }
}
