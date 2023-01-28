using Outloud.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Dal.ReposInterfaces
{
    public interface INewsRepository:IBaseRepository<News>
    {
        Task<IEnumerable<News>> GetUnreadNews(DateTime startDate);
        Task NewsAsRead(int id);
    }
}
