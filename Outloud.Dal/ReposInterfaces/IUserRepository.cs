using Outloud.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Dal.ReposInterfaces
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task GetByLogin(string login);
    }
}
