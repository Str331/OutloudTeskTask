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
            try
            {
                var feed = await _feedRepository.GetAll().FirstOrDefaultAsync(x => x.Subs == model.Subs);
                if (feed != null)
                {
                    return new BaseResponse<Feed>()
                    {
                        Description = "Feed is already exist",
                    };
                }
                feed = new Feed()
                {
                    Subs = model.Subs,
                };
                await _feedRepository.Create(feed);
                return new BaseResponse<Feed>()
                {
                    Data = feed,
                    Description = "Feed added",
                };
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
