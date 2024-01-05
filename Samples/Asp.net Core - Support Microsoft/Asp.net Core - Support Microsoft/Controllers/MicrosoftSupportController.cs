using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using Asp.net_Core___Support_Microsoft.NewFolder;
using Asp.net_Core___Support_Microsoft.Model;
using System.Text;

namespace Asp.net_Core___Support_Microsoft.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MicrosoftSupportController : ControllerBase
    {
        private readonly string url = "https://support.microsoft.com/search/results?query=";
        private readonly string language = "en";

        [HttpGet(Name = "GetMicrosoftSupportResponse")]
        public async Task<IActionResult> GetMicrosoftSupportResponse([FromQuery] string question)
        {
            HttpClient _client = new HttpClient();
            HttpRequestMessage r = new HttpRequestMessage(HttpMethod.Get, $"{url}{question.Replace(" ", "+")}&isEnrichedQuery=true");
            r.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
            r.Headers.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
            };
            r.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            r.Headers.UserAgent.ParseAdd("PostmanRuntime/7.29.2");
            var response = await _client.SendAsync(r);
            var html = await response.Content.ReadAsStringAsync();

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

            var returnedResponse = results.Select(n => new MicrosoftSupport
            {
                Title = n.Title,
                Link = n.Link,
                LinkEncoded = Encode(n.Link),
                Description = n.Description?.Replace("\r\n", string.Empty)?.Replace("\t", string.Empty),
                AppliesTo = n.AppliesTo
            }).ToList();

            return Ok(returnedResponse.First());
        }

        private string Encode(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text)).TrimEnd('=').Replace('+', '-')
                .Replace('/', '_');
        }
    }
}
