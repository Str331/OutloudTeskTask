using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Outloud.Dal.ReposInterfaces;
using Outloud.Domain.Entity;
using Outloud.Domain.Response;
using Outloud.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Outloud.Service.Implementations
{
    public class FeedService : IFeedService
    {
        private readonly ILogger<FeedService> _logger;
        private readonly IBaseRepository<Feed> _feedRepository;

        public FeedService(ILogger<FeedService> logger, IBaseRepository<Feed> feedRepository)
        {
            _logger = logger;
            _feedRepository = feedRepository;
        }

        public async Task<IBaseResponse<Feed>> Create(Feed model)
        {
            var model = "https://www.hltv.org/rss/news";
            List<Feed> feeds = new List<Feed>();
            try
            {
                XDocument xDoc = new XDocument();
                xDoc = XDocument.Load(RssFeedUrl);
                var items = (from x in xDoc.Descendants("item")
                             select new
                             {
                                 Designation = x.Element("Designation").Value,
                                 URLadress = x.Element("urladress").Value,
                                 pubDate = x.Element("pubDate").Value,
                                 News = x.Element("news").Value
                             });
                if (items != null)
                {
                    foreach (var i in items)
                    {
                        Feed f = new Feed
                        {
                            Designation = i.Designation,
                            URLadress = i.URLadress,
                            PublishDate = i.pubDate,
                            News = i.News,
                            
                        };

                        feeds.Add(f);
                    }
                }
                return feeds ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FeedService.Create] error: {ex.Message}");
                return new BaseResponse<Feed>()
                {
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public  Task<DateTime> Date(string date)
        {
            try
            {
                Feed feed = new Feed();
                if (DateTime.Now > date)
                {
                    feed.Active = true;
                }
                return DateTime.Now;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FeedService.Date] error: {ex.Message}");
                return new DateTime.Now
                {
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Feed>>> GetAll()
        {
            try
            {
                var feed = await _feedRepository.GetAll()
                    .Select(x => new Feed()
                    {
                        Id = x.Id,
                        Subs = x.Subs,
                        URLadress=x.URLadress,
                        Designation=x.Designation,
                    })
                    .ToListAsync();


                _logger.LogInformation($"[FeedService.GetAll] получено элементов {feed.Count}");
                return new BaseResponse<IEnumerable<Feed>>()
                {
                    Data = feed,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FeedSerivce.GetAll] error: {ex.Message}");
                return new BaseResponse<IEnumerable<Feed>>()
                {
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
