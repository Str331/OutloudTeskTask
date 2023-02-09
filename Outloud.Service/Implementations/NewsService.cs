using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Outloud.Dal.ReposInterfaces;
using Outloud.Domain.Entity;
using Outloud.Domain.Helpers;
using Outloud.Domain.Response;
using Outloud.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Service.Implementations
{
    public class NewsService : INewsService
    {
        private readonly ILogger<NewsService> _logger;
        private readonly IBaseRepository<News> _newsRepository;
        public NewsService(ILogger<NewsService> logger, IBaseRepository<News> newsRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
        }

        public async Task<BaseResponse<News>> Create(News model)
        {
            try
            {
                var news = await _newsRepository.GetAll().FirstOrDefaultAsync(x => x.Title == model.Title);
                if (news != null)
                {
                    return new BaseResponse<News>()
                    {
                        Description = "Данная новость уже существует",
                    };
                }
                news = new News()
                {
                    Title = model.Title,
                };
                await _newsRepository.Create(news);
                return new BaseResponse<News>()
                {
                    Data = news,
                    Description = "Новость добавлена",
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[NewsService.Create] error: {ex.Message}");
                return new BaseResponse<News>()
                {
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<News>>> GetUnreadNews(DateTime startDate)
        {
            try
            {
                var news = await _newsRepository.GetAll().FirstOrDefaultAsync(x => x.DateofAdd == startDate.Date);
                if (news is null)
                {
                    return new BaseResponse<IEnumerable<News>>()
                    {
                        Description = "Нет новостей",
                    };
                }
                return new BaseResponse<IEnumerable<News>>()
                {
                    Data = (IEnumerable<News>)news,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<News>>()
                {
                    Description = $"[NewsAsRead] : {ex.Message}",
                };
            }
        }

        public async Task<IBaseResponse<bool>> NewsAsRead(long id)
        {
            try
            {
                var news = await _newsRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (news == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "News not found",
                        Data = false
                    };
                }
                await _newsRepository.Delete(news);
                return new BaseResponse<bool>()
                {
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[NewsAsRead] : {ex.Message}",
                };
            }
        }
    }
}
