using SeoFriendlySpa.Models;

namespace SeoFriendlySpa.Services;

public class ItemsService : IItemsService
{
    private static readonly List<Item?> testData = new()
    {
        new() {Id = 1, Title = "Test item 1", Content = "Test item content 1", ImagePath = "https://eshopblob.blob.core.windows.net/arishop/thumbnails/1/23/e3974daf-a94c-456f-9064-072a3656a3f7.png?sv=2022-11-02&ss=b&srt=sco&sp=rtfx&se=2034-02-26T03:51:34Z&st=2024-02-25T19:51:34Z&spr=https&sig=i9hLQq1BP6CPdAz7iFc2lQk3i5xHArgXL55zmyvkRtg%3D\n"},
        new() {Id = 2, Title = "Test item 2", Content = "Test item content 2", ImagePath = "https://eshopblob.blob.core.windows.net/arishop/thumbnails/1/2/7f2a7d4d-dbe3-4f66-9e6f-9805fe71826d.jpg?sv=2022-11-02&ss=b&srt=sco&sp=rtfx&se=2034-02-26T03:51:34Z&st=2024-02-25T19:51:34Z&spr=https&sig=i9hLQq1BP6CPdAz7iFc2lQk3i5xHArgXL55zmyvkRtg%3D"},
    };

    public async Task<Item?> GetItem(int id)
    {
        //simulate IO operation e.g. query db e.t.c...
        await Task.Delay(200);
        
        return testData.Find(x => x.Id == id);
    }
 }