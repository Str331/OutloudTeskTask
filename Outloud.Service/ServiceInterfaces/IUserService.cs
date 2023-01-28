using Outloud.Domain.Entity;
using Outloud.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Service.ServiceInterfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> Create(User model);

        Task<BaseResponse<IEnumerable<User>>> GetUsers();

        Task<IBaseResponse<bool>> DeleteUser(long id);
    }
}
