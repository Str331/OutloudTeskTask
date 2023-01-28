using Outloud.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Dal.ReposInterfaces
{
    public interface IFeedRepository : IBaseRepository<Feed>
    {
        Task<Feed> GetByURLadress(string urlAdress);
    }
}
