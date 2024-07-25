using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc;

namespace SeoFriendlySpa.Services;

public class SeoService : ISeoService
{
    private static readonly Lazy<Task<IDocument?>> HtmlDocument = new (async () =>
    {
        //Use the default configuration for AngleSharp
        AngleSharp.IConfiguration config = Configuration.Default
            .WithDefaultLoader();

        //Create a new context for evaluating webpages with the given config
        IBrowsingContext context = BrowsingContext.New(config);
        var address = $"https://{BaseAddres}";
        var document = await context.OpenAsync(address);
        return document;
    });

    private static string BaseAddres = "";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public SeoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        BaseAddres = _httpContextAccessor.HttpContext.Request.Host.Value;
    }

    public async Task<ContentResult> SetMetasAndGetContentResult(string title,
        IDictionary<HtmlMetaTagKey, string> metasDictionary)
    {
        var doc = await HtmlDocument.Value;

        var clonedDoc = doc.Clone() as IDocument;

        //set title
        clonedDoc.Title = title;

        var metas = clonedDoc.QuerySelectorAll("meta");

        var keysToExclude = new HashSet<HtmlMetaTagKey>();
        foreach (IHtmlMetaElement meta in metas)
        {
            var metaValue = meta.Name;
            if (!string.IsNullOrEmpty(metaValue))
            {
                var key = new HtmlMetaTagKey("name", metaValue);
                if (metasDictionary.TryGetValue(key, out var content))
                {
                    meta.Content = content;
                    keysToExclude.Add(key);
                }
            }

            metaValue = meta.Attributes["property"]?.Value;
            if (!string.IsNullOrEmpty(metaValue))
            {
                var key = new HtmlMetaTagKey("property", metaValue);
                if (metasDictionary.TryGetValue(key, out var content))
                {
                    meta.Content = content;
                    keysToExclude.Add(key);
                }
            }
        }

        var metKeysToAdd = metasDictionary.Keys.Except(keysToExclude).ToList();

        foreach (var htmlMetaTagKey in metKeysToAdd)
        {
            var meta = clonedDoc.CreateElement("meta") as IHtmlMetaElement;
            meta.SetAttribute(htmlMetaTagKey.Name, htmlMetaTagKey.Value);
            meta.Content = metasDictionary[htmlMetaTagKey];

            clonedDoc.Head.AppendChild(meta);
        }


        return new ContentResult()
        {
            ContentType = "text/html",
            Content = clonedDoc.DocumentElement.OuterHtml
        };
    }

}

public record HtmlMetaTagKey(string Name, string Value);
