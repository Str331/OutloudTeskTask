using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Outloud.Domain.Entity;
using Outloud.Service.ServiceInterfaces;

namespace OutloudTeskTask.Controllers
{
    [Authorize, ApiController, Route("News")]
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _log;
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService, ILogger<NewsController> log)
        {
            _log = log;
            _newsService = newsService;
        }
        [HttpGet]
        public async Task<IEnumerable<News>> GetUnreadNews(DateTime startDate)
        {
            await _newsService.GetUnreadNews(startDate);
            return (IEnumerable<News>)this.Ok(startDate);
        }

        [HttpPost]
        public async Task<IActionResult> Create(News model)
        {

            await _newsService.Create(model);
            return Ok();
        }
        [HttpPut("{id}/read")]
        public async Task<IActionResult> NewsAsRead(int id)
        {
            await _newsService.NewsAsRead(id);
            return Ok();
    } 
    }
}
