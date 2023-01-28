using Outloud.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Dal.ReposInterfaces
{
    public interface ISubRepository : IBaseRepository<Sub>
    {
        Task<bool> isExist(string userLogin, int feedId);
    }
}
