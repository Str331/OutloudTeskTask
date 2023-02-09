using Outloud.Domain.Entity;
using Outloud.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Service.ServiceInterfaces
{
    public interface IFeedService
    {
        public Task<DateTime> Date(string date);
        Task<IBaseResponse<Feed>> Create(Feed model);
        Task<BaseResponse<IEnumerable<Feed>>> GetAll();
    }
}
