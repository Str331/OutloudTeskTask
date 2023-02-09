using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Outloud.Dal.ReposInterfaces;
using Outloud.Domain.Entity;
using Outloud.Domain.Helpers;
using Outloud.Domain.Response;
using Outloud.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloud.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<User> _userRepository;

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<User>> Create(User model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);
                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Пользователь с данным логином уже существует",
                    };
                }
                user = new User()
                {
                    Login = model.Login,
                    Password = HashPasswordHelpes.HashPassword(model.Password)
                };
                await _userRepository.Create(user);
                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = "Пользователь добавлен",
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
                return new BaseResponse<User>()
                {

                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false
                    };
                }
                await _userRepository.Delete(user);
                _logger.LogInformation($"[UserService.DeleteUser] пользователь удален");

                return new BaseResponse<bool>
                {
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new User()
                    {
                        Id = x.Id,
                        Login = x.Login,
                        Subs = x.Subs
                    })
                    .ToListAsync();


                _logger.LogInformation($"[UserService.GetUsers] получено элементов {users.Count}");
                return new BaseResponse<IEnumerable<User>>()
                {
                    Data = users,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
