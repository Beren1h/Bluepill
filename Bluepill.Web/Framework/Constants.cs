using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Framework
{
    public static class Constants
    {
        public const string RETRIEVAL_SESSION_KEY = "last_retrieval";
        public const int IMG_WIDTH = 600;
        public const int IMG_HEIGHT = 600;
        public const int PER_PAGE = 20;
        public const string GET_PICTURE_URL_FORMAT = "\\application\\picture\\getpicture?file={0}";
        public const string GET_RESIZE_PICTURE_URL_FORMAT = "\\application\\picture\\getresizepicture?file={0}&width={1}&height={2}";
    }
}