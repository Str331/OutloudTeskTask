using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Outloud.Domain.Entity;
using Outloud.Service.ServiceInterfaces;
using System.Globalization;

namespace OutloudTeskTask.Controllers
{
    [Authorize,ApiController,Route("Feed")]
    public class FeedController : Controller
    {
        private readonly ILogger<FeedController> _log;
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService,ILogger<FeedController> log)
        {
            _log = log;
            _feedService = feedService;
        }
        public DateTime Date(string date)
        {
            var resultDate = DateTime.MinValue;
            var DateFormats = new string[] {
                "ddd, dd MMM yyyy HH:mm:ss 'EST'",
                "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
            };
            foreach (var dateFormat in DateFormats)
            {
                try
                {
                    resultDate = DateTime.ParseExact(
                        date, dateFormat,
                        CultureInfo.InvariantCulture
                    );
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            if (resultDate == DateTime.MinValue)
                throw new Exception($"{date} doesn`t exist");
            else
                return resultDate;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var feeds = await this._feedService.GetAll();
            return this.Ok(feeds);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Feed url)
        {
            if (url == null)
            {
                return BadRequest();
            }
            else
                await _feedService.Create(url);
            return this.Ok();
        }
        }
}
