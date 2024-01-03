using System.Text.RegularExpressions;

namespace Asp.net_Core___Support_Microsoft.NewFolder
{
    public static class HtmlExtention
    {
        public static string RemoveHtmlFormatting(this string input)
        {
            // Supprimer les balises HTML
            string strippedHtml = Regex.Replace(input, "<.*?>", string.Empty);

            // Remplacer les caractères d'échappement HTML par leurs équivalents
            strippedHtml = System.Net.WebUtility.HtmlDecode(strippedHtml);

            return strippedHtml;
        }
    }
}
