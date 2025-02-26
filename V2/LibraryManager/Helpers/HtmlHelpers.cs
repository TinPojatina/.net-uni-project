using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent BookStatusHelper(this IHtmlHelper htmlHelper, bool isAvailable)
        {
            string statusText = isAvailable ? "Available" : "Checked Out";
            string badgeClass = isAvailable ? "badge badge-success" : "badge badge-danger";

            return new HtmlString($"<span class='{badgeClass}'>{statusText}</span>");
        }
    }
}