using System.Text.RegularExpressions;

namespace Asp.net_Core___Support_Microsoft.NewFolder
{
    public static class HtmlExtensions
    {
        public static string RemoveHtmlFormatting(this string input)
        {
            // Remove HTML tags
            string strippedHtml = Regex.Replace(input, "<.*?>", string.Empty);

            // Replace HTML escape characters with their equivalents
            strippedHtml = System.Net.WebUtility.HtmlDecode(strippedHtml);

            return strippedHtml;
        }
    }
}
