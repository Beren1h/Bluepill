using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Framework
{
    public static class Constants
    {
        public const string PREFERENCE_COOKIE_FORMAT = "bluepill.{0}.preferences";
        public const string WORKING_COLLECTION_COOKIE_KEY = "wc";
        public const string RETRIEVAL_SESSION_KEY = "last_retrieval";
        public const string CREATE_PATH = "c:\\bluepill\\input";
        public const string COMPLETE_PATH = "c:\\bluepill\\completed";
        public const int DISPLAY_COUNT = 1;
        public const int IMG_WIDTH = 600;
        public const int IMG_HEIGHT = 600;
        public const int PER_PAGE = 22;
        public const string GET_PICTURE_URL_FORMAT = "\\application\\picture\\getpicture?file={0}";
        public const string GET_RESIZE_PICTURE_URL_FORMAT = "\\application\\picture\\getresizepicture?file={0}&width={1}&height={2}";

    }
}