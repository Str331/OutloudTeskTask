using Outloud.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Service.ServiceInterfaces
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetUnreadNews(DateTime startDate);
        Task<News> Create(News model);
        Task NewsAsRead(int id);
    }
}
