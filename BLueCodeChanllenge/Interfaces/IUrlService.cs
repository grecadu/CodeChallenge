using BLueCodeChanllenge.Models;

namespace BLueCodeChanllenge.Interfaces
{
    public interface IUrlService
    {
                  
        ShortUrls GenerateShortUrl(string longUrl);
        ShortUrls GetLongUrl(string shortUrl);
        List<ShortUrls> GetTopVisited();
    }
}
