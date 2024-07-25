

This is ASPNetCore Angular SPA application, with Title and Meta tags update workaround on server side. It just proxies Angular routes, 
which needed to be SEO friendly (check ClientApp/proxy.conf.js , where added "/item" and "/fetch-data") and then Appropriate routes created on SeoController

```
    [HttpGet("item/{id}")]
    public async Task<ContentResult> Get(int id)
    {
        var item = await _itemsService.GetItem(id);

        return await _seoService.SetMetasAndGetContentResult(item.Title,
            new Dictionary<HtmlMetaTagKey, string>()
            {
                {new HtmlMetaTagKey("name", "description"), item.Content},
                {new HtmlMetaTagKey("name", "keywords"), $"buy in USA {item.Title}"},
                {new HtmlMetaTagKey("property", "og:image"), item.ImagePath},
            });
    }
    
    [HttpGet("fetch-data")]
    public async Task<ContentResult> GetFetchData()
    {

        return await _seoService.SetMetasAndGetContentResult("Fetch data title set from the server",
            new Dictionary<HtmlMetaTagKey, string>()
            {
                {new HtmlMetaTagKey("name", "description"), "weather forecasts"},
                {new HtmlMetaTagKey("name", "keywords"), $"weather USA"},
            });
    }
```

It reads the static Html content, and using AngleSharp updates the Title and existing Meta tags, or creates them, if they do not exist.
