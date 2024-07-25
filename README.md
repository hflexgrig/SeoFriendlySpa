

This is ASPNetCore Angular SPA application, with Title and Meta tags update workaround on server side. It just proxies Angular routes, 
which needed to be SEO friendly (check ClientApp/proxy.conf.js , where added "/item" and "/fetch-data") and then Appropriate routes created on SeoController

```
    [HttpGet("item/{id}")]
    public async Task<ContentResult> Get(int id)
    {
        //Gets the data, to update meta tags and title
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
        //just updating random harcoded values for Title and Meta tags from the server
        return await _seoService.SetMetasAndGetContentResult("Fetch data title set from the server",
            new Dictionary<HtmlMetaTagKey, string>()
            {
                {new HtmlMetaTagKey("name", "description"), "weather forecasts"},
                {new HtmlMetaTagKey("name", "keywords"), $"weather USA"},
            });
    }
```

It reads the static Html content, and using AngleSharp updates the Title and existing Meta tags, or creates them, if they do not exist.

Please note!!! 

This is only workaround for Title and Meta tags. If you need full SSR rendering , then check https://github.com/hflexgrig/MintPlayer.AspNetCore.SpaServices 
