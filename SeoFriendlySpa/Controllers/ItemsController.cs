using Microsoft.AspNetCore.Mvc;
using SeoFriendlySpa.Models;

namespace SeoFriendlySpa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{


    private static readonly List<Item?> testData = new()
    {
        new() {Id = 1, Title = "Test item 1", Content = "Test item content 1", ImagePath = "images/item1.jpg"},
        new() {Id = 2, Title = "Test item 2", Content = "Test item content 2", ImagePath = "images/item2.jpg"},
    };
    
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<Item?> Get(int id)
    {
        //simulate IO operation e.g. query db e.t.c...
        await Task.Delay(200);

        return testData.Find(x => x.Id == id);
    }
}