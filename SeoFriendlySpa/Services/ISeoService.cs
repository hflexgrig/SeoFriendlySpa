using Microsoft.AspNetCore.Mvc;

namespace SeoFriendlySpa.Services;

public interface ISeoService
{
    Task<ContentResult> SetMetasAndGetContentResult(string title, IDictionary<HtmlMetaTagKey, string> metasDictionary);
}