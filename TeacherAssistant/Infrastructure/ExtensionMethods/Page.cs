using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TeacherAssistant.Infrastructure.ExtensionMethods
{
    public static class PagingExtension
    {
        public static string Page(this HtmlHelper htmlHelper, int pageIndex, int startIndex, int endIndex,string url)
        {
            var strBuilder = new StringBuilder();

            for (var i = startIndex; i <= endIndex; i++)
            {
                var tagBuilder = new TagBuilder("a");
                tagBuilder.InnerHtml = (i + 1).ToString();
                tagBuilder.Attributes.Add("href", string.Format(url, (i + 1).ToString()));
                if (i == pageIndex)
                    tagBuilder.Attributes.Add("class", "selectedLink");
                else tagBuilder.Attributes.Add("class","normalLink");
                strBuilder.Append(tagBuilder.ToString());
            }

            return strBuilder.ToString();
        }
    }
}