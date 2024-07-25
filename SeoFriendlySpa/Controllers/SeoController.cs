using Microsoft.AspNetCore.Mvc;
using SeoFriendlySpa.Models;

namespace SeoFriendlySpa.Controllers;

[ApiController]
[Route("")]
public class SeoController : ControllerBase
{


    
    private readonly ILogger<SeoController> _logger;

    public SeoController(ILogger<SeoController> logger)
    {
        _logger = logger;
    }

    [HttpGet("item/{id}")]
    public async Task<ContentResult> Get(int id)
    {
        var result = new ContentResult()
        {
            ContentType = "text/html",
            Content = "<p>content works</p>"
        };

        return result;
    }
}