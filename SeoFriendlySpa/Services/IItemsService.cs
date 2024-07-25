using SeoFriendlySpa.Models;

namespace SeoFriendlySpa.Services;

public interface IItemsService
{
    Task<Item?> GetItem(int id);
}