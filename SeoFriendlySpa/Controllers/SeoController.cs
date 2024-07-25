using Microsoft.AspNetCore.Mvc;
using SeoFriendlySpa.Services;

namespace SeoFriendlySpa.Controllers;

[ApiController]
[Route("")]
public class SeoController : ControllerBase
{
    private readonly ILogger<SeoController> _logger;
    private readonly IItemsService _itemsService;
    private readonly ISeoService _seoService;

    public SeoController(ILogger<SeoController> logger,
        IItemsService itemsService,
        ISeoService seoService)
    {
        _logger = logger;
        _itemsService = itemsService;
        _seoService = seoService;
    }

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
}