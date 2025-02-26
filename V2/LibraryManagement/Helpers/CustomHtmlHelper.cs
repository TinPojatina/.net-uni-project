using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace LibraryManagement.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent BookAvailabilityLabel(this IHtmlHelper htmlHelper, bool isAvailable)
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("badge");

            if (isAvailable)
            {
                tag.AddCssClass("badge-success");
                tag.InnerHtml.Append("Available");
            }
            else
            {
                tag.AddCssClass("badge-danger");
                tag.InnerHtml.Append("Not Available");
            }

            var writer = new System.IO.StringWriter();
            tag.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}