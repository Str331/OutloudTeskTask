using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Dal.ReposInterfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        Task Delete(T entity);
        Task<T> Update(T entity);
    }
}