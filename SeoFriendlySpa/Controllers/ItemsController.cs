using Microsoft.AspNetCore.Mvc;
using SeoFriendlySpa.Models;
using SeoFriendlySpa.Services;

namespace SeoFriendlySpa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{


    
    
    private readonly ILogger<ItemsController> _logger;
    private readonly IItemsService _itemsService;

    public ItemsController(ILogger<ItemsController> logger,
        IItemsService itemsService)
    {
        _logger = logger;
        _itemsService = itemsService;
    }

    [HttpGet("{id}")]
    public async Task<Item?> Get(int id)
    {
        return await _itemsService.GetItem(id);
    }
}