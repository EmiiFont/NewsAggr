using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAggr.Helpers;
using AngularAggr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AngularAggr.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {private readonly ILoggerFactory _loggerFactory;
        public SampleDataController(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            
        }
        [HttpGet("[action]")]
        public IEnumerable<FeedItem> WeatherForecasts()
        {
            var feedWebReader = new FeedWebReader(_loggerFactory);
           
            var rssItems = feedWebReader.GetFeed();
            
            return rssItems.ToList();
        }
        
        [HttpGet("[action]")]
        public IEnumerable<FeedItem> GetNextNews(int page = 1)
        {
            var feedWebReader = new FeedWebReader(_loggerFactory);
           
            var rssItems = feedWebReader.GetFeed(page);
          
           return rssItems;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
