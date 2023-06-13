using BLueCodeChanllenge.Context;
using BLueCodeChanllenge.Interfaces;
using BLueCodeChanllenge.Models;
using BLueCodeChanllenge.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BLueCodeChanllenge.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ChallengeController : Controller 
    {
        private readonly ILogger<ChallengeController> _logger;
        private readonly IUrlService _urlService;



        public ChallengeController(ILogger<ChallengeController> logger, IUrlService urlService)
        {
            _logger = logger;
            _urlService = urlService;
        }


        [HttpPost(Name = "ShortUrl")]
        public ShortUrls ShortUrl(string longUrl)
        {

            // id
            // url 
            // shortcut 
            // https://localhost:3000/TygerWods ? abc123 
            // https://localhost:3000/aj2343
            // abc123 =    https://localhost:3000/TygerWods

            // x = y 
            // y = x 

            var result = _urlService.GenerateShortUrl(longUrl);
            return result;
        }

        [HttpGet("/{url}")]

        public ActionResult Get(string url) 
        {

            var result = _urlService.GetLongUrl(url);
            return Redirect(result.LongUrl);
        }

        [HttpGet(Name = "Top Visited")]

        public List<ShortUrls> GetTopVisited()
        {

            var result = _urlService.GetTopVisited();
            return result;
        }







    }
}
