using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using Asp.net_Core___Support_Microsoft.NewFolder;
using Asp.net_Core___Support_Microsoft.Model;
using System.Text;
using StringWithQualityHeaderValue = System.Net.Http.Headers.StringWithQualityHeaderValue;
using CacheControlHeaderValue = System.Net.Http.Headers.CacheControlHeaderValue;

namespace Asp.net_Core___Support_Microsoft.Controllers
{
    /// <summary>
    /// API controller containing the method of calling Microsoft Support, 
    /// this endpoint is for calls by GPT Pro
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MicrosoftSupportController : ControllerBase
    {
        private readonly string url = "https://support.microsoft.com/search/results?query=";
        private readonly IHttpClientFactory _httpClientFactory;

        public MicrosoftSupportController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// GET method of calling Microsoft support, the response is processed and sent back to GPT Pro in Json format.
        /// </summary>
        /// <param name="originalInput">Represents the question to ask Microsoft support in natural language as asked by the user</param>
        /// <param name="originalLanguageFromIsoCode">Represents the language used by the user for the question, the response must be in same language</param>
        /// <returns></returns>
        [HttpGet(Name = "GetMicrosoftSupportResponse")]
        public async Task<IActionResult> GetMicrosoftSupportResponse([FromQuery] string originalInput, [FromQuery] string originalLanguageFromIsoCode)
        {
            var _client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{url}{originalInput.Replace(" ", "+")}&isEnrichedQuery=true");

            httpRequestMessage.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(originalLanguageFromIsoCode));
            httpRequestMessage.Headers.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            httpRequestMessage.Headers.UserAgent.ParseAdd("PostmanRuntime/7.29.2");

            var httpResponseMessage = await _client.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return Ok(FormatResponse(httpResponseMessage, originalLanguageFromIsoCode));
            }

            return BadRequest();
        }

        private async Task<MicrosoftSupport> FormatResponse(HttpResponseMessage httpResponseMessage, string originalLanguageFromIsoCode)
        {
            var html = await httpResponseMessage.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var results = doc.DocumentNode.Descendants()
                .Where(n => n.Attributes["class"]?.Value == "page")
                .Select(n => new
                {
                    Title = n.Descendants("a").FirstOrDefault()?.Attributes["aria-label"]?.Value?.RemoveHtmlFormatting(),
                    Link = n.Descendants("a").FirstOrDefault()?.Attributes["href"]?.Value,
                    Description = n.Descendants("div").FirstOrDefault(c => c.Attributes["class"]?.Value == "description")?.InnerText,
                    AppliesTo = n.Descendants("span").ToList().Select(e => e.InnerText).ToList()
                })
                .Take(1)
                .Select(n =>
                {
                    var link = Uri.TryCreate(n.Link, UriKind.Absolute, out _) ? n.Link : $"https://support.microsoft.com{n.Link}";
                    link = HttpUtility.HtmlDecode(link);
                    return new
                    {
                        Title = WebUtility.HtmlDecode(n.Title).Replace(" - Support ...", ""),
                        Link = link,
                        Description = WebUtility.HtmlDecode(n.Description),
                        AppliesTo = WebUtility.HtmlDecode(string.Join(" ", n.AppliesTo)),
                    };
                });

            return results.Select(n => new MicrosoftSupport
            {
                Title = n.Title,
                Link = n.Link,
                LinkEncoded = Encode(n.Link),
                Description = n.Description?.Replace("\r\n", string.Empty)?.Replace("\t", string.Empty),
                AppliesTo = n.AppliesTo,
                IsoCodeLanguage = originalLanguageFromIsoCode
            }).First();
        }

        private string Encode(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text)).TrimEnd('=').Replace('+', '-')
                .Replace('/', '_');
        }
    }
}
