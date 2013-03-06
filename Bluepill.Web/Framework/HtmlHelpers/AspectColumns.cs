using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bluepill.Web.Framework.HtmlHelpers
{
    public static class AspectColumns
    {
        public static MvcHtmlString GetColumnMarkup(this HtmlHelper helper, IEnumerable<Aspect> aspects, int columns, int itemsPerColumn)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < aspects.Count(); i++)
            {
                if (i % 5 == 0)
                {
                    sb.Append("<div class=\"span4 hidden-phone\">");
                    sb.Append("<ul>");

                    var list = aspects.Skip(i).Take(5);

                    foreach (var aspect in list)
                    {
                        sb.AppendFormat("<li><label for=\"\" class=\"off\" />{0}</label></li>", aspect.Text);
                    }

                    sb.Append("</ul>");
                    sb.Append("</div>");



                }

            }

            return MvcHtmlString.Create(sb.ToString());

        }

        public static MvcHtmlString GetColumnMobileMarkup(this HtmlHelper helper, IEnumerable<Aspect> aspects)
        {
            var sb = new StringBuilder();

            sb.Append("<div class=\"span12 hidden-desktop\">");
            sb.Append("<ul>");

            foreach (var aspect in aspects)
            {
                sb.AppendFormat("<li><label for=\"\" class=\"off\" />{0}</label></li>", aspect.Text);
            }


            sb.Append("</ul>");
            sb.Append("</div>");

            return MvcHtmlString.Create(sb.ToString());

        }



    }
}