using BLueCodeChanllenge.Interfaces;
using BLueCodeChanllenge.Models;
using Microsoft.Extensions.Configuration;

namespace BLueCodeChanllenge.Services
{
    public class UrlService : IUrlService
    {
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly int Base = Characters.Length;
        private readonly IDbService<ShortUrls> _urlsDbService;
        private readonly IConfiguration _configuration;

        public UrlService(IDbService<ShortUrls> urlsDbService, IConfiguration configuration)
        {
            _urlsDbService = urlsDbService;
            _configuration = configuration;
        }
        private string Encode(int i)
        {
            if (i == 0) return Characters[0].ToString();

            var s = string.Empty;

            while (i > 0)
            {
                s += Characters[i % Base];
                i = i / Base;
            }

            return string.Join(string.Empty, s.Reverse());
        }

        private ShortUrls GetNewShortUrl(string longUrl)
        {
            var result = ValidateIfLongUrlExists(longUrl);
            if (result is null)
            {
                return new ShortUrls
                {
                    Id = _urlsDbService.GetNextId()

                };
                
            }
            return result;
           

        }

        private ShortUrls ValidateIfLongUrlExists(string longUrl)
        {
            return _urlsDbService.GetAll().FirstOrDefault(x => x.LongUrl.ToLower().Equals(longUrl.ToLower()));
        }

        private ShortUrls GetUrl(string shortUrl)
        {
            return _urlsDbService.GetAll().FirstOrDefault(x => x.ShortUrl.ToLower().Equals(shortUrl.ToLower()));
        }
        private bool CreateDbEntry(ShortUrls shortUrl) 
        {
            try
            {
                _urlsDbService.Add(shortUrl);
            }
            catch (Exception)
            {

                return false;
            }
           
            return true;

        }

        private bool UpdateUrlCounter(ShortUrls shortUrl)
        {
            try
            {
                _urlsDbService.Update(shortUrl);
            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }

        public List<ShortUrls> GetTopVisited() 
        {
            var topRecordsCount = _configuration.GetValue<int>("AppSettings:TopRecordsCount");

            var querableList = _urlsDbService.GetAll();

            var topRecords = querableList
            .OrderByDescending(e => e.Counter)
            .Take(topRecordsCount)
            .ToList();

            return topRecords;
        }
        public ShortUrls GetLongUrl(string shortUrl) 
        {
            var result = GetUrl(shortUrl);
            if (result is null) 
            {
                return new ShortUrls();
            }
            result.Counter = result.Counter + 1;
            UpdateUrlCounter(result);
            return result;
        }
        public ShortUrls GenerateShortUrl(string longUrl)
        {

            var url = GetNewShortUrl(longUrl);
            if (url.LongUrl is not  null)
                return url;
          
            string shortUrl = Encode(url.Id);

            url.ShortUrl = shortUrl;
            url.LongUrl = longUrl;
            url.Id = 0;
            CreateDbEntry(url);

            return url;
        }


      
    }
}
