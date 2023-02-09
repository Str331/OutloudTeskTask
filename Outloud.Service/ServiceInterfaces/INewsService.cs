using Outloud.Domain.Entity;
using Outloud.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Service.ServiceInterfaces
{
    public interface INewsService
    {
        Task<BaseResponse<IEnumerable<News>>> GetUnreadNews(DateTime startDate);
        Task<BaseResponse<News>> Create(News model);
        Task<IBaseResponse<bool>> NewsAsRead(long id);
    }
}
