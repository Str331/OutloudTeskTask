using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        //описание ошибки
        public string Description { get; set; }
        //запись результата запроса
        public T Data { get; set; }
    }
    public interface IBaseResponse<T>
    {
        string Description { get; }
        T Data { get; }
    }
}
